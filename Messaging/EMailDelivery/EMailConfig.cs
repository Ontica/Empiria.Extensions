/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : EMail Services                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Information Holder                    *
*  Type     : EmailConfig                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains configuration data for email clients.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.Messaging.EMailDelivery {

  /// <summary>Configuration data for email clients.</summary>
  internal class EmailConfig {

    #region Constructor and parsers

    private EmailConfig(JsonObject json) {
      this.LoadData(json);
    }

    static public EmailConfig Default {
      get {
        return EmailConfig.Parse("Default");
      }
    }

    static public EmailConfig Parse(string serverName) {
      Assertion.Require(serverName, "serverName");

      var jsonObject = ConfigurationData.Get<JsonObject>(serverName + ".Email.ConfigData");

      return new EmailConfig(jsonObject);
    }

    #endregion Constructor and parsers

    #region Private properties

    public EmailSenderTechnology EmailSenderTechnology {
      get; private set;
    }

    public string Host {
      get; private set;
    }

    public int Port {
      get; private set;
    }

    public bool EnableSsl {
      get; private set;
    }

    public bool UseDefaultCredentials {
      get; private set;
    }

    public string SenderEMailAddress {
      get; private set;
    }

    public string NetworkUserName {
      get; private set;
    }

    public string NetworkUserPasssword {
      get; private set;
    }

    public string SenderName {
      get; private set;
    }

    public string BccMirrorEMailAddress {
      get; private set;
    }

    #endregion Private properties

    #region Private methods

    private void LoadData(JsonObject json) {
      this.EmailSenderTechnology = json.Get("EmailSenderTechnology", EmailSenderTechnology.SmtpEmailSender);
      this.Host = json.Get<string>("SmtpClientHost");
      this.Port = json.Get<int>("SmtpClientPort");
      this.EnableSsl = json.Get("EnableSsl", false);
      this.UseDefaultCredentials = json.Get("UseDefaultCredentials", false);
      this.NetworkUserName = json.Get("NetworkUserName", string.Empty);
      this.NetworkUserPasssword = json.Get("NetworkUserPasssword", string.Empty);
      this.SenderEMailAddress = json.Get<string>("SenderEMailAddress");
      this.SenderName = json.Get<string>("SenderName");
      this.BccMirrorEMailAddress = json.Get<string>("BccMirrorEMailAddress", String.Empty);
    }

    #endregion Private methods

  } //class EmailConfig

} //namespace Empiria.Messaging.EMailDelivery
