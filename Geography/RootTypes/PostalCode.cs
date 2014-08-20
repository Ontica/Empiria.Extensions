/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                     System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : Settlement                                     Pattern  : Empiria Object Type                 *
*  Version   : 6.0        Date: 23/Oct/2014                   License  : GNU AGPLv3  (See license.txt)       *
*                                                                                                            *
*  Summary   : Represents a postal code.                                                                     *
*                                                                                                            *
********************************** Copyright (c) 2009-2014 La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Data;

namespace Empiria.Geography {

  /// <summary>Represents a postal code.</summary>
  public class PostalCode : GeographicRegionItem {

    #region Fields

    private const string thisTypeName = "ObjectType.GeographicItem.GeographicRegionItem.PostalCode";

    #endregion Fields

    #region Constructors and parsers

    protected PostalCode() : base(thisTypeName) {
      // For create instances use GeographicItemType.CreateInstance method instead
    }

    protected PostalCode(string typeName) : base(typeName) {
      // Required by Empiria Framework. Do not delete. Protected in not sealed classes, private otherwise
    }

    static public new PostalCode Parse(int id) {
      return BaseObject.Parse<PostalCode>(thisTypeName, id);
    }

    static internal new PostalCode Parse(DataRow row) {
      return BaseObject.Parse<PostalCode>(thisTypeName, row);
    }

    static public new PostalCode Empty {
      get {
        return BaseObject.ParseEmpty<PostalCode>(thisTypeName);
      }
    }

    static public new PostalCode Unknown {
      get {
        return BaseObject.ParseUnknown<PostalCode>(thisTypeName);
      }
    }

    static public new FixedList<PostalCode> GetList(string filter) {
      return GeographicData.GetRegions<PostalCode>(filter);
    }

    #endregion Constructors and parsers

    #region Public methods

    public void AddSettlement(Settlement settlement) {
      var role = base.ObjectTypeInfo.Associations["PostalCode_Settlements"];
      base.Link(role, settlement);
    }

    public FixedList<Settlement> GetSettlements() {
      return base.GetLinks<Settlement>("PostalCode_Settlements");
    }

    #endregion Public methods

  } // class PostalCode

} // namespace Empiria.Geography
