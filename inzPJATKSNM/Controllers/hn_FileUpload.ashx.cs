using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace inzPJATKSNM.Controllers
{
    /// <summary>
    /// Summary description for hn_FileUpload_ashx
    /// </summary>
    public class hn_FileUpload_ashx : IHttpHandler
    {

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string dirFullPath = HttpContext.Current.Server.MapPath("~/Images/SurveyPhotos");
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            numFiles = files.Length;
            numFiles = numFiles + 1;

            string str_image = "";

            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                //  int fileSizeInBytes = file.ContentLength;
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExtension = Path.GetExtension(fileName);
                    str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                    string pathToSave_100 = HttpContext.Current.Server.MapPath("~/MediaUploader/") + str_image;
                    file.SaveAs(pathToSave_100);
                }
            }
            context.Response.Write(str_image);
        }
    }
}