/* Empiria Extensions Framework ******************************************************************************
*                                                                                                            *
*  Solution  : Empiria Extensions Framework                     System   : Presentation Services             *
*  Namespace : Empiria.Presentation                             Assembly : Empiria.Presentation.dll          *
*  Type      : PresentationException                            Pattern  : Exception Class                   *
*  Version   : 6.7                                              License  : Please read license.txt file      *
*                                                                                                            *
*  Summary   : The exception that is thrown when a user interface process problem occurs.                    *
*                                                                                                            *
********************************* Copyright (c) 2002-2016. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Reflection;

namespace Empiria.Presentation {

  /// <summary>The exception that is thrown when a user interface process problem occurs.</summary>
  [Serializable]
  public sealed class PresentationException : EmpiriaException {

    public enum Msg {
      CantLoadUIItemData,
      CantParseUIItem,
      CurrentWorkplaceIsNull,
      NullCommandHandler,
      NullViewManager,
      NullWorkplace,
      UnexpectedUIComponentItem,
      UnknownDataItemDirection,
      UnknownDataItemType,
      UnknownWorkplaceCommand,
      ViewIdNotFound,
      WorkplaceGuidNotSet,
      WorkplaceNotFound,
    }

    static private string resourceBaseName = "Empiria.Presentation.RootTypes.PresentationExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance of PresentationException class with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public PresentationException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of PresentationException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="innerException">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public PresentationException(Msg message, Exception innerException, params object[] args)
      : base(message.ToString(), GetMessage(message, args), innerException) {

    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class PresentationException

} // namespace Empiria.Presentation
