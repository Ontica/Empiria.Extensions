///* Entrada Framework *****************************************************************************************
//*																																																						 *
//*  Solution  : Entrada Web Services                             System   : Security Services                 *
//*  Namespace : Entrada.WebServices.Security                     Assembly : Entrada.WebServices.dll           *
//*  Type      : WebServicesAuthenticator                         Pattern  : Static Library                    *
//*  Date      : Apr, 2013                                        Version  : 2.0      Pattern version: 1.0     *
//*																																																						 *
//*  Summary   : Provides security services used in XML Web Services invocation.                               *
//*																																																						 *
//************************************************************************ Copyright (C) Entrada, Inc. 2013. **/
//using System;
//using System.Collections.Generic;
//using System.Security.Cryptography;
//using System.Text;

//using Empiria.Security;

//namespace Empiria.WebServices {

//  /// <summary>Provides security services used in XML Web Services invocation.</summary>
//  internal class WebServicesAuthenticator {

//    #region Fields

//    static private readonly object lockInstance = new Object();
//    static private readonly Guid globalSessionGuid = Guid.NewGuid();
//    static private Dictionary<string, WebServicesSession> sessionDictionary =
//                                                    new Dictionary<string, WebServicesSession>();

//    #endregion Fields

//    #region Public methods

//    static internal string Authenticate(string apiToken, string userID, string password) {
//      password = CalculateMD5Hash(password);

//      int contactId = SecurityData.GetContactIdForAuthentication(userType, userID, password);

//      if (contactId != -1) {
//        return WebServicesAuthenticator.CreateSignInToken(userType, contactId);
//      }
//      return null;
//    }

//    static internal EmpiriaIdentity GetCurrentUser(string signInToken) {
//      lock (lockInstance) {
//      EmpiriaIdentity o;
//        if (sessionDictionary.ContainsKey(signInToken)) {
//          return sessionDictionary[signInToken].User;
//        } else {
//          throw new WebServicesException(WebServicesException.Msg.InvalidSessionToken);
//        }
//      }
//    }

//    static internal bool IsAuthenticated(string signInToken) {
//      lock (lockInstance) {
//        return sessionDictionary.ContainsKey(signInToken);
//      }
//    }

//    internal static void InvalidateToken(string signInToken) {
//      lock (lockInstance) {
//        if (sessionDictionary.ContainsKey(signInToken)) {
//          sessionDictionary.Remove(signInToken);
//        }
//      }
//    }

//    static public bool SignOff(string signInToken) {
//      lock (lockInstance) {
//        if (sessionDictionary.ContainsKey(signInToken)) {
//          sessionDictionary.Remove(signInToken);
//          return true;
//        }
//        return false;
//      }
//    }

//    static internal void UpdateASPMembershipUsers() {
//      SecurityData.UpdateASPMembershipUsers();
//    }

//    #endregion Public methods

//    #region Private methods

//    static private string CalculateMD5Hash(string input) {
//      // step 1, calculate MD5 hash from input 
//      MD5 md5 = System.Security.Cryptography.MD5.Create();
//      byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
//      byte[] hash = md5.ComputeHash(inputBytes);
//      // step 2, convert byte array to hex string 
//      StringBuilder sb = new StringBuilder();
//      for (int i = 0; i < hash.Length; i++)
//        sb.Append(hash[i].ToString("X2"));
//      return sb.ToString().ToLower();
//    }

//    static private string CreateSignInToken(EntradaUserType userType, int contactId) {
//      WebServicesSession session = new WebServicesSession(userType, contactId);

//      lock (lockInstance) {
//        sessionDictionary[session.Token] = session;
//        return session.Token;
//      }
//    }

//    #endregion Private methods

//    /// <summary>Inner class that holds XML Web Services session data.</summary>
//    private class WebServicesSession {

//      #region Fields

//      private readonly Guid sessionGuid = Guid.NewGuid();
//      private readonly string userID = String.Empty;
//      private readonly string token = String.Empty;
//      private readonly string ipAddress = String.Empty;
//      private readonly DateTime startTime = DateTime.Now;
//      private readonly EntradaUser entradaUser = new EntradaUser();

//      #endregion Fields

//      #region Constructors and parsers

//      internal WebServicesSession(EntradaUserType userType, int contactId) {
//        this.userType = userType;
//        this.token = Cryptographer.Encrypt(ProtectionMode.PublicKey, this.sessionGuid + userID + userType.ToString(),
//                                           globalSessionGuid.ToString());
//        this.entradaUser = SecurityData.GetEntradaUser(userType, SecurityData.GetContact(contactId), token);
//        this.userID = entradaUser.UserID;
//      }

//      #endregion Constructors and parsers

//      #region Properties

//      internal string IPAddress {
//        get { return this.ipAddress; }
//      }

//      internal DateTime StartTime {
//        get { return this.startTime; }
//      }

//      internal Guid SessionGuid {
//        get { return this.sessionGuid; }
//      }

//      internal string Token {
//        get { return this.token; }
//      }

//      internal EmpiriaUser User {
//        get { return this.entradaUser; }
//      }

//      #endregion Properties

//    } // inner class WebServicesSession

//  } // class WebServicesAuthenticator

//} // namespace Empiria.WebServices.Security