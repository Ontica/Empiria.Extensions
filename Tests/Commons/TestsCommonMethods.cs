/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Tests                              Component : Infrastructure provider                 *
*  Assembly : Empiria.Tests.dll                          Pattern   : Static methods library                  *
*  Type     : CommonMethods                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides common use methods used in testing projects.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;
using System.Threading;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;

namespace Empiria.Tests {

  /// <summary>Provides common use methods used in testing projects.</summary>
  static public class TestsCommonMethods {

    #region Auxiliary methods

    static public void Authenticate() {
      string sessionToken = ConfigurationData.Get<string>(typeof(TestsCommonMethods),
                                                          "SessionToken");

      Authenticate(sessionToken, "127.0.0.1");
    }


    static public void Authenticate(string sessionToken, string userHostAddress) {
      Assertion.Require(sessionToken, nameof(sessionToken));
      Assertion.Require(userHostAddress, nameof(userHostAddress));

      IEmpiriaPrincipal principal = AuthenticationService.Authenticate(sessionToken, userHostAddress);

      Thread.CurrentPrincipal = principal;
    }


    static public Contact GetCurrentUser() {
      return ExecutionServer.CurrentContact;
    }


    static public T ReadTestDataFromFile<T>(string fileNamePrefix) {
      var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

      string path = Path.Combine(directory.Parent.FullName,
                                @"tests-data",
                                $"{fileNamePrefix}.test-data.json");

      var jsonString = File.ReadAllText(path);

      return JsonConverter.ToObject<T>(jsonString);
    }


    static public void SetDefaultJsonSettings() {
      Newtonsoft.Json.JsonConvert.DefaultSettings = () => JsonConverter.JsonSerializerDefaultSettings();
    }

    #endregion Auxiliary methods

  }  // CommonMethods

}  // namespace Empiria.Tests
