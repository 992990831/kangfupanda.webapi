using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace kangfupanda.dataentity.Model
{
    public class Comments : BaseModel
    {
        /// <summary>
        /// 评论的主键
        /// </summary>
        public int comment_id { get; set; }

        /// <summary>
        /// 帖子的id
        /// </summary>
        public int comment_post_id { get; set; }

        /// <summary>
        /// 帖子的类型：视频，图文 等
        /// video, graphic
        /// </summary>
        public string comment_post_type { get; set; }

        /// <summary>
        /// 评论人的id
        /// 也就是微信的openid
        /// </summary>
        public int comment_user_id { get; set; }

        /// <summary>
        /// 评论人的名字，名字可能包含emoji表情
        /// </summary>
        public string comment_user_name { get; set; }
        
        /// <summary>
        /// 评论人的头像
        /// </summary>
        public string comment_user_pic { get; set; }

        /// <summary>
        /// 评论人的IP地址
        /// </summary>
        public string comment_user_IP { get; set; }

        /// <summary>
        /// 评论内容，支持表情包
        /// </summary>
        public string comment_content { get; set; }

        /// <summary>
        /// 审核状态：0未通过，1通过，2待审核
        /// </summary>
        public int comment_audit_status { get; set; }

        /// <summary>
        /// 评论的点赞数
        /// </summary>
        public int comment_like_count { get; set; }

        /// <summary>
        /// 评论的踩数
        /// </summary>
        public int comment_dislike_count { get; set; }

        /// <summary>
        /// 此条评论是否被推荐，也就是是否精华评论
        /// </summary>
        public bool comment_is_recommend { get; set; }

        /// <summary>
        /// 评论的父ID，也就是支持评论的评论
        /// </summary>
        public int comment_parent_id { get; set; }

        /// <summary>
        /// 发帖人的ID
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// 发帖人的名字，名字可能包含emoji表情
        /// </summary>
        public string user_name { get; set; }


    }
}
