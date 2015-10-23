/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebViewModel                                     Pattern  : MVC (Model part)                  *
*  Version   : 6.5                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Type  that holds view model information used in web based applications.                       *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
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
