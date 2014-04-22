/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : ViewModel                                        Pattern  : MVC (Model part)                  *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Abstract class that stores information in a model-view-controller pattern.                    *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Specialized;

namespace Empiria.Presentation {

  /// <summary>Abstract class that stores information in a model-view-controller type.</summary>
  public abstract class ViewModel : GeneralObject {

    #region Fields

    private const string thisTypeName = "ObjectType.GeneralObject.ViewModel";

    private string title = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    private ViewModel()
      : base(thisTypeName) {
      // Abstract class. Object creation of this type not allowed.
    }

    protected ViewModel(string typeName)
      : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public ViewModel Parse(int id) {
      return BaseObject.Parse<ViewModel>(thisTypeName, id);
    }

    static public ViewModel Parse(string viewModelNamespace) {
      return BaseObject.Parse<ViewModel>(thisTypeName, viewModelNamespace);
    }

    #endregion Constructors and parsers

    #region Public properties

    public string ContentHint {
      get { return base.GetAttribute<string>("ContentHint"); }
    }

    public string ContentTitle {
      get { return base.GetAttribute<string>("ContentTitle"); }
    }

    public bool DisplayContentTitle {
      get { return (this.ContentTitle != String.Empty); }
    }

    public string FullNamespace {
      get { return base.NamedKey; }
    }

    public string ImageName {
      get { return base.GetAttribute<string>("ImageName"); }
    }

    public string Namespace {
      get { return base.NamedKey; }
    }

    public string Source {
      get { return base.GetAttribute<string>("Source"); }
    }

    public string Title {
      get { return ((title.Length == 0) ? base.Name : title); }
      set { title = value; }
    }

    #endregion Public properties

    #region Internal properties

    internal bool KeepInNavigationHistory {
      get { return base.GetAttribute<bool>("KeepInHistory"); }
    }

    #endregion Internal properties

    #region Public methods

    //OOJJOO: refactoring Parece ser que este método está deprecated A PESAR de que se emplea en la aplicación cliente
    public string GetQueryString(NameValueCollection viewParameters) {
      //RelationMemberHashList parameters = base.Relations.CloneGroup("RunTimeParameters", viewParameters);

      //IEnumerator<RelationMember> enumerator = parameters.GetEnumerator();
      string queryString = String.Empty;
      //while (enumerator.MoveNext()) {
      //  RelationMember parameter = enumerator.Current;
      //  string value = Convert.ToString(parameter.GetTarget());
      //  if (!String.IsNullOrEmpty(value)) {
      //    if (queryString.Length != 0) {
      //      queryString += "&";
      //    }
      //    queryString += parameter.Name + "=" + value;
      //  }
      //}
      return queryString;
    }

    #endregion Public methods

  } // class ViewModel

} // namespace Empiria.Presentation