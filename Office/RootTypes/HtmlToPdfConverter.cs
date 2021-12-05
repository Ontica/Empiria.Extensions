/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                Component : Spreadsheet service provider            *
*  Assembly : Empiria.Office.dll                         Pattern   : Provider                                *
*  Type     : HtmlToPdfConverter                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services to create a PDF file from Html documents.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using iText.Html2pdf;
using iText.Kernel.Pdf;

namespace Empiria.Office {

  public class PdfConverterOptions {

    public string BaseUri {
      get; set;
    } = string.Empty;

  }

  /// <summary>Provides services to create a PDF file from Html documents.</summary>
  public class HtmlToPdfConverter {

    #region Public Methods

    public void Convert(string html, string fullPdfPath, PdfConverterOptions options) {
      using (var pdfWriter = new PdfWriter(fullPdfPath)) {
        var properties = new ConverterProperties();

        properties.SetBaseUri(options.BaseUri);

        HtmlConverter.ConvertToPdf(html, pdfWriter, properties);
      }
    }

    #endregion Public Methods

  }  // class HtmlToPdfConverter

}  // namespace Empiria.Office
