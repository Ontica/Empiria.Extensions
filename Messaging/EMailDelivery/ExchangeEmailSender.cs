/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : EMail Services                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Service Provider                      *
*  Type     : ExchangeEmailSender                          License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides email delivery services using Microsoft Exchange Servers.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net;
using System.Net.Mail;

using Empiria.StateEnums;

using Microsoft.Exchange.WebServices.Data;

namespace Empiria.Messaging.EMailDelivery {

  /// <summary>Provides email delivery services using Microsoft Exchange Servers.</summary>
  internal class ExchangeEmailSender : IEmailSender {

    #region Public methods

    public void Send(SendTo sendTo, EmailContent content) {
      Assertion.Require(sendTo, nameof(sendTo));
      Assertion.Require(content, nameof(content));

      MailMessage message = GetMailMessage(sendTo, content);

      EmailConfig emailConfig = GetDefaultEmailConfig();

      ExchangeService service = new ExchangeService();

      service.Credentials = new NetworkCredential(emailConfig.NetworkUserName,
                                                  emailConfig.NetworkUserPasssword);

      service.AutodiscoverUrl(emailConfig.SenderEMailAddress);

      EmailMessage emailMessage = new EmailMessage(service);
      emailMessage.Subject = message.Subject;
      emailMessage.Body = message.Body;

      emailMessage.ToRecipients.Add(sendTo.Address);
      emailMessage.SendAndSaveCopy();
    }


    public async System.Threading.Tasks.Task SendAsync(SendTo sendTo, EmailContent content) {
      Assertion.Require(sendTo, nameof(sendTo));
      Assertion.Require(content, nameof(content));

      await System.Threading.Tasks.Task.FromException(new NotImplementedException());
    }

    #endregion Public methods

    #region Private methods


    static private EmailConfig _eMailConfig = null;
    static private EmailConfig GetDefaultEmailConfig() {
      if (_eMailConfig == null) {
        _eMailConfig = EmailConfig.Default;
      }
      return _eMailConfig;
    }


    static private MailMessage GetMailMessage(SendTo sendTo, EmailContent content) {
      EmailConfig emailConfig = GetDefaultEmailConfig();

      var message = new MailMessage();

      message.From = new MailAddress(emailConfig.SenderEMailAddress, emailConfig.SenderName);

      SetMessageReceiverAddresses(message, sendTo.Address);
      SetMessagePriority(message, sendTo.Priority);

      if (emailConfig.BccMirrorEMailAddress.Length != 0) {
        message.Bcc.Add(new MailAddress(emailConfig.BccMirrorEMailAddress));
      }

      message.Subject = content.Subject;
      message.Body = content.Body;
      message.IsBodyHtml = content.IsBodyHtml;
      message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

      return message;
    }


    static private void SetMessagePriority(MailMessage message, Priority priority) {
      if (priority == Priority.High) {
        message.Priority = MailPriority.High;

      } else if (priority == Priority.Low) {
        message.Priority = MailPriority.Low;

      } else {
        message.Priority = MailPriority.Normal;
      }
    }


    static private void SetMessageReceiverAddresses(MailMessage message, string address) {
      // Converts any semicolon to comma and removes spaces
      if (address.Contains(";")) {
        address = address.Replace(";", ",");
      }
      address = address.Replace(" ", String.Empty);

      if (address.Contains(",")) {
        message.To.Add(address);  // Multiple addresses separated by commas
      } else {
        message.To.Add(new MailAddress(address));
      }
    }

    #endregion Private methods

  } //class EMail

} //namespace Empiria.Messaging.EMailDelivery
