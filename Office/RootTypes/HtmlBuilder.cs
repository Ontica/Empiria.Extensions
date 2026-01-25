/* Empiria Extensions ****************************************************************************************
*                                                                                                            *
*  Module   : Office Integration Services                   Component : Service provider                     *
*  Assembly : Empiria.Office.dll                            Pattern   : Builder                              *
*  Type     : BudgetTransactionAsOrderVoucherBuilder        License   : Please read LICENSE.txt file         *
*                                                                                                            *
*  Summary  : Abstract class used to build Html documents and export them to PDF files.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.IO;
using System.Text;

using Empiria.Security;
using Empiria.Storage;

namespace Empiria.Office {

  /// <summary>Abstract class used to build Html documents and export them to PDF files.</summary>
  abstract public class HtmlBuilder {

    private readonly FileTemplateConfig _templateConfig;

    private readonly string _htmlTemplate;

    private readonly StringBuilder _builder;

    public HtmlBuilder(FileTemplateConfig templateConfig) {
      Assertion.Require(templateConfig, nameof(templateConfig));

      _templateConfig = templateConfig;

      _htmlTemplate = File.ReadAllText(_templateConfig.TemplateFullPath);

      _builder = new StringBuilder(_htmlTemplate);
    }

    #region Methods

    abstract protected void Build();

    public FileDto CreatePdf(string relativePath, string fileNamePrefix) {

      Build();

      string filename = _templateConfig.CreatePdfFileName(relativePath, fileNamePrefix);

      SaveHtmlAsPdf(_builder.ToString(), filename);

      return ToPdfFileDto(filename);
    }


    protected string GetSection(string sectionName) {
      int startIndex = _htmlTemplate.IndexOf("{{" + sectionName + ".START}}");
      int endIndex = _htmlTemplate.IndexOf("{{" + sectionName + ".END}}");

      var template = _htmlTemplate.Substring(startIndex, endIndex - startIndex);

      return template.Replace("{{" + sectionName + ".START}}", string.Empty);
    }


    protected void Hide(string value) {
      _builder.Replace(value, "hide");
    }


    protected void HideIf(string value, bool condition) {
      if (condition) {
        Hide(value);
      } else {
        Remove(value);
      }
    }


    protected string Normal(string value) {
      return $"<span class='normal'>{value}</span>";
    }


    protected void Remove(string value) {
      _builder.Replace(value, string.Empty);
    }


    protected void Set(string oldValue, string newValue) {
      _builder.Replace(oldValue, newValue);
    }


    protected void Set(string oldValue, decimal newValue) {
      _builder.Replace(oldValue, newValue.ToString("C2"));
    }


    protected void Set(string oldValue, DateTime dateTime) {
      _builder.Replace(oldValue, dateTime.ToString("dd/MMM/yyyy"));
    }


    protected void SetIf(string oldValue, bool condition, string newValue) {
      if (condition) {
        _builder.Replace(oldValue, newValue);
      } else {
        _builder.Replace(oldValue, string.Empty);
      }
    }


    protected void SetIf(string oldValue, bool condition, string trueValue, string falseValue) {
      if (condition) {
        _builder.Replace(oldValue, trueValue);
      } else {
        _builder.Replace(oldValue, falseValue);
      }
    }


    protected void SetSection(string sectionName, string html) {
      int startIndex = _builder.ToString().IndexOf("{{" + sectionName + ".START}}");
      int endIndex = _builder.ToString().IndexOf("{{" + sectionName + ".END}}");

      _builder.Remove(startIndex, endIndex - startIndex);

      _builder.Replace("{{" + sectionName + ".END}}", html);
    }


    protected void SetWarning(string oldValue, string newValue) {
      _builder.Replace(oldValue, Warning(newValue));
    }


    protected void SignIf(bool condition) {

      if (!condition) {
        SetWarning("{{SECURITY_CODE}}", "** SIN GENERAR ** ");
        SetWarning("{{SECURITY_SEAL}}", "ESTE DOCUMENTO NO TIENE VALIDEZ OFICIAL");

        Set("{{SYSTEM.DATETIME}}", $"Impresión: {DateTime.Now.ToString("dd/MMM/yyyy HH:mm")}");
        return;
      }

      string hashCode = _builder.ToString().GetHashCode().ToString();

      string securityCode = Cryptographer.GetSHA256(hashCode);

      Set("{{SECURITY_CODE}}", securityCode);

      string seal = Cryptographer.Encrypt(EncryptionMode.EntropyKey, _builder.ToString(), securityCode);

      seal = EmpiriaString.TruncateLast(seal, 320);

      seal = EmpiriaString.DivideLongString(seal, 112, "<br/>");

      Set("{{SECURITY_SEAL}}", seal);

      Set("{{SYSTEM.DATETIME}}", $"Impresión: {DateTime.Now.ToString("dd/MMM/yyyy HH:mm")}");
    }


    protected string Space(int count) {
      return EmpiriaString.Duplicate(" &nbsp; ", count);
    }


    protected string Strong(string value) {
      return $"<strong>{value}</strong>";
    }


    protected string Warning(string value) {
      return $"<span class='warning'>{value}</span>";
    }

    #endregion Methods

    #region Helpers

    private string GetFileName(string filename) {
      return Path.Combine(FileTemplateConfig.GenerationStoragePath, filename);
    }


    private void SaveHtmlAsPdf(string html, string filename) {
      string fullpath = GetFileName(filename);

      var pdfConverter = new HtmlToPdfConverter();

      var options = new PdfConverterOptions {
        BaseUri = FileTemplateConfig.TemplatesStoragePath,
        Landscape = true
      };

      pdfConverter.Convert(html, fullpath, options);
    }


    private FileDto ToPdfFileDto(string filename) {
      return new FileDto(FileType.Pdf, $"{FileTemplateConfig.GeneratedFilesBaseUrl}/{filename}");
    }

    #endregion Helpers

  }  // class HtmlBuilder

}  // namespace Empiria.Office
