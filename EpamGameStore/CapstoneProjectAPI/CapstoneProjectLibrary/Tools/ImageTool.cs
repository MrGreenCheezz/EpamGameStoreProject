using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CapstoneProjectLibrary.Tools
{
    public class ImageTool
    {
        public static string SaveImage(IFormFile file)
        {
            string imageUrl = "";
                try
                {
                    if (Directory.Exists(Directory.GetCurrentDirectory() + "/Images"))
                    {
                        using (var fileStream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/Images/" + file.FileName))
                        {
                            file.CopyTo(fileStream);
                        }
                        imageUrl = "http://localhost:21409/api/pictures/getPicture?PicName=" + file.FileName;
                    }
                    else
                    {
                        Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Images");
                        using (var fileStream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/Images/" + file.FileName))
                        {
                            file.CopyTo(fileStream);
                        }
                        imageUrl = "http://localhost:21409/api/pictures/getPicture?PicName=" + file.FileName;
                    }
                }
                catch (PathTooLongException ex)
                {
                var Logger = NLog.LogManager.GetCurrentClassLogger();
                Logger.Error(ex.Message);
                }
            return imageUrl;
        }
    }
}
