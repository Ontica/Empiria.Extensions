/* Empiria® Presentation Framework 2014 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : IContentParser                                   Pattern  : Model View Controller             *
*  Date      : 28/Mar/2014                                      Version  : 5.5     License: CC BY-NC-SA 4.0  *
*                                                                                                            *
*  Summary   : Interface that provides methods for user interface content parsing.                           *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2014. **/

namespace Empiria.Presentation {

  /// <summary>Interface that represents a view manager. Each type of user interface technology has an
  ///associated view manager, making it easier to add different application types.</summary>
  public interface IContentParser {

    #region Members definition

    string ThemeFolder { get; }

    string GetContent(string uiTemplateNamedKey);

    #endregion Members definition

  } // class IContentParser

} // namespace Empiria.Presentation
