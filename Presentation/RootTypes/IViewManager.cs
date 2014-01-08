/* Empiria® Presentation Framework 2014 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : IViewManager                                     Pattern  : Model View Controller             *
*  Date      : 28/Mar/2014                                      Version  : 5.5     License: CC BY-NC-SA 4.0  *
*                                                                                                            *
*  Summary   : Interface that represents a view manager. Each type of user interface technology has an       *
*              associated view manager, making it easier to add different application types.                 *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2014. **/

namespace Empiria.Presentation {

  /// <summary>Interface that represents a view manager. Each type of user interface technology has an
  ///associated view manager, making it easier to add different application types.</summary>
  public interface IViewManager {

    #region Methods definition

    void ActivateView(ViewModel viewModel);
    void ActivateView(ViewModel viewModel, bool preserve);
    void ActivateView(ViewModel viewModel, string viewParameters);
    void ActivateView(ViewModel viewModel, string viewParameters, bool preserve);

    #endregion Methods definition

  } // class IViewManager

} // namespace Empiria.Presentation
