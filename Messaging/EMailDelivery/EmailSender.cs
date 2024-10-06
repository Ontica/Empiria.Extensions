/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : Providers                             *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Service provider                      *
*  Types    : EmailSender                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Sends email messages.                                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net.Mail;

using System.Threading.Tasks;

namespace Empiria.Messaging.EMailDelivery {

/// <summary>Describes the email sender technology</summary>
  internal enum EmailSenderTechnology {

    ExchangeEmailSender,

    SmtpEmailSender,

  }  // enum EmailSenderType


  /// <summary>Sends email messages.</summary>
  public class EmailSender {

    #region Methods

    public bool IsValidAddress(string address) {
      try {
        var addr = new MailAddress(address);

        return addr.Address == address;

      } catch {
        return false;
      }
    }


    public void Send(SendTo sendTo, EmailContent content) {
      IEmailSender sender = GetEmailSender();

      sender.Send(sendTo, content);
    }


    public async Task SendAsync(SendTo sendTo, EmailContent content) {
      IEmailSender sender = GetEmailSender();

      await sender.SendAsync(sendTo, content);
    }

    #endregion Methods

    #region Helpers

    private IEmailSender GetEmailSender() {
      var config = EmailConfig.Default;

      if (config.EmailSenderTechnology == EmailSenderTechnology.SmtpEmailSender) {
        return new SmtpEmailSender();

      } else if (config.EmailSenderTechnology == EmailSenderTechnology.ExchangeEmailSender) {
        return new ExchangeEmailSender();

      } else {
        throw Assertion.EnsureNoReachThisCode($"Email sender type '{config.EmailSenderTechnology}' " +
                                              $"is undefined.");
      }

    }

    #endregion Helpers

  }  // class EmailSender

} // namespace Empiria.Messaging.EMailDelivery

