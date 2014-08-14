/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : MultiViewDashboard                               Pattern  : Model View Controller             *
*  Version   : 6.0        Date: 23/Oct/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Abstract type that represents a dashboard explorer web page. All Empiria web pages types      *
*              must be descendants of this class.                                                            *
*                                                                                                            *
********************************* Copyright (c) 2002-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

using Empiria.Presentation.Web.Content;

namespace Empiria.Presentation.Web {

  /// <summary>Abstract type that represents a dashboard explorer web page. 
  /// All Empiria object explorer web pages types must be descendants of this class.</summary>
  public abstract class MultiViewDashboard : WebPage {

    #region Abstract members

    public abstract Repeater ItemsRepeater { get; }

    protected abstract DataView LoadDataSource();
    protected abstract bool ExecutePageCommand();
    protected abstract void Initialize();
    protected abstract void LoadPageControls();
    protected abstract void SetRepeaterTemplates();

    #endregion Abstract members

    #region Fields

    private string hintContent = "Para obtener la información, primero se deben proporcionar las opciones de búsqueda y luego hacer clic en el botón 'Buscar'.";
    private int pageSize = 20;
    private bool repeaterIsEmpty = false;
    private int selectedTabStrip = 0;
    private bool showNavigationBar = true;
    private bool showTabStripMenu = true;
    private string userViewSortExpression = String.Empty;
    private int viewColumnsCount = 0;
    private int itemsPerRow = 1;
    private string viewTitle = String.Empty;

    private DataView dataSource = new DataView();
    private PagedDataSource pagedDataSource = new PagedDataSource();

    private Dictionary<string, decimal> totals = new Dictionary<string, decimal>();
    private string paretoAnalysisColumnName = String.Empty;
    private decimal paretoAnalysisColumnTotal = decimal.Zero;
    private bool loadedFromCache = false;
    private string navigationBar = String.Empty;
    private bool loadInboxesInQuickMode = false;

    #endregion Fields

    #region Public properties

    public int CurrentPageIndex {
      get { return this.pagedDataSource.CurrentPageIndex; }
    }

    public DataView DataSource {
      get { return this.dataSource; }
    }

    protected bool LoadInboxesInQuickMode {
      get { return this.loadInboxesInQuickMode; }
      set { this.loadInboxesInQuickMode = value; }
    }

    public string HintContent {
      get {
        string temp = String.Empty;
        if (this.RepeaterIsEmpty) {
          temp = "<tbody><tr class='hintDataRow'><td colspan='" + this.viewColumnsCount.ToString() + "'>";
          temp += this.hintContent + "</td></tr></tbody>";
        } else if (this.DataSource.Count == 0) {
          temp = "<tbody><tr class='notFoundDataRow'><td colspan='" + this.viewColumnsCount.ToString() + "'>";
          temp += "No encontré información que cumpla con las condiciones de búsqueda proporcionadas.</td></tr></tbody>";
        }
        return temp;
      }
      protected set { this.hintContent = value; }
    }

    public PagedDataSource PagedDataSource {
      get { return this.pagedDataSource; }
    }

    public int ItemsPerRow {
      get { return this.itemsPerRow; }
      set { this.itemsPerRow = value; }
    }

    public int PageSize {
      get { return this.pageSize; }
      protected set { this.pageSize = value; }
    }

    public bool RepeaterIsEmpty {
      get { return this.repeaterIsEmpty; }
    }

    public bool ShowGridNavigationBar {
      get { return this.showNavigationBar; }
      protected set { this.showNavigationBar = value; }
    }

    public int SelectedTabStrip {
      get { return this.selectedTabStrip; }
    }

    public bool ShowTabStripMenu {
      get { return this.showTabStripMenu; }
    }

    public Dictionary<string, decimal> Totals {
      get { return this.totals; }
    }

    public string UserViewSortExpression {
      get { return this.userViewSortExpression; }
      set { this.userViewSortExpression = value; }
    }

    public int ViewColumnsCount {
      get { return this.viewColumnsCount; }
      protected set { this.viewColumnsCount = value; }
    }

    public string ViewTitle {
      get {
        if (this.viewTitle.Length != 0) {
          return "<tr class='titleDataRow'><th colspan='" + this.viewColumnsCount + "'>" + this.viewTitle + "</th></tr>";
        } else {
          return String.Empty;
        }
      }
      protected set { this.viewTitle = value; }
    }

