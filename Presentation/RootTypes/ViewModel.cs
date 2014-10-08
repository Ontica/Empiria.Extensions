/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : ViewModel                                        Pattern  : MVC (Model part)                  *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Abstract class that stores information in a model-view-controller pattern.                    *
*                                                                                                            *
********************************* Copyright (c) 2002-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Specialized;

namespace Empiria.Presentation {

  /// <summary>Abstract class that stores information in a model-view-controller type.</summary>
  public abstract class ViewModel : GeneralObject {

    #region Constructors and parsers

    protected ViewModel() {
      // Required by Empiria Framework.
    }

    static public ViewModel Parse(int id) {
      return BaseObject.ParseId<ViewModel>(id);
    }

    static public ViewModel Parse(string viewModelNamespace) {
      return BaseObject.ParseKey<ViewModel>(viewModelNamespace);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField(ExtensionDataFieldName + ".ContentHint")]
    public string ContentHint {
      get;
      private set;
    }

    [DataField(ExtensionDataFieldName + ".ContentTitle")]
    public string ContentTitle {
      get;
      set;
    }

    public bool DisplayContentTitle {
      get {
        return (this.ContentTitle != String.Empty);
      }
    }

    public string FullNamespace {
      get {
        return base.NamedKey;
      }
    }

    [DataField(ExtensionDataFieldName + ".ImageName")]
    public string ImageName {
      get;
      private set;
    }

    public string Namespace {
      get {
        return base.NamedKey;
      }
    }

    [DataField(ExtensionDataFieldName + ".Source", IsOptional = false)]
    public string Source {
      get;
      private set;
    }

    private string _title = String.Empty;
    public string Title {
      get {
        return ((_title.Length == 0) ? base.Name : _title);
      }
      set {
        _title = value;
      }
    }

    #endregion Public properties

    #region Internal properties

    [DataField(ExtensionDataFieldName + ".KeepInHistory", Default = true)]
    internal bool KeepInNavigationHistory {
      get;
      private set;
    }

    #endregion Internal properties

  } // class ViewModel

} // namespace Empiria.Presentation
