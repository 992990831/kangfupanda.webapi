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
    [RoutePrefix("feedback")]
    public class FeedbackController : ApiController
    {
        /// <summary>
        /// 提交产品反馈
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ResponseEntity<string> Add(Feedback feedback)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new FeedbackDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.AddFeedback(feedback);
            if (success)
            {
                response = new ResponseEntity<string>(true, "反馈成功", "反馈成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "反馈失败", "反馈失败");
            }
            return response;
        }

        [HttpGet]
        [Route("list")]
        public AllFeedbacks GetAllFollows(int pageIndex, int pageSize)
        {
            AllFeedbacks result = new AllFeedbacks();

            var feedbackDao = new FeedbackDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            string filter = string.Empty;

            //if (!string.IsNullOrEmpty(followerName))
            //{
            //    filter = $" and u2.nickName like '%{followerName}%' ";
            //}
            var allFeedbacks = feedbackDao.GetFeedbacks(pageIndex, pageSize, filter);
            var count = feedbackDao.GetFeedbackCount(filter);

            result.feedbacks = allFeedbacks;
            result.count = count;

            return result;
        }
    }

    public class AllFeedbacks
    {
        public List<Feedback> feedbacks { get; set; }
        public long count { get; set; }
    }
}
