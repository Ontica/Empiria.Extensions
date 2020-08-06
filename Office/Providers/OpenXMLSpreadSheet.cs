/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : OpenXML Services                           Component : Service provider                        *
*  Assembly : Empiria.Cognition.dll                      Pattern   : SpreadSheet                             *
*  Type     : OpenXMLSpreadSheet                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Define data types for Microsoft Azure Cognition Translator Services.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace OpenXMLProvider {
  /// <summary>Provides basic SpreadSheet services usign OpenXML</summary>
  internal class SpreadSheet {

    #region Constructors and parsers
        
    public SpreadSheet(string workBookFilePath) {
      fileName = workBookFilePath;
    }

    private string fileName = "";


    #endregion Constructors and parsers

    #region Public Methods

    internal void CreateSpreadsheetWorkbook(string sheetName) {
      try {
        using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook)) {
          WorkbookPart workbookpart = spreadsheetDocument.AddWorkbookPart();
          workbookpart.Workbook = new Workbook();

          WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
          worksheetPart.Worksheet = new Worksheet(new SheetData());

          Sheets sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());

          Sheet sheet = new Sheet() {
            Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
            SheetId = 1,
            Name = sheetName
          };
          sheets.Append(sheet);
          workbookpart.Workbook.Save();
        } //using

      } catch (Exception ex) {
        throw new Exception("I can´t create this file: " + fileName + " ", ex);
      }
    }


    internal void InsertWorksheet(string sheetName) {
      try {
        using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Open(fileName, true)) {
          WorksheetPart newWorksheetPart = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
          newWorksheetPart.Worksheet = new Worksheet(new SheetData());

          Sheets sheets = spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
          string relationshipId = spreadSheet.WorkbookPart.GetIdOfPart(newWorksheetPart);

          uint sheetId = 1;
          if (sheets.Elements<Sheet>().Count() > 0) {
            sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
          }

          Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
          sheets.Append(sheet);
        } // using

      } catch (Exception ex) {
        throw new Exception("I can´t open this file: " + fileName + " ", ex);
      }
    }


    internal void InsertCellValue(string sheetName, string columnName, int rowIndex, string cellValue) {
      AddCellValue(sheetName, columnName, rowIndex, cellValue, new EnumValue<CellValues>(CellValues.String));
    }

    internal void InsertCellNumericValue(string sheetName, string columnName, int rowIndex, double cellValue) {
      AddCellValue(sheetName, columnName, rowIndex, cellValue.ToString(), new EnumValue<CellValues>(CellValues.Number));
    }

    internal void InsertCellBooleanValue(string sheetName, string columnName, int rowIndex, bool cellValue) {
      AddCellValue(sheetName, columnName, rowIndex, cellValue.ToString(), new EnumValue<CellValues>(CellValues.Boolean));
    }

    internal void InsertCellDateValue(string sheetName, string columnName, int rowIndex, DateTime cellValue) {
      AddCellValue(sheetName, columnName, rowIndex, cellValue.ToString(), new EnumValue<CellValues>(CellValues.Date));
    }

    internal void ReplaceCellValue(string sheetName, string valueToSearch, string newValue) {
      try {
        using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, true)) {
          WorkbookPart workbookPart = document.WorkbookPart;
          WorksheetPart worksheetPart = GetSheet(workbookPart, sheetName);

          if (worksheetPart == null) {
            throw new Exception("I cant find sheet: " + sheetName);
          }

          ReplaceCellValue(workbookPart, worksheetPart, valueToSearch, newValue);
        } //using

      } catch (Exception ex) {
        throw new Exception("I can´t open this file: " + fileName + " ", ex);
      }
    }

    #endregion Public Methods

    #region Private Methods
    private void AddCellValue(string sheetName, string columnName, int rowIndex, string cellValue, EnumValue<CellValues> cellDataType) {
      try {
        using (SpreadsheetDocument document = SpreadsheetDocument.Open(fileName, true)) {
          WorkbookPart workbookPart = document.WorkbookPart;
          WorksheetPart worksheetPart = GetSheet(workbookPart, sheetName);

          if (worksheetPart == null) {
            throw new Exception("I cant find sheet: " + sheetName);
          }

          Row row = GetRow(worksheetPart.Worksheet, rowIndex);
          Cell cell = InsertCell(worksheetPart.Worksheet, row, columnName, rowIndex);

          cell.CellValue = new CellValue(cellValue);
          cell.DataType = cellDataType;

          worksheetPart.Worksheet.Save();
        } //usign

      } catch (Exception ex) {
        throw new Exception("I can´t open this file: " + fileName + " ", ex);
      }
    }
        

    private void ReplaceCellValue(WorkbookPart workbookPart, WorksheetPart worksheetPart, string valueToSearch, string newValue) {
      SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

      foreach (Row row in sheetData.Elements<Row>()) {
        foreach (Cell cell in row.Elements<Cell>()) {
          if (GetCellValue(workbookPart, cell) == valueToSearch) {
            cell.CellValue = new CellValue(newValue);
            cell.DataType = new EnumValue<CellValues>(CellValues.String);

            worksheetPart.Worksheet.Save();
          } // if
        } //foreach Cell

      }//foreach Cell Row
    }


    private string GetCellValue(WorkbookPart workbookPart, Cell cell) {
      string value = "";
      if (cell.InnerText.Length > 0) {
        value = cell.InnerText;
        if (cell.DataType != null) {

          switch (cell.DataType.Value) {
            case CellValues.SharedString:
              var stringTable = workbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
              if (stringTable != null) {
                value = stringTable.SharedStringTable.ElementAt(int.Parse(value)).InnerText;
              }
              break;

            case CellValues.Boolean:
              switch (value) {
                case "0":
                  value = "FALSE";
                  break;
                default:
                  value = "TRUE";
                  break;
              }
              break;
          } // switch
        } //if cell.DataType != null
      }

      return value;
    }


    private WorksheetPart GetSheet(WorkbookPart workbookPart, string sheetName) {
      Sheet sheet = workbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == sheetName).FirstOrDefault();

      if (sheet == null) {
        throw new ArgumentException("I cant find sheet: " + sheetName);
      }

      return (WorksheetPart) (workbookPart.GetPartById(sheet.Id));
    }


    private bool ExistRow(Worksheet worksheet, int rowIndex) {
      int count = worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).Count();

      if (count == 0) {
        return false;
      }

      return true;
    }


    private Row GetRow(Worksheet worksheet, int rowIndex) {
      if (!ExistRow(worksheet, rowIndex)) {
        Row row = new Row() {
          RowIndex = (uint) rowIndex
        };
        SheetData sheetData = worksheet.GetFirstChild<SheetData>();
        sheetData.Append(row);
      }

      return worksheet.GetFirstChild<SheetData>().Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
    }


    private Cell InsertCell(Worksheet worksheet, Row row, string columnName, int rowIndex) {
      string cellReference = columnName + rowIndex;

      if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0) {
        return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
      } else {
        Cell refCell = null;

      foreach (Cell cell in row.Elements<Cell>()) {
        if (string.Compare(cell.CellReference.Value, cellReference, true) > 0) {
          refCell = cell;
          break;
        }
       }

        Cell newCell = new Cell() { CellReference = cellReference };
        row.InsertBefore(newCell, refCell);

        worksheet.Save();
        return newCell;
      } // else
    }


    #endregion Private Methods

  }
}
