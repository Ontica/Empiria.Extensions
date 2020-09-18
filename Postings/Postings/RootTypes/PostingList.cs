/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Domain services                       *
*  Assembly : Empiria.Postings.dll                         Pattern   : Information Holder                    *
*  Type     : PostingList                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains static methods to retrive list of postings, node objects or posted items.             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;


namespace Empiria.Postings {

  /// <summary>Contains static methods to retrive list of postings, node objects or posted items.</summary>
  static public class PostingList {


    static public FixedList<T> GetNodeObjects<T>(string postingType) where T : BaseObject {
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetNodeObjectsList<T>(postingType);
    }


    static public FixedList<T> GetNodeObjects<T>(BaseObject postedItem,
                                                 string postingType) where T : BaseObject {
      Assertion.AssertObject(postedItem, "postedItem");
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetNodeObjectsList<T>(postedItem, postingType);
    }


    static public FixedList<T> GetPostedItems<T>(string postingType) where T : BaseObject {
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetPostedItemsList<T>(postingType);
    }


    static public FixedList<T> GetPostedItems<T>(BaseObject nodeObject,
                                                 string postingType) where T: BaseObject {
      Assertion.AssertObject(nodeObject, "nodeObject");
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetPostedItemsList<T>(nodeObject, postingType);
    }


    static public FixedList<Posting> GetPostings(string postingType) {
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetPostingsList(postingType);

    }


    static public FixedList<Posting> GetPostings(BaseObject nodeObject, string postingType) {
      Assertion.AssertObject(nodeObject, "nodeObject");
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetPostingsList(nodeObject, postingType);
    }


    static public FixedList<Posting> GetPostingsForPostedItem(BaseObject postedItem, string postingType) {
      Assertion.AssertObject(postedItem, "postedItem");
      Assertion.AssertObject(postingType, "postingType");

      return PostingsData.GetPostingsForPostedItemList(postedItem, postingType);
    }


  } // class PostingList

}  // namespace Empiria.Postings
