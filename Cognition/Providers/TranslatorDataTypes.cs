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

namespace Empiria.Cognition.Providers {
  /// <summary>Summary  : Define data types for Microsoft Azure Cognition Translator Services.</summary>

  internal class Alignment {
    internal string Proj {
      get; set;
    }

  }


  internal class SentenceLength {
    internal int[] SrcSentLen {
      get; set;
    }

    internal int[] TransSentLen {
      get; set;
    }

  }


  internal class Translation {
    internal string Text {
      get; set;
    }

    internal TextResult Transliteration {
      get; set;
    }

    internal string To {
      get; set;
    }

    internal Alignment Alignment {
      get; set;
    }
    internal SentenceLength SentLen {
      get; set;
    }

  }


  internal class DetectedLanguage {
    internal string Language {
      get; set;
    }

    internal float Score {
      get; set;
    }

  }


  internal class TextResult {
    internal string Text {
      get; set;
    }

    internal string Script {
      get; set;
    }

  }


  internal class TranslatorDataTypes {
    internal DetectedLanguage DetectedLanguage {
      get; set;
    }

    internal TextResult SourceText {
      get; set;
    }

    internal Translation[] Translations {
      get; set;
    }

  }

}
