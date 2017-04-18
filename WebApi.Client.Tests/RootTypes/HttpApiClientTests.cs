using System;
using Xunit;

using Empiria.Security;
using Empiria.WebApi.Client;

namespace Empiria {

  public class HttpApiClientTests {

    [Fact]
    public async void ShouldGetLicenseUsingHttpGetAndResponseModel() {
      var apiClient = new HttpApiClient("http://empiria.land/microservices/");

      var license = await apiClient.GetAsync<ResponseModel<string>>("v1/system/license");

      Assert.Equal("Tlaxcala", license.Data);
    }


    [Fact]
    public async void ShouldGetLicenseUsingHttpGetAndScopeParameter() {
      var apiClient = new HttpApiClient("http://empiria.land/microservices/");

      var license = await apiClient.GetAsync<string>("v1/system/license::data");

      Assert.Equal("Tlaxcala", license);
    }


    [Fact]
    public async void ShouldGetDataItemsUsingHttpGetAndScopeParameter() {
      var apiClient = new HttpApiClient("http://empiria.land/microservices/");

      var dataItems = await apiClient.GetAsync<int>("v1/system/license::dataItems");

      Assert.Equal(1, dataItems);
    }


    [Fact]
    public async void ShouldGetNextIdUsingHttpGetAndResponseModel() {
      HttpApiClient apiClient = this.GetAuthenticatedMicroservicesHttpClient();

      var nextId = await apiClient.GetAsync<ResponseModel<int>>("v1/id-generator/table-rows/LRSPayments");

      Assert.True(nextId.Data > 0);
    }


    [Fact]
    public async void ShouldGetNextIdUsingHttpGetAndScopeParameter() {
      HttpApiClient apiClient = this.GetAuthenticatedMicroservicesHttpClient();

      var nextId = await apiClient.GetAsync<int>("v1/id-generator/table-rows/LRSPayments::data");

      Assert.True(nextId > 0);
    }


    #region Auxiliary methods

    private void Authenticate() {
      string sessionToken = "5ae818af-9085-4c56-99cd-cd91dadc01ab-45da6fc3ca1097df604908cb67451efdc752ee5074c42949f9e7e73c6abfa5c8";

      System.Threading.Thread.CurrentPrincipal = EmpiriaIdentity.Authenticate(sessionToken);
    }


    private HttpApiClient GetAuthenticatedMicroservicesHttpClient() {
      Authenticate();

      return new HttpApiClient("http://empiria.land/microservices/") {
        IncludeAuthorizationHeader = true
      };
    }

    #endregion Auxiliary methods

  }  // HttpApiClientTests

}  // namespace Empiria
