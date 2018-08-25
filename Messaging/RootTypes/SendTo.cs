/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : Messaging Core                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Information Holder                    *
*  Type     : SendTo                                       License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data holder used to describe message's receivers rules, typically e-mail addresses.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.StateEnums;

namespace Empiria.Messaging {

  /// <summary>Data holder used to describe message's receivers rules, typically e-mail addresses.</summary>
  public class SendTo {

    #region Constructors and parsers

    private SendTo() {
      // no-op
    }


    public SendTo(string address) {
      Assertion.AssertObject(address, "address");

      this.Address = address;
    }


    public SendTo(string address, string displayName = null,
                  string sendWhen = null, string ruleName = "Default") {
      Assertion.AssertObject(address, "address");

      this.Address = address;
      this.DisplayName = displayName ?? String.Empty;
      this.SendWhen = sendWhen ?? String.Empty;
      this.RuleName = ruleName ?? "Default";
    }


    static public SendTo Parse(JsonObject json) {
      if (json.IsEmptyInstance) {
        return SendTo.Empty;
      }

      var sendTo = new SendTo();

      sendTo.Address = json.Get<string>("address", String.Empty);
      sendTo.DisplayName = json.Get<string>("displayName", String.Empty);
      sendTo.SendWhen = json.Get<string>("sendWhen", String.Empty);
      sendTo.RuleName = json.Get<string>("ruleName", "Default");
      sendTo.Priority = json.Get<Priority>("priority", Priority.Normal);
      sendTo.Format = json.Get<string>("format", String.Empty);

      return sendTo;
    }


    static public SendTo Empty {
      get;
    } = new SendTo() {
      IsEmptyInstance = true
    };


    #endregion Constructors and parsers

    #region Properties

    public bool IsEmptyInstance {
      get;
      private set;
    } = false;


    public string RuleName {
      get;
      private set;
    } = String.Empty;


    public string Address {
      get;
      private set;
    } = String.Empty;


    public string DisplayName {
      get;
      private set;
    } = String.Empty;


    public string SendWhen {
      get;
      private set;
    } = String.Empty;


    public Priority Priority {
      get;
      private set;
    } = Priority.Normal;


    public string Format {
      get;
      private set;
    } = String.Empty;


    #endregion Properties

    #region Methods

    public void SetPriority(Priority priority) {
      this.Priority = priority;
    }


    public virtual JsonObject ToJson() {
      var json = new JsonObject();

      json.Add("address", this.Address);
      json.AddIfValue("displayName", this.DisplayName);
      json.AddIfValue("sendWhen", this.SendWhen);
      json.AddIf("ruleName", this.RuleName, this.RuleName != "Default");
      json.AddIf("priority", this.Priority, this.Priority != Priority.Normal);
      json.AddIfValue("format", this.Format);

      return json;
    }


    public override string ToString() {
      return this.ToJson().ToString();
    }

    #endregion Methods

  } // class SendTo

} // namespace Empiria.Messaging
