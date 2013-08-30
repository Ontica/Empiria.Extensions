using System.Data;
using System.Web.Services;

using Empiria.Data;
using Empiria.Data.Integration;
using Empiria.Security;
using Empiria.Services;

namespace Empiria.WebServices {

  [WebService(Namespace = "http://empiria.ontica.org/web.services/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class DataIntegrationServices : EmpiriaWebService {

    public DataIntegrationServices() {
      //Uncomment the following line if using designed components 
      //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public int CountData(string dataOperationMessage) {
      DataOperation dataOperation = DataOperation.ParseFromMessage(dataOperationMessage);

      return DataReader.Count(dataOperation);
    }

    [WebMethod(EnableSession = true)]
    public int CreateObjectId(string sourceName) {
      return DataWriter.CreateId(sourceName);
    }

    [WebMethod(EnableSession = true)]
    public int Execute(string singleSignOnTokenMessage, string dataOperationMessage) {
      SingleSignOnToken ssoToken = SingleSignOnToken.ParseFromMessage(singleSignOnTokenMessage);
      DataOperation dataOperation = DataOperation.ParseFromMessage(dataOperationMessage);

      return DataWriter.Execute(ssoToken, dataOperation);
    }

    [WebMethod(EnableSession = true)]
    public int ExecuteList(string singleSignOnTokenMessage, string[] dataOperationMessages) {
      SingleSignOnToken ssoToken = SingleSignOnToken.ParseFromMessage(singleSignOnTokenMessage);
      DataOperationList dataOperationList = DataOperationList.Parse(dataOperationMessages);

      return DataWriter.Execute(ssoToken, dataOperationList);
    }

    [WebMethod(EnableSession = true)]
    public object GetFieldValue(string dataOperationMessage, string fieldName) {
      DataOperation dataOperation = DataOperation.ParseFromMessage(dataOperationMessage);

      return DataReader.GetFieldValue(dataOperation, fieldName);
    }

    [WebMethod(EnableSession = true)]
    public DataTable GetDataTable(string dataOperationMessage, string filter, string sort) {
      DataOperation dataOperation = DataOperation.ParseFromMessage(dataOperationMessage);

      return DataReader.GetDataView(dataOperation, filter, sort).ToTable();
    }


    [WebMethod(EnableSession = true)]
    public object GetScalar(string dataOperationMessage) {
      DataOperation dataOperation = DataOperation.ParseFromMessage(dataOperationMessage);

      return DataReader.GetScalar<object>(dataOperation);
    }

    [WebMethod(EnableSession = true)]
    public bool StartIntegrator() {
      try {
        DataIntegrationEngine.Instance.Start();
        return true;
      } catch {
        return false;
      }
    }

    [WebMethod(EnableSession = true)]
    public bool StopIntegrator() {
      try {
        DataIntegrationEngine.Instance.Stop();
        return true;
      } catch {
        return false;
      }
    }

    [WebMethod(EnableSession = true)]
    public bool IsIntegratorExecuting() {
      try {
        return DataIntegrationEngine.Instance.IsExecuting;
      } catch {
        return false;
      }
    }

    [WebMethod(EnableSession = true)]
    public bool IsIntegratorStopped() {
      try {
        return DataIntegrationEngine.Instance.IsStopped;
      } catch {
        return false;
      }
    }

    [WebMethod(EnableSession = true)]
    public int IntegratorCurrentTasks() {
      try {
        return DataIntegrationEngine.Instance.CurrentTasks;
      } catch {
        return -1;
      }
    }

  } //class DataIntegrationServices

} // namespace Empiria.WebServices
