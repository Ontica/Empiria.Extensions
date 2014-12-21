/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : XhtmlTemplate                                    Pattern  : Storage Item                      *
*  Version   : 6.0        Date: 04/Jan/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a XHTML template that serves as a user interface item pattern.                     *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Presentation.Web.Content {

  /// <summary>Represents a XHTML template that serves as a user interface item pattern.</summary>
  public class XhtmlTemplate : GeneralObject {

    #region Constructors and parsers

    private XhtmlTemplate() {
      // Required by Empiria Framework.
    }

    static public XhtmlTemplate Parse(int id) {
      return BaseObject.ParseId<XhtmlTemplate>(id);
    }

    static public XhtmlTemplate Parse(string itemNamedKey) {
      return BaseObject.ParseKey<XhtmlTemplate>(itemNamedKey);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField(GeneralObject.ExtensionDataFieldName + ".Template", IsOptional = true)]
    public string TemplateString {
      get;
      private set;
    }

    #endregion Public properties

  } // class XhtmlTemplate

} // namespace Empiria.Presentation.Web.Content
