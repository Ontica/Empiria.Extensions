/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Presentation Framework Library    *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : UIComponentItemList                              Pattern  : Empiria List Class                *
*  Version   : 5.5        Date: 28/Mar/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : Represents an ordered named list of storage Item instances. Implements the IList interface    *
*              again because needs convert DataRow items to Item items in the IList indexer.                 *
*                                                                                                            *
********************************* Copyright (c) 2009-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

using Empiria.Collections;

namespace Empiria.Presentation {

  public class UIComponentItemList : EmpiriaList<UIComponentItem> {

    #region Fields

    #endregion Fields

    #region Constructors and parsers

    public UIComponentItemList() {
      //no-op
    }

    public UIComponentItemList(int capacity)
      : base(capacity) {
      // no-op
    }

    //public UIComponentItemList(DataRowCollection dataRows) : base(dataRows != null ? dataRows.Count : 0) {
    //  if(dataRows != null) {
    //    for (int i = 0; i < dataRows.Count; i++) {
    //      this.Add(UIComponentItem.LoadWithDataRow(dataRows[i]));
    //    }      
    //  }
    //}

    public UIComponentItemList(string name, int capacity)
      : base(name, capacity, false) {
      //no-op
    }

    //public UIComponentItemList(DataRowCollection dataRows, string name) : 
    //                              base(name, dataRows != null ? dataRows.Count : 0, false) {
    //  if(dataRows != null) {
    //    this.dataRows = dataRows;
    //    base.AddRange(new T[dataRows.Count]);
    //  }
    //  isHybrid = true;
    //}

    #endregion Constructors and parsers

    #region Public methods

    public new void Add(UIComponentItem item) {
      base.Add(item);
    }

    public override UIComponentItem this[int index] {
      get {
        return (UIComponentItem) base[index];
      }
    }

    public override void CopyTo(UIComponentItem[] array, int index) {
      for (int i = index, j = Count; i < j; i++) {
        array.SetValue(base[i], i);
      }
    }

    public int Remove(UIComponentItemList objectList) {
      IEnumerator<UIComponentItem> enumerator = this.GetEnumerator();

      int counter = 0;
      for (int i = 0; i < objectList.Count; i++) {
        if (base.Remove(objectList[i])) {
          counter++;
        }
      }
      return counter;
    }

    public void RemoveProtected() {
      throw new NotImplementedException();
    }

    #endregion Public methods

  } // class ObjectList

} // namespace Empiria.Ontology