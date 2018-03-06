using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empiria.Documents.Pdf {
    enum DocumentType
    {
        None,
        Contract,
        Law
    };
    class Settings
    {
        public string titleRegex = "";
        public string numberRegex = "";
        public string sectionRegex = "";
        public string deleteTextRegex = "";
        public DocumentType documentType = DocumentType.None;
    }
}
