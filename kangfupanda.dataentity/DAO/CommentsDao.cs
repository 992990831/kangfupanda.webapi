using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class CommentsDao : BaseDao
    {
        public CommentsDao(string _connStr) : base(_connStr)
        {
        }

        public bool AddComments(Comments comments)
        {
            if (comments == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into comments(comment_post_id, comment_post_type, comment_user_id, comment_user_name, comment_user_pic, comment_user_IP, comment_content,comment_audit_status,comment_like_count,comment_dislike_count,comment_is_recommend,comment_parent_id,user_id,user_name,createdAt,updatedAt) " +
                        "values(@post_id, @post_type,  @comment_user_id, @comment_user_name, @comment_user_pic, @user_IP,@content,@audit_status,@like_count,@dislike_count,@is_recommend,@parent_id,@user_id,@user_name,now(),now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("post_id", comments.comment_post_id));
                    cmd.Parameters.Add(new MySqlParameter("post_type", comments.comment_post_type));
                    cmd.Parameters.Add(new MySqlParameter("comment_user_id", comments.comment_user_id));
                    cmd.Parameters.Add(new MySqlParameter("comment_user_name", comments.comment_user_name));
                    cmd.Parameters.Add(new MySqlParameter("comment_user_pic", comments.comment_user_pic));
                    cmd.Parameters.Add(new MySqlParameter("user_IP", comments.comment_user_IP));
                    cmd.Parameters.Add(new MySqlParameter("content", comments.comment_content));
                    cmd.Parameters.Add(new MySqlParameter("audit_status", comments.comment_audit_status));
                    cmd.Parameters.Add(new MySqlParameter("like_count", comments.comment_like_count));
                    cmd.Parameters.Add(new MySqlParameter("dislike_count", comments.comment_dislike_count));
                    cmd.Parameters.Add(new MySqlParameter("is_recommend", comments.comment_is_recommend));
                    cmd.Parameters.Add(new MySqlParameter("parent_id", comments.comment_parent_id));
                    cmd.Parameters.Add(new MySqlParameter("user_id", comments.user_id));
                    cmd.Parameters.Add(new MySqlParameter("user_name", comments.user_name));
                    
                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        /// <summary>
        /// 审核状态：0未通过，1通过，2待审核
        /// </summary>
        /// <param name="comments"></param>
        /// <returns></returns>
        public bool AuditComments(Comments comments)
        {
            if (comments.comment_user_id <= 0)
            {
                return false;
            }
            //1. 鉴权：确认用户是否有权限审核

            //TODO

            //2. 鉴权通过，进行审核
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update comments set comment_audit_status = @audit_status where comment_id=@id", conn);
                    cmd.Parameters.Add(new MySqlParameter("id", comments.comment_id));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally {
                    conn.Close();
                }
            }
        }

    }
}
