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
        public string comment_user_id { get; set; }

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
        /// 审核状态：0待审核，1通过，2未通过
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
        /// 有该字段说明该评论是作者加的，需要关联到另一个评论上
        /// </summary>
        public int parentId { get; set; }

        /// <summary>
        /// 发帖人的ID
        /// </summary>
        public int user_id { get; set; }

        /// <summary>
        /// 发帖人的名字，名字可能包含emoji表情
        /// </summary>
        public string user_name { get; set; }


    }

    /// <summary>
    /// 后台使用
    /// </summary>
    public class CommentsEx : Comments { 
        public string title { get; set; }
    }

    /// <summary>
    /// 评论和作者的回复
    /// </summary>
    public class CommentNReplies : Comments
    {
        /// <summary>
        /// 作者对评论的回复
        /// </summary>
        public List<Comments> Replies { get; set; }

    }
}
