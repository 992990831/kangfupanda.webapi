using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class FeedbackDao : BaseDao
    {
        public FeedbackDao(string _connStr) : base(_connStr)
        {
        }

        public bool AddFeedback(Feedback feedback)
        {
            if (feedback == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into feedback(openid, nickname, phone, productname, comment, createdAt,updatedAt) " +
                        "values(@openid, @nickname, @phone, @productname, @comment, now(),now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("openid", feedback.openId));
                    cmd.Parameters.Add(new MySqlParameter("nickname", feedback.nickName));
                    cmd.Parameters.Add(new MySqlParameter("phone", feedback.phone));
                    cmd.Parameters.Add(new MySqlParameter("productname", feedback.productName));
                    cmd.Parameters.Add(new MySqlParameter("comment", feedback.comment));
                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Feedback> GetFeedbacks(int pageIndex, int pageSize, string filter)
        {
            List<Feedback> feedbacks = new List<Feedback>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int skip = (pageIndex - 1) * pageSize;

                    MySqlCommand cmd = new MySqlCommand($"select * from feedback where expiredat is null order by createdat desc limit {skip},{pageSize}", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        feedbacks.Add(
                        new Feedback()
                        {
                            id = (int)reader["id"],
                            openId = reader["openId"] == DBNull.Value ? string.Empty : (string)reader["openId"],
                            nickName = reader["nickName"] == DBNull.Value ? string.Empty : (string)reader["nickName"],
                            phone = reader["phone"] == DBNull.Value ? string.Empty : (string)reader["phone"],
                            productName = reader["productName"] == DBNull.Value ? string.Empty : (string)reader["productName"],
                            comment = reader["comment"] == DBNull.Value ? string.Empty : (string)reader["comment"],
                            //createdAtStr = reader["createdat"] == DBNull.Value ? string.Empty : ((DateTime)reader["createdat"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        });
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return feedbacks;
        }

        public Int64 GetFeedbackCount(string filter)
        {

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from `feedback` where expiredat is null", conn);
                    var result = cmd.ExecuteScalar();
                    return (Int64)result;
                }
                finally
                {
                    conn.Close();
                }
            }

        }
    }
}
