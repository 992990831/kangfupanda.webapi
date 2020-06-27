using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
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
    [RoutePrefix("tag")]
    public class TagController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public ResponseEntity<string> Add(Tag tag)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new TagDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.AddTag(tag);
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

        [HttpPost]
        [Route("update")]
        public ResponseEntity<string> Update(Tag tag)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new TagDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.Update(tag);
            if (success)
            {
                response = new ResponseEntity<string>(true, "修改成功", "修改成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "修改失败", "修改失败");
            }
            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public ResponseEntity<string> DeleteTag(int id)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "删除成功", string.Empty);
            (new TagDao(ConfigurationManager.AppSettings["mysqlConnStr"])).DeleteById(id);

            return response;
        }

        /// <summary>
        /// 给管理后台使用的接口
        /// </summary>
        [HttpGet]
        [Route("list")]
        public TagList GetTagList(int pageIndex = 1, int pageSize = 10, string text = "")
        {
            List<Tag> tags = new List<Tag>();
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(text))
            {
                sb.Append($" and text like '%{text}%'");
            }

            var dao = new TagDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            tags = dao.GetList(sb.ToString(), pageIndex, pageSize);
            long count = dao.GetListCount(sb.ToString());
            return new TagList()
            {
                count = count,
                list = tags
            };
        }
    }

    public class TagList
    {
        public List<Tag> list { get; set; }
        public Int64 count { get; set; }
    }
}