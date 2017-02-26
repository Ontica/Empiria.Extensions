/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                   System   : Empiria I/O Services                *
*  Namespace : Empiria.IO                                     Assembly : Empiria.IO.dll                      *
*  Type      : FilesFolderList                                Pattern  : Empiria List Class                  *
*  Version   : 6.8                                            License  : Please read license.txt file        *
*                                                                                                            *
*  Summary   : List structure of FilesFolder instances.                                                      *
*                                                                                                            *
********************************* Copyright (c) 2004-2017. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Collections.Generic;

using Empiria.Collections;

namespace Empiria.Documents.IO {

  /// <summary>List structure of FilesFolder instances.</summary>
  public class FilesFolderList : BaseList<FilesFolder> {

    #region Constructors and parsers

    public FilesFolderList() {
      //no-op
    }

    public FilesFolderList(List<FilesFolder> list) : base(list) {
      //no-op
    }

    public FilesFolderList(int capacity) : base(capacity) {
      // no-op
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

    public int Remove(FilesFolderList list) {
      IEnumerator<FilesFolder> enumerator = this.GetEnumerator();

      int counter = 0;
      for (int i = 0; i < list.Count; i++) {
        if (base.Remove(list[i])) {
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
