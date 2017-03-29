using System;
using System.ComponentModel;

namespace Empiria.Services {

  public class EmpiriaWebService : System.Web.Services.WebService {

    private IContainer components = null;

    public EmpiriaWebService() {

    }

    protected override void Dispose(bool disposing) {
      if (disposing && components != null) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

  } // class EmpiriaWebService

} //namespace Empiria.Services
