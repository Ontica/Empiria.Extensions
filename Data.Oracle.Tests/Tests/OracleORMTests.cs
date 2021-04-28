/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Oracle Data Handler                        Component : Test Helpers                            *
*  Assembly : Empiria.Data.Oracle.Tests.dll              Pattern   : Unit tests                              *
*  Type     : OracleORMTests                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Integration tests for Empiria Oracle Data Handler with Empiria ORM Framework.                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.ORM;
using Empiria.Reflection;

namespace Empiria.Data.Handlers.Tests {

  /// <summary>Integration tests for Empiria Oracle Data Handler with Empiria ORM Framework.</summary>
  public class OracleORMTests {

    #region Facts

    [Fact]
    public void Should_Fill_Object_Structure() {
      var dataOperation = DataOperation.Parse("getUserSession", TestingConstants.SESSION_TOKEN);

      var rules = DataMappingRules.Parse(typeof(SessionTest));

      var oracleMethods = new OracleMethods();

      var dataRow = oracleMethods.GetDataRow(dataOperation);

      SessionTest session = ObjectFactory.CreateObject<SessionTest>();

      rules.DataBind(session, dataRow);

      Assert.NotNull(session);

      Assert.Equal(long.Parse("123456789012345678"), session.Id);
      Assert.Equal(TestingConstants.SESSION_TOKEN, session.SessionToken);
      Assert.Equal(3600, session.ExpiresIn);
      Assert.NotNull(session.ExtData);
      Assert.Equal(string.Empty, session.ExtData);
      Assert.Equal(new DateTime(2021, 03, 28), session.StartTime);
    }


    #endregion Facts

  }  // class OracleORMTests


  internal class SessionTest {

    [DataField("SessionId")]
    public long Id {
      get; private set;
    }

    [DataField("SessionToken")]
    public string SessionToken {
      get; private set;
    }

    [DataField("ExpiresIn")]
    public int ExpiresIn {
      get; private set;
    }

    [DataField("SessionExtData")]
    public string ExtData {
      get; private set;
    }

    [DataField("StartTime")]
    public DateTime StartTime {
      get; private set;
    }

  }  // SessionTest

}  // namespace Empiria.Data.Handlers.Tests
