﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : BreadcrumbBarContent                             Pattern  : Standard Class with Items Cache   *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the XHTML content for a breadcrumb bar navigator list for a web page.                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

namespace Empiria.Presentation.Web.Content {

  /// <summary> Holds the XHTML content for a breadcrumb bar navigator list for a web page.</summary>
  public sealed class BreadcrumbBarContent : WebContent {

    #region Fields

    private Dictionary<string, string> breadcrumbsCache = new Dictionary<string, string>(32);

    #endregion Fields

    #region Constructors and parsers

    public BreadcrumbBarContent(WebPage webPage)
      : base(webPage) {

    }

    #endregion Constructors and parsers

    #region Public methods

    public string GetContent() {
      string viewNamespace = WebPage.ViewModel.FullNamespace;
      string xhtml = String.Empty;

      if (!breadcrumbsCache.TryGetValue(viewNamespace, out xhtml)) {
        xhtml = BuildContent(viewNamespace);
        breadcrumbsCache.Add(viewNamespace, xhtml);
      }
      return xhtml;
    }

    #endregion Public methods

    #region Private methods

    private string BuildContent(string viewNamespace) {
      string container = GetContent("Container");
      string firstBreadcrumb = GetContent("Breadcrumb.First");
      string internalBreadcrumb = GetContent("Breadcrumb.Internal");
      WebViewModel viewModel = WebPage.ViewModel;
      string item = String.Empty;
      string xhtml = String.Empty;

      //Empiria.Ontology.FixedList breadcrumbs = viewModel.BreadcrumbCommandsMap;

      //CommandContent commandContent = new CommandContent(this.WebPage);
      //for (int i = 0; i < breadcrumbs.Count; i++) {
      //  //Empiria.Presentation.NavigationCommand command = (Empiria.Presentation.NavigationCommand) breadcrumbs[i];
      //  //if (i == 0) {
      //  //  item = commandContent.GetContent(command, firstBreadcrumb);
      //  //} else {
      //  //  item = commandContent.GetContent(command, internalBreadcrumb);
      //  //}
      //  xhtml += item;
      //}
      return container.Replace("{CONTENTS}", xhtml);
    }

    #endregion Private methods

  } // class BreadcrumbBarContent

} // namespace Empiria.Presentation.Web.Content
