﻿using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Route.C41.G02.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1.Get Located Folder Path.

            // string folderPath = $"D:\\FOLDER 1\\web Development Profrssional\\route\\back-end\\C# Sessions\\MVC\\new update\\Route.C41.G01.PL Solution\\Route.C41.G01.PL\\wwwroot\\files\\{folderName}";
            // string folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{folderName}";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            //2.GEt FileName and Make it Unique
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            //3.Get File Path
            string filePath = Path.Combine(folderPath, fileName);

            //4.Save File as Streams
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);
            return fileName;
        }

        public static void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName, fileName);

            if(File.Exists(filePath))
            {
                File.Delete(filePath);  
            }
        }
    }
}
