/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Http Api Client                            Component : Test cases                              *
*  Assembly : Empiria.WebApi.Client.Tests.dll            Pattern   : Integration tests                       *
*  Type     : HttpApiClientTests                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Empiria HttpApiClient tests.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Xunit;

using Empiria.WebApi.Client;

namespace Empiria.Tests {

  /// <summary>Empiria HttpApiClient tests.</summary>
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
    public async Task Should_Get_NextId_Using_ResponseModel() {
      var apiClient = new HttpApiClient(BASE_ADDRESS);

      TestsCommonMethods.Authenticate();

      var nextId = await apiClient.GetAsync<ResponseModel<int>>("v1/id-generator/table-rows/LRSPayments");

      Assert.True(nextId.Data > 0);
    }


    [Fact]
    public async Task Should_Get_NextId_Using_ScopeParameter() {
      var apiClient = new HttpApiClient(BASE_ADDRESS);

      TestsCommonMethods.Authenticate();

      var nextId = await apiClient.GetAsync<int>("v1/id-generator/table-rows/LRSPayments::data");

      Assert.True(nextId > 0);
    }

  }  // HttpApiClientTests

}  // namespace Empiria.Tests
