using BookStore.ApplicationServices.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.ApplicationServices.Services
{
    public class BlobFileService : IFileService
    {
        private readonly CloudBlobClient _client;
        public BlobFileService(string connectionString)
        {
            _client = CloudStorageAccount
                .Parse(connectionString)
                .CreateCloudBlobClient();
        }

        public async Task<Stream> GetFileStream(string fileName)
        {
            var containerRef = _client.GetContainerReference("books");

            var fileRef = containerRef.GetBlockBlobReference(fileName);

            if (await fileRef.ExistsAsync())
            {
                var streamRef = new MemoryStream();
                await fileRef.DownloadToStreamAsync(streamRef);
                return streamRef;
            }

            return null;
        }

        public async Task UploadFileFromStream(Stream stream, string fileName)
        {
            var containerRef = _client.GetContainerReference("books");

            var fileRef = containerRef.GetBlockBlobReference(fileName);

            if (await fileRef.ExistsAsync())
            {
                return;
            }

            await fileRef.UploadFromStreamAsync(stream);
        }

    }
}