    #endregion Public properties

    #region Public method

    public bool IsLastItem(int index) {
      if (dataSource == null || dataSource.Count == 0) {
        return true;
      }
      if ((index + 1) == this.pageSize) {
        return true;
      }
      return false;
    }

    public decimal GetColumnPercentage(string dataItemName, string dataTotalItemName) {
      if (!this.totals.ContainsKey(dataItemName) || !this.totals.ContainsKey(dataTotalItemName)) {
        return decimal.Zero;
      }
      if (((decimal) this.totals[dataTotalItemName]) != 0m) {
        return ((decimal) this.totals[dataItemName]) * 100m / ((decimal) this.totals[dataTotalItemName]);
      } else {
        return decimal.Zero;
      }
    }

    public decimal GetColumnTotal(string dataItemName) {
      if (this.totals.ContainsKey(dataItemName)) {
        return (decimal) this.totals[dataItemName];
      } else {
        return decimal.Zero;
      }
    }

    #endregion Public method

    #region Protected methods

    protected void LoadEmptyRepeater() {
      this.repeaterIsEmpty = true;
      this.userViewSortExpression = String.Empty;
      this.paretoAnalysisColumnName = String.Empty;
      this.paretoAnalysisColumnTotal = decimal.Zero;
      RemoveTotalsColumns();
      LoadRepeater(new DataView(), 0);
    }

    protected void LoadRepeater() {
      DataView dataSource = LoadDataSource();
      this.repeaterIsEmpty = false;
      LoadRepeater(dataSource, 0);
    }

    protected string NavigationBarContent() {
      return navigationBar;
    }

    protected override void OnLoadComplete(EventArgs e) {
      ClientScript.RegisterHiddenField("hdnEmpiriaPageUserViewSortExpression", this.userViewSortExpression);
      ClientScript.RegisterHiddenField("hdnEmpiriaPageSelectedTabStripIndex", this.selectedTabStrip.ToString());
      base.Master.AppendEndLoadScript("showTabStripItem(" + this.selectedTabStrip.ToString() + ");");

      base.OnLoadComplete(e);
    }

    protected override void OnPreLoad(EventArgs e) {
      base.OnPreLoad(e);
      if (!String.IsNullOrEmpty(Request.Form["hdnEmpiriaPageSelectedTabStripIndex"])) {
        this.selectedTabStrip = int.Parse(Request.Form["hdnEmpiriaPageSelectedTabStripIndex"]);
      }
      if (!String.IsNullOrEmpty(Request.Form["hdnEmpiriaPageUserViewSortExpression"])) {
        this.userViewSortExpression = Request.Form["hdnEmpiriaPageUserViewSortExpression"];
      }
    }

    protected void Page_Load(object sender, EventArgs e) {
      if (!String.IsNullOrEmpty(Request.QueryString["showTabStripMenu"])) {
        this.showTabStripMenu = false;
      }
      Initialize();
      if (IsPostBack) {
        ProcessCommand();
        LoadPageControls();
      } else {
        SetDefaultInbox();
        LoadPageControls();
        if (this.loadInboxesInQuickMode) {
          LoadRepeater();
        } else {
          LoadEmptyRepeater();
        }
      }
    }

    protected void SetParetoAnalysisColumn(string paretoAnalysisColumnName) {
      if (this.loadedFromCache) {
        return;
      }
      this.paretoAnalysisColumnName = paretoAnalysisColumnName;
    }

    protected void SetTotalsColumns(string firstColumnName, params string[] additionalColumnNames) {
      if (this.loadedFromCache) {
        return;
      }
      Assertion.AssertObject(firstColumnName, "firstColumnName");
      totals.Clear();
      totals.Add(firstColumnName, decimal.Zero);
      for (int i = 0; i < additionalColumnNames.Length; i++) {
        totals.Add(additionalColumnNames[i], decimal.Zero);
      }
    }

    protected void RemoveTotalsColumns() {
      this.paretoAnalysisColumnName = String.Empty;
      this.totals = new Dictionary<string, decimal>();
    }

    #endregion Protected methods

    #region Private methods

