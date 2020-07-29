using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    [RoutePrefix("apilog")]
    public class ApiLogController : ApiController
    {
        [HttpGet]
        [Route("list")]
        public VisitLogList GetApiLogList(int pageIndex = 1, int pageSize = 10)
        {
            List<VisitLog> logs = new List<VisitLog>();
            var dao = new ApiLogDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            logs = dao.GetList(pageIndex, pageSize);

            logs.ForEach(log =>
            {
                log.visitCountLastWeek = dao.GetLastWeekCount(log.openId);
                //log.lastVisitedAt = dao.GetLastVisitedTime(log.openId);
            });

            long count = dao.GetListCount();
            return new VisitLogList()
            {
                count = count,
                list = logs
            };
        }
    }

    public class VisitLogList
    {
        public List<VisitLog> list { get; set; }
        public Int64 count { get; set; }
    }


}
