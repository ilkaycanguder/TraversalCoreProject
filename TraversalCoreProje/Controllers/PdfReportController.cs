using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace TraversalCoreProje.Controllers
{
    public class PdfReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult StaticPdfReport()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreports/" + "dosya1.pdf");
            var stream = new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Font path
            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fonts/", "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(bf, 12, Font.NORMAL);

            // Create paragraph with Turkish characters
            Paragraph paragraph = new Paragraph("Traversal Rezervasyon Pdf Raporu", font);
            document.Add(paragraph);
            document.Close();
            return File("/pdfreports/dosya1.pdf", "application/pdf", "dosya1.pdf");
        }

        public IActionResult StaticCustomerReport()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfreports/" + "dosya2.pdf");
            var stream = new FileStream(path, FileMode.Create);
            Document document = new Document(PageSize.A4);
            PdfWriter.GetInstance(document, stream);
            document.Open();

            // Font path
            string fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/fonts/", "arial.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(bf, 12, Font.NORMAL);

            // Create table
            PdfPTable pdfPTable = new PdfPTable(3);

            pdfPTable.AddCell(new PdfPCell(new Phrase("Misafir Adı", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("Misafir Soyadı", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("Misafir TC", font)));

            pdfPTable.AddCell(new PdfPCell(new Phrase("İlkay", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("Cangüder", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("11111111110", font)));

            pdfPTable.AddCell(new PdfPCell(new Phrase("Kemal", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("Yıldırım", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("22222222220", font)));

            pdfPTable.AddCell(new PdfPCell(new Phrase("Mehmet", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("Cangüder", font)));
            pdfPTable.AddCell(new PdfPCell(new Phrase("12345111103", font)));

            document.Add(pdfPTable);
            document.Close();
            return File("/pdfreports/dosya2.pdf", "application/pdf", "dosya2.pdf");
        }
    }
}
