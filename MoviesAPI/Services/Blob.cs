using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class Blob : IBlob
    {
        private readonly string Container = "textimages";
        private readonly BlobServiceClient _blobServiceClient;
        public Blob(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task Upload(IFormFile model)
        {
            var blobClient = GetBlobServiceClient(model.FileName);
            await blobClient.UploadAsync(model.OpenReadStream(), overwrite: true);

        }
        public Uri GetUri(string filename) 
        {
            var blobClient = GetBlobServiceClient(filename);
            if (blobClient.ExistsAsync().Result)
            {
                var s = blobClient.Uri;
                return s;
            }
            
            return null;
        }
        private BlobClient GetBlobServiceClient(string name) 
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient(Container);
            var blobClient = blobContainer.GetBlobClient(name);
            return blobClient;
        }
    }
}

