/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Test cases                              *
*  Assembly : Empiria.WebApi.Client.Tests.dll            Pattern   : Unit tests                              *
*  Type     : HttpApiClientTests                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Unit tests for HttpApiClient type services.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using System.Threading.Tasks;

using Empiria.WebApi.Client;

namespace Empiria.Tests {

  /// <summary>Unit tests for HttpApiClient type services.</summary>
  public class HttpApiClientTests {

    private readonly string BASE_ADDRESS = ConfigurationData.Get<string>("Testing.WebApi.BaseAddress");

    [Fact]
    public async Task Should_Get_License_Using_HttpGet_And_ResponseModel() {
      var apiClient = new HttpApiClient(BASE_ADDRESS);

      var license = await apiClient.GetAsync<ResponseModel<string>>("v1/system/license");

      Assert.Equal(ExecutionServer.LicenseName, license.Data);
    }


    [Fact]
    public async Task Should_Get_License_Using_Scope_Parameter() {
      var apiClient = new HttpApiClient(BASE_ADDRESS);

      var license = await apiClient.GetAsync<string>("v1/system/license::data");

      Assert.Equal(ExecutionServer.LicenseName, license);
    }


    [Fact]
    public async Task Should_Get_DataItems_Using_Scope_Parameter() {
      var apiClient = new HttpApiClient(BASE_ADDRESS);

      var dataItems = await apiClient.GetAsync<int>("v1/system/license::dataItems");

      Assert.Equal(1, dataItems);
    }


    [Fact]
    public async Task Should_Get_Secure_Data() {

      var apiClient = new HttpApiClient(BASE_ADDRESS);

      TestsCommonMethods.Authenticate();

      apiClient.Authenticate();

      var response = await apiClient.GetAsync<string>("v1/tests/secure-data::data");

      Assert.NotEmpty(response);
    }


    [Fact]
    public async Task Should_Get_Secure_Data_Response_Model() {
      var apiClient = new HttpApiClient(BASE_ADDRESS);

      TestsCommonMethods.Authenticate();

      apiClient.Authenticate();

      var response = await apiClient.GetAsync<ResponseModel<string>>("v1/tests/secure-data");

      Assert.NotEmpty(response.Data);
      Assert.Equal(1, response.DataItems);
    }

  }  // HttpApiClientTests

}  // namespace Empiria.Tests
