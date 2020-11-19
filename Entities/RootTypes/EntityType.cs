/* Empiria Steps *********************************************************************************************
*                                                                                                            *
*  Module   : Steps Design Services                      Component : Domain Types                            *
*  Assembly : Empiria.Steps.Design.dll                   Pattern   : Information Holder                      *
*  Type     : EntityType                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes a data entity with its properties.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Entities {

  /// <summary>Describes a data entity with its properties.</summary>
  public class EntityType {

    #region Public properties

    public string Name {
      get;
      private set;
    }


    public string Identifier {
      get;
      private set;
    }


    public string Description {
      get;
      private set;
    }


    public EntityType BaseEntityType {
      get;
      private set;
    }


    public FixedList<PropertyType> Properties {
      get;
      private set;
    }


    public FixedList<PropertyType> Key {
      get;
      private set;
    }


    #endregion Public properties

  }  // class EntityType

}  // namespace Empiria.Entities
