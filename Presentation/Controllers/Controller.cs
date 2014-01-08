/* Empiria® Presentation Framework 2014 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation.Controllers                 Assembly : Empiria.Presentation.dll          *
*  Type      : Controller                                       Pattern  : State Machine                     *
*  Date      : 28/Mar/2014                                      Version  : 5.5     License: CC BY-NC-SA 4.0  *
*                                                                                                            *
*  Summary   : General class that controls the behavior of a finite state machine.                           *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2014. **/
using System;

namespace Empiria.Presentation.Controllers {

  /// <summary>General class that controls the behavior of a finite state machine.</summary>
  public class Controller {

    #region Fields

    private string exception = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    public Controller() {

    }

    #endregion Constructors and parsers

    #region Public properties

    public string Exception {
      get { return exception; }
    }

    #endregion Public properties

    #region Protected methods

    protected void SetException(string message) {
      exception = message;
    }

    #endregion Protected methods

  } // class Controller

} // namespace Empiria.Presentation.Controllers