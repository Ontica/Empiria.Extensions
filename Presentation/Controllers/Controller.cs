/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation.Controllers                 Assembly : Empiria.Presentation.dll          *
*  Type      : Controller                                       Pattern  : State Machine                     *
*  Version   : 6.0        Date: 04/Jan/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : General class that controls the behavior of a finite state machine.                           *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
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
