using System;
using System.Threading.Tasks;

using Xunit;

using Empiria.Security;
using Empiria.WebApi.Client;

namespace Empiria.Tests {

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
      Authenticate();
      var webApiClient = new WebApiClient();

      var nextId = await webApiClient.GetAsync<ResponseModel<int>>("Empiria.IdGenerator.NextTableRowId", "LRSPayments");

      Assert.True(nextId.Data > 0);
    }


    [Fact]
    public async Task Should_Get_NextId_Using_NamedEndpoint() {
      Authenticate();
      var webApiClient = new WebApiClient();

      var nextId = await webApiClient.GetAsync<int>("Empiria.IdGenerator.NextTableRowId", "LRSPayments");

      Assert.True(nextId > 0);
    }


    #region Auxiliary methods

    private void Authenticate() {
      string sessionToken = ConfigurationData.GetString("Testing.SessionToken");

      System.Threading.Thread.CurrentPrincipal = AuthenticationService.Authenticate(sessionToken);
    }


    #endregion Auxiliary methods

  }  // WebApiClientTests

}  // namespace Empiria.Tests
