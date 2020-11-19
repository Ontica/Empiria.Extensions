/* Empiria Storage *******************************************************************************************
*                                                                                                            *
*  Module   : Document Management                        Component : Domain Layer                            *
*  Assembly : Empiria.Storage.dll                        Pattern   : Information holder                      *
*  Type     : Document                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A document.                                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Storage.Documents {

  public class Document : MediaFile {

    #region Constructors and parsers

    static public new Document Empty => BaseObject.ParseEmpty<Document>();

    public object DocumentType {
      get;
      internal set;
    }

    #endregion Constructors and parsers

  }  // class Document

} // namespace Empiria.Storage.Documents
