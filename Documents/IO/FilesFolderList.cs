/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework 2013                 System   : Document Management Services      *
*  Namespace : Empiria.Documents.IO                             Assembly : Empiria.Documents.dll             *
*  Type      : FilesFolderList                                  Pattern  : Empiria List Class                *
*  Date      : 25/Jun/2013                                      Version  : 5.1     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : List structure of FilesFolder instances.                                                     *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;
using System.Collections.Generic;

using Empiria.Collections;

namespace Empiria.Documents.IO {

  /// <summary>List structure of FilesFolder instances.</summary>
  public class FilesFolderList : EmpiriaList<FilesFolder> {

    #region Constructors and parsers

    public FilesFolderList() {
      //no-op
    }

    public FilesFolderList(int capacity)
      : base(capacity) {
      // no-op
    }

    public FilesFolderList(string name, int capacity)
      : base(name, capacity, false) {
      //no-op
    }

    #endregion Constructors and parsers

    #region Public methods

    public new void Add(FilesFolder item) {
      base.Add(item);
    }

    public override FilesFolder this[int index] {
      get {
        return (FilesFolder) base[index];
      }
    }

    public override void CopyTo(FilesFolder[] array, int index) {
      for (int i = index, j = Count; i < j; i++) {
        array.SetValue(base[i], i);
      }
    }

    public int Remove(FilesFolderList objectList) {
      IEnumerator<FilesFolder> enumerator = this.GetEnumerator();

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

  } // class FilesFolderList

} // namespace Empiria.Documents.IO