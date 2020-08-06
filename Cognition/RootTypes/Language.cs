/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Cognitive Services                         Component : Service provider                        *
*  Assembly : Empiria.Cognition.ESign.dll                Pattern   : Value Type                              *
*  Type     : Language                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes a text translation language.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Cognition {

  /// <summary>Describes a text translation language.</summary>
  public class Language {

    #region Constructors and parsers

    private Language(string name, string code) {
      this.Name = name;
      this.Code = code;
    }

    static public readonly Language English = new Language("English", "en");

    static public readonly Language Spanish = new Language("Spanish", "sp");


    #endregion Constructors and parsers

    #region Properties

    public string Name {
      get;
    }


    public string Code {
      get;
    }

    #endregion Properties


    #region Methods

    public override bool Equals(object obj) {
      if (obj == null || this.GetType() != obj.GetType()) {
        return false;
      }
      return base.Equals(obj) && (this.Code == ((Language) obj).Code);
    }


    public override int GetHashCode() {
      return this.Code.GetHashCode();
    }


    #endregion Methods

  }  // class Language

}