/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebUserControl                                   Pattern  : None                              *
*  Version   : 6.5        Date: 25/Jun/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Abstract type that represents an ASP user control (ascx). All Empiria user controls           *
*              must be derived from this type.                                                               *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
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
