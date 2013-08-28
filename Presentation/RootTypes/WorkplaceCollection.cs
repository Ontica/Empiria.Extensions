﻿/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Presentation Framework                  System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : WorkplaceCollection                              Pattern  : Hash List                         *
*  Date      : 23/Oct/2013                                      Version  : 5.2     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : Internal class that holds a collection of Workplace objects using a hash list.                *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using Empiria.Collections;

namespace Empiria.Presentation {

  /// <summary>Internal class that holds a collection of Workplace objects using a hash list.</summary>
  internal sealed class WorkplaceCollection : EmpiriaHashList<Workplace> {

    #region Constructors and parsers

    public WorkplaceCollection(int size, bool synchronized)
      : base(size, synchronized) {

    }

    #endregion Constructors and parsers

    #region Public properties

    public Workplace this[System.Guid workplaceGuid] {
      get { return (Workplace) base[workplaceGuid.ToString()]; }
      set { base[workplaceGuid.ToString()] = value; }
    }

    #endregion Public properties

    #region Public methods

    public void Add(Workplace workplace) {
      base.Add(workplace.Guid.ToString(), workplace);
    }

    public new void Clear() {
      base.Clear();
    }

    public bool Contains(System.Guid workplaceGuid) {
      return base.ContainsKey(workplaceGuid.ToString());
    }

    public void Remove(System.Guid workplaceGuid) {
      base.Remove(workplaceGuid.ToString());
    }

    #endregion Public methods

  } // class WorkplaceCollection

} // namespace Empiria.Presentation
