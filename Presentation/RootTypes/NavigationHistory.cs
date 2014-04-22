/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : NavigationHistory                                Pattern  : Standard Class                    *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Handles the navigation history for a workplace.                                               *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

namespace Empiria.Presentation {

  /// <summary>Handles the navigation history for a workplace.</summary>
  public sealed class NavigationHistory {

    #region Fields

    private Workplace workplace = null;
    private List<string> history = new List<string>(32);
    private int currentPosition = -1;

    #endregion Fields

    #region Constructors and parsers

    internal NavigationHistory(Workplace workplace) {
      this.workplace = workplace;
    }

    #endregion Constructors and parsers

    #region Public properties

    public bool CanBack {
      get { return (0 <= (currentPosition - 1)); }
    }

    public bool CanForward {
      get { return ((currentPosition + 1) < history.Count); }
    }

    public int Count {
      get { return history.Count; }
    }

    #endregion Public properties

    #region Public methods

    public string Back() {
      return Back(1);
    }

    public string Back(int distance) {
      if (distance <= currentPosition) {
        currentPosition -= distance;
      } else {
        currentPosition = 0;
      }
      return GetCurrentView();
    }

    public string Forward() {
      return Forward(1);
    }

    public string Forward(int distance) {
      if (distance <= (history.Count - currentPosition)) {
        currentPosition += (int) distance;
      } else {
        currentPosition = history.Count - 1;
      }
      return GetCurrentView();
    }

    #endregion Public methods

    #region Internal methods

    internal void Push(string viewName) {
      Push(viewName, null);
    }

    internal void Push(string viewName, string queryString) {
      if (!CanPush(viewName, queryString)) {
        return;
      }
      if ((currentPosition + 1) < history.Count) {
        history.RemoveRange(currentPosition + 1, history.Count - currentPosition - 1);
      }
      history.Add(BuildViewName(viewName, queryString));
      currentPosition = history.Count - 1;
    }

    #endregion Internal methods

    #region Private methods

    private string BuildViewName(string viewName, string queryString) {
      if (String.IsNullOrEmpty(queryString)) {
        return viewName;
      } else {
        return viewName + "?" + queryString;
      }
    }

    private bool CanPush(string viewName) {
      return CanPush(viewName, null);
    }

    private bool CanPush(string viewName, string queryString) {
      if (history.Count == 0) {
        return true;
      }
      string toPushViewName = BuildViewName(viewName, queryString);
      return (toPushViewName != history[currentPosition]);
    }

    private string GetCurrentView() {
      return history[this.currentPosition];
    }

    #endregion Private methods

  } // class NavigationHistory

} // namespace Empiria.Presentation