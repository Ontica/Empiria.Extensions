/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : WorkplaceCollection                              Pattern  : Hash List                         *
*  Version   : 6.8                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : Internal class that holds a collection of Workplace objects using a hash list.                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Collections;

namespace Empiria.Presentation {

  /// <summary>Internal class that holds a collection of Workplace objects using a hash list.</summary>
  internal sealed class WorkplaceCollection {

    #region Fields

    private EmpiriaDictionary<System.Guid, Workplace> collection =
                                            new EmpiriaDictionary<System.Guid, Workplace>(4);

    #endregion Fields

    #region Constructors and parsers

    public WorkplaceCollection() {

    }

    #endregion Constructors and parsers

    #region Public properties

    public Workplace this[System.Guid workplaceGuid] {
      get {
        return collection[workplaceGuid];
      }
    }

    #endregion Public properties

    #region Public methods

    public void Add(Workplace workplace) {
      collection.Insert(workplace.Guid, workplace);
    }

    public void Clear() {
      collection.Clear();
    }

    public bool Contains(System.Guid workplaceGuid) {
      return collection.ContainsKey(workplaceGuid);
    }

    public void Remove(System.Guid workplaceGuid) {
      collection.Remove(workplaceGuid);
    }

    #endregion Public methods

  } // class WorkplaceCollection

} // namespace Empiria.Presentation
