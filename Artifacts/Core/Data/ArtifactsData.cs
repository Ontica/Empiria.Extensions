/* Empiria OnePoint Artifacts ********************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Data Layer                          *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Data services                       *
*  Type     : ArtifactsData                                  License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Reads and writes artifacts data to a peristent repository.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Data;

namespace Empiria.Artifacts.Data {

  static internal class ArtifactsData {

    static internal List<Artifact> GetArtifacts(SoftwareProduct system) {
      var sql = "SELECT * FROM ARTSystemArtifacts " +
               $"WHERE SystemId = {system.Id} AND Status <> 'X' " +
                "ORDER BY Position";

      var op = DataOperation.Parse(sql);

     return DataReader.GetList<Artifact>(op);
    }

  }  // class ArtifactsData

}  // namespace Empiria.Artifacts.Data
