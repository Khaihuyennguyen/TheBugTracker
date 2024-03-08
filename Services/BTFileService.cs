using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TheBugTracker.Services.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

using TheBugTracker.Models;

using TheBugTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.IO;
namespace TheBugTracker.Services
{
    public class BTFileService : IBTFileService
    {
        private readonly string[] suffixes = { "Bytes", "KB", "MB", "GB", "TB", "PB" };
        public string ConvertByteArrayToFile(byte[] fileData, string extension)
        {
            try
            {
                string imageBsed64Data = Convert.ToBase64String(fileData);
                return string.Format($"data: {extension}; base64, {imageBsed64Data}");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            try
            {
                MemoryStream memoryStream = new();
                await file.CopyToAsync(memoryStream);
                byte[] byteFile = memoryStream.ToArray();
                memoryStream.Close();
                memoryStream.Dispose();

                return byteFile;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string FormatFileSize(long bytes)
        {
            int counter = 0;
            decimal fileSize = bytes;
            while (Math.Round(fileSize/ 1024) >= 1)
            {
                fileSize /= 1024;
                counter++;
            }

            return string.Format("{0:n1}{1}", fileSize, suffixes[counter]);

        }

        public string GetFileIcon(string file)
        {
            string fileImage = "default";
            if (!string.IsNullOrWhiteSpace(file))
            {
                fileImage = Path.GetExtension(file).Replace(".", "");
                return $"/image/png/{fileImage}.png";
            }

            return fileImage;
        }
    }
}
