using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using SQ.Core.Upload;
using System.Web;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MB.Controllers
{
    [RoutePrefix("api/Upload")]
    public class UploadController : ApiController
    {
        [HttpPost]
        [Route("")]
        public async Task<UploadResult> UploadFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string folder = ConfigurationManager.AppSettings["FileIntMarketPath"].ToString()
                + UploadHelper.GetDateFolder();

            string directory = HttpContext.Current.Server.MapPath("~/" + folder);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var provider = new SQStreamProvider(directory);

            var result = new UploadResult();

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                if (provider.FileData.Count() > 1)
                {
                    result.Error = "上传数量出错";
             
                    return result;
                }
                FileInfo fi = new FileInfo(provider.FileData[0].LocalFileName);
               
                UploadHelper.Crop(fi.FullName, 120);
                UploadHelper.Crop(fi.FullName, 430);
                UploadHelper.Crop(fi.FullName, 800);
                result.ImageName = fi.Name;
                result.Status = "success";
                result.ImageUrl = UploadHelper.GetImgSaveUrl(folder, fi.Name.Replace(fi.Extension,""), fi.Extension);
                result.ImageUrl_120 = UploadHelper.GetImgSaveUrl(folder, fi.Name.Replace(fi.Extension, ""), fi.Extension, 120);
                return result;
            }
            catch (System.Exception e)
            {
                result.Error = e.Message;
                return result;
            }
        }

    }
}
