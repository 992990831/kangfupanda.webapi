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
