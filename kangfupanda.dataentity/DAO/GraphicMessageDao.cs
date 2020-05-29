using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class GraphicMessageDao : BaseDao
    {
        public GraphicMessageDao(string _connStr) : base(_connStr)
        {            
        }

        public bool AddGraphicMessage(GraphicMessage msg)
        {
            if (msg == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into graphicmessage(text, pic01, pic02, pic03, pic04, pic05, pic06, openid, author, createdAt, updatedAt) values(@text, @pic01, @pic02, @pic03, @pic04, @pic05, @pic06, @openid, @author, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("text", msg.text));
                    cmd.Parameters.Add(new MySqlParameter("pic01", msg.pic01));
                    cmd.Parameters.Add(new MySqlParameter("pic02", msg.pic02));
                    cmd.Parameters.Add(new MySqlParameter("pic03", msg.pic03));
                    cmd.Parameters.Add(new MySqlParameter("pic04", msg.pic04));
                    cmd.Parameters.Add(new MySqlParameter("pic05", msg.pic05));
                    cmd.Parameters.Add(new MySqlParameter("pic06", msg.pic06));
                    cmd.Parameters.Add(new MySqlParameter("openid", msg.openId));
                    cmd.Parameters.Add(new MySqlParameter("author", msg.author));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool DeleteById(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update graphicmessage set expiredAt=now() where id=@id", conn);
                    cmd.Parameters.Add(new MySqlParameter("id", id));

                    cmd.ExecuteNonQuery();
                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<GraphicMessage> GetList(string filter = "")
        {
            List<GraphicMessage> msgList = new List<GraphicMessage>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from graphicmessage where expiredAt is null" + filter, conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        GraphicMessage msg = new GraphicMessage();
                        msg.id = (int)sqlReader["id"];
                        msg.text = sqlReader["text"] == DBNull.Value ? string.Empty : (string)sqlReader["text"];
                        msg.pic01 = sqlReader["pic01"] == DBNull.Value ? string.Empty : (string)sqlReader["pic01"];
                        msg.pic02 = sqlReader["pic02"] == DBNull.Value ? string.Empty : (string)sqlReader["pic02"];
                        msg.pic03 = sqlReader["pic03"] == DBNull.Value ? string.Empty : (string)sqlReader["pic03"];
                        msg.pic04 = sqlReader["pic04"] == DBNull.Value ? string.Empty : (string)sqlReader["pic04"];
                        msg.pic05 = sqlReader["pic05"] == DBNull.Value ? string.Empty : (string)sqlReader["pic05"];
                        msg.pic06 = sqlReader["pic06"] == DBNull.Value ? string.Empty : (string)sqlReader["pic06"];
                        msg.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        msg.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        msg.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                        msgList.Add(msg);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return msgList;
        }
    }
}
