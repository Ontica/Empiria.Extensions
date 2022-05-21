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

      Authenticate(sessionToken);
    }


    static public void Authenticate(string sessionToken) {
      Assertion.AssertObject(sessionToken, nameof(sessionToken));

      Thread.CurrentPrincipal = AuthenticationService.Authenticate(sessionToken);
    }


    static public Contact GetCurrentUser() {
      return Contact.Parse(ExecutionServer.CurrentUserId);
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
