using System.IO;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Interfaces
{
    public interface IFileService
    {
        Task<Stream> GetFileStream(string fileName);
        Task UploadFileFromStream(Stream stream, string fileName);
    }
}
