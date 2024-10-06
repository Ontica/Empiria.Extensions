/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Web Api                                 *
*  Assembly : Empiria.Artifacts.WebApi.dll               Pattern   : Web Api Controller                      *
*  Type     : SoftwareProductController                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api with search and edition services for software products.                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Artifacts.UseCases;

namespace Empiria.Artifacts.WebApi {

  /// <summary>Web api with search and edition services for software products.</summary>
  public class SoftwareProductController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v1/artifacts/software-products")]
    public CollectionModel GetSoftwareProducts() {

      using (var usecases = SoftwareProductUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> list = usecases.GetSoftwareProducts();

        return new CollectionModel(this.Request, list);
      }
    }

    #endregion Web Apis

  }  // class SoftwareProductController

}  // namespace Empiria.Artifacts.WebApi
