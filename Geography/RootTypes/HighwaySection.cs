/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Geographic Data Services            *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : HighwaySection                                 Pattern  : Value object                        *
*  Version   : 6.7                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : String value to describe a highway stretch or section typically comprised between two places, *
*              like  'NY - Philadelphia' or 'Ciudad de México - Cuernavaca'. A HighwaySection could also be  *
*              used to name a highway. Some highways are described only with their destination or end place. *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Linq;

using Empiria.Ontology;

namespace Empiria.Geography {

  /// <summary>String value to describe a highway stretch or section typically comprised between two places,
  /// like  'NY - Philadelphia' or 'Ciudad de México - Cuernavaca'. A HighwaySection could also be used to
  /// name a highway. Some highways are described only with their destination or ending place.</summary>
  public class HighwaySection : ValueObject<string> {

    #region Fields

    private const string thisTypeName = "ValueType.HighwaySection";

    #endregion Fields

    #region Constructors and parsers

    private HighwaySection(bool privateConstructor, string sectionValue) : base(sectionValue) {

    }

    /// <summary>
    /// Used to name highways only with their destination or ending place
    /// (e.g, Road to San Bartolo or Camino a San Juan).
    /// </summary>
    public HighwaySection(string destinationPlace)
                          : base(HighwaySection.BuildName(destinationPlace)) {

    }

    /// <summary>Used both to name highways with their origin and destination places
    /// (like Highway Washington DC - Baltimore) and also to describe highway stretches.
    /// </summary>
    public HighwaySection(string originPlace, string destinationPlace)
                          : base(HighwaySection.BuildName(originPlace, destinationPlace)) {

    }

    static public HighwaySection Parse(string section) {
      Assertion.AssertObject(section, "section");

      return new HighwaySection(true, section);
    }

    static public HighwaySection Empty {
      get {
        var highwaySection = new HighwaySection("No determinado");
        highwaySection.MarkAsEmpty();

        return highwaySection;
      }
    }

    static public ValueTypeInfo ValueTypeInfo {
      get {
        return ValueTypeInfo.Parse(thisTypeName);
      }
    }

    #endregion Constructors and parsers

    #region Private methods

    static private string BuildName(string destinationPlace) {
      Assertion.AssertObject(destinationPlace, "destinationPlace");

      return "a " + EmpiriaString.ToProperNoun(destinationPlace);
    }

    static private string BuildName(string originPlace, string destinationPlace) {
      Assertion.AssertObject(originPlace, "originPlace");
      Assertion.AssertObject(destinationPlace, "destinationPlace");

      return EmpiriaString.ToProperNoun(originPlace) + " - " +
             EmpiriaString.ToProperNoun(destinationPlace);
    }

    #endregion Private methods

  } // class HighwaySection

} // namespace Empiria.Geography
