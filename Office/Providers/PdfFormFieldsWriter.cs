/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration servuices               Component : Service provider                        *
*  Assembly : Empiria.Cognition.dll                      Pattern   : Provider                                *
*  Type     :                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Define data types for Microsoft Azure Cognition Translator Services.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using iText.Forms;
using iText.Kernel.Pdf;


namespace PDFFormProvider {
  internal class PdfFormFieldsWriter {

    static private PdfAcroForm form;
    #region Public Methods

     static public MemoryStream WriteFields(Stream stream, IEnumerable<PdfFormFieldDTO> newFields) {
       var FilledStream = new MemoryStream();

       using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(stream), new PdfWriter(FilledStream))) {
         form = PdfAcroForm.GetAcroForm(pdfDoc, false);

         foreach (var newField in newFields.ToList()) {
           SetFieldValue(newField.Key, "NTVC");
         }
       }

       return FilledStream;
     }


    #endregion Public Methods


    #region Private Methods

    static private void SetFieldValue(string key, string value) {
      if (ExistField(key)) {
        form.GetField(key).SetValue(value);
      } else {
        throw new Exception("The field with key: " + key + " does not exist!!");
      }

    }


    static private bool ExistField(string key) {
      return form.GetFormFields().ContainsKey(key);
    }


    #endregion Private Methods
  }
}
