using System.IO;

namespace BookStore.DocumentParser.Interfaces
{
    public interface IPdfParser
    {
        string ExtractTextFromPdf(Stream fileStream);
    }
}
