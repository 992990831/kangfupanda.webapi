using kangfupanda.dataentity.DAO;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    /// <summary>
    /// 研习社页面接口
    /// </summary>
    [RoutePrefix("club")]
    public class ClubController : ApiController
    {
        [Route("list")]
        public List<ClubItem> GetClubList()
        {
            List<ClubItem> results = new List<ClubItem>();

            var videos = (new VideoDao(ConfigurationManager.AppSettings["mysqlConnStr"])).GetList();

            if (videos != null)
            {
                videos.ForEach(video =>
                {
                    results.Add(new ClubItem()
                    {
                        postId=video.id,
                        author = video.author,
                        name = video.name,
                        posterUri = video.posterUri,
                        videoUri = video.videoUri,
                        itemType = "video",
                        createdAt = video.createdAt
                    });
                });
            }

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var messages = dao.GetList();

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

                    results.Add(new ClubItem()
                    {
                        postId = msg.id,
                        author = msg.author,
                        name = msg.text.Length > 20 ? $"{msg.text.Substring(0, 17)}..." : msg.text,
                        posterUri = msg.pic01,
                        pics = pics,
                        itemType = "graphic",
                        text = msg.text,
                        createdAt = msg.createdAt
                    });
                });
            }

            results = results.OrderByDescending(r => r.createdAt).ToList();

            int id = 0;
            results.ForEach(r => { r.id = ++id; });

            return results;
        }
    }

    /// <summary>
    /// 研习社首页列表对象
    /// </summary>
    public class ClubItem
    {
        public int id { get; set; }

        public int postId { get; set; }

        /// <summary>
        /// 视频、图文的简介
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 封面照
        /// </summary>
        public string posterUri { get; set; }

        public string text { get; set; }

        /// <summary>
        /// 类型，视频或图文
        /// </summary>
        public string itemType { get; set; }

        public string videoUri { get; set; }

        public List<string> pics { get; set; }
        public DateTime createdAt { get; set; }
    }
}
