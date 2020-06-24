using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Common;
using kangfupanda.webapi.Models;
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
    [RoutePrefix("complain")]
    public class ComplainController : ApiController
    {
        [HttpPost]
        [Route("")]
        public ResponseEntity<string> Add(Complaint complaint)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new ComplaintDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.AddComplaint(complaint);
            if (success)
            {
                response = new ResponseEntity<string>(true, "投诉成功", "投诉成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "投诉失败", "投诉失败");
            }
            return response;
        }
    }
}