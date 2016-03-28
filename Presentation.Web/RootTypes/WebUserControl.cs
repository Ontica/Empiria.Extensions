/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebUserControl                                   Pattern  : None                              *
*  Version   : 6.7                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract type that represents an ASP user control (ascx). All Empiria user controls           *
*              must be derived from this type.                                                               *
*                                                                                                            *
********************************* Copyright (c) 2002-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.UI;
using System.Collections.Specialized;

using Empiria.Security;

namespace Empiria.Presentation.Web {

  /// <summary>Abstract type that represents an ASP user control (ascx). All Empiria user controls
  ///must be derived from this type.</summary>
  public abstract class WebUserControl : UserControl {

    #region Public properties

    public EmpiriaUser User {
      get {
        return Empiria.Security.EmpiriaUser.Current;
      }
    }

    #endregion Public properties

    #region Public methods

    public Command GetCurrentCommand() {
      return new Command(this.Request);
    }

    #endregion Public methods

  } // class WebUserControl

} // namespace Empiria.Presentation.Web
