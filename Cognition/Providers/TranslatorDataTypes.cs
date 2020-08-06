/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Cognitive Services                         Component : Service provider                        *
*  Assembly : Empiria.Cognition.dll                      Pattern   : Data Transfer Objects                   *
*  Type     : TranslatorDataTypes                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Define data types for Microsoft Azure Cognition Translator Services.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Newtonsoft.Json;

namespace Empiria.Cognition.Providers {

  internal class Translation {

    [JsonProperty]
    internal string Text {
      get; set;
    }

    [JsonProperty]
    internal string To {
      get; set;
    }

  }


  internal class TranslatorResult {

    [JsonProperty]
    internal Translation[] Translations {
      get; set;
    }

  }


}
