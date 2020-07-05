using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class User : BaseModel
    {
        public string nickName { get; set; }
        public string openId { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string sex { get; set; }
        public string phone { get; set; }

        /// <summary>
        /// 微信头像uri
        /// </summary>
        public string headpic { get; set; }

        /// <summary>
        /// 用户类型：治疗师/用户
        /// </summary>
        public string usertype { get; set; }

        /// <summary>
        /// 个人简介
        /// </summary>
        public string note { get; set; }

        /// <summary>
        /// 特长
        /// </summary>
        public string expertise { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool verified { get; set; }

        /// <summary>
        /// 是否在app中显示
        /// </summary>
        public bool displayinapp { get; set; }

        /// <summary>
        /// 用户详情二级页面的图片
        /// </summary>
        public string detailimage { get; set; }
    }

    public class UserProfile : User { 
        
        /// <summary>
        /// 粉丝数
        /// </summary>
        public Int64 fansCount { get; set; }

        /// <summary>
        /// 关注数
        /// </summary>
        public Int64 followeeCount { get; set; }

        /// <summary>
        /// 获赞数
        /// </summary>
        public Int64 likeCount { get; set; }
    }

    public class UserExt : User {
        public string lastVisitedAt { get; set; }

        public long visitCountLastWeek { get; set; }
    }
}
