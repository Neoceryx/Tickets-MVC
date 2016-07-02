using App.Common;
using App.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.BLL
{
    public class FileUploadHelperBusiness
    {
        private static readonly string[] allowedExtensions = new string[] {".jpg", ".gif", ".png"};

        #region Variables
        private FileUploadHelper _fileUploadHelper;
        #endregion

        #region Constructors
        public FileUploadHelperBusiness()
        {
            _fileUploadHelper = new FileUploadHelper();
        }
        #endregion

        #region Methods
        public string FileSaveUploadFile(HttpPostedFileBase file, string directory)
        {
            if (!IsExtensionValid(file))
            {
                throw new InvalidFileExtension();
            }
            
            return _fileUploadHelper.SaveUploadFile(file, directory);

        }

        private bool IsExtensionValid(HttpPostedFileBase file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(fileExtension))
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
