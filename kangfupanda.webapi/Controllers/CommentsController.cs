using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Exceptions;
using kangfupanda.webapi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

namespace kangfupanda.webapi.Controllers
{
    /// <summary>
    /// 评论相关
    /// </summary>
    public class CommentsController : ApiController
    {
        private static string mysqlConnection = ConfigurationManager.AppSettings["mysqlConnStr"];
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(CommentsController));

        /// <summary>
        /// 评论管理-添加
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Comments/Add")]
        public ActionResult AddComments(Comments comments)
        {
            try
            {
                bool success =  new CommentsDao(mysqlConnection).AddComments(comments);
                var responseEntity = new ResponseEntity<string>(true, "添加成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-添加失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "添加失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }

        }

        /// <summary>
        /// 评论管理-审核
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Comments/Audit")]
        public ActionResult AuditComments(Comments comments)
        {
            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditComments(comments);
                var responseEntity = new ResponseEntity<string>(true, "审核成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
        }

        /// <summary>
        /// 评论管理-获取评论，根据评论ID
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Comments/Get/ID")]
        public ActionResult GetCommentsByID(Comments comments)
        {
            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditComments(comments);
                var responseEntity = new ResponseEntity<string>(true, "审核成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
        }

        /// <summary>
        /// 评论管理-获取评论，根据回复人ID
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Comments/Get/ReplyUserID")]
        public ActionResult GetCommentsByReplyUserID(Comments comments)
        {
            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditComments(comments);
                var responseEntity = new ResponseEntity<string>(true, "审核成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
        }


        /// <summary>
        /// 评论管理-获取评论，根据帖子/视频ID
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Comments/Get/PostID")]
        public ActionResult GetCommentsByPostID(Comments comments)
        {
            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditComments(comments);
                var responseEntity = new ResponseEntity<string>(true, "审核成功", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                var responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
                return new JsonResult()
                {
                    Data = responseEntity
                };
            }
        }
    }
}
