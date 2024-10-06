/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Information Holder                  *
*  Type     : Feature                                        License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Abstract class that describes a software feature.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Artifacts {

  internal class CosmicSize {

    internal CosmicSize() {
      // Required by Empiria Framework.
    }


    [DataField("EntriesCount")]
    public int Entries {
      get; private set;
    }

    [DataField("ReadsCount")]
    public int Reads {
      get; private set;
    }

    [DataField("WritesCount")]
    public int Writes {
      get; private set;
    }

    [DataField("ExitsCount")]
    public int Exits {
      get; private set;
    }

    public int Total {
      get {
        return Entries + Reads + Writes + Exits;
      }
    }


    internal CosmicSize Sum(CosmicSize size) {
      var sum = new CosmicSize();

      sum.Entries = this.Entries + size.Entries;
      sum.Reads   = this.Reads   + size.Reads;
      sum.Writes  = this.Writes  + size.Writes;
      sum.Exits   = this.Exits   + size.Exits;

      return sum;
    }

  } // class CosmicSize

}  // namespace Empiria.Artifacts
