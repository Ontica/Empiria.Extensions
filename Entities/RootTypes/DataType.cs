/* Empiria Steps *********************************************************************************************
*                                                                                                            *
*  Module   : Steps Design Services                      Component : Domain Types                            *
*  Assembly : Empiria.Steps.Design.dll                   Pattern   : Information Holder                      *
*  Type     : DataType                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes a primitive or compound data type.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Entities {

  public class DataType {


    #region Constructors and parsers

    private DataType(string name, string identifier, string description) {
      this.Name = name;
      this.Identifier = identifier;
      this.Description = description;
      this.IsPrimitive = true;
    }


    public DataType Boolean => new DataType("Boolean", "Boolean",
                                            "Represents a Boolean (true or false value)");

    public DataType Integer => new DataType("Integer", "Integer",
                                            "Represents an integer number.");

    public DataType Decimal => new DataType("Decimal", "Decimal",
                                            "Represents a decimal number.");

    public DataType DateTime => new DataType("DateTime", "DateTime",
                                             "Represents an instance in time expressed as a date and time of the day.");

    public DataType String => new DataType("String", "String",
                                           "Represents text as a Unicode sequence of characters.");


    #endregion Constructors and parsers


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
    } = System.String.Empty;


    public bool IsPrimitive {
      get;
      private set;
    }


    public bool IsCompound {
      get {
        return !this.IsPrimitive;
      }
      set {
        this.IsPrimitive = !this.IsPrimitive;
      }
    }


    #endregion Public properties

  }  // class DataType

}  // namespace Empiria.Entities
