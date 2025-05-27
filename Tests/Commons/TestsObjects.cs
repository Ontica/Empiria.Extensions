/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Empiria Tests                              Component : Infrastructure provider                 *
*  Assembly : Empiria.Tests.dll                          Pattern   : Service provider                        *
*  Type     : TestsObjects                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides services to get Empiria BaseObject instances used for tests.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;

namespace Empiria.Tests {

  /// <summary>Provides services to get Empiria BaseObject instances used for tests.</summary>
  static public class TestsObjects {

    static public T TryGetObject<T>() where T: BaseObject {
      var objects = BaseObject.GetFullList<T>()
                              .FindAll(x => !x.IsEmptyInstance);

      if (objects.Count == 0) {
        return null;
      }

      int randomIndex = EmpiriaMath.GetRandom(0, objects.Count - 1);

      return objects[randomIndex];
    }


    static public T TryGetObject<T>(Predicate<T> match) where T : BaseObject {
      var objects = BaseObject.GetFullList<T>()
                              .FindAll(match);

      if (objects.Count == 0) {
        return null;
      }

      int randomIndex = EmpiriaMath.GetRandom(0, objects.Count - 1);

      return objects[randomIndex];
    }


    static public T TryGetObject<T>(IEnumerable<T> objects) {
      var list = objects.ToFixedList();

      if (list.Count == 0) {
        return default(T);
      }

      int randomIndex = EmpiriaMath.GetRandom(0, list.Count - 1);

      return list[randomIndex];
    }

  }  // class TestsObjects

}  // namespace Empiria.Tests
