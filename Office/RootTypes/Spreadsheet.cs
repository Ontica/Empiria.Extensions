/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                Component : Spreadsheet service provider            *
*  Assembly : Empiria.Office.dll                         Pattern   : Provider                                *
*  Type     : Spreadsheet                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services to interact with Office spreadsheets through SpreadsheetLight.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using SpreadsheetLight;

namespace Empiria.Office {

  public enum Style {
    Bold,
    LineThrough,
    FontColor,
    BackgroundColor
  }

  /// <summary>Provides services to interact with Office spreadsheets through SpreadsheetLight.</summary>
  public class Spreadsheet : IDisposable {

    private const string DEFAULT_WORKSHEET_NAME = "Hoja 1";

    private readonly SLDocument _spreadsheet;

    #region Constructors and parsers

    protected Spreadsheet() {
      _spreadsheet = new SLDocument();
    }

    private Spreadsheet(string path) {
      _spreadsheet = new SLDocument(path);
    }

    private Spreadsheet(SLDocument document) {
      _spreadsheet = document;
    }

    static public Spreadsheet Create() {
      return new Spreadsheet();
    }


    static public Spreadsheet CreateFromTemplate(string templatePath) {
      return new Spreadsheet(templatePath);
    }


    static public Spreadsheet CreateFromCSVFile(string csvFilePath) {
      var document = new SLDocument();

      document.RenameWorksheet(SLDocument.DefaultFirstSheetName, DEFAULT_WORKSHEET_NAME);

      var options = new SLTextImportOptions();

      options.UseCommaDelimiter = true;

      document.ImportText(csvFilePath, "A1", options);

      document.SelectWorksheet(DEFAULT_WORKSHEET_NAME);

      return new Spreadsheet(document);
    }


    static public Spreadsheet Open(string path) {
      try {
        return new Spreadsheet(path);
      } catch {
        throw Assertion.EnsureNoReachThisCode(
          "La versión del archivo es anterior a Excel 2007, " +
          "o tiene celdas con referencias o valores incorrectos.");
      }
    }


    #endregion Constructors and parsers

    #region Public Methods

    public string SelectedWorksheet {
      get; private set;
    } = DEFAULT_WORKSHEET_NAME;


    public void Close() {
      this.Dispose();
    }


    public bool IsNotEmpty(string cellName, bool includeFormulas = false) {
      return _spreadsheet.HasCellValue(cellName, includeFormulas);
    }


    public bool IsEmpty(string cellName, bool includeFormulas = false) {
      return !IsNotEmpty(cellName, includeFormulas);
    }


    public bool HasFormula(string cellName) {
      return !_spreadsheet.HasCellValue(cellName) &&
             _spreadsheet.HasCellValue(cellName, true);
    }


    public bool HasValue(string cellName) {
      return _spreadsheet.HasCellValue(cellName);
    }


    public void IndentCell(string cellName, int value) {
      SLStyle style = new SLStyle();

      style.Alignment.Indent = (uint) value;

      _spreadsheet.SetCellStyle(cellName, style);
    }


    public T ReadCellValue<T>(string cellName) {
      if (HasFormula(cellName)) {
        Assertion.EnsureNoReachThisCode(
          $"Hubo un problema al intentar leer la fórmula de la celda {cellName} en [{SelectedWorksheet}]");
      }

      if (typeof(T) == typeof(decimal)) {
        return (T) (object) _spreadsheet.GetCellValueAsDecimal(cellName);
      } else if (typeof(T) == typeof(string)) {
        return (T) (object) _spreadsheet.GetCellValueAsString(cellName);
      } else if (typeof(T) == typeof(DateTime)) {
        return (T) (object) _spreadsheet.GetCellValueAsDateTime(cellName);
      } else if (typeof(T) == typeof(Int32)) {
        return (T) (object) _spreadsheet.GetCellValueAsInt32(cellName);
      } else if (typeof(T) == typeof(Int64)) {
        return (T) (object) _spreadsheet.GetCellValueAsInt64(cellName);
      } else if (typeof(T) == typeof(bool)) {
        return (T) (object) _spreadsheet.GetCellValueAsBoolean(cellName);
      } else {
        return (T) (object) _spreadsheet.GetCellValueAsString(cellName);
      }
    }


    public T ReadCellValue<T>(string cellName, T defaultValue) {
      if (IsEmpty(cellName)) {
        return defaultValue;
      }
      return ReadCellValue<T>(cellName);
    }


    public void Save() {
      _spreadsheet.Save();
    }

    public void SaveAs(string fileName) {
      _spreadsheet.SaveAs(fileName);
    }

    public void SelectWorksheet(string worksheetName) {
      Assertion.Require(worksheetName, "worksheetName");

      if (!_spreadsheet.SelectWorksheet(worksheetName)) {
        Assertion.RequireFail($"A worksheet with name {worksheetName} does not exists in the Excel file.");
      }
      this.SelectedWorksheet = worksheetName;
    }


    public void InsertRow(int startRowIndex) {
      _spreadsheet.InsertRow(startRowIndex, 1);
    }


    public bool IsWorksheetHidden(string worksheetName) {
      return _spreadsheet.IsWorksheetHidden(worksheetName);
    }

