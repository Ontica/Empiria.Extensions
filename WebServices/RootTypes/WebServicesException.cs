/* Empiria® Presentation Framework 2013 **********************************************************************
*                                                                                                            *
*  Solution  : Empiria® Service-Oriented Framework              System   : Empiria Web Services              *
*  Namespace : Empiria.WebServices                              Assembly : Empiria.WebServices.dll           *
*  Type      : Global                                           Pattern  : Empiria Exception Class           *
*  Date      : 23/Oct/2013                                      Version  : 5.2     License: CC BY-NC-SA 3.0  *
*                                                                                                            *
*  Summary   : The exception that is thrown when a web service operation call fails.                         *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;
using System.Reflection;

namespace Empiria.WebServices {

  /// <summary>The exception that is thrown when a web service operation call fails.</summary>
  [Serializable]
  public sealed class WebServicesException : EmpiriaException {

    public enum Msg {
      AjaxInvocationError,
      CommandProcessingError,
      InvalidMaxLogAttempts,
      InvalidSessionTimeout,
      InvalidSessionToken,
      InvalidTargetServerId,
      InvalidWebServicePassword,
      NullCommandName,
      NullCommandParameter,
      SessionTimeout,
      UnrecognizedCommandName,
      WebServicesServerInitializationFails,
      BadAuthenticationHeaderFormat,
      AuthenticationHeaderMissed,
      RequestHeaderMissed,
    }

    static private string resourceBaseName = "Empiria.WebServices.RootTypes.WebServicesExceptionMsg";

    #region Constructors and parsers


    /// <summary>Initializes a new instance of WebServicesException class with a specified error 
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebServicesException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of WebServicesException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="exception">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public WebServicesException(Msg message, Exception exception, params object[] args)
      : base(message.ToString(), GetMessage(message, args), exception) {
    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class WebServicesException

} // namespace Empiria.WebServices