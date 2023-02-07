/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Test cases                              *
*  Assembly : Empiria.WebApi.Client.Tests.dll            Pattern   : Integration tests                       *
*  Type     : WebApiClientTests                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Empiria WebApiClient tests.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Xunit;

using Empiria.WebApi.Client;

namespace Empiria.Tests {

  /// <summary>Empiria WebApiClient tests.</summary>
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
    public async Task Should_Get_NextId_Using_NamedEndpoint() {
      TestsCommonMethods.Authenticate();

      var webApiClient = new WebApiClient();

      var nextId = await webApiClient.GetAsync<int>("Empiria.IdGenerator.NextTableRowId", "LRSPayments");

      Assert.True(nextId > 0);
    }

  }  // WebApiClientTests

}  // namespace Empiria.Tests
