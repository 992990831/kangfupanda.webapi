using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class Feedback : BaseModel
    {
        public string openId { get; set; }
        
        public string nickName { get; set; }

        public string phone { get; set; }

        public string productName { get; set; }

        public string comment { get; set; }
    }
}
