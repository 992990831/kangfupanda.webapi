using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class Like : BaseModel
    {
        /// <summary>
        /// 被点赞对象的Id，
        /// 被点赞对象可能是video、graphic或user
        /// </summary>
        public int itemId { get; set; }

        /// <summary>
        /// 被点赞对象的Unique Id，
        /// 被点赞对象可能是video、graphic或user
        /// </summary>
        public string itemUId { get; set; }

        /// <summary>
        /// 被点赞对象的类型，
        /// 被点赞对象可能是video、graphic或user
        public string itemType { get; set; }

        /// <summary>
        /// 点赞人的Id
        /// </summary>
        public int likeById { get; set; }

        /// <summary>
        /// 点赞人的openId
        /// </summary>
        public string likeByOpenId { get; set; }
    }
}
