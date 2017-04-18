using System;
using Xunit;

using Empiria.Security;
using Empiria.WebApi.Client;

namespace Empiria {

  public class WebApiClientTests {

    [Fact]
    public async void Should_Get_License_Using_Named_Endpoint() {
      var webApiClient = new WebApiClient();

      var license = await webApiClient.GetAsync<string>("System.GetLicense");

      Assert.Equal("Tlaxcala", license);
    }


    [Fact]
    public async void Should_Get_License_Using_Uri_And_Response_Model() {
      var webApiClient = new WebApiClient();

      var license = await webApiClient.GetAsync<ResponseModel<string>>("v1/system/license");

      Assert.Equal("Tlaxcala", license.Data);
    }


    [Fact]
    public async void Should_Get_License_Using_Uri_And_Scope_Parameter() {
      var webApiClient = new WebApiClient();

      var license = await webApiClient.GetAsync<string>("v1/system/license::data");

      Assert.Equal("Tlaxcala", license);
    }


    [Fact]
    public async void Should_Get_NextId_Using_Response_Model_With_Named_Endpoint() {
      Authenticate();
      var webApiClient = new WebApiClient();

      var nextId = await webApiClient.GetAsync<ResponseModel<int>>("Empiria.IdGenerator.NextTableRowId", "LRSPayments");

      Assert.True(nextId.Data > 0);
    }


    [Fact]
    public async void Should_Get_NextId_Using_Named_Endpoint() {
      Authenticate();
      var webApiClient = new WebApiClient();

      var nextId = await webApiClient.GetAsync<int>("Empiria.IdGenerator.NextTableRowId", "LRSPayments");

      Assert.True(nextId > 0);
    }


    #region Auxiliary methods

    private void Authenticate() {
      string sessionToken = "5ae818af-9085-4c56-99cd-cd91dadc01ab-45da6fc3ca1097df604908cb67451efdc752ee5074c42949f9e7e73c6abfa5c8";

      System.Threading.Thread.CurrentPrincipal = EmpiriaIdentity.Authenticate(sessionToken);
    }


    #endregion Auxiliary methods

  }  // WebApiClientTests

}  // namespace Empiria
