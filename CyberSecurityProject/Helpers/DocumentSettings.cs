using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace CyberSecurityProject.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // Get Folder Path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName);

            // Get FileName Must be unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //Get FilePath
            string filePath = Path.Combine(folderPath, fileName);

            // Turn Data into streaming and upload file
            using var fileStreaming = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStreaming);

            return fileName;

        }
    }
}
