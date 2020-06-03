using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Exceptions;
using kangfupanda.webapi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    [RoutePrefix("comments")]
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
        [HttpPost]
        [Route("add")]
        public ResponseEntity<string> AddComments(Comments comments)
        {
            var responseEntity = new ResponseEntity<string>(true, "添加成功", string.Empty);
            try
            {
                bool success = new CommentsDao(mysqlConnection).AddComments(comments);
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-添加失败！", ex);
                responseEntity = new ResponseEntity<string>(false, "添加失败", string.Empty);
            }

            return responseEntity;
        }

        [HttpGet]
        [Route("list")]
        public List<Comments> GetList(int postId, string postType)
        {
            List<Comments> comments = new List<Comments>();
            try
            {
                comments = new CommentsDao(mysqlConnection).GetList(postId, postType);
            }
            catch (Exception ex)
            {
                logger.Error("获取评论失败", ex);
            }

            return comments;
        }

        [HttpGet]
        [Route("audit/list")]
        public List<Comments> GetPendingAuditList()
        {
            List<Comments> comments = new List<Comments>();
            try
            {
                comments = new CommentsDao(mysqlConnection).GetPendingAuditList();
            }
            catch (Exception ex)
            {
                logger.Error("获取评论失败", ex);
            }

            return comments;
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("audit/approve")]
        public ResponseEntity<string> Approve(int commentId)
        {
            var responseEntity = new ResponseEntity<string>(true, "审核成功", string.Empty);
            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditApprove(commentId);
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
            }

            return responseEntity;
        }

        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("audit/reject")]
        public ResponseEntity<string> Reject(int commentId)
        {
            var responseEntity = new ResponseEntity<string>(true, "拒绝成功", string.Empty);

            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditReject(commentId);
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
            }

            return responseEntity;
        }

        /// <summary>
        /// 评论管理-获取评论，根据回复人ID
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get/replyUserID")]
        public ResponseEntity<string> GetCommentsByReplyUserID(Comments comments)
        {
            var responseEntity = new ResponseEntity<string>(true, "审核成功", string.Empty);

            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditComments(comments);
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
            }

            return responseEntity;
        }


        /// <summary>
        /// 评论管理-获取评论，根据帖子/视频ID
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get/postID")]
        public ResponseEntity<string> GetCommentsByPostID(Comments comments)
        {
            var responseEntity = new ResponseEntity<string>(true, "审核成功", string.Empty);

            try
            {
                bool success = new CommentsDao(mysqlConnection).AuditComments(comments);
            }
            catch (Exception ex)
            {
                logger.Error("评论管理-审核失败！", ex);
                responseEntity = new ResponseEntity<string>(false, "审核失败", string.Empty);
            }

            return responseEntity;
        }
    }
}
