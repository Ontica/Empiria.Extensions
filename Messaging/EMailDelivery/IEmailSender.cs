/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : EMail Services                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Common interface                      *
*  Type     : IEmailSender                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Common interface for email delivery services.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Threading.Tasks;

namespace Empiria.Messaging.EMailDelivery {

  /// <summary>Common interface for email delivery services.</summary>
  public interface IEmailSender {

    void Send(SendTo sendTo, EmailContent content);

    Task SendAsync(SendTo sendTo, EmailContent content);

  }  // interface IEmailSender

}  // namespace Empiria.Messaging.EMailDelivery
