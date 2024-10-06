/* Empiria OnePoint Artifacts ********************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : FeatureList                                    License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : A list of software features                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using Empiria.Data;

namespace Empiria.Artifacts.Data {

  static internal class FeaturesData {

    internal static FixedList<BaseFeature> GetChildren(GroupingFeature group) {
      var sql = "SELECT * FROM ARTSoftwareFeatures " +
                $"WHERE ProductId = {group.Product.Id} AND " +
                $"ParentFeatureId = {group.Id} AND " +
                $"Status <> 'X' " +
                "ORDER BY Position";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<BaseFeature>(op);
    }


    internal static List<BaseFeature> GetFeatures(SoftwareProduct product) {
      var sql = "SELECT * FROM ARTSoftwareFeatures " +
               $"WHERE ProductId = {product.Id} AND Status <> 'X' " +
                "ORDER BY Position";

      var op = DataOperation.Parse(sql);

      return DataReader.GetList<BaseFeature>(op);
    }


    internal static void WriteFeature(Feature o) {
      //var op = DataOperation.Parse("writeFeature", o.Id, o.UID, o.Product.Id, o.Position);

      //return DataReader.GetList<BaseFeature>(op);
    }

  }  // class FeatruresData

}  // namespace Empiria.Artifacts.Data
