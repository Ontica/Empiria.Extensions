﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : EMail Services                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Information Holder                    *
*  Type     : EMailConfig                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains configuration data for email SMTP clients.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.Messaging.EMailDelivery {

  /// <summary>Configuration data for email SMTP clients.</summary>
  internal class EMailConfig {

    #region Constructor and parsers

    private EMailConfig(JsonObject json) {
      this.LoadData(json);
    }

    static public EMailConfig Default {
      get {
        return EMailConfig.Parse("Default");
      }
    }

    static public EMailConfig Parse(string serverName) {
      Assertion.AssertObject(serverName, "serverName");

      var jsonObject = ConfigurationData.Get<JsonObject>(serverName + ".Email.ConfigData");

      return new EMailConfig(jsonObject);
    }

    #endregion Constructor and parsers

    #region Private properties

    public string Host {
      get;
      private set;
    }

    public int Port {
      get;
      private set;
    }

    public bool EnableSsl {
      get;
      private set;
    }

    public bool UseDefaultCredentials {
      get;
      private set;
    }

    public string SenderEMailAddress {
      get;
      private set;
    }

    public string SenderName {
      get;
      private set;
    }

    public string SenderEMailPassword {
      get;
      private set;
    }

    public string BccMirrorEMailAddress {
      get;
      private set;
    }

    #endregion Private properties

    #region Private methods

    private void LoadData(JsonObject json) {
      this.Host = json.Get<string>("SmtpClientHost");
      this.Port = json.Get<int>("SmtpClientPort");
      this.EnableSsl = json.Get("EnableSsl", false);
      this.UseDefaultCredentials = json.Get("UseDefaultCredentials", true);
      this.SenderEMailAddress = json.Get<string>("SenderEMailAddress");
      this.SenderName = json.Get<string>("SenderName");
      this.SenderEMailPassword = json.Get<string>("SenderEMailPassword");
      this.BccMirrorEMailAddress = json.Get<string>("BccMirrorEMailAddress", String.Empty);
    }

    #endregion Private methods

  } //class EMailConfig

} //namespace Empiria.Messaging.EMailDelivery
