/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Empiria Web API Services          *
*  Namespace : Empiria.Core.WebApi                              Assembly : Empiria.WebApi.dll                *
*  Type      : NextObjectIdController                           Pattern  : Web API Controller                *
*  Version   : 1.1                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Contains web api methods that serves to generate object, relations and tables unique IDs.     *
*                                                                                                            *
********************************* Copyright (c) 2009-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http;

using Empiria.Security;
using Empiria.Data;
using Empiria.Ontology;
using Empiria.WebApi;

namespace Empiria.Core.WebApi {

  /// <summary>Contains web api methods that serves to generate object IDs.</summary>
  public class NextObjectIdController : WebApiController {

    #region Public APIs

    /// <summary>Retrives the next object id for a given object type name.</summary>
    /// <param name="objectTypeName">The object type name.</param>
    [HttpGet]
    [Route("v1/id-generator/types/{objectTypeName}")]
    public int GetNextObjectId(string objectTypeName) {
      try {
        base.RequireResource(objectTypeName, "objectTypeName");

        var objectType = ObjectTypeInfo.Parse(objectTypeName);

        return DataWriter.CreateId(objectType.DataSource);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    /// <summary>Retrives the next object id for a given relation type.</summary>
    /// <param name="relationName">The relation type name.</param>
    [HttpGet]
    [Route("v1/id-generator/relations/{relationTypeName}")]
    public int GetNextRelationId(string relationTypeName) {
      try {
        base.RequireResource(relationTypeName, "relationTypeName");

        var relationType = TypeRelationInfo.Parse<TypeRelationInfo>(relationTypeName);

        return DataWriter.CreateId(relationType.DataSource);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    /// <summary>Retrives the next object id for a given database table.</summary>
    /// <param name="tableName">The database table name.</param>
    [HttpGet]
    [Route("v1/id-generator/tables/{tableName}")]
    public int GetNextTableId(string tableName) {
      try {
        base.RequireResource(tableName, "tableName");

        return Data.DataWriter.CreateId(tableName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Public APIs

  }  // class NextObjectIdController

}  // namespace Empiria.Core.WebApi
