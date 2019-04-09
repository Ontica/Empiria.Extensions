/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Documentation Services                       Component : Document Management                   *
*  Assembly : Empiria.Documents.dll                        Pattern   : Abstract Domain Type                  *
*  Type     : ImagingItem                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Abstract class that represents an imaging folder or a file (a real image or document).         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.StateEnums;

namespace Empiria.Documents {

  /// <summary>Abstract class that represents an imaging folder or a file (a real image or document)</summary>
  abstract public class ImagingItem : BaseObject {

    #region Constructors and parsers

    protected ImagingItem() {
      // Required by Empiria Framework.
    }


    static public ImagingItem Parse(int id) {
      return BaseObject.ParseId<ImagingItem>(id);
    }


    static public ImagingItem Empty {
      get {
        return BaseObject.ParseEmpty<ImagingItem>();
      }
    }


    #endregion Constructors and parsers

    #region Public properties


    [DataField("FilesCount")]
    public int FilesCount {
      get;
      protected set;
    }


    [DataField("ImagingItemPath")]
    public string ItemPath {
      get;
      protected set;
    }


    [DataField("DigitalizationDate")]
    public DateTime DigitalizationDate {
      get;
      protected set;
    } = ExecutionServer.DateMinValue;


    [DataField("DigitalizedById")]
    public Contact DigitalizedBy {
      get;
      protected set;
    }


    [DataField("ImagingItemExtData")]
    public JsonObject ImagingItemExtData {
      get;
      protected set;
    }

    [DataField("ImagingItemStatus", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get;
      protected set;
    }

    #endregion Public properties

  } // class ImagingItem

} // namespace Empiria.Documents
