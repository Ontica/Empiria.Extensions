﻿/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiExceptionHandler                           Pattern  : Http Message Handler              *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Message handler that controls the output of all exceptions throwed by the web api.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace Empiria.WebApi {

  /// <summary>Message handler that controls the output of all exceptions throwed by the web api</summary>
  public class WebApiExceptionHandler : ExceptionHandler {

    #region Public methods

    public override void Handle(ExceptionHandlerContext context) {
      ExceptionModel model = new ExceptionModel(context.Request, context.Exception);

      var response = model.CreateResponse();

      //Uncomment these lines to log the exception and the request in an external eventlog.

      //Empiria.Messaging.Publisher.Publish(context.Exception);
      //Empiria.Messaging.Publisher.Publish(context.Request.ToString());

      context.Result = new ResponseMessageResult(response);
    }

    public async override Task HandleAsync(ExceptionHandlerContext context,
                                           CancellationToken cancellationToken) {
      await Task.Factory.StartNew(() => this.Handle(context), cancellationToken);
    }

    public override bool ShouldHandle(ExceptionHandlerContext context) {
      return true;
    }

    #endregion Public methods

  }  // class WebApiExceptionHandler

}  // namespace Empiria.WebApi
