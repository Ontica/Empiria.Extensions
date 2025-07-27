/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Web Api Client                             Component : Services Layer                          *
*  Assembly : Empiria.WebApi.Client.dll                  Pattern   : Separated Interface                     *
*  Type     : IWebApiClient                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface used to call Empiria WebApiClient services from a separated component.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Threading.Tasks;

namespace Empiria.WebApi {

  /// <summary>Interface used to call Empiria WebApiClient services from a separated component.</summary>
  public interface IWebApiClient {

    Task DeleteAsync(string path, params object[] pars);


    Task<T> DeleteAsync<T>(string path, params object[] pars);


    Task<T> GetAsync<T>(string path, params object[] pars);


    Task<T> PostAsync<T>(string path, params object[] pars);


    Task<T> PostAsync<T>(object body, string path, params object[] pars);


    Task<T> PutAsync<T>(object body, string path, params object[] pars);


  }  // interface IWebApiClient

}  // namespace Empiria.WebApi
