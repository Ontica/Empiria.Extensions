/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Entities                           Component : Domain and Use Cases Layer              *
*  Assembly : Empiria.Entities.dll                       Pattern   : Information Holder                      *
*  Type     : IPODataModel                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes an input-process-output entity.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Entities {

  public class IPODataModel {


    #region Constructors and parsers

    public IPODataModel(IIdentifiable modelFor) {
      Assertion.AssertObject(modelFor, "modelFor");

      this.ModelFor = modelFor;
    }


    static public IPODataModel GetFor(IIdentifiable modelFor) {
      throw new NotImplementedException();
      //IPODataModel model = new IPODataModel(modelFor);

      //IPODataModelRepo repo = IPODataModelRepo.Get();

      //var data = repo.GetIPODataModel(modelFor);

      //model.Load(data);

      //return model;
    }


    #endregion Constructors and parsers


    #region Properties

    public IIdentifiable ModelFor {
      get;
    }


    public FixedList<DataObject> Inputs {
      get;
      private set;
    }


    public DataObject Output {
      get;
      private set;
    }


    //public IPOProcessor Processor {
    //  get;
    //  private set;
    //}


    #endregion Properties;


    #region Methods


    private void LoadData() {

    }


    #endregion Methods

  }  // class IPODataModel

}  // namespace Empiria.Entities
