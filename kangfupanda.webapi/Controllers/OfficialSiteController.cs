using kangfupanda.dataentity.DAO;
using kangfupanda.webapi.Common;
using kangfupanda.webapi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    [RoutePrefix("site")]
    public class OfficialSiteController : ApiController
    {
        const string fileUploadPath = "Site";

        [Route("save/video")]
        [HttpPost]
        public ResponseEntity<string> SaveVideo(SiteVideo video)
        {
            string videoStr = JsonConvert.SerializeObject(video);

            string folder = ConfigurationManager.AppSettings["SiteFolderPath"]; //System.Web.Hosting.HostingEnvironment.MapPath("/site/");
            string fileName = "video.txt";

            File.WriteAllText(folder + fileName, videoStr);

            var response = new ResponseEntity<string>(true, "上传成功", "");
            return response;
        }

        [Route("list/video")]
        public SiteVideo GetVideo()
        {
            string folder = ConfigurationManager.AppSettings["SiteFolderPath"]; //System.Web.Hosting.HostingEnvironment.MapPath("/site/");
            string fileName = "video.txt";
            string videoStr = File.ReadAllText(folder + fileName);

            var video = JsonConvert.DeserializeObject<SiteVideo>(videoStr);

            return video;
        }

        [Route("save/graphic")]
        [HttpPost]
        public ResponseEntity<string> SaveGraphic(SiteGraphic graphic)
        {
            string graphicStr = JsonConvert.SerializeObject(graphic);

            string folder = ConfigurationManager.AppSettings["SiteFolderPath"]; //System.Web.Hosting.HostingEnvironment.MapPath("/site/");
            string fileName = "graphic.txt";

            File.WriteAllText(folder + fileName, graphicStr);

            var response = new ResponseEntity<string>(true, "上传成功", "");
            return response;
        }

        [Route("list/graphic")]
        public SiteGraphic GetGraphic()
        {
            string folder = ConfigurationManager.AppSettings["SiteFolderPath"]; //System.Web.Hosting.HostingEnvironment.MapPath("/site/");
            string fileName = "graphic.txt";
            string graphicStr = File.ReadAllText(folder + fileName);

            var graphic = JsonConvert.DeserializeObject<SiteGraphic>(graphicStr);

            return graphic;
        }

        [Route("save/doctor")]
        [HttpPost]
        public ResponseEntity<string> SaveDoctor(SiteDoctor doctor)
        {
            string doctorStr = JsonConvert.SerializeObject(doctor);

            string folder = ConfigurationManager.AppSettings["SiteFolderPath"]; //System.Web.Hosting.HostingEnvironment.MapPath("/site/");
            string fileName = "doctor.txt";

            File.WriteAllText(folder + fileName, doctorStr);

            var response = new ResponseEntity<string>(true, "上传成功", "");
            return response;
        }

        [Route("list/doctor")]
        public SiteDoctor GetDoctor()
        {
            string folder = ConfigurationManager.AppSettings["SiteFolderPath"]; //System.Web.Hosting.HostingEnvironment.MapPath("/site/");
            string fileName = "doctor.txt";
            string doctorStr = File.ReadAllText(folder + fileName);

            var doctor = JsonConvert.DeserializeObject<SiteDoctor>(doctorStr);

            return doctor;
        }

        /// <summary>
        /// 官网 - 研习社
        /// </summary>
        /// <returns></returns>
        [Route("club/list")]
        public List<ClubItem> GetClubList()
        {
            List<ClubItem> results = new List<ClubItem>();

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            var messages = dao.GetListExt(count: 4, endId: int.MaxValue);

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
                    poster = !string.IsNullOrEmpty(msg.poster) ? msg.poster : msg.pic01,
                    pics = pics,
                    audioes = audioes,
                    itemType = ClubItemType.Graphic,
                    text = msg.text,
                    likeCount = msg.likeCount,
                    commentCount = msg.commentCount,
                    createdAt = msg.createdAt
                });
            });

            //SQL里面已经有order by g.id desc，所以这里不用排序了
            //results = results.OrderByDescending(r => r.createdAt).ToList();


            int id = 0;
            results.ForEach(r => { r.id = ++id; });

            return results;
        }
    }

    public class SiteVideo { 
        public string author01 { get; set; }
        public string author02 { get; set; }
        public string author03 { get; set; }
        public string author04 { get; set; }

        public string text01 { get; set; }
        public string text02 { get; set; }
        public string text03 { get; set; }
        public string text04 { get; set; }

        public string poster01 { get; set; }
        public string poster02 { get; set; }
        public string poster03 { get; set; }
        public string poster04 { get; set; }

        public string video01 { get; set; }
        public string video02 { get; set; }
        public string video03 { get; set; }
        public string video04 { get; set; }
    }

    public class SiteGraphic
    {
        public string author01 { get; set; }
        public string author02 { get; set; }
        public string author03 { get; set; }
        public string author04 { get; set; }

        public string text01 { get; set; }
        public string text02 { get; set; }
        public string text03 { get; set; }
        public string text04 { get; set; }

        public string pic01 { get; set; }
        public string pic02 { get; set; }
        public string pic03 { get; set; }
        public string pic04 { get; set; }
    }

    public class SiteDoctor
    {
        public string name01 { get; set; }
        public string name02 { get; set; }
        public string name03 { get; set; }
        public string name04 { get; set; }

        public string comment01 { get; set; }
        public string comment02 { get; set; }
        public string comment03 { get; set; }
        public string comment04 { get; set; }

        public string pic01 { get; set; }
        public string pic02 { get; set; }
        public string pic03 { get; set; }
        public string pic04 { get; set; }
    }
}
