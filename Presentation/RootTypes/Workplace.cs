/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : Workplace                                        Pattern  : Standard Class                    *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains the information of running instance of Empiria Workplace. Workplaces are the main    *
*              objects of the user interface process model.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
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
      get { return ExecutionServer.IsAuthenticated; }
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
