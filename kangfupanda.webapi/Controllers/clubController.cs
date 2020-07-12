using kangfupanda.dataentity.DAO;
using kangfupanda.webapi.Common;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebGrease.Css.Extensions;

namespace kangfupanda.webapi.Controllers
{
    /// <summary>
    /// 研习社页面接口
    /// </summary>
    [RoutePrefix("club")]
    public class ClubController : ApiController
    {
        [Route("list")]
        public List<ClubItem> GetClubList(string openId, int count=10)
        {
            List<ClubItem> results = new List<ClubItem>();

            //var videos = (new VideoDao(ConfigurationManager.AppSettings["mysqlConnStr"])).GetList();
            //var videos = (new VideoDao(ConfigurationManager.AppSettings["mysqlConnStr"])).GetListExt();

            //if (videos != null)
            //{
            //    videos.ForEach(video =>
            //    {
            //        results.Add(new ClubItem()
            //        {
            //            postId=video.id,
            //            openId = video.openId,
            //            author = video.author,
            //            authorHeadPic = video.authorHeadPic,
            //            name = video.name,
            //            poster = video.posterUri,
            //            videoUri = video.videoUri,
            //            itemType = ClubItemType.Video,
            //            likeCount = video.likeCount,
            //            commentCount = video.commentCount,
            //            createdAt = video.createdAt
            //        });
            //    });
            //}

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            var ids = IDCache.AllIDs; //dao.GetAllIds();
            var idArray = ids.ToArray();
            var randomIds = new int[count];

            RandomFetch(idArray, randomIds, count);
            StringBuilder sb = new StringBuilder();
            sb.Append(" and g.id in ('' ");
            randomIds.ForEach((randomId) =>
            {
                sb.Append($",'{randomId}'");
            });
            sb.Append(") ");

            var messages = dao.GetListExt(filter: sb.ToString(), count: count);

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
                        poster = !string.IsNullOrEmpty(msg.poster)? msg.poster : msg.pic01,
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

            int id = 0;
            results.ForEach(r => { r.id = ++id; });

            //如果传入了Follower的openId
            if (!string.IsNullOrEmpty(openId))
            {
                results.ForEach(r => {
                    var dao2 = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
                    bool isFollowed = dao2.GetFollowed(r.openId, openId);

                    r.followed = isFollowed;
                });
            }

            return results;
        }

        [Route("{postId}")]
        public ClubItem GetPostById(string postId)
        {
            ClubItem result = new ClubItem();

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            var messages = dao.GetListExt(filter: $" and g.id={postId}");

            if (messages.Count > 0)
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

                    result = new ClubItem()
                    {
                        postId = msg.id,
                        openId = msg.openId,
                        author = msg.author,
                        authorHeadPic = msg.authorHeadPic,
                        name = msg.name,
                        poster = !string.IsNullOrEmpty(msg.poster) ? msg.poster : msg.pic01,
                        pics = pics,
                        audioes = audioes,
                        itemType = ClubItemType.Graphic,
                        text = msg.text,
                        likeCount = msg.likeCount,
                        commentCount = msg.commentCount,
                        createdAt = msg.createdAt
                    };
                });
            }

            return result;
        }


        /// <summary>
        /// 从Id数组中随机抽取若干个
        /// </summary>
        /// <param name="inputIds"></param>
        /// <param name="outputIds"></param>
        /// <param name="count"></param>
        void RandomFetch(int[] inputIds, int[] outputIds, int count)
        {
            if (inputIds.Length == 0 || count == 0)
            {
                return;
            }

            if (count-- > 0)
            {
                var randomId = (new Random(Guid.NewGuid().ToString().GetHashCode())).Next(0, inputIds.Length - 1);

                outputIds[outputIds.Length - count - 1] = inputIds[randomId];

                if (randomId == 0)
                {
                    var newArray = new int[inputIds.Length - 1];
                    for (int i = 0; i < newArray.Length; i++)
                    {
                        newArray[i] = inputIds[i + 1];
                    }
                    inputIds = newArray;
                }
                else
                {
                    var leftArray = new int[randomId];
                    for (int i = 0; i < randomId; i++)
                    {
                        leftArray[i] = inputIds[i];
                    }
                    var rightArray = new int[inputIds.Length - randomId - 1];
                    for (int i = randomId + 1; i < inputIds.Length; i++)
                    {
                        rightArray[i - randomId - 1] = inputIds[i];
                    }
                    //inputIds.CopyTo(rightArray, randomId+1);

                    var newArray = leftArray.Concat(rightArray).ToArray();

                    inputIds = newArray;
                }


            }

            if (inputIds.Count() > 0 && count > 0)
            {
                RandomFetch(inputIds, outputIds, count);
            }

        }

    }

    /// <summary>
    /// 研习社首页列表对象
    /// </summary>
    public class ClubItem
    {
        public int id { get; set; }

        /// <summary>
        /// 不同类型的item在各自表中的id
        /// </summary>
        public int postId { get; set; }

        /// <summary>
        /// 视频、图文的简介
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 作者openId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 作者昵称
        /// </summary>
        public string author { get; set; }

        /// <summary>
        /// 作者头像
        /// </summary>
        public string authorHeadPic { get; set; }

        /// <summary>
        /// 封面照
        /// </summary>
        public string poster { get; set; }

        public string text { get; set; }

        /// <summary>
        /// 类型，视频或图文
        /// </summary>
        public string itemType { get; set; }

        public string videoUri { get; set; }

        public List<string> pics { get; set; }

        /// <summary>
        /// 音频
        /// </summary>
        public List<string> audioes { get; set; }

        public DateTime createdAt { get; set; }

        /// <summary>
        /// 是否关注
        /// </summary>
        public bool followed { get; set; }

        /// <summary>
        /// 点赞数
        /// </summary>
        public long likeCount { get; set; }

        /// <summary>
        /// 评论数
        /// </summary>
        public long commentCount { get; set; }
    }
}
