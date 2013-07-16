/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIControl                                        Pattern  : Composite (GoF Leaf part)         *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Represents a leaf user interface control that can not contains other controls.                *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/

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