using System.Web.Services;

using Empiria.Data;
using Empiria.Services;

namespace Empiria.WebServices {

  [WebService(Namespace = "http://empiria.ontica.org/web.services/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class WebApplicationServices : EmpiriaWebService {

    public WebApplicationServices() {
      //Uncomment the following line if using designed components 
      //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public bool UpdateCache(string cachedObjectName) {
      return (DataCache.Remove(cachedObjectName) != null);
    }

  } //class WebApplicationServices

} // namespace Empiria.WebServices
