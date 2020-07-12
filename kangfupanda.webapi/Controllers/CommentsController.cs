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
using System.Text;
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
        public List<Comments> GetList(int postId, string postType, string openId="")
        {
            List<Comments> comments = new List<Comments>();
            try
            {
                var dao = new CommentsDao(mysqlConnection);
                comments.AddRange(dao.GetList(postId, postType));

                if (!string.IsNullOrEmpty(openId))
                {
                    comments.AddRange(dao.GetPendingList(postId, postType, openId));
                }
            }
            catch (Exception ex)
            {
                logger.Error("获取评论失败", ex);
            }

            return comments.OrderByDescending(comm => comm.createdAt).ToList();
        }

        /// <summary>
        /// 获取待审核列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("audit/list")]
        public CommentsAuditList GetPendingAuditList(int pageIndex=1, int pageSize=10, string openId="")
        {
            CommentsAuditList result = new CommentsAuditList();
            List<Comments> comments = new List<Comments>();
            try
            {
                var dao = new CommentsDao(mysqlConnection);

                StringBuilder sb = new StringBuilder();
                if (!string.IsNullOrEmpty(openId))
                {
                    sb.Append($" comment_user_id='{openId}'");
                }

                comments = dao.GetAuditList(0, pageIndex, pageSize, sb.ToString());
                Int64 count = dao.GetAuditListCount(0);

                result.lists = comments;
                result.count = count;
            }
            catch (Exception ex)
            {
                logger.Error("获取评论失败", ex);
            }

            return result;
        }

        [HttpGet]
        [Route("audit/pending/count")]
        public Int64 GetPendingAuditCount()
        {
            try
            {
                var dao = new CommentsDao(mysqlConnection);

                Int64 count = dao.GetAuditListCount(0);
                return count;
            }
            catch (Exception ex)
            {
                logger.Error("获取评论失败", ex);
            }

            return 0;
        }


        [HttpGet]
        [Route("audit/list/approved")]
        public CommentsAuditList GetApprovedAuditList(int pageIndex = 1, int pageSize = 10)
        {
            CommentsAuditList result = new CommentsAuditList();
            List<Comments> comments = new List<Comments>();
            try
            {
                var dao = new CommentsDao(mysqlConnection);

                comments = dao.GetAuditList(1, pageIndex, pageSize);
                Int64 count = dao.GetAuditListCount(1);

                result.lists = comments;
                result.count = count;
            }
            catch (Exception ex)
            {
                logger.Error("获取评论失败", ex);
            }

            return result;
        }

        [HttpGet]
        [Route("audit/list/rejected")]
        public CommentsAuditList GetRejectedAuditList(int pageIndex = 1, int pageSize = 10)
        {
            CommentsAuditList result = new CommentsAuditList();
            List<Comments> comments = new List<Comments>();
            try
            {
                var dao = new CommentsDao(mysqlConnection);

                comments = dao.GetAuditList(2, pageIndex, pageSize);
                Int64 count = dao.GetAuditListCount(2);

                result.lists = comments;
                result.count = count;
            }
            catch (Exception ex)
            {
                logger.Error("获取评论失败", ex);
            }

            return result;
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

    public class CommentsAuditList { 
        public List<Comments> lists { get; set; }
        public Int64 count { get; set; }
    }
}
