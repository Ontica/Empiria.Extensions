/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Module   : Message Queue Services                       Component : Message Queue                         *
*  Assembly : Empiria.Messaging.dll                        Pattern   : Power type                            *
*  Type     : MessageType                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Power type that defines the type of a queued message.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Ontology;

namespace Empiria.Messaging {

  /// <summary>Power type that defines the type of a queued message.</summary>
  [Powertype(typeof(Message))]
  public sealed class MessageType : Powertype {

    #region Constructors and parsers

    private MessageType() {
      // Empiria power types always have this constructor.
    }

    static public new MessageType Parse(int typeId) {
      return ObjectTypeInfo.Parse<MessageType>(typeId);
    }

    static internal new MessageType Parse(string typeName) {
      return ObjectTypeInfo.Parse<MessageType>(typeName);
    }

    #endregion Constructors and parsers

    #region Types constants

    public static MessageType Default {
      get {
        return ObjectTypeInfo.Parse<MessageType>("ObjectType.Message");
      }
    }

    #endregion Types constants

  } // class MessageType

} // namespace Empiria.Messaging