    private string BuildSortExpression() {
      if (String.IsNullOrEmpty(Request.Form["hdnEmpiriaPageUserViewSortExpression"])) {
        return base.CommandParametersRaw;
      }
      string lastSortExpression = Request.Form["hdnEmpiriaPageUserViewSortExpression"];
      if (base.CommandParametersRaw == lastSortExpression) {
        return SwapSortExpression(base.CommandParametersRaw);
      } else {
        return base.CommandParametersRaw;
      }
    }

    private void CalculateColumnTotals() {
      if (this.totals.Count == 0 || this.loadedFromCache) {
        return;
      }
      string[] totalsKeys = new string[this.totals.Count];
      this.totals.Keys.CopyTo(totalsKeys, 0);

      for (int i = 0, length = this.dataSource.Count; i < length; i++) {
        for (int j = 0; j < totalsKeys.Length; j++) {
          string key = totalsKeys[j];
          this.totals[key] += Convert.ToDecimal(dataSource[i][key]);
          if (paretoAnalysisColumnName.Length != 0) {
            this.paretoAnalysisColumnTotal += Convert.ToDecimal(dataSource[i][paretoAnalysisColumnName]);
          }
        } // for j
      } // for i
    }

    private void CalculateParetoColumns() {
      if (this.paretoAnalysisColumnName.Length == 0 || this.loadedFromCache) {
        return;
      }
      if (this.dataSource.Table == null) {
        return;
      }
      this.dataSource.Table.Columns.Add("ParetoItemPercentage", typeof(decimal));
      this.dataSource.Table.Columns.Add("ParetoCumulativePercentage", typeof(decimal));
      this.dataSource.Table.Columns.Add("ParetoItemRanking", typeof(int));
      this.dataSource.Table.Columns.Add("ParetoItemClass", typeof(string));

      decimal cumulativePercentage = decimal.Zero;
      for (int i = 0, length = this.dataSource.Count; i < length; i++) {
        decimal itemPercentage = (paretoAnalysisColumnTotal != decimal.Zero) ?
                                      Convert.ToDecimal(dataSource[i][paretoAnalysisColumnName]) / paretoAnalysisColumnTotal : decimal.Zero;
        cumulativePercentage += itemPercentage;
        this.dataSource[i]["ParetoItemPercentage"] = itemPercentage;
        this.dataSource[i]["ParetoCumulativePercentage"] = cumulativePercentage;
        this.dataSource[i]["ParetoItemRanking"] = i + 1;
        if (cumulativePercentage <= 0.8m) {
          this.dataSource[i]["ParetoItemClass"] = "A";
        } else if (cumulativePercentage > 0.8m && cumulativePercentage <= 0.95m) {
          this.dataSource[i]["ParetoItemClass"] = "B";
        } else if (itemPercentage > decimal.Zero) {
          this.dataSource[i]["ParetoItemClass"] = "C";
        } else if (itemPercentage <= decimal.Zero) {
          this.dataSource[i]["ParetoItemClass"] = "D";
        }
      }
    }

    private void ExecuteGridCommand(DataView dataView) {
      int currentPageIndex = 0;
      if (String.IsNullOrEmpty(Request.Form["hdnGridCurrentPageIndex"])) {
        currentPageIndex = 0;
      } else {
        currentPageIndex = int.Parse(Request.Form["hdnGridCurrentPageIndex"]);
      }
      int maxPageIndex = (dataView.Count / this.pageSize);

      if (maxPageIndex != 0 && ((dataView.Count % this.pageSize) == 0)) {
        maxPageIndex--; // Decrement one if dataSource.Count complete fills the last page
      }
      switch (this.CommandName) {
        case "moveFirst":
          currentPageIndex = 0;
          break;
        case "movePrevious":
          currentPageIndex = Math.Max(currentPageIndex - 1, 0);
          break;
        case "moveNext":
          currentPageIndex = Math.Min(currentPageIndex + 1, maxPageIndex);
          break;
        case "moveLast":
          currentPageIndex = maxPageIndex;
          break;
      }
      LoadRepeater(dataView, currentPageIndex);
    }

    private string ExtractDataSourceName(DataView dataSource) {
      if (dataSource.Table != null) {
        return dataSource.Table.TableName;
      } else {
        return String.Empty;
      }
    }

