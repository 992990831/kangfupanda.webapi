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
    [RoutePrefix("tagxgraphic")]
    public class TagXGraphicController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public ResponseEntity<string> Add(List<TagXGraphic> tags)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new TagXGraphicDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            bool success = true;
            tags.ForEach(tag =>
            {
                dao.AddTagXGraphic(tag);
            });
            
            if (success)
            {
                response = new ResponseEntity<string>(true, "添加成功", "添加成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "添加失败", "添加失败");
            }
            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public ResponseEntity<string> DeleteTag(int graphicId)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new TagXGraphicDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.DeleteTags(graphicId);

            if (success)
            {
                response = new ResponseEntity<string>(true, "删除标签成功", "删除标签成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "删除标签失败", "删除标签失败");
            }

            return response;
        }

        [HttpGet]
        [Route("selected")]
        public List<TagXGraphic> GetSelected(int graphicId)
        {
            var dao = new TagXGraphicDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var tags = dao.GetList(graphicId);

            return tags;
        }
    }
}
