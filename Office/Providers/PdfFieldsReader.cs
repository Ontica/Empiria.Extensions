/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                Component : PDF service provider                    *
*  Assembly : Empiria.Office.dll                         Pattern   : Provider                                *
*  Type     : PdfFieldsReader                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Reads the fields of a PDF form.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.IO;

using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;

namespace Empiria.Office.Providers {

  /// <summary>Reads the fields of a PDF form.</summary>
  internal class PdfFieldsReader {

    #region Internal Methods

    static internal IEnumerable<PdfFieldDTO> GetFields(string path) {
      Assertion.AssertObject(path, "path");

      using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(path))) {
        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

        if (!FormHasFields(form)) {
          return new List<PdfFieldDTO>();
        }

        return GetFormFields(form);
      }
    }


    static internal IEnumerable<PdfFieldDTO> GetFields(Stream stream) {
      Assertion.AssertObject(stream, "stream");

      using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(stream))) {
        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

        if (!FormHasFields(form)) {
          return new List<PdfFieldDTO>();
        }

        return GetFormFields(form);
      }
    }


    #endregion Public Methods

    #region Private Methods

    static private PdfFieldDTO ConvertToFormFieldDTO(string Key, PdfFormField field) {
      PdfFieldDTO dto = new PdfFieldDTO();

      dto.Key = Key;
      dto.Name = (field.GetFieldName() != null) ? field.GetFieldName().ToString() : "";
      dto.Value = (field.GetValue() != null) ? field.GetValueAsString() : "";
      dto.Type = field.GetType().Name;
      dto.IsMultiline = field.IsMultiline();
      dto.IsReadOnly = field.IsReadOnly();
      dto.IsRequired = field.IsRequired();

      return dto;
    }


    static private bool FormHasFields(PdfAcroForm form) {
      IDictionary<String, PdfFormField> fields = form.GetFormFields();

      return (fields.Count != 0);
    }


    static private List<PdfFieldDTO> GetFormFields(PdfAcroForm form) {
      IDictionary<String, PdfFormField> fields = form.GetFormFields();
      List<PdfFieldDTO> formFieldDTOList = new List<PdfFieldDTO>();

      foreach (var field in fields) {
        if (field.Value == null) {
          throw new Exception("This field is Null: " + field.Key);
        } else {
          PdfFieldDTO fieldConvertedToDTO = ConvertToFormFieldDTO(field.Key, field.Value);
          formFieldDTOList.Add(fieldConvertedToDTO);
        }

      }

      return formFieldDTOList;
    }

    #endregion Private Methods

  }  // class PdfFieldsReader

}  // namespace Empiria.Office.Providers
