/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebViewModel                                     Pattern  : MVC (Model part)                  *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Type  that holds view model information used in web based applications.                       *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1994-2013. **/

namespace Empiria.Presentation.Web {

  /// <summary>Type that holds view model information used in web based applications.</summary>
  public class WebViewModel : ViewModel {

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.ViewModel.WebViewModel";

    #endregion Fields

    #region Constructors and parsers

    public WebViewModel()
      : base(thisTypeName) {

    }

    protected WebViewModel(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new WebViewModel Parse(int id) {
      return BaseObject.Parse<WebViewModel>(thisTypeName, id);
    }

    #endregion Constructors and parsers

  } // class WebViewModel

} // namespace Empiria.Presentation.Web
