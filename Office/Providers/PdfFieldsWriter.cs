/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                Component : PDF service provider                    *
*  Assembly : Empiria.Office.dll                         Pattern   : Provider                                *
*  Type     : PdfFieldsWriter                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Writes fields values inside of a PDF form.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using iText.Forms;
using iText.Kernel.Pdf;

namespace Empiria.Office.Providers {

  /// <summary>Writes fields values inside of a PDF form.</summary>
  public class PdfFieldsWriter {

    private PdfAcroForm form;


    private PdfFieldsWriter(PdfDocument document) {
      this.form = PdfAcroForm.GetAcroForm(document, false);
    }


    static public MemoryStream WriteFields(Stream stream, IEnumerable<PdfFieldDTO> newFields) {
      Assertion.AssertObject(stream, "stream");
      Assertion.AssertObject(newFields, "newFields");

      var outputStream = new MemoryStream();

      using (PdfDocument document = new PdfDocument(new PdfReader(stream), new PdfWriter(outputStream))) {
        var writer = new PdfFieldsWriter(document);

        foreach (var newField in newFields.ToList()) {
          writer.SetFieldValue(newField.Key, newField.Value);
        }
      }

      return outputStream;
    }


    #region Private Methods

    private bool ExistsField(string key) {
      return form.GetFormFields().ContainsKey(key);
    }


    private void SetFieldValue(string key, string value) {
      if (this.ExistsField(key)) {
        this.form.GetField(key).SetValue(value);
      } else {
        throw new Exception("The field with key: " + key + " does not exist.");
      }
    }

    #endregion Private Methods

  }  // class PdfFieldsWriter

}  // namespace Empiria.Office.Providers