    private DataView GetCachedDataSource() {
      if (this.Session["lastMultiViewDataSource"] != null) {
        DataView cachedDataSource = (DataView) this.Session["lastMultiViewDataSource"];
        string cachedDataSourceName = ExtractDataSourceName(cachedDataSource);
        if (cachedDataSourceName.Length != 0 && cachedDataSourceName == (string) this.Session["lastMultiViewDataSourceName"]) {
          this.totals = (Dictionary<string, decimal>) this.Session["lastMultiViewDataSourceTotals"];
          this.loadedFromCache = true;
          return cachedDataSource;
        }
      }
      this.loadedFromCache = false;
      this.repeaterIsEmpty = true;
      this.userViewSortExpression = String.Empty;
      this.paretoAnalysisColumnName = String.Empty;
      this.paretoAnalysisColumnTotal = decimal.Zero;
      return new DataView();
    }

    private void LoadRepeater(DataView dataSource, int pageIndex) {
      this.dataSource = dataSource;
      SetRepeaterTemplates();

      if (this.userViewSortExpression.Length != 0 && this.dataSource.Table != null) {
        try {     // Avoids badly formed userViewSortExpression 
          this.dataSource.Sort = this.userViewSortExpression;
        } catch {
          this.userViewSortExpression = String.Empty;
        }
      }
      CalculateColumnTotals();
      CalculateParetoColumns();
      PushDataSourceOnCache();
      LoadPagedDataSource(pageIndex);
      navigationBar = GetNavigationBarContent();
    }

    private void LoadPagedDataSource(int pageIndex) {
      this.pagedDataSource = new PagedDataSource();
      this.pagedDataSource.DataSource = this.dataSource;

      this.pagedDataSource.AllowPaging = true;
      this.pagedDataSource.PageSize = this.pageSize;
      this.pagedDataSource.CurrentPageIndex = pageIndex;
      ItemsRepeater.DataSource = this.pagedDataSource;
      ItemsRepeater.DataBind();
    }

    private string GetNavigationBarContent() {
      if (!this.showNavigationBar) {
        return String.Empty;
      }
      GridControlContent gridControlContent = new GridControlContent(this);
      gridControlContent.PagedDataSource = this.PagedDataSource;

      return gridControlContent.GetContent();
    }

    private void ProcessCommand() {
      if (ExecutePageCommand()) {
        return;
      }
      switch (this.CommandName) {
        case "":
          return;
        case "loadData":
          LoadRepeater();
          return;
        case "sortData":
          this.userViewSortExpression = BuildSortExpression();
          LoadRepeater(GetCachedDataSource(), 0);
          return;
        case "setInbox":
          selectedTabStrip = int.Parse(this.CommandParametersRaw);
          SetRepeaterTemplates();
          if (this.loadInboxesInQuickMode) {
            this.userViewSortExpression = String.Empty;
            this.paretoAnalysisColumnName = String.Empty;
            this.paretoAnalysisColumnTotal = decimal.Zero;
            LoadRepeater();
          } else {
            LoadEmptyRepeater();
          }
          return;
        case "moveFirst":
        case "movePrevious":
        case "moveNext":
        case "moveLast":
          ExecuteGridCommand(GetCachedDataSource());
          return;
        default:
          throw new WebPresentationException(WebPresentationException.Msg.UnrecognizedCommandName, this.CommandName);
      }
    }

    private void PushDataSourceOnCache() {
      lock (this.Session.SyncRoot) {
        this.Session["lastMultiViewDataSourceName"] = ExtractDataSourceName(this.dataSource);
        this.Session["lastMultiViewDataSource"] = this.dataSource;
        this.Session["lastMultiViewDataSourceTotals"] = this.totals;
      }
    }

    private void SetDefaultInbox() {
      if (!IsPostBack) {
        if (!String.IsNullOrEmpty(Request.QueryString["inbox"])) {
          this.selectedTabStrip = int.Parse(Request.QueryString["inbox"]);
        }
      }
    }

    private string SwapSortExpression(string sortExpression) {
      string temp = sortExpression.Replace(" ASC", " SRTTEMP");
      temp = temp.Replace(" DESC", " ASC");
      return temp.Replace(" SRTTEMP", " DESC");
    }

    #endregion Private methods

  } // class MultiViewDashboard

} // namespace Empiria.Presentation.Web
