/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Data Objects                       Component : Service Provider                        *
*  Assembly : Empiria.Data.DataObjects.dll               Pattern   : Information Holder                      *
*  Type     : DataItemType                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Power type that defines a data item type.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Ontology;

namespace Empiria.Data.DataObjects {

  /// <summary>Power type that defines a data item type.</summary>
  [Powertype(typeof(DataItem))]
  public class DataItemType : Powertype {

    private DataItemType() {
      // Empiria powertypes always have this constructor.
    }

    static public new DataItemType Parse(int typeId) {
      return ObjectTypeInfo.Parse<DataItemType>(typeId);
    }

    static internal new DataItemType Parse(string typeName) {
      return ObjectTypeInfo.Parse<DataItemType>(typeName);
    }

  }  // class DataItemType

}  // namespace Empiria.Data.DataObjects
