/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebViewModel                                     Pattern  : MVC (Model part)                  *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Type  that holds view model information used in web based applications.                       *
*                                                                                                            *
********************************* Copyright (c) 2002-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Presentation.Web {

  /// <summary>Type that holds view model information used in web based applications.</summary>
  public class WebViewModel : ViewModel {

    #region Constructors and parsers

    private WebViewModel() {
      // Required by Empiria Framework.
    }

    static public new WebViewModel Parse(int id) {
      return BaseObject.ParseId<WebViewModel>(id);
    }

    #endregion Constructors and parsers

  } // class WebViewModel

} // namespace Empiria.Presentation.Web
