/* Empiria Extended Framework 2014 ***************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extended Framework                       System   : Expressions Runtime Library       *
*  Namespace : Empiria.Expressions                              Assembly : Empiria.Expressions.dll           *
*  Type      : StatementList                                    Pattern  : Collection Class                  *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Defines an ordered list of statements objects.                                                *
*                                                                                                            *
********************************* Copyright (c) 2008-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;

using Empiria.Collections;

namespace Empiria.Expressions {

  /// <summary>Defines an ordered list of statements objects.</summary>
  public class StatementList<T> : EmpiriaList<T> where T : IStatement {

    #region Fields

    #endregion Fields

    #region Constructors and parsers

    public StatementList() {

    }

    static public StatementList<Assignment> ParseAssignments(string assignmentsTextForm) {
      StatementList<Assignment> assignments = new StatementList<Assignment>();

      assignmentsTextForm = assignmentsTextForm.Replace(',', ';');
      assignmentsTextForm = assignmentsTextForm.Replace(" ", "");
      assignmentsTextForm = assignmentsTextForm.Replace(":=", "~");

      string[] assignmentsArray = assignmentsTextForm.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
      for (int i = 0; i < assignmentsArray.Length; i++) {
        string[] assignmentParts = assignmentsArray[i].Split('~');
        if (assignmentParts.Length != 2) {
          throw new ExpressionsException(ExpressionsException.Msg.NotWellFormedAssignment, assignmentsArray[i]);
        }
        Assignment assignment = Assignment.Parse(assignmentParts[0], Expression.Parse(assignmentParts[1]));
        assignments.Add(assignment);
      }
      return assignments;
    }

    #endregion Constructors and parsers

    #region Public properties

    #endregion Public properties

    #region Internal methods

    internal new void Add(T statement) {
      Assertion.EnsureObject(statement, "statement");

      base.Add(statement);
    }

    internal new void Clear() {
      base.Clear();
    }

    #endregion Public methods

  }  // class StatementList

} // namespace Empiria.Expressions