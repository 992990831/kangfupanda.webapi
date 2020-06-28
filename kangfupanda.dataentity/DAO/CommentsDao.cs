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
                        "values(@comment_post_id, @comment_post_type,  @comment_user_id, @comment_user_name, @comment_user_pic, @user_IP,@content,@audit_status,@like_count,@dislike_count,@is_recommend,@parent_id,@user_id,@user_name,now(),now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("comment_post_id", comments.comment_post_id));
                    cmd.Parameters.Add(new MySqlParameter("comment_post_type", comments.comment_post_type));
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

        public List<Comments> GetList(int postId, string postType)
        {
            List<Comments> comList = new List<Comments>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from comments where expiredAt is null and comment_audit_status=1 and comment_post_id=@comment_post_id and comment_post_type=@comment_post_type", conn);
                    cmd.Parameters.Add(new MySqlParameter("comment_post_id", postId));
                    cmd.Parameters.Add(new MySqlParameter("comment_post_type", postType));

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Comments com = new Comments();
                        com.comment_id = (int)sqlReader["comment_id"];
                        com.comment_post_id = sqlReader["comment_post_id"] == DBNull.Value ? 0 : (int)sqlReader["comment_post_id"];
                        com.comment_post_type = sqlReader["comment_post_type"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_post_type"];
                        com.comment_user_id = sqlReader["comment_user_id"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_id"];
                        com.comment_user_name = sqlReader["comment_user_name"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_name"];
                        com.comment_user_pic = sqlReader["comment_user_pic"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_pic"];
                        com.comment_content = sqlReader["comment_content"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_content"];
                        com.comment_audit_status = sqlReader["comment_audit_status"] == DBNull.Value ? 0 : (int)sqlReader["comment_audit_status"];
                        com.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                        comList.Add(com);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return comList;
        }

        public List<Comments> GetPendingList(int postId, string postType, string openId)
        {
            List<Comments> comList = new List<Comments>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from comments where expiredAt is null and comment_audit_status=0 and comment_post_id=@comment_post_id and comment_post_type=@comment_post_type and comment_user_id=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("comment_post_id", postId));
                    cmd.Parameters.Add(new MySqlParameter("comment_post_type", postType));
                    cmd.Parameters.Add(new MySqlParameter("openId", openId));

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Comments com = new Comments();
                        com.comment_id = (int)sqlReader["comment_id"];
                        com.comment_post_id = sqlReader["comment_post_id"] == DBNull.Value ? 0 : (int)sqlReader["comment_post_id"];
                        com.comment_post_type = sqlReader["comment_post_type"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_post_type"];
                        com.comment_user_id = sqlReader["comment_user_id"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_id"];
                        com.comment_user_name = sqlReader["comment_user_name"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_name"];
                        com.comment_user_pic = sqlReader["comment_user_pic"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_pic"];
                        com.comment_content = sqlReader["comment_content"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_content"];
                        com.comment_audit_status = sqlReader["comment_audit_status"] == DBNull.Value ? 0 : (int)sqlReader["comment_audit_status"];
                        com.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                        comList.Add(com);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return comList;
        }

        /// <summary>
        /// 审批列表
        /// </summary>
        /// <returns></returns>

        public List<Comments> GetAuditList(int statusCode, int pageIndex=1, int pageSize=10, string filter="" )
        {
            List<Comments> comList = new List<Comments>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int skip = (pageIndex - 1) * pageSize;
                    MySqlCommand cmd = new MySqlCommand($"select * from comments where expiredAt is null and comment_audit_status={statusCode} {filter} limit {skip},{pageSize} ", conn);
                 
                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Comments com = new Comments();
                        com.comment_id = (int)sqlReader["comment_id"];
                        com.comment_post_id = sqlReader["comment_post_id"] == DBNull.Value ? 0 : (int)sqlReader["comment_post_id"];
                        com.comment_post_type = sqlReader["comment_post_type"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_post_type"];
                        com.comment_user_id = sqlReader["comment_user_id"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_id"];
                        com.comment_user_name = sqlReader["comment_user_name"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_name"];
                        com.comment_user_pic = sqlReader["comment_user_pic"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_user_pic"];
                        com.comment_content = sqlReader["comment_content"] == DBNull.Value ? string.Empty : (string)sqlReader["comment_content"];
                        com.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                        comList.Add(com);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return comList;
        }

        public Int64 GetAuditListCount(int statusCode)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from comments where expiredAt is null and comment_audit_status={statusCode}", conn);

                    count = (Int64)cmd.ExecuteScalar();
                  
                }
                finally
                {
                    conn.Close();
                }
            }

            return count;
        }

        /// <summary>
        /// 审批拒绝
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public bool AuditApprove(int commentId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update comments set comment_audit_status=1, updatedAt=now() where comment_id=@comment_id", conn);
                    cmd.Parameters.Add(new MySqlParameter("comment_id", commentId));
                   
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
        /// 审批拒绝
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public bool AuditReject(int commentId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update comments set comment_audit_status=2, updatedAt=now() where comment_id=@comment_id", conn);
                    cmd.Parameters.Add(new MySqlParameter("comment_id", commentId));

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
        /// 审核状态：0待审核，1通过，2未通过
        /// </summary>
        /// <param name="comments"></param>
        /// <returns></returns>
        public bool AuditComments(Comments comments)
        {
            if (string.IsNullOrEmpty(comments.comment_user_id))
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
