﻿/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : Reporting Services                           Component : Excel Exporters                       *
*  Assembly : FinancialAccounting.Reporting.dll            Pattern   : Information Holder                    *
*  Type     : ExcelFile                                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides edition services for Microsoft Excel files.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;

using Empiria.Storage;

namespace Empiria.Office {

  /// <summary>Provides edition services for Microsoft Excel files.</summary>
  public class ExcelFile {

    #region Fields

    private readonly FileTemplateConfig _templateConfig;

    private Spreadsheet _excel;

    #endregion Fields

    public ExcelFile(FileTemplateConfig templateConfig) {
      Assertion.Require(templateConfig, nameof(templateConfig));

      _templateConfig = templateConfig;
    }

    #region Properties

    public string Url {
      get {
        return $"{FileTemplateConfig.GeneratedFilesBaseUrl}/{FileInfo.Name}";
      }
    }


    public FileInfo FileInfo {
      get; private set;
    }

    #endregion Properties

    #region Methods

    public void Open() {
      _excel = Spreadsheet.Open(_templateConfig.TemplateFullPath);
    }

    public void Close() {
      if (_excel != null) {
        _excel.Close();
      }
    }

    public void Save() {
      if (_excel != null) {
        var path = _templateConfig.NewFilePath();
        _excel.SaveAs(path);
        this.FileInfo = new FileInfo(path);
      }
    }

    public void IndentCell(string cell, int indent) {
      if (_excel != null) {
        _excel.IndentCell(cell, indent);
      }
    }

    public void InsertRow(int startRowIndex) {
      _excel.InsertRow(startRowIndex);
    }

    public void RemoveColumn(string column) {
      _excel.RemoveColumn(column);
    }

    public int SearchColumnValue(string column, string value, int startIndex, int endIndex) {
      for (int i = startIndex; i < endIndex; i++) {
        var currentValue = _excel.ReadCellValue<string>($"{column}{i}");
        if (currentValue == value) {
          return i;
        }
      }
      return 0;
    }

    public void SetCell(string cell, string value) {
      if (value == null) {
        value = string.Empty;
      }

      if (_excel != null) {
        _excel.SetCell(cell, value);
      }
    }

    public void SetCell(string cell, decimal? value) {
      if (!value.HasValue) {
        return;
      }
      if (_excel != null) {
        _excel.SetCell(cell, value.Value);
      }
    }

    public void SetCell(string cell, decimal value) {
      if (_excel != null) {
        _excel.SetCell(cell, value);
      }
    }

    public void SetCell(string cell, int value) {
      if (_excel != null) {
        _excel.SetCell(cell, value);
      }
    }

    public void SetCell(string cell, DateTime value) {
      if (_excel != null) {
        _excel.SetCell(cell, value);
      }
    }

    public void SetCellStyleLineThrough(string cell) {
      if (_excel != null) {
        _excel.SetCellStyle(Style.LineThrough, cell);
      }
    }

    public void SetRowStyleBold(int rowIndex) {
      if (_excel != null) {
        _excel.SetRowStyle(Style.Bold, rowIndex);
      }
    }

    public void SetCellFontColorStyle(string cell, System.Drawing.Color color) {
      if (_excel != null) {
        _excel.SetFontColor(Style.FontColor, cell, color);
      }
    }

    public void SetRowFontColorStyle(int rowIndex, System.Drawing.Color color) {
      if (_excel != null) {
        _excel.SetRowFontColor(Style.FontColor, rowIndex, color);
      }
    }

    public void SetCellBackgroundStyle(string cell, System.Drawing.Color color) {
      if (_excel != null) {
        _excel.SetCellBackgroundStyle(Style.BackgroundColor, cell, color);
      }
    }

    public void SetRowBackgroundStyle(int rowIndex, System.Drawing.Color color) {
      if (_excel != null) {
        _excel.SetRowBackgroundStyle(Style.BackgroundColor, rowIndex, color);
      }
    }

    public void SetCellWrapText(string cell) {
      if (_excel != null) {
        _excel.SetCellWrapText(cell);
      }
    }

    public void SetTextAlignment(string cell, DocumentFormat.OpenXml.Spreadsheet.VerticalAlignmentValues alignment) {
      if (_excel != null) {
        _excel.SetTextAlignment(cell, alignment);
      }
    }

    public void SetCellFontFamily(string cell, string fontFamily) {
      if (_excel != null) {
        _excel.SetCellFontFamily(cell, fontFamily);
      }
    }

    public void SetRowFontFamily(int row, string fontFamily) {
      if (_excel != null) {
        _excel.SetRowFontFamily(row, fontFamily);
      }
    }

    public void SetRowFontFamily(int row, string fontFamily, int fontSize) {
      if (_excel != null) {
        _excel.SetRowFontFamily(row, fontFamily, fontSize);
      }
    }


    public FileReportDto ToFileReportDto() {
      return new FileReportDto(FileType.Excel, this.Url);
    }



    #endregion Methods

  }  // class ExcelFile

}  // namespace Empiria.FinancialAccounting.Reporting.Exporters.Excel
