/* Empiria Postings ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria Postings                             Component : Domain services                       *
*  Assembly : Empiria.Postings.dll                         Pattern   : Exception type                        *
*  Type     : PostingServiceException                      License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : The exception that is thrown when a posting service problem occurs.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Reflection;

namespace Empiria.Postings {

  /// <summary>The exception that is thrown when a posting service problem occurs.</summary>
  [Serializable]
  public sealed class PostingServiceException : EmpiriaException {

    public enum Msg {

    }

    static private string resourceBaseName = "Empiria.Postings.RootTypes.PostingServiceExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance PostingServiceException with a specified error
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public PostingServiceException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of PostingServiceException with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="innerException">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public PostingServiceException(Msg message, Exception innerException, params object[] args)
      : base(message.ToString(), GetMessage(message, args), innerException) {

    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class PostingServiceException

} // namespace Empiria.Postings
