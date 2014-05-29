/* Empiria Presentation Framework 2014 ***********************************************************************
*                                                                                                            *
*  Solution  : Empiria Presentation Framework                   System   : Web Presentation Framework        *
*  Namespace : Empiria.Presentation.Web                         Assembly : Empiria.Presentation.Web.dll      *
*  Type      : WebPresentationException                         Pattern  : Empiria Exception Class           *
*  Version   : 5.5        Date: 25/Jun/2014                     License  : GNU AGPLv3  (See license.txt)     *
*                                                                                                            *
*  Summary   : The exception that is thrown when a web user interface process operation fails.               *
*                                                                                                            *
********************************* Copyright (c) 2002-2014. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Reflection;

namespace Empiria.Presentation.Web {

  /// <summary>The exception that is thrown when a web user interface process operation fails.</summary>
  [Serializable]
  public sealed class WebPresentationException : EmpiriaException {

    public enum Msg {
      AjaxInvocationError,
      CantRetriveContent,
      CommandParameterError,
      CommandProcessingError,
      NullCommandName,
      NullCommandParameter,
      InvalidMaxLogAttempts,
      InvalidSessionTimeout,
      SessionTimeout,
      UnrecognizedCommandName,
      WebApplicationInitializationFails,
    }

    static private string resourceBaseName = "Empiria.Presentation.Web.RootTypes.WebPresentationExceptionMsg";

    #region Constructors and parsers


    /// <summary>Initializes a new instance of WebPresentationException class with a specified error 
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebPresentationException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of WebPresentationException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="exception">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebPresentationException(Msg message, Exception exception, params object[] args)
      : base(message.ToString(), GetMessage(message, args), exception) {
    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class WebPresentationException

} // namespace Empiria.Presentation.Web
