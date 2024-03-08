using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheBugTracker.Models;
using Microsoft.AspNetCore.Http;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTFileService
    {
        public Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file);
        public string ConvertByteArrayToFile(byte[] fileData, string extension);
        public string GetFileIcon(string file);
        public string FormatFileSize(long bytes);
    }
}
