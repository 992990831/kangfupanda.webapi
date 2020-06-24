using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class Complaint : BaseModel
    {
        public int itemId { get; set; }
        public string itemType { get; set; }

        //投诉人的手机号
        public string phone { get; set; }

        public string title { get; set; }

        public string complain { get; set; }

        /// <summary>
        /// 0未处理，1已处理
        /// </summary>
        public int status { get; set; }
    }
}
