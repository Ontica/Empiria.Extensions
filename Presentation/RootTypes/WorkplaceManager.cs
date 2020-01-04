/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : WorkplaceManager                                 Pattern  : Model View Controller             *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Coordinates and manages user session workplaces and user interface interaction.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Presentation {

  /// <summary>Coordinates and manages user session workplaces and user interface interaction.</summary>
  public sealed class WorkplaceManager {

    #region Fields

    static private readonly string defaultViewModel = ConfigurationData.GetString("Session.DefaultViewModel");

    private IViewManager viewManager = null;
    private WorkplaceCollection workplaceCollection = new WorkplaceCollection();
    private System.Guid currentWorkplaceGuid = System.Guid.Empty;

    #endregion Fields

    #region Constructors and parsers

    public WorkplaceManager(IViewManager viewManager) {
      if (viewManager != null) {
        this.viewManager = viewManager;
      } else {
        throw new PresentationException(PresentationException.Msg.NullViewManager);
      }
    }

    #endregion Constructors and parsers

    #region Internal properties

    internal WorkplaceCollection Workplaces {
      get { return workplaceCollection; }
    }

    #endregion Internal properties

    #region Public methods

    public Workplace CreateWorkplace() {
      Workplace workplace = new Workplace(viewManager);

      workplaceCollection.Add(workplace);
      currentWorkplaceGuid = workplace.Guid;

      return workplace;
    }

    public void Dispose() {
      //workplaceCollection.Clear();
    }

    public void DisposeWorkplace(System.Guid workplaceGuid) {
      if (workplaceCollection.Contains(workplaceGuid)) {
        workplaceCollection.Remove(workplaceGuid);
      }
    }

    public Workplace GetCurrent() {
      if (currentWorkplaceGuid != Guid.Empty) {
        return GetWorkplace(currentWorkplaceGuid);
      } else {
        throw new PresentationException(PresentationException.Msg.CurrentWorkplaceIsNull);
      }
    }

    public Workplace GetWorkplace(System.Guid workplaceGuid) {
      if (workplaceCollection.Contains(workplaceGuid)) {
        currentWorkplaceGuid = workplaceCollection[workplaceGuid].Guid;
        return workplaceCollection[workplaceGuid];
      } else {
        throw new PresentationException(PresentationException.Msg.WorkplaceNotFound);
      }
    }

    public void Start(bool gotoMainPage) {
      Dispose();

      Workplace workplace = CreateWorkplace();

      if (gotoMainPage) {
        ViewModel viewModel = ViewModel.Parse(defaultViewModel);

        workplace.ActivateView(viewModel);
      }
    }

    #endregion Public methods

  } // class WorkplaceManager

} // namespace Empiria.Presentation
