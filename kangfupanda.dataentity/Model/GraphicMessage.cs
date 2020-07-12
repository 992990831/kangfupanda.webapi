using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class GraphicMessage : BaseModel
    {
        public string name { get; set; }
        public string text { get; set; }

        /// <summary>
        /// 封面照
        /// </summary>
        public string poster { get; set; }

        public string pic01 { get; set; }
        public string pic02 { get; set; }
        public string pic03 { get; set; }
        public string pic04 { get; set; }
        public string pic05 { get; set; }
        public string pic06 { get; set; }

        public string audio01 { get; set; }
        public string audio02 { get; set; }
        public string audio03 { get; set; }

        /// <summary>
        /// 作者的openId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 作者的昵称
        /// </summary>
        public string author { get; set; }
    }

    public class GraphicMessageExt : GraphicMessage
    {
        public string authorHeadPic { get; set; }

        public string postId { get; set; }

        public long likeCount { get; set; }

        public long commentCount { get; set; }
    }
}
