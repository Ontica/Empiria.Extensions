/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Cognitive Services                         Component : Service provider                        *
*  Assembly : Empiria.Cognition.dll                      Pattern   : Service provider                        *
*  Type     : TranslatorProvider                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides text translation services usign Microsoft Azure Cognition Services.                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Newtonsoft.Json;

namespace Empiria.Cognition.Providers {

  /// <summary>Provides text translation services usign Microsoft Azure Cognition Services.</summary>
  public class TranslatorProvider {

    private string subscriptionKey = string.Empty;
    private const string endpoint = "https://api.cognitive.microsofttranslator.com/";

    public TranslatorProvider(string translationToken) {
      subscriptionKey = translationToken;
    }

    #region Public Methods

    public async Task<string> TranslateToText(string text, string toLanguaje) {
      try {
        string translation = await TranslateToJson(text, toLanguaje);

        TranslatorDataTypes[] deserializedOutput = JsonConvert.DeserializeObject<TranslatorDataTypes[]>(translation);
        TranslatorDataTypes result = deserializedOutput[0];

        return result.Translations[0].Text;
      } catch (Exception ex) {
        throw new Exception("Sorry I can't translate this text: " + text + ". ", ex);
      }
    }

    #endregion

    #region Private Methods

    private async Task<string> TranslateToJson(string text, string toLanguage) {
      string uri = endpoint + "/translate?api-version=3.0&to=" + toLanguage;

      object[] body = new object[] { new { Text = text } };
      var requestBody = JsonConvert.SerializeObject(body);

      using (var client = new HttpClient()) {
        using (var request = new HttpRequestMessage()) {
          request.Method = HttpMethod.Post;
          request.RequestUri = new Uri(uri);
          request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
          request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

          HttpResponseMessage response = await client.SendAsync(request);
          var responseBody = await response.Content.ReadAsStringAsync();
          dynamic result = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(responseBody), Formatting.Indented);

          return result;
        }
      }  // using
    }

    #endregion

  }  // class Translator
}
