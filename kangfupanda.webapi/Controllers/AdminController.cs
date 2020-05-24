using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Models;

namespace kangfupanda.webapi.Controllers
{
    public class AdminController : ApiController
    {
        private static string mysqlConnection = ConfigurationManager.AppSettings["mysqlConnStr"];
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(AdminController));

        /// <summary>
        /// Admin-登录
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Admin/Logon")]
        public ActionResult Logon()
        {
            try
            {
                var responseEntity = new ResponseEntity<string>(true, "登录成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("登录失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "登录失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }

        }

        /// <summary>
        /// Admin-登出
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Admin/Logoff")]
        public ActionResult Logoff()
        {
            try
            {
                var responseEntity = new ResponseEntity<string>(true, "登出成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("登出失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "登出失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }

        }

        /// <summary>
        /// 视频管理-修改
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Admin/Video/Edit")]
        public ActionResult EditVideos(Video video)
        {
            try
            {
                bool success = new VideoDao(mysqlConnection).EditVideo(video);
                var responseEntity = new ResponseEntity<string>(true, "修改成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("视频管理-修改失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "修改失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }

        }

    }
}
