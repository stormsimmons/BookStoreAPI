using BookStore.DocumentParser.Interfaces;
using System.IO;

namespace BookStore.DocumentParser.PdfParsers
{
    public class PdfParser : IPdfParser
    {

        public string ExtractTextFromPdf(Stream fileStream)
        {
            var doc = new IronPdf.PdfDocument(fileStream);

            var text = doc.ExtractAllText();

            return text;
        }
    }
}
