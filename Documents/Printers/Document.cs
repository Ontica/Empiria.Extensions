using System;
using System.Drawing;
using System.IO;

namespace Empiria.Documents.Printing {

  public class Document {

    private string[] rawLines = new string[0];
    private int currentLine = -1;
    private Font currentFont = null;
    private StringFormat currentStringFormat = new StringFormat();
    private string fontName = String.Empty;
    private int fontSize = 8;
    private string barcodeString = String.Empty;

    public Document(string fontName, int fontSize) {
      // TODO: Complete member initialization
      this.fontName = fontName;
      this.fontSize = fontSize;

      currentFont = new Font(this.fontName, this.fontSize);
    }

    public string BarcodeString {
      get { return barcodeString; }
      set { barcodeString = value; }
    }

    internal Font Font {
      get { return currentFont; }
      set { currentFont = value; }
    }

    internal int FontSize {
      get { return fontSize; }
      set { fontSize = value; }
    }

    internal StringFormat StringFormat {
      get { return currentStringFormat; }
      private set { currentStringFormat = value; }
    }

    public void LoadTemplate(string path) {
      rawLines = File.ReadAllLines(path);
      currentFont = new Font(this.fontName, this.fontSize);
    }

    public void Replace(string itemTemplate, string value) {
      for (int i = 0; i < rawLines.Length; i++) {
        if (!rawLines[i].Contains(itemTemplate)) {
          continue;
        }
        rawLines[i] = rawLines[i].Replace(itemTemplate, value);
        if (rawLines[i].Trim().Length == 0) {
          rawLines[i] = "{VOID_LINE}";
        }
      }
    }

    private bool IsCommand(string text) {
      if (text.StartsWith("{") && text.EndsWith("}")) {
        return true;
      }
      return false;
    }

    private void ProcessCommand(string text) {
      switch (text) {
        case "{FONT_NORMAL}":
          currentFont = new Font(this.fontName, this.FontSize);
          return;
        case "{FONT_BOLD}":
          currentFont = new Font(currentFont.Name, currentFont.Size, FontStyle.Bold);
          return;
        case "{FONT_BIG}":
          currentFont = new Font(this.fontName, this.FontSize + 2);
          return;
        case "{FONT_HUGE}":
          currentFont = new Font(this.fontName, this.FontSize + 5);
          return;
        case "{ALIGN_LEFT}":
          currentStringFormat = new StringFormat();
          return;
        case "{ALIGN_CENTER}":
          currentStringFormat = new StringFormat();
          currentStringFormat.Alignment = StringAlignment.Center;
          return;
        case "{FONT_LUCIDA_SMALL}":
          currentFont = new Font(this.fontName, this.FontSize - 1);
          return;
      }
    }

    internal string GetCurrentLine() {
      return rawLines[currentLine];
    }

    internal bool ReadLine() {
      while (true) {
        currentLine++;
        if (currentLine >= rawLines.Length) {
          return false;
        }
        if (IsCommand(rawLines[currentLine])) {
          ProcessCommand(rawLines[currentLine]);
        } else {
          return true;
        }
      }
    }

    internal void Reset() {
      currentLine = -1;
    }

  } // class Document

} // namespace Empiria.Documents.Printing