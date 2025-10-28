/* Empiria Financial *****************************************************************************************
*                                                                                                            *
*  Module   : CashFlow Explorer                          Component : Domain Layer                            *
*  Assembly : Empiria.CashFlow.Explorer.dll              Pattern   : Information Holder                      *
*  Type     : DynamicDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a dynamic result after a query execution.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.DynamicData {

  /// <summary>Holds a dynamic result after a query execution.</summary>
  public class DynamicDto<T> {

    public DynamicDto(FixedList<DataTableColumn> columns, FixedList<T> entries) {
      Assertion.Require(columns, nameof(columns));
      Assertion.Require(entries, nameof(entries));

      Columns = columns;
      Entries = entries;
    }

    public DynamicDto(IQuery query,
                      FixedList<DataTableColumn> columns,
                      FixedList<T> entries) : this(columns, entries) {
      Assertion.Require(query, nameof(query));

      Query = query;
    }


    public IQuery Query {
      get;
    }

    public FixedList<DataTableColumn> Columns {
      get;
    }

    public FixedList<T> Entries {
      get;
    }

  }  // class DynamicDto<T>

}  // namespace Empiria.DynamicData
