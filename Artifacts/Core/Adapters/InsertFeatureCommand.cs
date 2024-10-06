/* Empiria OnePoint Artifacts ********************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management          Component : Adpters Layer                           *
*  Assembly : Empiria.Artifacts.dll                      Pattern   : Command DTO                             *
*  Type     : InsertFeatureCommand                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for read and write software features.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Artifacts.Adapters {

  public class InsertFeatureCommand {

    public string SoftwareProductUID {
      get; set;
    }

    public bool IsGroupingFeature {
      get; set;
    } = false;

    public string Name {
      get; set;
    }

    public int Index {
      get; set;
    } = -1;

  }  // class InsertFeatureCommand

}  // Empiria.Artifacts.Adapters
