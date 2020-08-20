/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                Component : PDF service provider                    *
*  Assembly : Empiria.Office.dll                         Pattern   : Data Transfer Object                    *
*  Type     : PdfFieldDTO                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Properties of a PDF form field.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Office.Providers {

  /// <summary>Properties of a PDF form field.</summary>
  internal class PdfFieldDTO {

    public string Key {
      get; set;
    } = String.Empty;


    public string Name {
      get; set;
    } = String.Empty;


    public string Value {
      get; set;
    } = String.Empty;


    public string Type {
      get; set;
    } = String.Empty;


    public bool IsReadOnly {
      get; set;
    }


    public bool IsMultiline {
      get; set;
    }


    public bool IsRequired {
      get; set;
    }

  }  // class PdfFieldDTO

} // namespace Empiria.Office.Providers
