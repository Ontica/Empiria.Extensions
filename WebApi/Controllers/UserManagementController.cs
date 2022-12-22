/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Empiria Web Api                              Component : Web Api                               *
*  Assembly : Empiria.WebApi.dll                           Pattern   : Controller                            *
*  Type     : UserManagementController                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods for user management.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Services.UserManagement;

namespace Empiria.WebApi.Controllers {

  /// <summary>Web api methods for user management.</summary>
  public class UserManagementController : WebApiController {

    #region Web Apis


    [HttpGet]
    [Route("v3/security/management/users")]
    public CollectionModel GetUsers([FromUri] string keywords = "") {

      keywords = keywords ?? string.Empty;

      using (var usecases = UserManagementUseCases.UseCaseInteractor()) {
        FixedList<UserDescriptorDto> users = usecases.SearchUsers(keywords);

        return new CollectionModel(base.Request, users);
      }
    }

    #endregion Web Apis

  }  // class UserManagementController

}  // namespace Empiria.WebApi.Controllers
