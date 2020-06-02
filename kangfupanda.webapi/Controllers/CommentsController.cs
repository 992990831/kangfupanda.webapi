﻿using kangfupanda.dataentity.DAO;
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

        /// <summary>
        /// 评论管理-审核
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("audit")]
        public ResponseEntity<string> AuditComments(Comments comments)
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
        /// 评论管理-获取评论，根据评论ID
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("get/id")]
        public ResponseEntity<string> GetCommentsByID(Comments comments)
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
