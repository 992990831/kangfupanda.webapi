using kangfupanda.dataentity.DAO;
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
    }
}
