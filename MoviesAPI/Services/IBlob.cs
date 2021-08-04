using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public  interface IBlob
    {
        Task Upload(IFormFile model);
        Uri GetUri(string filename);
    }
}
