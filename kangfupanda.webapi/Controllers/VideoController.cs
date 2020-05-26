﻿using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Models;
using kangfupanda.webapi.Util;
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

                string fileUploadPath = ConfigurationManager.AppSettings["UploadPath"];
                //string fileUplaodUrl = ConfigurationManager.AppSettings["UploadUrl"].ToString();

                string filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + file.FileName;
                filename = filename.Replace("+", "");
                filename = filename.Replace("%", "");
                filename = filename.Replace(" ", "");

                //string fold = $"{DateTime.Today.ToString("yyyyMMdd")}";
                //string url = fileUplaodUrl + filename;

                string fullFolder = this.HttpContext.Server.MapPath("/") + fileUploadPath + "\\";

                if (!Directory.Exists(fileUploadPath))
                {
                    Directory.CreateDirectory(fullFolder);
                }

                file.SaveAs(fullFolder + "\\" + filename);

                var response = new ResponseEntity<string>(true, "上传成功", filename);

                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                //Loger.LogErr("UploadFile " + ex.ToString());

                var response = new ResponseEntity<string>(false, "上传失败", null);

                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetVideos()
        {
            try
            {
                var videos = (new VideoDao(ConfigurationManager.AppSettings["mysqlConnStr"])).GetList();

                return Json(videos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            
        }

        /// <summary>
        /// 给前端App使用
        /// </summary>
        /// <returns></returns>
        public ActionResult GetVideosFront()
        {
            try
            {
                var videos = (new VideoDao(ConfigurationManager.AppSettings["mysqlConnStr"])).GetList(" order by createdat desc");

                return Json(videos, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        [System.Web.Http.HttpPost]
        public ActionResult AddVideo(Video video)
        {
            var responseEntity = new ResponseEntity<string>(true, "添加成功", string.Empty);

            bool success = (new VideoDao(ConfigurationManager.AppSettings["mysqlConnStr"])).AddVideo(video);

            return Json(responseEntity, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteVideo(int id)
        {
            var responseEntity = new ResponseEntity<string>(true, "删除成功", string.Empty);
            (new VideoDao(ConfigurationManager.AppSettings["mysqlConnStr"])).DeleteById(id);

            return Json(responseEntity, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 生成微信分享所需的config
        /// </summary>
        /// <returns></returns>
        public ActionResult getShareMessage()
        {
            var wxHelper = new WeixinHelper();
            var response = wxHelper.GenerateWXConfig();

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
