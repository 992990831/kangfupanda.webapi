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
    [RoutePrefix("found")]
    public class FoundController : ApiController
    {
        [HttpPost]
        [Route("add")]
        public ResponseEntity<string> Add(Found found)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new FoundDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.AddFound(found);
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
        public ResponseEntity<string> Update(Found found)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new FoundDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.Update(found);
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
        public ResponseEntity<string> DeleteUser(string id)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "删除成功", string.Empty);
            (new FoundDao(ConfigurationManager.AppSettings["mysqlConnStr"])).DeleteById(id);

            return response;
        }

        /// <summary>
        /// 给管理后台使用的接口
        /// </summary>
        [HttpGet]
        [Route("list")]
        public FoundList GetFoundList(int pageIndex = 1, int pageSize = 10, string name = "")
        {
            List<Found> founds = new List<Found>();
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(name))
            {
                sb.Append($" and name like '%{name}%'");
            }

            var dao = new FoundDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            founds = dao.GetList(sb.ToString(), pageIndex, pageSize);
            long count = dao.GetListCount(sb.ToString());
            return new FoundList() {
                count = count,
                list = founds
            };
        }

        /// <summary>
        /// 给前台使App用的接口
        /// </summary>
        [HttpGet]
        [Route("list/app")]
        public List<Found> GetAllFoundList()
        {
            List<Found> founds = new List<Found>();
           
            var dao = new FoundDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            founds = dao.GetList(string.Empty, 1, 9999);
           
            return founds;
        }
    }

    public class FoundList
    {
        public List<Found> list { get; set; }
        public Int64 count { get; set; }
    }
}