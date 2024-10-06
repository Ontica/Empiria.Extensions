/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Software Configuration Management              Component : Domain Layer                        *
*  Assembly : Empiria.Artifacts.dll                          Pattern   : Service provider                    *
*  Type     : DevOpsCryptographer                            License   : Please read LICENSE.txt file        *
*                                                                                                            *
*  Summary  : Holds information about an Empiria application deployment for an specific environment and      *
*             customer. Deployment is the root aggregate of the Empiria DevOps domain model.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Empiria.Artifacts {

  public class DevOpsCryptographer {

    #region Fields

    private readonly Deployment _deployment = null;

    #endregion Fields

    #region Constructors and parsers

    public DevOpsCryptographer(Deployment deployment) {
      Assertion.Require(deployment, "deployment");

      _deployment = deployment;
    }

    #endregion Constructors and parsers

    #region Public methods

    public string CreateSerialNumber(string hardwareCode) {
      string serialNumber = String.Empty;

      string[] licArray = _deployment.License.LicenseNumber.Split('-');
      for (int i = 0; i < licArray.Length; i++) {
        string part = hardwareCode.Substring(i * 11, 11) + licArray[i];
        serialNumber += this.GenerateSerialNumberPart(this.GetLicenseHashCode(part), hardwareCode) + "-";
      }
      return serialNumber.TrimEnd('-');
    }


    /// <summary>Takes a plaintext string and encrypts it with the giving public key.</summary>
    /// <param name="plaintext">Text string to be encrypted.</param>
    public string Encrypt(string plainText) {
      Assertion.Require(plainText, "plainText");

      string entropy = _deployment.License.LicenseName + _deployment.License.LicenseNumber;

      return EncryptString(plainText, entropy);
    }

    #endregion Public methods

    #region Private methods

    private byte[] ConstructIV(string publicKey) {
      byte[] result = new byte[16];
      byte[] key = _deployment.License.IV;
      byte[] license = GetLicenseKey();

      int x = 0;
      for (int i = 1; i < license.Length; i++) {
        x += (key[i] * ((license[license.Length - i - 1] % 7) + 1)) -
              (license[i] * ((key[key.Length - i - 1] % 13) + 1));
      }
      result[0] = (byte)(Math.Abs((x * 11) - 23) % 255);
      for (int i = 1; i < key.Length / 2; i++) {
        result[i] = (byte)((((x + 53) * result[i - 1]) + key[i]) + (x * key[key.Length - i - 1]) % 255);
        x += Math.Abs((license[i] * key[key.Length - i - 1]) - result[i]);
      }
      if (publicKey != null) {
        result = ParsePublicKey(publicKey, result);
      }
      return result;
    }


    private byte[] ConstructKey(string publicKey) {
      byte[] result = new byte[32];
      byte[] key = _deployment.License.Key;
      byte[] license = GetLicenseKey();

      int x = 0;
      for (int i = 1; i < license.Length; i++) {
        x += (key[i] * ((license[license.Length - i - 1] % 7) + 1)) -
          (license[i] * ((key[key.Length - i - 1] % 13) + 1));
      }
      result[0] = (byte)(Math.Abs((x * 3) - 11) % 255);
      for (int i = 1; i < key.Length / 2; i++) {
        result[i] = (byte)((((x + 43) * result[i - 1]) + key[i]) + ((x + 5) * key[key.Length - i - 1]) % 255);
        x += Math.Abs((license[i] * key[key.Length - i - 1]) - result[i]);
      }
      if (publicKey != null) {
        result = ParsePublicKey(publicKey, result);
      }
      return result;
    }


    private string EncryptString(string plainText, string publicKey) {
      var textConverter = new UTF8Encoding();
      var rijndael = new RijndaelManaged() {
        Padding = PaddingMode.Zeros,
        Key = ConstructKey(publicKey),
        IV = ConstructIV(publicKey)
      };
      ICryptoTransform encryptor = rijndael.CreateEncryptor();
      var memoryStream = new MemoryStream();
      var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

      byte[] plainTextArray = textConverter.GetBytes(plainText);
      cryptoStream.Write(plainTextArray, 0, plainTextArray.Length);
      cryptoStream.FlushFinalBlock();

      return Convert.ToBase64String(memoryStream.ToArray());
    }


    private string GenerateSerialNumberPart(string hashCode, string hardwareCode) {
      SHA256Managed sha = new SHA256Managed();
      byte[] hash = sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(hashCode + hardwareCode));

      return GetLicenseHashCode(Convert.ToBase64String(hash).ToUpper());
    }


    private string GetLicenseHashCode(string license) {
      char[] licenseArray = license.ToCharArray();
      string hashCode = String.Empty;

      int x = licenseArray[0] * 3;
      for (int i = 1; i < licenseArray.Length; i++) {
        switch (i % 3) {
          case 0:
            if ((x % 3) != 2) {
              x = 65 + (x % 26);
              hashCode += Convert.ToChar(x);
            } else {
              x = 49 + (x % 9);
              hashCode += Convert.ToChar(x);
            }
            x += licenseArray[i] * 3;
            break;
          case 1:
            x += licenseArray[i] * 5;
            break;
          case 2:
            x += licenseArray[i] * 7;
            break;
        } // switch
      }  // for
      if ((x % 3) != 2) {
        x = 65 + (x % 26);
        hashCode += Convert.ToChar(x);
      } else {
        x = 48 + (x % 10);
        hashCode += Convert.ToChar(x);
      }
      return hashCode;
    }


    private byte[] GetLicenseKey() {
      byte[] result = new byte[32];
      byte[] key = _deployment.License.LKey;
      var licenseName = String.Empty;
      var license = String.Empty;

      licenseName = _deployment.License.LicenseName;
      license = _deployment.License.LicenseNumber;
      int x = 0;
      for (int i = 1; i < license.Length; i++) {
        x += licenseName[i % licenseName.Length] +
             (key[i] * ((license[license.Length - i - 1] % 5) + 1)) -
             (license[i] * ((key[key.Length - i - 1] % 3) + 1));
      }
      result[0] = (byte)(Math.Abs(x) % 255);
      for (int i = 1; i < key.Length / 2; i++) {
        result[i] = (byte)(((x * result[i - 1]) + key[i]) + (x * key[key.Length - i - 1]) % 255);
        x += license[i] + license[license.Length - i - 1];
      }

      return result;
    }


    static private byte[] ParsePublicKey(string publicKey, byte[] byteArray) {
      var result = new byte[byteArray.Length];

      if (publicKey.IndexOf('.') != -1) {
        publicKey = publicKey.Substring(0, publicKey.IndexOf('.'));
      }
      for (int i = 0; i < byteArray.Length; i++) {
        result[i] = (byte)(byteArray[i] + publicKey[i % publicKey.Length] % 255);
      }
      return result;
    }

    #endregion Private methods

  } //class DevOpsCryptographer

}  // namespace Empiria.Artifacts
