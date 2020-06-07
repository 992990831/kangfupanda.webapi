using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class GraphicMessage : BaseModel
    {
        public string text { get; set; }
        public string pic01 { get; set; }
        public string pic02 { get; set; }
        public string pic03 { get; set; }
        public string pic04 { get; set; }
        public string pic05 { get; set; }
        public string pic06 { get; set; }
        /// <summary>
        /// 作者的openId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 作者的昵称
        /// </summary>
        public string author { get; set; }
    }
}
