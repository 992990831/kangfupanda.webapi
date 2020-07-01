using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class ApiLog : BaseModel
    {
        public string openId { get; set; }
        
        public string nickName { get; set; }

        public string endpoint { get; set; }

        public string ip { get; set; }

        /// <summary>
        /// 客户端设备，比如Chrome、微信浏览器或其他
        /// </summary>
        public string client { get; set; }
    }

    public class VisitLog
    {
        public string openId { get; set; }
        public string nickName { get; set; }

        public string lastVisitedAt { get; set; }

        public long visitCountLastWeek { get; set; }
    }
}
