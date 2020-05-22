using kangfupanda.webapi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace kangfupanda.webapi.Controllers
{
    public class VideoController : Controller
    {
        public string Hello()
        {
            return "hello";
        }

        public ActionResult Upload()
        {
            try
            {
                var file = this.Request.Files[0];
                string strFileExtName = file.FileName;

                string fileUploadPath = ConfigurationManager.AppSettings["UploadPath"].ToString();
                string fileUplaodUrl = ConfigurationManager.AppSettings["UploadUrl"].ToString();

                string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + file.FileName;
                filename = filename.Replace("+", "");
                filename = filename.Replace("%", "");
                filename = filename.Replace(" ", "");

                string fold = $"{DateTime.Today.ToString("yyyyMMdd")}";
                string url = fileUplaodUrl + fold + "/" + filename;

                string fullFolder = this.HttpContext.Server.MapPath("/") + fileUploadPath + "\\" + fold;

                if (!Directory.Exists(fileUploadPath + fold))
                {
                    Directory.CreateDirectory(fullFolder);
                }

                file.SaveAs(fullFolder + "\\" + filename);

                var response = new ResponseEntity<string>(true, "上传成功", url);

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Loger.LogErr("UploadFile " + ex.ToString());

                var response = new ResponseEntity<string>(false, "上传失败", null);

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
