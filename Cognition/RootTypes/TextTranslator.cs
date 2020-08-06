/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Cognitive Services                         Component : Service provider                        *
*  Assembly : Empiria.Cognition.dll                      Pattern   : Service provider                        *
*  Type     : TextTranslator                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides text translation services.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Empiria.Cognition.Providers;

namespace Empiria.Cognition {

  /// <summary>Provides text translation services.</summary>
  static public class TextTranslator {

    #region Public services

    static public Task<string> Translate(string text, Language translateTo) {
      Assertion.AssertObject(translateTo, "translateTo");

      if (String.IsNullOrWhiteSpace(text)) {
        return Task.FromResult(text);
      }

      string subscriptionKey = ConfigurationData.GetString("TranslatorProvider.SubcriptionKey");

      var translator = new TranslatorProvider(subscriptionKey);

      return translator.TranslateToText(text, translateTo.Code);
    }

    #endregion Public services

  } // class TextTranslator

}

