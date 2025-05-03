﻿/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Dynamic Data                       Component : Common Types                            *
*  Assembly : Empiria.DynamicData.dll                    Pattern   : Dynamic information holder              *
*  Type     : DynamicFields                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Information holder type with dynamic fields.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Collections.Generic;
using System.Dynamic;

using Empiria.Json;

namespace Empiria.DynamicData {

  /// <summary>Information holder type with dynamic fields.</summary>
  public class DynamicFields : DynamicObject {

    private readonly IDictionary<string, object> _fields = new Dictionary<string, object>();


    public bool IsEmptyInstance {
      get {
        return this._fields.Keys.Count == 0;
      }
    }

    #region Methods


    public bool ContainsField(string fieldName) {
      return _fields.ContainsKey(fieldName);
    }


    public override IEnumerable<string> GetDynamicMemberNames() {
      return this._fields.Keys;
    }


    public decimal GetTotalField(string fieldName) {
      if (_fields.ContainsKey(fieldName)) {
        return (decimal) _fields[fieldName];
      } else {
        return 0;
      }
    }


    public void SetTotalField(string fieldName, decimal value) {
      if (_fields.ContainsKey(fieldName)) {
        _fields[fieldName] = value;
      } else {
        _fields.Add(fieldName, value);
      }
    }


    public IDictionary<string, object> ToDictionary() {
      return new Dictionary<string, object>(this._fields);
    }


    public JsonObject ToJson() {
      var json = new JsonObject();

      foreach (var fieldName in GetDynamicMemberNames()) {
        json.AddIfValue(fieldName, this._fields[fieldName]);
      }
      return json;
    }


    public override bool TryGetMember(GetMemberBinder binder, out object result) {
      string fieldName = binder.Name;

      if (_fields.ContainsKey(fieldName)) {
        result = _fields[fieldName];
      } else {
        result = null;
      }

      return result != null;
    }


    public override bool TrySetMember(SetMemberBinder binder, object value) {
      string fieldName = binder.Name;

      if (_fields.ContainsKey(fieldName)) {
        _fields[fieldName] = value;
      } else {
        _fields.Add(fieldName, value);
      }

      return true;
    }

    #endregion Methods

  } // class DynamicFields

} // namespace Empiria.DynamicData
