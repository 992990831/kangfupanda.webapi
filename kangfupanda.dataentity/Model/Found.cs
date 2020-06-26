using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class Found : BaseModel
    {
        public string openId { get; set; }
        public string name { get; set; }
        public string nickName { get; set; }
        public string headpic { get; set; }
        public string city { get; set; }
        public string note { get; set; }
        public string detailimage { get; set; }
    }
}