    public void RemoveColumn(string column) {
      _spreadsheet.DeleteColumn(column, 1);
    }


    public void RemoveFormat(string startCellName, string endCellName) {
      _spreadsheet.RemoveCellStyle(startCellName, endCellName);
      _spreadsheet.ApplyNamedCellStyle(startCellName, endCellName, SLNamedCellStyleValues.Normal);
    }


    public void SetCell(string cellName, string value) {
      if (value.StartsWith("=")) {
        value = "'" + value;
      }
      _spreadsheet.SetCellValue(cellName, value);
    }

    public void SetCell(string cellName, decimal value) {
      _spreadsheet.SetCellValue(cellName, value);
    }

    public void SetCell(string cellName, int value) {
      _spreadsheet.SetCellValue(cellName, value);
    }

    public void SetCell(string cellName, DateTime value) {
      _spreadsheet.SetCellValue(cellName, value);
    }

    public void SetCellStyle(string cellName, Style styleType) {
      SLStyle style = GetSLStyle(styleType);

      _spreadsheet.SetCellStyle(cellName, style);
    }

    internal void SetCellWrapText(string cellName) {
      SLStyle style = new SLStyle();

      style.SetWrapText(true);
      _spreadsheet.SetCellStyle(cellName, style);
    }

    internal void SetTextAlignment(string cellName, DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues alignment) {
      SLStyle style = new SLStyle();

      style.SetVerticalAlignment(alignment);
      _spreadsheet.SetCellStyle(cellName, style);
    }

    internal void SetCellFontName(string cellName, string fontName) {
      SLStyle style = new SLStyle();

      style.Font.FontName = fontName;
      _spreadsheet.SetCellStyle(cellName, style);
    }

    public void SetFontColor(string cellName, Style styleType, System.Drawing.Color color) {
      SLStyle style = GetSLColorStyle(styleType, color);

      _spreadsheet.SetCellStyle(cellName, style);
    }


    public void SetCellBackgroundStyle(string cellName, Style styleType, System.Drawing.Color color) {
      SLStyle style = GetSLBackgroundStyle(styleType, color);

      _spreadsheet.SetCellStyle(cellName, style);
    }


    public void SetRowBackgroundStyle(int rowIndex, int lastColumnIndex, Style styleType, System.Drawing.Color color) {
      SLStyle style = GetSLBackgroundStyle(styleType, color);

      ApplyRowStyle(style, rowIndex, lastColumnIndex);
    }


    public void SetRowFontColor(int rowIndex, int lastColumnIndex, Style styleType, System.Drawing.Color color) {
      SLStyle style = GetSLColorStyle(styleType, color);

      ApplyRowStyle(style, rowIndex, lastColumnIndex);
    }


    internal void SetRowFontName(int rowIndex, int lastColumnIndex, string fontName) {
      SLStyle style = new SLStyle();

      style.Font.FontName = fontName;

      ApplyRowStyle(style, rowIndex, lastColumnIndex);
    }


    internal void SetRowFontName(int rowIndex, int lastColumnIndex, string fontName, int fontSize) {
      SLStyle style = new SLStyle();

      style.Font.FontName = fontName;
      style.Font.FontSize = fontSize;

      ApplyRowStyle(style, rowIndex, lastColumnIndex);
    }


    public void SetRowStyle(int rowIndex, int lastColumnIndex, Style styleType) {
      SLStyle style = GetSLStyle(styleType);

      ApplyRowStyle(style, rowIndex, lastColumnIndex);
    }


    public string[] Worksheets() {
      return _spreadsheet.GetWorksheetNames()
                         .ToArray();
    }

    #endregion Public Methods

    #region Helpers

    private void ApplyRowStyle(SLStyle style, int rowIndex, int lastColumnIndex) {
      for (int i = 1; i <= lastColumnIndex; i++) {
        _spreadsheet.SetCellStyle(rowIndex, i, style);
      }
    }

    private SLStyle GetSLStyle(Style styleType) {
      SLStyle style = new SLStyle();

      if (styleType == Style.Bold) {
        style.Font.Bold = true;
      } else if (styleType == Style.LineThrough) {
        style.Font.Strike = true;
      }
      return style;
    }

    private SLStyle GetSLColorStyle(Style styleType, System.Drawing.Color color) {
      SLStyle style = new SLStyle();

      if (styleType == Style.FontColor) {
        style.Font.FontColor = color;
      } else {
        style.Font.FontColor = System.Drawing.Color.Black;
      }
      return style;
    }

    private SLStyle GetSLBackgroundStyle(Style styleType, System.Drawing.Color color) {
      SLStyle style = new SLStyle();

      if (styleType == Style.BackgroundColor) {
        style.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid,
                              color, color);
      } else {
        style.Fill.SetPattern(DocumentFormat.OpenXml.Spreadsheet.PatternValues.None,
                              System.Drawing.Color.White, System.Drawing.Color.White);
      }
      return style;
    }


    #endregion Helpers

    #region IDisposable Support

    protected virtual void Dispose(bool disposing) {
      _spreadsheet.Dispose();
    }


    public void Dispose() {
      Dispose(true);
    }

    #endregion IDisposable Support

  }  // class Spreadsheet

}  // namespace Empiria.Office.Providers
