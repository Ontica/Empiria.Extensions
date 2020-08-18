/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration serv ices               Component : Service provider                        *
*  Assembly : Empiria.Office   .dll                      Pattern   : Provider                                *
*  Type     : PdfFormFieldsReader                       License   : Please read LICENSE.txt file             *
*                                                                                                            *
*  Summary  : Get Pdf Form Fields using iText.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using iText.Forms;
using iText.Forms.Fields;
using iText.Kernel.Pdf;

namespace Empiria.Office.Providers {
  /// <summary>Get Pdf Form fields using iText.</summary>
  internal class PdfFormFieldsReader {


    #region Internal Methods

    static public IEnumerable<PdfFormFieldDTO> GetFields(string path) {
      using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(path))) {
        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

        if (!IsFormHasFields(form)) {
          return new List<PdfFormFieldDTO>();
        }

        return GetFormFields(form);
      }

    }


    static public IEnumerable<PdfFormFieldDTO> GetFields(System.IO.Stream stream) {
      using (PdfDocument pdfDoc = new PdfDocument(new PdfReader(stream))) {
        PdfAcroForm form = PdfAcroForm.GetAcroForm(pdfDoc, true);

        if (!IsFormHasFields(form)) {
          return new List<PdfFormFieldDTO>();
        }

        return GetFormFields(form);
      }

    }


    #endregion Public Methods

    #region Private Methods

    static private bool IsFormHasFields(PdfAcroForm form) {
      IDictionary<String, PdfFormField> fields = form.GetFormFields();

      if (fields.Count == 0) {
        return false;
      }

      return true;
    }


    static private List<PdfFormFieldDTO> GetFormFields(PdfAcroForm form) {
      IDictionary<String, PdfFormField> fields = form.GetFormFields();
      List<PdfFormFieldDTO> formFieldDTOList = new List<PdfFormFieldDTO>();

      foreach (var field in fields) {
        if (field.Value == null) {
          throw new Exception("This field is Null: " + field.Key);
        } else {
          PdfFormFieldDTO fieldConvertedToDTO = ConvertToFormFieldDTO(field.Key, field.Value);
          formFieldDTOList.Add(fieldConvertedToDTO);
        }

      }

      return formFieldDTOList;
    }


    static private PdfFormFieldDTO ConvertToFormFieldDTO(string Key, PdfFormField field) {
      PdfFormFieldDTO dto = new PdfFormFieldDTO();

      dto.Key = Key;
      dto.Name = (field.GetFieldName() != null) ? field.GetFieldName().ToString() : "";
      dto.Value = (field.GetValue() != null) ? field.GetValueAsString() : "";
      dto.Type = field.GetType().Name;
      dto.IsMultiline = field.IsMultiline();
      dto.IsReadOnly = field.IsReadOnly();
      dto.IsRequired = field.IsRequired();

      return dto;
    }


    #endregion Private Methods

  }
}
