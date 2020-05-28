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
    [RoutePrefix("message")]
    public class MessageController : ApiController
    {
        [Route("list")]
        public List<GraphicMessage> GetUserList()
        {
            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var messages = dao.GetList();

            return messages;
        }

        [Route("add")]
        public ResponseEntity<string> AddGraphicMessage(GraphicMessage msg)
        {
            msg.pic01 = ConfigurationManager.AppSettings["UploadUrl"] + msg.pic01;
            msg.pic02 = ConfigurationManager.AppSettings["UploadUrl"] + msg.pic02;
            msg.pic03 = ConfigurationManager.AppSettings["UploadUrl"] + msg.pic03;
            msg.pic04 = ConfigurationManager.AppSettings["UploadUrl"] + msg.pic04;
            msg.pic05 = ConfigurationManager.AppSettings["UploadUrl"] + msg.pic05;
            msg.pic06 = ConfigurationManager.AppSettings["UploadUrl"] + msg.pic06;

            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.AddGraphicMessage(msg);

            if (success)
            {
                response = new ResponseEntity<string>(true, "添加图文成功", "添加图文成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "添加图文失败", "添加图文失败");
            }

            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public ResponseEntity<string> DeleteGraphicMessage(int id)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.DeleteById(id);

            if (success)
            {
                response = new ResponseEntity<string>(true, "删除图文成功", "删除图文成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "删除图文失败", "删除图文失败");
            }

            return response;
        }
    }
}
