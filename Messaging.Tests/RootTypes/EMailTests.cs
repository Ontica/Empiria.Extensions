/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Message Queue Services                       Component : Message Queue                         *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Testing methods class                 *
*  Type     : EMailServiceTests                            License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Test methods for email service.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading;

using Xunit;

using Empiria.Security;

using Empiria.Messaging;

namespace Empiria.Tests {

  public class EMailServiceTests {

    [Theory]
    [InlineData("a@b.net.mx")]
    [InlineData("abc.com.yu@server.com")]
    [InlineData("abc@server.net.mx")]
    public void Should_Detect_Valid_EMail(string validAddress) {

      bool result = EMail.IsValidAddress(validAddress);

      Assert.True(result, $"Email address '{validAddress}' should be valid.");
    }


    [Theory]
    [InlineData("mail.server.mx")]
    [InlineData("a@x")]
    [InlineData("abc-..2e3r@server.net.mx")]
    [InlineData("inva,id-email")]
    [InlineData("abc-..&2e3r@server.net.mx")]
    public void Should_Detect_Invalid_EMail(string invalidAddress) {

      bool result = EMail.IsValidAddress(invalidAddress);

      Assert.False(result, $"Email address '{invalidAddress}' should be invalid.");
    }


    #region Auxiliary methods

    private void Authenticate() {
      string sessionToken = ConfigurationData.GetString("Testing.SessionToken");

      Thread.CurrentPrincipal = AuthenticationService.Authenticate(sessionToken);
    }


    #endregion Auxiliary methods

  }  // EMailServiceTests

}  // namespace Empiria.Tests
