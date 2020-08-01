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

namespace Empiria.Cognition {

  /// <summary>Provides text translation services.</summary>
  static public class TextTranslator {

    #region Public services


    static public string Translate(string text, Language translateTo = Language.English) {
      if (String.IsNullOrWhiteSpace(text)) {
        return text;
      }
      if (translateTo != Language.English) {
        new NotImplementedException($"TranslateTo language has not been implemented {translateTo.ToString()}.");
      }

      return TranslateText(text, translateTo);
    }


    #endregion Public services

    #region Private methods

    static private string TranslateText(string text, Language translateTo) {
      throw new NotImplementedException("TranslateText");
    }

    #endregion Private methods

  } // class TextTranslator

}
