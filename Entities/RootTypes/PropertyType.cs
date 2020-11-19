/* Empiria Steps *********************************************************************************************
*                                                                                                            *
*  Module   : Steps Design Services                      Component : Domain Types                            *
*  Assembly : Empiria.Steps.Design.dll                   Pattern   : Information Holder                      *
*  Type     : PropertyType                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes a data entity with its attributes.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Entities {

  /// <summary>Describes a data entity with its attributes.</summary>
  public class PropertyType {

    #region Public properties


    public string Name {
      get;
      private set;
    }


    public string Identifier {
      get;
      private set;
    }


    public DataType Type {
      get;
      private set;
    }


    public string Description {
      get;
      private set;
    }


    public bool Nullable {
      get;
      private set;
    }


    public object DefaultValue {
      get;
      private set;
    }


    public string DefaultFormat {
      get;
      private set;
    }


    #endregion Public properties

  }  // class PropertyType

}  // namespace Empiria.Entities
