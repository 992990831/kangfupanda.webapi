using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class Follow : BaseModel
    {
        /// <summary>
        /// 被关注人的openId
        /// </summary>
        public string followee { get; set; }

        /// <summary>
        /// 关注人的openId
        /// </summary>
        public string follower { get; set; }
    }

    public class FollowEx: Follow
    {
        /// <summary>
        /// 被关注人的姓名
        /// </summary>
        public string followeeName { get; set; }

        /// <summary>
        /// 关注人的姓名
        /// </summary>
        public string followerName { get; set; }

        public string createdAtStr { get; set; }
    }
}
