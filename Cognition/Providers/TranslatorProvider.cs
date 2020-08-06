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
  internal class TranslatorProvider {

    private string subscriptionKey = string.Empty;

    private const string endpoint = "https://api.cognitive.microsofttranslator.com/";


    public TranslatorProvider(string subscriptionKey) {
      this.subscriptionKey = subscriptionKey;
    }


    #region Public Methods

    public async Task<string> TranslateToText(string text, string toLanguaje) {
      try {
        TranslatorResult result = await TranslateToJson(text, toLanguaje);

        return result.Translations[0].Text;

      } catch (Exception ex) {
        throw new Exception("I can not translate the text: " + text + ". ", ex);
      }
    }


    #endregion

    #region Private Methods

    private async Task<TranslatorResult> TranslateToJson(string text, string toLanguage) {
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

          string responseBody = await response.Content.ReadAsStringAsync();

          return JsonConvert.DeserializeObject<TranslatorResult[]>(responseBody)[0];
        }

      }  // using

    }

    #endregion

  }  // class Translator

}