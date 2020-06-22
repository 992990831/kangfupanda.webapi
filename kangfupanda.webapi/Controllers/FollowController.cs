﻿using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Common;
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
    [RoutePrefix("follow")]
    public class FollowController : ApiController
    {
        [HttpPost]
        [Route("")]
        public ResponseEntity<string> Follow(Follow follow)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.Add(follow);
            if (success)
            {
                response = new ResponseEntity<string>(true, "关注成功", "关注成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "关注失败", "关注失败");
            }
            return response;
        }

        [HttpGet]
        [Route("{followeeOpenId}/{followerOpenId}")]
        public bool GetFollowed(string followeeOpenId, string followerOpenId)
        {
            var dao = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool isFollowed = dao.GetFollowed(followeeOpenId, followerOpenId);
            
            return isFollowed;
        }

        /// <summary>
        /// 根据关注人的openId，获取所有被关注人的所有发帖
        /// </summary>
        /// <param name="followeeOpenId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list/{followerOpenId}")]
        public List<ClubItem> GetFollowerPost(string followerOpenId)
        {
            List<ClubItem> results = new List<ClubItem>();

            var followDao = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var followees = followDao.GetFollowersList(followerOpenId);

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            StringBuilder sb = new StringBuilder();
            sb.Append(" and g.openid in (''");
            followees.ForEach(followeeOpenId =>
            {
                sb.Append($",'{followerOpenId}'");
            });
            sb.Append(")");
            var messages = dao.GetListExt(sb.ToString());

            if (messages != null)
            {
                messages.ForEach(msg =>
                {
                    List<string> pics = new List<string>();
                    if (!string.IsNullOrEmpty(msg.pic01))
                    {
                        pics.Add(msg.pic01);
                    }
                    if (!string.IsNullOrEmpty(msg.pic02))
                    {
                        pics.Add(msg.pic02);
                    }
                    if (!string.IsNullOrEmpty(msg.pic03))
                    {
                        pics.Add(msg.pic03);
                    }
                    if (!string.IsNullOrEmpty(msg.pic04))
                    {
                        pics.Add(msg.pic04);
                    }
                    if (!string.IsNullOrEmpty(msg.pic05))
                    {
                        pics.Add(msg.pic05);
                    }
                    if (!string.IsNullOrEmpty(msg.pic06))
                    {
                        pics.Add(msg.pic06);
                    }

                    List<string> audioes = new List<string>();
                    if (!string.IsNullOrEmpty(msg.audio01))
                    {
                        audioes.Add(msg.audio01);
                    }
                    if (!string.IsNullOrEmpty(msg.audio02))
                    {
                        audioes.Add(msg.audio02);
                    }
                    if (!string.IsNullOrEmpty(msg.audio03))
                    {
                        audioes.Add(msg.audio03);
                    }

                    results.Add(new ClubItem()
                    {
                        postId = msg.id,
                        openId = msg.openId,
                        author = msg.author,
                        authorHeadPic = msg.authorHeadPic,
                        name = msg.name,
                        posterUri = msg.pic01,
                        pics = pics,
                        audioes = audioes,
                        itemType = ClubItemType.Graphic,
                        text = msg.text,
                        likeCount = msg.likeCount,
                        commentCount = msg.commentCount,
                        createdAt = msg.createdAt
                    });
                });
            }

            results = results.OrderByDescending(r => r.createdAt).ToList();

            return results;
        }

    }
}
