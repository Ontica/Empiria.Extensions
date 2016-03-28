/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.WebApi                                   Assembly : Empiria.WebApi.dll                *
*  Type      : WebApiRequest                                    Pattern  : Information Holder                *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains data about a web api request that can be used by audit trails or in the web api      *
*              request-response pipline.                                                                     *
*                                                                                                            *
********************************* Copyright (c) 2014-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web;

using Empiria.Security;

namespace Empiria.WebApi {

  /// <summary>Contains data about a web api request that can be used by audit trails or in the web api
  /// request-response pipline.</summary>
  public class WebApiRequest : IRequest {

    #region Fields

    static private readonly string KeyName = "EMPIRIA_WEB_API_REQUEST_OBJECT";
    static private readonly object _syncRoot = new object();

    #endregion Fields

    #region Constructors and parsers

    private WebApiRequest() {
      this.StartTime = DateTime.Now;
      this.Guid = Guid.NewGuid();
      this.AppliedToId = -1;
    }

    static public WebApiRequest Current {
      get {
        if (!HttpContext.Current.Items.Contains(KeyName)) {
          lock (_syncRoot) {
            if (!HttpContext.Current.Items.Contains(KeyName)) {
              var request = new WebApiRequest();
              HttpContext.Current.Items.Add(KeyName, request);
            }
          }
        }
        return (WebApiRequest) HttpContext.Current.Items[KeyName];
      }
    }

    #endregion Constructors and parsers

    #region Properties

    public Guid Guid {
      get;
      private set;
    }

    public EmpiriaPrincipal Principal {
      get;
      private set;
    }

    public DateTime StartTime {
      get;
      private set;
    }

    public int AppliedToId {
      get;
      private set;
    }

    #endregion Properties

    #region Methods

    internal void SetPrincipal(EmpiriaPrincipal principal) {
      Assertion.AssertObject(principal, "principal");

      this.Principal = principal;
    }

    #endregion Methods

  }  // class WebApiRequest

} // namespace Empiria.WebApi
