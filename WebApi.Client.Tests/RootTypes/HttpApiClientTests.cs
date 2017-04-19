using System;
using Xunit;

using Empiria.Security;
using Empiria.WebApi.Client;

namespace Empiria.Tests {

  public class HttpApiClientTests {

    [Fact]
    public async void Should_Get_License_Using_HttpGet_And_ResponseModel() {
      var apiClient = new HttpApiClient("http://empiria.land/microservices/");

      var license = await apiClient.GetAsync<ResponseModel<string>>("v1/system/license");

      Assert.Equal(ExecutionServer.LicenseName, license.Data);
    }


    [Fact]
    public async void Should_Get_License_Using_Scope_Parameter() {
      var apiClient = new HttpApiClient("http://empiria.land/microservices/");

      var license = await apiClient.GetAsync<string>("v1/system/license::data");

      Assert.Equal(ExecutionServer.LicenseName, license);
    }


    [Fact]
    public async void Should_Get_DataItems_Using_Scope_Parameter() {
      var apiClient = new HttpApiClient("http://empiria.land/microservices/");

      var dataItems = await apiClient.GetAsync<int>("v1/system/license::dataItems");

      Assert.Equal(1, dataItems);
    }


    [Fact]
    public async void Should_Get_NextId_Using_ResponseModel() {
      HttpApiClient apiClient = this.GetAuthenticatedMicroservicesHttpClient();

      var nextId = await apiClient.GetAsync<ResponseModel<int>>("v1/id-generator/table-rows/LRSPayments");

      Assert.True(nextId.Data > 0);
    }


    [Fact]
    public async void Should_Get_NextId_Using_ScopeParameter() {
      HttpApiClient apiClient = this.GetAuthenticatedMicroservicesHttpClient();

      var nextId = await apiClient.GetAsync<int>("v1/id-generator/table-rows/LRSPayments::data");

      Assert.True(nextId > 0);
    }


    #region Auxiliary methods

    private void Authenticate() {
      string sessionToken = ConfigurationData.GetString("Testing.SessionToken");

      System.Threading.Thread.CurrentPrincipal = EmpiriaIdentity.Authenticate(sessionToken);
    }


    private HttpApiClient GetAuthenticatedMicroservicesHttpClient() {
      Authenticate();

      var webApiBaseAddress = ConfigurationData.GetString("Testing.WebApi.BaseAddress");

      return new HttpApiClient(webApiBaseAddress) {
        IncludeAuthorizationHeader = true
      };

    }

    #endregion Auxiliary methods

  }  // HttpApiClientTests

}  // namespace Empiria.Tests
