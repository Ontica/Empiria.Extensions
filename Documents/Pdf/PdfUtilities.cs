using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
namespace Empiria.Documents.Pdf {
  class PdfUtilities {
  

    static private string LoadHtmlFile(string fileName) {
      string htmlStringFile = File.ReadAllText(fileName, Encoding.UTF8);

      return htmlStringFile;
    }

    static public List<SectionDef> ConvertSectionsToList(string filePath, Settings settings) {
      string htmlStringFile = LoadHtmlFile(filePath);
           
      Section section = new Section();
      section.settings = settings;
      section.htmlStringFile = htmlStringFile;
      return section.GetSections();
    }

        


    

  }
}
