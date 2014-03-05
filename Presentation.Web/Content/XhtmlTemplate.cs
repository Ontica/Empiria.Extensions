/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : XhtmlTemplate                                    Pattern  : Storage Item                      *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Represents a XHTML template that serves as a user interface item pattern.                     *
*                                                                                                            *
********************************* Copyright (c) 1999-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/

namespace Empiria.Presentation.Web.Content {

  /// <summary>Represents a XHTML template that serves as a user interface item pattern.</summary>
  public class XhtmlTemplate : GeneralObject {

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.XhtmlTemplate";

    #endregion Fields

    #region Constructors and parsers

    public XhtmlTemplate()
      : base(thisTypeName) {

    }

    protected XhtmlTemplate(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public XhtmlTemplate Parse(int id) {
      return BaseObject.Parse<XhtmlTemplate>(thisTypeName, id);
    }

    static public XhtmlTemplate Parse(string itemNamedKey) {
      return BaseObject.Parse<XhtmlTemplate>(thisTypeName, itemNamedKey);
    }

    #endregion Constructors and parsers

    #region Public properties

    public string TemplateString {
      get { return base.Description; }
      set { base.Description = value; }
    }

    #endregion Public properties

  } // class XhtmlTemplate

} // namespace Empiria.Presentation.Web.Content