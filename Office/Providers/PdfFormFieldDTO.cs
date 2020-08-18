/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Office                         Component : Service provider                            *
*  Assembly : Empiria.Office.dll                     Pattern   : Data Transfer Objects                       *
*  Type     : PdfFormFieldDTO                        License   : Please read LICENSE.txt file                *
*                                                                                                            *
*  Summary  : Define data types for Pdf Form Field Services.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Office.Providers {
  /// <summary>Define data types for Pdf Form Field Services.</summary>
  internal class PdfFormFieldDTO {
    public string Key {
      get; set;
    }


    public string Name {
      get; set;
    }


    public string Value {
      get; set;
    }


    public string Type {
      get; set;
    }


    public bool IsReadOnly {
      get; set;
    }


    public bool IsMultiline {
      get; set;
    }


    public bool IsRequired {
      get; set;
    }


  }
}
