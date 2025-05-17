/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                Component : Spreadsheet service provider            *
*  Assembly : Empiria.Office.dll                         Pattern   : Provider                                *
*  Type     : HtmlToPdfConverter                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services to create a PDF file from Html documents.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using iText.Html2pdf;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

namespace Empiria.Office {

  public class PdfConverterOptions {

    public string BaseUri {
      get; set;
    } = string.Empty;


    public bool Landscape {
      get; set;
    }

  }  // class PdfConverterOptions



  /// <summary>Provides services to create a PDF file from Html documents.</summary>
  public class HtmlToPdfConverter {

    #region Public Methods

    public void Convert(string html, string fullPdfPath, PdfConverterOptions options) {
      if (options.Landscape) {
        ConvertLandscape(html, fullPdfPath, options);
        return;
      }

      using (var pdfWriter = new PdfWriter(fullPdfPath)) {

        var properties = new ConverterProperties();

        properties.SetBaseUri(options.BaseUri);

        HtmlConverter.ConvertToPdf(html, pdfWriter, properties);

      }  // pdfWriter

    }

    private void ConvertLandscape(string html, string fullPdfPath, PdfConverterOptions options) {

      using (var pdfWriter = new PdfWriter(fullPdfPath)) {

        using (var pdfDocument = new PdfDocument(pdfWriter)) {

          pdfDocument.SetDefaultPageSize(PageSize.A4.Rotate());

          using (var document = new Document(pdfDocument, PageSize.A4.Rotate())) {

            document.SetMargins(20, 20, 20, 20);

            var properties = new ConverterProperties();

            properties.SetBaseUri(options.BaseUri);

            HtmlConverter.ConvertToPdf(html, pdfDocument, properties);

          }  // document

        }  // pdfDocument

      }  // pdfWriter
    }

    #endregion Public Methods

  }  // class HtmlToPdfConverter

}  // namespace Empiria.Office
