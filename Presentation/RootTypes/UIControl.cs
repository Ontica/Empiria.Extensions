/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIControl                                        Pattern  : Composite (GoF Leaf part)         *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Represents a leaf user interface control that can not contains other controls.                *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/

namespace Empiria.Presentation {

  public enum ComboControlUseMode {
    ObjectCreation = 'C',
    ObjectSearch = 'S',
  }

  /// <summary>Represents a leaf user interface control that can not contains other controls.</summary>
  public class UIControl : UIComponentItem {

    #region Fields

    #endregion Fields

    #region Constructors and parsers

    internal UIControl(UIComponent component)
      : base(component) {

    }

    #endregion Constructors and parsers

    #region Public properties

    #endregion Public properties

    #region Protected methods

    protected override string ImplementsParseContentAsString(IContentParser contentParser) {
      return base.UITargetComponent.ParseComponentItemAsString(this, contentParser);
    }

    #endregion Protected methods

    #region Private methods

    #endregion Private methods

  } // class UIControl

} // namespace Empiria.Presentation.Web.Content
