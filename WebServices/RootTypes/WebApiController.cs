/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Service-Oriented Framework              System   : Empiria Web Services              *
*  Namespace : Empiria.WebServices                              Assembly : Empiria.WebServices.dll           *
*  Type      : WebApiGlobal                                     Pattern  : Global ASP .NET Class             *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Defines the methods, properties, and events common to all application objects used by         *
*              Empiria® ASP.NET Web Services platform.                                                       *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1994-2013. **/
using System;
using System.Linq;
using System.Web.Http;

namespace Empiria.WebServices {

  /// <summary>Defines the methods, properties, and events common to all application objects used by
  /// Empiria® ASP.NET Web Api Services platform.</summary>
  public class WebApiController : ApiController {

    public WebApiController() {

    }

    protected object ToJson<T>(T instance, Func<T, object> serializer) where T : IStorable {
      return serializer.Invoke(instance);
    }

    protected IQueryable ToJson<T>(ObjectList<T> list, Func<T, object> serializer) where T : IStorable {
      object[] array = new object[list.Count];

      for (int i = 0; i < list.Count; i++) {
        array[i] = serializer.Invoke(list[i]);
      }
      return array.AsQueryable();
    }

  } // class WebApiController

} // namespace Empiria.WebServices