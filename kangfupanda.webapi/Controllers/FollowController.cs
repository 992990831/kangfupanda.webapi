using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Common;
using kangfupanda.webapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    [RoutePrefix("follow")]
    public class FollowController : ApiController
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(FollowController));

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
        public List<ClubItem> GetFolloweePost(string followerOpenId)
        {
            logger.Info($"GetFolloweePost -> followerOpenId:{followerOpenId}");
            List<ClubItem> results = new List<ClubItem>();

            var followDao = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var followees = followDao.GetFolloweesList(followerOpenId);

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            StringBuilder sb = new StringBuilder();
            sb.Append(" and g.openid in (''");
            followees.ForEach(followeeOpenId =>
            {
                sb.Append($",'{followeeOpenId}'");
            });
            sb.Append(")");
            var messages = dao.GetListExt(sb.ToString(), count: int.MaxValue, endId: int.MaxValue);

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
                        poster = msg.poster,
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

            logger.Info($"GetFolloweePost -> return:{results.Count} items");
            return results;
        }

        /// <summary>
        /// 根据被关注人进行分组
        /// </summary>
        /// <param name="followerOpenId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list/v2/{followerOpenId}")]
        public List<PostsByDoctor> GetFolloweePostV2(string followerOpenId)
        {
            List<PostsByDoctor> result = new List<PostsByDoctor>();

            var followDao = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var userDao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            var followees = followDao.GetFolloweesList(followerOpenId);

            followees.ForEach(followeeOpenId =>
            {
                PostsByDoctor post = new PostsByDoctor();
                var author = userDao.GetUser(followeeOpenId);
                post.author = author;

                StringBuilder sb = new StringBuilder();
                sb.Append($" and g.openid='{followeeOpenId}' ");
                var messages = dao.GetListExt(sb.ToString(), count: int.MaxValue, endId: int.MaxValue);

                var clubItems = new List<ClubItem>();
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

                        clubItems.Add(new ClubItem()
                        {
                            postId = msg.id,
                            openId = msg.openId,
                            author = msg.author,
                            authorHeadPic = msg.authorHeadPic,
                            name = msg.name,
                            poster = msg.poster,
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

                post.posts = clubItems;

                result.Add(post);
            });

            return result;
        }


        /// <summary>
        /// 根据postId找到作者，然后关注该作者
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="followerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("post/{postId}/{followerId}")]
        public void FollowByPost(string postId, string followerId)
        {
            var graphicMessageDao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var graphicMsg = graphicMessageDao.GetById(postId);

            if (graphicMsg != null && !string.IsNullOrEmpty(graphicMsg.openId))
            {
                var followDao = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

                bool isFollowed = followDao.GetFollowed(graphicMsg.openId, followerId);

                if(!isFollowed)
                {
                    followDao.Add(new dataentity.Model.Follow() { followee = graphicMsg.openId, follower = followerId });
                }
            }
        }
    }

    /// <summary>
    ///  前台  发现 -> 关注 页面的对象模型。
    ///  由所关注的作者进行分组
    /// </summary>
    public class PostsByDoctor { 
        public User author { get; set; }

        public List<ClubItem> posts { get; set; }
    }
}
