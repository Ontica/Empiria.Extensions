/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                Component : Spreadsheet service provider            *
*  Assembly : Empiria.Office.dll                         Pattern   : Provider                                *
*  Type     : OpenXMLSpreadsheet                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides basic services to interact with Office spreadsheets through OpenXML.                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace Empiria.Office.Providers {

  /// <summary>Provides basic services to interact with Office spreadsheets through OpenXML.</summary>
  public class OpenXMLSpreadsheet : IDisposable {

    private SpreadsheetDocument spreadsheetDocument;


    #region Constructors and parsers

    private OpenXMLSpreadsheet(string path) {
      spreadsheetDocument = SpreadsheetDocument.Open(path, true);
    }


    private OpenXMLSpreadsheet(SpreadsheetDocument spreadsheetDocument) {
      this.spreadsheetDocument = spreadsheetDocument;
    }


    static public OpenXMLSpreadsheet Create() {
      string defaultTemplatePath = ConfigurationData.GetString("Spreadsheet.NewFileTemplate");

      return CreateFromTemplate(defaultTemplatePath);
    }


    static public OpenXMLSpreadsheet CreateFromTemplate(string templatePath) {
      try {
        var spreadsheetDocument = SpreadsheetDocument.CreateFromTemplate(templatePath);

        return new OpenXMLSpreadsheet(spreadsheetDocument);

      } catch (Exception ex) {
        throw new Exception("I can´t create from template file: " + templatePath + " ", ex);
      }
    }


    static public OpenXMLSpreadsheet Open(string path) {
      return new OpenXMLSpreadsheet(path);
    }


    #endregion Constructors and parsers

    #region Public Methods

    public void Close() {
      spreadsheetDocument.Close();
    }


    public void Save() {
      this.UpdateDOM();

      spreadsheetDocument.Save();
    }


    public void SaveAs(string fileName) {
      this.UpdateDOM();

      OpenXmlPackage package = spreadsheetDocument.SaveAs(fileName);

      package.Close();
    }


    public void SetCell<T>(string cellName, T value) {
      Tuple<string, int> cellNameParts = this.GetCellNameParts(cellName);

      this.SetCell<T>(cellNameParts.Item1, cellNameParts.Item2, value);
    }


    public void SetCell<T>(string columnName, int rowIndex, T value) {
      try {
        Worksheet worksheet = GetDefaultWorksheet();

        Cell cell = GetOrCreateCell(worksheet, columnName, rowIndex);

        SetCellValue<T>(cell, value);

      } catch (Exception ex) {
        throw new Exception("I can not set cell value.", ex);
      }
    }


    #endregion Public Methods

    #region Private Methods

    private void AddNewWorksheet(string sheetName) {
      WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

      WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
      worksheetPart.Worksheet = new Worksheet(new SheetData());

      Sheets sheets = new Sheets();

      if (IsWorkbookEmpty(workbookPart.Workbook)) {
        sheets = workbookPart.Workbook.AppendChild<Sheets>(new Sheets());
      } else {
        sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
      }

      Sheet sheet = CreateWorksheet(sheetName);
      sheets.Append(sheet);
    }


    private Sheet CreateWorksheet(string sheetName) {
      WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
      Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
      string relationshipId = workbookPart.GetIdOfPart(spreadsheetDocument.WorkbookPart.WorksheetParts.FirstOrDefault());

      uint sheetId = 1;
      if (sheets.Elements<Sheet>().Count() > 0) {
        sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
      }

      Sheet sheet = new Sheet() {
        Id = relationshipId,
        SheetId = sheetId,
        Name = sheetName
      };

      return sheet;
    }


    private bool ExistRow(Worksheet worksheet, int rowIndex) {
      return this.GetRows(worksheet).Count(r => r.RowIndex == rowIndex) > 0;
    }


    private Tuple<string, int> GetCellNameParts(string cellName) {
      string digits = "123456789";

      char firstDigit = cellName.First(x => digits.Contains(x));

      string columnNamePart = cellName.Substring(0, cellName.IndexOf(firstDigit));

      string rowIndexPart = cellName.Substring(cellName.IndexOf(firstDigit));

      return new Tuple<string, int>(columnNamePart, int.Parse(rowIndexPart));
    }


    private Worksheet defaultWorksheet = null;
    private Worksheet GetDefaultWorksheet() {
      if (defaultWorksheet == null) {
        WorksheetPart worksheetPart = spreadsheetDocument.WorkbookPart.WorksheetParts.FirstOrDefault();

        defaultWorksheet = worksheetPart.Worksheet;
      }

      return defaultWorksheet;
    }


    private Cell GetOrCreateCell(Worksheet worksheet, string columnName, int rowIndex) {
      Row row = GetRow(worksheet, rowIndex);

      string cellName = columnName + rowIndex;

      Cell refCell = row.Elements<Cell>().FirstOrDefault(c => c.CellReference.Value == cellName);

      if (refCell != null) {
        return refCell;
      }

      foreach (Cell cell in row.Elements<Cell>()) {
        if (cell.CellReference.Value.Length == cellName.Length) {
          if (string.Compare(cell.CellReference.Value, cellName, true) > 0) {
            refCell = cell;
            break;
          }
        } // if cell.CellReference
      } // foreach

      Cell newCell = new Cell() {
        CellReference = cellName
      };
      row.InsertBefore(newCell, refCell);

      // worksheet.Save();

      return newCell;
    }


    private IEnumerable<Row> GetRows(Worksheet worksheet) {
      return worksheet.GetFirstChild<SheetData>().Elements<Row>();
    }


    private Row GetRow(Worksheet worksheet, int rowIndex) {
      if (!ExistRow(worksheet, rowIndex)) {
        Row row = new Row() {
          RowIndex = (uint) rowIndex
        };
        SheetData sheetData = worksheet.GetFirstChild<SheetData>();
        sheetData.Append(row);
      }

      return worksheet.GetFirstChild<SheetData>().Elements<Row>().First(r => r.RowIndex == rowIndex);
    }


    private bool IsWorkbookEmpty(Workbook workbook) {
      if (workbook.Sheets == null) {
        return true;
      }
      return false;
    }


    private void SetCellValue<T>(Cell cell, T value) {
      cell.CellValue = new CellValue(System.Convert.ToString(value));

      if (typeof(T) == typeof(int)) {
        cell.DataType = new EnumValue<CellValues>(CellValues.Number);
      }

      if (typeof(T) == typeof(string)) {
        cell.DataType = new EnumValue<CellValues>(CellValues.String);
      }

      if (typeof(T) == typeof(double)) {
        cell.DataType = new EnumValue<CellValues>(CellValues.Number);
      }

      if (typeof(T) == typeof(DateTime)) {
        cell.DataType = new EnumValue<CellValues>(CellValues.Date);
      }

      if (typeof(T) == typeof(bool)) {
        cell.DataType = new EnumValue<CellValues>(CellValues.Boolean);
      }
    }


    private void UpdateDOM() {
      Worksheet worksheet = GetDefaultWorksheet();

      worksheet.Save();
    }


    #endregion Private Methods

    #region IDisposable Support

    private bool disposedValue = false;

    protected virtual void Dispose(bool disposing) {
      if (!disposedValue) {
        if (disposing) {
          // TODO
        }
        this.Close();
        this.spreadsheetDocument.Dispose();

        disposedValue = true;
      }
    }


    public void Dispose() {
      Dispose(true);
    }


    #endregion IDisposable Support

  }  // class OpenXMLSpreadsheet

}  // namespace Empiria.Office.Providers

