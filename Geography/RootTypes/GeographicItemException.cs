/* Empiria® Extended Framework 2013 **************************************************************************
*                                                                                                            *
*  Solution  : Empiria® Extended Framework                    System   : Geographic Information Services     *
*  Namespace : Empiria.Geography                              Assembly : Empiria.Geography.dll               *
*  Type      : GeographicItemException                        Pattern  : Empiria Exception Class             *
*  Date      : 23/Oct/2013                                    Version  : 5.2     License: CC BY-NC-SA 3.0    *
*                                                                                                            *
*  Summary   : The exception that is thrown when a problem occurs in Empiria® Geographic Information         *
*              Services System.                                                                              *
*                                                                                                            *
**************************************************** Copyright © La Vía Óntica SC + Ontica LLC. 1999-2013. **/
using System;
using System.Reflection;

namespace Empiria.Geography {

  /// <summary>The exception that is thrown when a problem occurs in Empiria® Geographic Information
  /// Services System.</summary>
  [Serializable]
  public sealed class GeographicItemException : EmpiriaException {

    public enum Msg {
      SettlementAlreadyExists
    }

    static private string resourceBaseName = "Empiria.Geography.RootTypes.GeographicItemExceptionMsg";

    #region Constructors and parsers

    /// <summary>Initializes a new instance of GeographicItemException class with a specified error 
    /// message.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public GeographicItemException(Msg message, params object[] args)
      : base(message.ToString(), GetMessage(message, args)) {

    }

    /// <summary>Initializes a new instance of GeographicItemException class with a specified error
    ///  message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">Used to indicate the description of the exception.</param>
    /// <param name="innerException">This is the inner exception.</param>
    /// <param name="args">An optional array of objects to format into the exception message.</param>
    public GeographicItemException(Msg message, Exception innerException, params object[] args)
      : base(message.ToString(), GetMessage(message, args), innerException) {

    }

    #endregion Constructors and parsers

    #region Private methods

    static private string GetMessage(Msg message, params object[] args) {
      return GetResourceMessage(message.ToString(), resourceBaseName, Assembly.GetExecutingAssembly(), args);
    }

    #endregion Private methods

  } // class GeographicItemException

} // namespace Empiria.Geography
