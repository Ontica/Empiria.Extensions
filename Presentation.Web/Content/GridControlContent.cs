﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Web Presentation Services         *
*  Namespace : Empiria.Presentation.Web.Content                 Assembly : Empiria.Presentation.Web.dll      *
*  Type      : GridControlContent                               Pattern  : Standard Class                    *
*  Version   : 6.5                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Holds the XHTML content for grid navigation buttons in a web application.                     *
*                                                                                                            *
********************************* Copyright (c) 2002-2015. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.UI.WebControls;

namespace Empiria.Presentation.Web.Content {

  /// <summary>Holds the XHTML content for the main navigation buttons in a web application.</summary>
  public sealed class GridControlContent : WebContent {

    #region Fields

    private PagedDataSource pagedDataSource = null;

    #endregion Fields

    #region Constructors and parsers

    public GridControlContent(WebPage webPage)
      : base(webPage) {

    }

    #endregion Constructors and parsers

    #region Public properties

    public PagedDataSource PagedDataSource {
      get { return pagedDataSource; }
      set { pagedDataSource = value; }
    }

    #endregion Public properties

    #region Public methods

    public string GetContent() {
      string xhtml = GetContent("Grid.Control");

      string itemsCounter = String.Empty;

      if (pagedDataSource.DataSourceCount == 0) {
        if (!WebPage.IsPostBack && (WebPage.MasterPageFile != null) && (WebPage.MasterPageFile.IndexOf("viewer.master") == -1)) {
          itemsCounter = "No se ha efectuado ninguna búsqueda";
        } else {
          itemsCounter = "Ningún elemento encontrado";
        }
      } else if (pagedDataSource.DataSourceCount == 1) {
        itemsCounter = "Un elemento encontrado";
      } else {
        itemsCounter = pagedDataSource.DataSourceCount.ToString("N0") + " elementos encontrados";
      }

      string currentPage = "Página " + ((int) (pagedDataSource.CurrentPageIndex + 1)).ToString("N0");
      currentPage += " de " + pagedDataSource.PageCount.ToString("N0");

      xhtml = xhtml.Replace("{GRID.ITEMS.COUNTER}", itemsCounter);
      xhtml = xhtml.Replace("{GRID.CURRENT.PAGE}", currentPage);
      xhtml = xhtml.Replace("{GRID.CURRENT.PAGE.INDEX}", pagedDataSource.CurrentPageIndex.ToString());
      xhtml = xhtml.Replace("{THEME_FOLDER}", ThemeFolder);

      return xhtml;
    }

    #endregion Public methods

  } // class GridControlContent

} // namespace Empiria.Presentation.Web.Content
