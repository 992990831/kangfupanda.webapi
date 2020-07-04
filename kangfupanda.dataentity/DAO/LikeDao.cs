using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class LikeDao : BaseDao
    {
        public LikeDao(string _connStr) : base(_connStr)
        {
        }

        public bool Like(Like like)
        {
            if (like == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into `like`(itemId, itemUId, itemType, likeById, likeByOpenId, createdAt, updatedAt) values(@itemId, @itemUId, @itemType, @likeById, @likeByOpenId, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("itemId", like.itemId));
                    cmd.Parameters.Add(new MySqlParameter("itemUId", like.itemUId));
                    cmd.Parameters.Add(new MySqlParameter("itemType", like.itemType));
                    cmd.Parameters.Add(new MySqlParameter("likeById", like.likeById));
                    cmd.Parameters.Add(new MySqlParameter("likeByOpenId", like.likeByOpenId));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool Dislike(Like like)
        {
            if (like == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update `like` set expiredAt=now() where itemId=@itemId and itemType=@itemType and likeByOpenId=@likeByOpenId", conn);
                    cmd.Parameters.Add(new MySqlParameter("itemId", like.itemId));
                    cmd.Parameters.Add(new MySqlParameter("itemType", like.itemType));
                    cmd.Parameters.Add(new MySqlParameter("likeByOpenId", like.likeByOpenId));

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
        /// 被点赞对象的Id，被点赞对象的类型，点赞人的likeByOpenId
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="itemType"></param>
        /// <param name="likeByOpenId"></param>
        /// <returns></returns>
        public Like GetLike(int itemId, string itemType, string likeByOpenId)
        {
            Like like = null;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select * from `like` where expiredat is null and itemId=@itemId and itemType=@itemType and likeByOpenId=@likeByOpenId", conn);
                    cmd.Parameters.Add(new MySqlParameter("itemId", itemId));
                    cmd.Parameters.Add(new MySqlParameter("itemType", itemType));
                    cmd.Parameters.Add(new MySqlParameter("likeByOpenId", likeByOpenId));

                    var sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        like = new Like();
                        like.id = (int)sqlReader["id"];
                        like.itemId = sqlReader["itemId"] == DBNull.Value ? 0 : (int)sqlReader["itemId"];
                        like.itemUId = sqlReader["itemUId"] == DBNull.Value ? string.Empty : (string)sqlReader["itemUId"];
                        like.itemType = sqlReader["itemType"] == DBNull.Value ? string.Empty : (string)sqlReader["itemType"];
                        like.likeById = sqlReader["likeById"] == DBNull.Value ? 0 : (int)sqlReader["likeById"];
                        like.likeByOpenId = sqlReader["likeByOpenId"] == DBNull.Value ? string.Empty : (string)sqlReader["likeByOpenId"];
                        like.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return like;
        }

        public Int64 GetLikeCount(int itemId, string itemType)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from `like` where expiredat is null and itemId=@itemId and itemType=@itemType", conn);
                    cmd.Parameters.Add(new MySqlParameter("itemId", itemId));
                    cmd.Parameters.Add(new MySqlParameter("itemType", itemType));

                    var result = cmd.ExecuteScalar();
                    count = (Int64)result;
                }
                finally
                {
                    conn.Close();
                }
            }

            return count;
        }

        public Int64 GetLikeCount(string openId)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from `like` as l left join `graphicmessage` as g on l.itemType='graphic' and itemid=g.id "
                    +" where l.expiredAt is null and g.expiredAt is null and g.openId = @openId; ", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", openId));

                    var graphicLikeCount = (Int64)cmd.ExecuteScalar();

                    count += graphicLikeCount;
                }
                finally
                {
                    conn.Close();
                }

                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from `like` as l left join `video` as v on l.itemType='video' and itemid=v.id "
                    + " where l.expiredAt is null and v.expiredAt is null and v.openId = @openId; ", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", openId));

                    var videoLikeCount = (Int64)cmd.ExecuteScalar();

                    count += videoLikeCount;
                }
                finally
                {
                    conn.Close();
                }
            }

            return count;
        }
    }
}
