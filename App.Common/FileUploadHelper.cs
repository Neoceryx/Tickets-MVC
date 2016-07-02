using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using App.Entities;

namespace App.Common
{
    public class FileUploadHelper
    {
        public string SaveUploadFile(HttpPostedFileBase file, string uploadDirectory)
        {
            string directory = uploadDirectory.Substring(0, uploadDirectory.LastIndexOf("\\") + 1);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            file.SaveAs(uploadDirectory);
            return uploadDirectory;
        }
        public bool IsUploadedFileValid(HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }
    }
}
