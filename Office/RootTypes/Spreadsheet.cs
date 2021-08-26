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

using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;

namespace Empiria.Office {

  public enum Style {
    Bold,
    LineThrough
  }

  /// <summary>Provides services to interact with Office spreadsheets through SpreadsheetLight.</summary>
  public class Spreadsheet : IDisposable {

    private readonly SLDocument _spreadsheet;

    #region Constructors and parsers

    protected Spreadsheet() {
      _spreadsheet = new SLDocument();
    }

    private Spreadsheet(string path) {
      _spreadsheet = new SLDocument(path);
    }


    static public Spreadsheet Create() {
      return new Spreadsheet();
    }


    static public Spreadsheet CreateFromTemplate(string templatePath) {
      return new Spreadsheet(templatePath);
    }


    static public Spreadsheet Open(string path) {
      return new Spreadsheet(path);
    }


    #endregion Constructors and parsers

    #region Public Methods

    public void Close() {
      this.Dispose();
    }


    public void Save() {
      _spreadsheet.Save();
    }


    public void SaveAs(string fileName) {
      _spreadsheet.SaveAs(fileName);
    }

    public void RemoveColumn(string column) {
      _spreadsheet.DeleteColumn(column, 1);
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

    public void SetCellStyle(Style styleType, string cellName) {
      SLStyle style = GetSLStyle(styleType);

      _spreadsheet.SetCellStyle(cellName, style);
    }

    public void SetRowStyle(Style styleType, int row) {
      SLStyle style = GetSLStyle(styleType);

      _spreadsheet.SetRowStyle(row, style);
    }

    #endregion Public Methods

    #region Private Methods

    private SLStyle GetSLStyle(Style styleType) {
      SLStyle style = new SLStyle();

      if (styleType == Style.Bold) {
        style.Font.Bold = true;
        style.Font.FontName = "Courier New";
      } else if (styleType == Style.LineThrough) {
        style.Font.Strike = true;
        style.Font.FontName = "Courier New";
      }
      return style;
    }

    #endregion Private Methods

    #region IDisposable Support

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // TODO
        }
        _spreadsheet.Dispose();

        disposedValue = true;
      }
    }


    public void Dispose() {
      Dispose(true);
    }


    #endregion IDisposable Support

  }  // class OpenXMLSpreadsheet

}  // namespace Empiria.Office.Providers

