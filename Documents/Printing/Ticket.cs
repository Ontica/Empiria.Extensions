using System;
using System.Drawing;
using System.Drawing.Printing;

namespace Empiria.Documents.Printing {

  public class Ticket {

    private Document document = null;

    public Ticket() {
      // TODO: Complete member initialization
    }

    public void Load(Document document) {
      this.document = document;
    }

    public void Print(string printerName) {
      PrintDocument pd = new PrintDocument();
      PrinterSettings ps = new PrinterSettings();
      ps.PrinterName = printerName;
      pd.PrinterSettings = ps;
      Margins ms = new Margins();
      ms.Left = (int) pd.DefaultPageSettings.HardMarginX + 6;
      ms.Right = (int) pd.DefaultPageSettings.HardMarginX + 6;
      ms.Top = (int) pd.DefaultPageSettings.HardMarginY + 6;
      ms.Bottom = (int) pd.DefaultPageSettings.HardMarginY;
      pd.DefaultPageSettings.Margins = ms;
      pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
      pd.Print();
    }

    // The PrintPage event is raised for each page to be printed.
    private void pd_PrintPage(object sender, PrintPageEventArgs ev) {
      float leftMargin = ev.MarginBounds.Left;  //(ev.MarginBounds.Width) / 2;
      float topMargin = ev.MarginBounds.Top;
      float yPos = topMargin;

      document.Reset();
      while (document.ReadLine()) {
        string text = document.GetCurrentLine();

        if (text == "---Barcode---") {
          System.Drawing.Image bc = GetBarcode();
          yPos += document.Font.GetHeight(ev.Graphics);
          ev.Graphics.DrawImage(bc, new PointF(25, yPos));
          continue;
        }
        yPos += document.Font.GetHeight(ev.Graphics);
        ev.Graphics.DrawString(text, document.Font, Brushes.Black,
                               leftMargin, yPos, document.StringFormat);
      }
      ev.HasMorePages = false;
    }

    private System.Drawing.Image GetBarcode() {
      C1.Win.C1BarCode.C1BarCode bc = new C1.Win.C1BarCode.C1BarCode();
      bc.CodeType = C1.Win.C1BarCode.CodeTypeEnum.Code128;
      bc.Text = this.document.BarcodeString;
      bc.ShowText = false;
      return bc.Image;
    }

  } // class Ticket

} // namespace Empiria.Documents.Printing
