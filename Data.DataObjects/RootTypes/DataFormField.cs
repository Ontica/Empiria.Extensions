/* Empiria Steps *********************************************************************************************
*                                                                                                            *
*  Module   : Steps Design Services                      Component : Data Objects                            *
*  Assembly : Empiria.Steps.Design.dll                   Pattern   : Information Holder                      *
*  Type     : DataFormField                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds information about a data form field used for dynamic form generation.                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.DataTypes;


namespace Empiria.Data.DataObjects {

  /// <summary>Holds information about a data form field used for dynamic form generation.</summary>
  public class DataFormField {

    public string Value {
      get;
      set;
    } = String.Empty;


    public string Key {
      get;
      set;
    } = String.Empty;


    public string Label {
      get;
      set;
    } = String.Empty;


    public bool Required {
      get;
      set;
    }


    public int Order {
      get;
      set;
    }


    public string ControlType {
      get;
      set;
    } = String.Empty;


    public string Type {
      get;
      set;
    } = String.Empty;


    public FixedList<KeyValue> Values {
      get;
      set;
    } = new FixedList<KeyValue>();


    public string Width {
      get;
      set;
    } = String.Empty;


    public string Unit {
      get;
      set;
    } = String.Empty;


  }  // class DataField

}  // namespace Empiria.Data.DataObjects
