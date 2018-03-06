using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Empiria.Documents.Pdf {
  class Section {

    private int clauseCount = 0;
    private string clauseNumber = "";

    #region Properties

    private Settings _settings;    
    public Settings settings {
      get {
        return _settings;
      }
      set {
        _settings = value;
      }
    }


    private string _htmlStringFile;
    public string htmlStringFile {
      get {
        return _htmlStringFile;
      }
      set {
        _htmlStringFile = value;
      }
    }

    #endregion

    #region public 

    public List<SectionDef> GetSections() {

      DeleteStylePorperty();
      DeleteWhiteSpacesInsideHTmlTags();
      DeleteSpecialText();

      List<string> htmlSections = GetHTMLSections();
     
      List<SectionDef> sections = getSectionList(htmlSections);
     
      return sections;
    }


    #endregion

    #region private 

    private string DeleteText(string text, string regularExpression) {
      Regex regex = new Regex(regularExpression);
      Match match = regex.Match(text);

      if (match.Success) {
        while (match.Success) {

          string foundValue = match.Value;
          text = text.Replace(foundValue, string.Empty);
          match = regex.Match(text);
        }
      } else {
        throw new Exception("No logré eliminar datos para la expresion regular " + regularExpression);
      }

      return text;
    }


    private string FindText(string source, string regularExpression) {
      Regex regex = new Regex(regularExpression);

      Match match = regex.Match(source);
      if (match.Success) {
        return match.Value;
      } else {
        throw new Exception("No encontre texto para la expresion: " + regularExpression);
      }

    }


    private void DeleteStylePorperty() {
      string regularExpressionToDeleteStyle = "(style=)(.+?)(?=>)";

      htmlStringFile =  DeleteText(htmlStringFile, regularExpressionToDeleteStyle);
    }


    private void DeleteWhiteSpacesInsideHTmlTags() {
      Regex regex = new Regex(@"\s>");
      Match match = regex.Match(htmlStringFile);

      if (match.Success) {
        while (match.Success) {

          string foundValue = match.Value;
          htmlStringFile = htmlStringFile.Replace(foundValue, ">");

          match = regex.Match(htmlStringFile);
        }
      }
            
    }


    private void DeleteSpecialText() {
      if (settings.deleteTextRegex != string.Empty) {

        string regularExpressionToDelete = settings.deleteTextRegex;
        htmlStringFile = DeleteText(htmlStringFile, regularExpressionToDelete);
      }

    }
       

    private List<string> GetHTMLSections() {
      List<string> sections = new List<string>();

      Regex regex = new Regex(settings.sectionRegex);           
      Match match = regex.Match(htmlStringFile);

      if (match.Success) {
        while (match.Success) {

          string foundValue = match.Value;
          sections.Add(foundValue);
          htmlStringFile = htmlStringFile.Replace(foundValue, string.Empty);

          match = regex.Match(htmlStringFile);
        }
      } else {
        throw new Exception("No logré extraer las secciones html del archivo: " + htmlStringFile);
      }

      return sections;
    }


    private List<SectionDef> getSectionList(List<string> htmlSections) {
      List<SectionDef> sections = new List<SectionDef>();

      foreach (string htmlSection in htmlSections) {
        SectionDef section = new SectionDef();
        section.Title = GetTitle(htmlSection);
        section.Number = GetNumber(section.Title);
        section.Content = GetContent(htmlSection);
        section.ContenAsHtml = htmlSection;
        sections.Add(section);
      }

      return sections;
    }


    private string GetTitle(string section) {
      return FindText(section, settings.titleRegex);
    }


    private string GetNumber(string title) {
      switch (settings.documentType) {

        case DocumentType.Contract: return GetClauseNumber(title);

        case DocumentType.Law: return GetNumbersInString(title);
      }
      return "";
    }


    private string GetClauseNumber(string clauseTitle) {
      string clauseFullNumber = "";

      Regex regex = new Regex(@"CLÁUSULA \d+.");
      Match match = regex.Match(clauseTitle);

      if (match.Success) {
        clauseCount = 0;
        clauseNumber = match.Value;
        clauseFullNumber = clauseNumber;
      } else {
        clauseCount++;
        clauseFullNumber = clauseNumber + clauseCount.ToString();
      }

      return clauseFullNumber;
    }
    

    private string GetNumbersInString(string sectionTitle) {
      return FindText(sectionTitle, @"(\d+)");
    }


    private string GetContent(string htmlSection) {
      return Regex.Replace(htmlSection, "<.*?>", String.Empty);
    }       


    #endregion

  }
}
