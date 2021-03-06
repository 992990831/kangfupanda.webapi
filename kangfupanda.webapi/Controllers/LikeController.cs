﻿using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
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
    [RoutePrefix("like")]
    public class LikeController : ApiController
    {
        [HttpPost]
        [Route("")]
        public ResponseEntity<string> Like(Like likeItem)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new LikeDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.Like(likeItem);
            if (success)
            {
                response = new ResponseEntity<string>(true, "点赞成功", "点赞成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "点赞失败", "点赞失败");
            }
            return response;
        }

        [HttpPost]
        [Route("~/dislike")]
        public ResponseEntity<string> Dislike(Like likeItem)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new LikeDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.Dislike(likeItem);
            if (success)
            {
                response = new ResponseEntity<string>(true, "取消点赞成功", "取消点赞成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "取消点赞失败", "取消点赞失败");
            }
            return response;
        }

        /// <summary>
        /// 返回点是否点赞过
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemId"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{itemType}/{itemId}/{openId}")]
        public bool GetLiked(string itemType, int itemId, string openId)
        {
            var dao = new LikeDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var like = dao.GetLike(itemId, itemType, openId);

            return like != null;
        }

        [HttpGet]
        [Route("{itemType}/{itemId}")]
        public Int64 GetLikeCount(string itemType, int itemId)
        {
            var dao = new LikeDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var count = dao.GetLikeCount(itemId, itemType);

            return count;
        }
    }
}
