/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Test cases                              *
*  Assembly : Empiria.WebApi.Client.Tests.dll            Pattern   : Unit tests                              *
*  Type     : WebApiClientTests                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Unit tests for WebApiClient type services.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using System.Threading.Tasks;

using Empiria.WebApi.Client;

namespace Empiria.Tests {

  /// <summary>Unit tests for WebApiClient type services.</summary>
  public class WebApiClientTests {

    [Fact]
    public async Task Should_Get_DataItems_Using_NamedEndpoint() {
      var webApiClient = new WebApiClient();

      var dataItems = await webApiClient.GetAsync<int>("System.GetLicense::dataItems");

      Assert.Equal(1, dataItems);
    }


    [Fact]
    public async Task Should_Get_License_Using_NamedEndpoint() {
      var webApiClient = new WebApiClient();

      var license = await webApiClient.GetAsync<string>("System.GetLicense");

      Assert.Equal(ExecutionServer.LicenseName, license);
    }


    [Fact]
    public async Task Should_Get_License_Using_Uri_And_ResponseModel() {
      var webApiClient = new WebApiClient();

      var license = await webApiClient.GetAsync<ResponseModel<string>>("v1/system/license");

      Assert.Equal(ExecutionServer.LicenseName, license.Data);
    }


    [Fact]
    public async Task Should_Get_License_Using_Uri_And_ScopeParameter() {
      var webApiClient = new WebApiClient();

      var license = await webApiClient.GetAsync<string>("v1/system/license::data");

      Assert.Equal(ExecutionServer.LicenseName, license);
    }


    [Fact]
    public async Task Should_Get_NextId_Using_Response_Model_With_NamedEndpoint() {
      TestsCommonMethods.Authenticate();

      var webApiClient = new WebApiClient();

      var nextId = await webApiClient.GetAsync<ResponseModel<int>>("Empiria.IdGenerator.NextTableRowId", "LRSPayments");

      Assert.True(nextId.Data > 0);
    }


    [Fact]
    public async Task Should_Get_Secure_Data() {
      TestsCommonMethods.Authenticate();

      var webApiClient = new WebApiClient();

      string sut = await webApiClient.GetAsync<string>("v1/tests/secure-data::data");

      Assert.NotEmpty(sut);
    }


    [Fact]
    public async Task Should_Get_Secure_Data_Response_Model() {
      TestsCommonMethods.Authenticate();

      var webApiClient = new WebApiClient();

      ResponseModel<string> sut = await webApiClient.GetAsync<ResponseModel<string>>("v1/tests/secure-data");

      Assert.NotNull(sut);
      Assert.NotEmpty(sut.Data);
      Assert.Equal(1, sut.DataItems);
    }

  }  // WebApiClientTests

}  // namespace Empiria.Tests
