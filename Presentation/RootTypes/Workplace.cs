/* Empiria Presentation Framework 2015 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : Workplace                                        Pattern  : Standard Class                    *
*  Version   : 6.0        Date: 04/Jan/2015                     License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains the information of running instance of Empiria Workplace. Workplaces are the main    *
*              objects of the user interface process model.                                                  *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

namespace Empiria.Presentation {

  /// <summary>Contains the information of a running instance of Empiria Workplace.
  ///Workplaces are the main objects of the user interface process model.</summary>
  public sealed class Workplace {

    #region Fields

    private readonly System.Guid guid = System.Guid.NewGuid();
    private System.Guid lastRequestGuid = System.Guid.Empty;
    private ViewModel currentViewModel = null;
    private string currentViewParameters = null;
    private IViewManager viewManager = null;
    private NavigationHistory navigationHistory = null;
    private string queryString = String.Empty;

    #endregion Fields

    #region Constructors and parsers

    internal Workplace(IViewManager viewManager) {
      this.viewManager = viewManager;
      this.navigationHistory = new NavigationHistory(this);
      this.lastRequestGuid = System.Guid.NewGuid();
    }

    #endregion Constructors and parsers

    #region Public properties

    public ViewModel CurrentViewModel {
      get { return currentViewModel; }
      set { currentViewModel = value; }
    }

    public string CurrentViewParameters {
      get { return currentViewParameters; }
      set { currentViewParameters = value; }
    }

    public System.Guid Guid {
      get { return guid; }
    }

    public bool IsUserAuthenticated {
      get { return (ExecutionServer.CurrentIdentity != null); }
    }

    public System.Guid LastRequestGuid {
      get { return lastRequestGuid; }
    }

    public NavigationHistory NavigationHistory {
      get { return navigationHistory; }
    }

    public string Theme {
      get { return "default"; }
    }

    public Empiria.Security.EmpiriaUser User {
      get { return Empiria.Security.EmpiriaUser.Current; }
    }

    #endregion Public properties

    #region Public methods

    public void ActivateView(ViewModel viewModel) {
      this.currentViewModel = viewModel;

      if (currentViewModel.KeepInNavigationHistory) {
        navigationHistory.Push(currentViewModel.Namespace, String.Empty);
      }
      viewManager.ActivateView(currentViewModel);
    }

    public void ActivateView(ViewModel viewModel, string viewParameters) {
      this.currentViewModel = viewModel;
      this.currentViewParameters = viewParameters;

      if (currentViewModel.KeepInNavigationHistory) {
        navigationHistory.Push(currentViewModel.Namespace, viewParameters);
      }
      viewManager.ActivateView(currentViewModel, viewParameters);
    }

    //public System.Guid UpdateLastRequestGuid() {
    //  lastRequestGuid = System.Guid.NewGuid();
    //  return lastRequestGuid;
    //}

    #endregion Public methods

  } // class Workplace

} // namespace Empiria.Presentation
