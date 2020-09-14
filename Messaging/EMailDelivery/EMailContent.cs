/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Messaging Services                           Component : EMail Services                        *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Information Holder                    *
*  Type     : EMailContent                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds the contents of an email message.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;

using System.Collections.Generic;

namespace Empiria.Messaging.EMailDelivery {

  /// <summary>Holds the contents of an email message.</summary>
  public class EMailContent {

    #region Fields

    List<FileInfo> _attachments = new List<FileInfo>();

    #endregion Fields

    #region Constructors and parsers


    public EMailContent(string subject, string body, bool isBodyHtml = false) {
      Assertion.AssertObject(subject, "subject");
      Assertion.AssertObject(body, "body");

      this.Subject = subject;
      this.Body = body;
      this.IsBodyHtml = isBodyHtml;
    }


    #endregion Constructors and parsers

    #region Properties

    public string Subject {
      get;
    }


    public string Body {
      get;
    }


    public bool IsBodyHtml {
      get;
    }


    public FixedList<FileInfo> Attachments {
      get {
        return this._attachments.ToFixedList();
      }
    }

    #endregion Properties

    #region Methods

    public void AddAttachment(FileInfo fileInfo) {
      Assertion.AssertObject(fileInfo, "fileInfo");
      Assertion.Assert(fileInfo.Exists, $"The file {fileInfo.FullName} does not exist.");

      this._attachments.Add(fileInfo);
    }


    public bool RemoveAttachment(FileInfo fileInfo) {
      Assertion.AssertObject(fileInfo, "fileInfo");

      return this._attachments.Remove(fileInfo);
    }

    #endregion Methods

  }  // class EMailContent

}  // namespace Empiria.Messaging.EMailDelivery
