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

    public WebApiClientTests() {
      TestsCommonMethods.Authenticate();
    }

    #region Tests

    [Fact]
    public async Task Should_Get_DataItems() {
      WebApiClient webApiClient = GetWebApiClient();

      var sut = await webApiClient.GetAsync<int>("v1/system/license::dataItems");

      Assert.Equal(1, sut);
    }


    [Fact]
    public async Task Should_Get_License() {
      WebApiClient webApiClient = GetWebApiClient();

      var sut = await webApiClient.GetAsync<string>("v1/system/license::data");

      Assert.Equal(ExecutionServer.LicenseName, sut);
    }


    [Fact]
    public async Task Should_Get_License_Using_ResponseModel() {
      WebApiClient webApiClient = GetWebApiClient();

      var sut = await webApiClient.GetAsync<ResponseModel<string>>("v1/system/license");

      Assert.Equal(ExecutionServer.LicenseName, sut.Data);
    }


    [Fact]
    public async Task Should_Get_Secure_Data() {
      WebApiClient webApiClient = GetWebApiClient();

      string sut = await webApiClient.GetAsync<string>("v1/tests/secure-data::data");

      Assert.NotEmpty(sut);
    }


    [Fact]
    public async Task Should_Get_Secure_Data_Using_ResponseModel() {
      WebApiClient webApiClient = GetWebApiClient();

      ResponseModel<string> sut = await webApiClient.GetAsync<ResponseModel<string>>("v1/tests/secure-data");

      Assert.NotNull(sut);
      Assert.NotEmpty(sut.Data);
      Assert.Equal(1, sut.DataItems);
    }

    #endregion Tests

    #region Helpers

    private WebApiClient GetWebApiClient() {
      return WebApiClient.GetInstance(TestingConstants.WEB_API_SERVER_NAME);
    }

    #endregion Helpers

  }  // WebApiClientTests

}  // namespace Empiria.Tests
