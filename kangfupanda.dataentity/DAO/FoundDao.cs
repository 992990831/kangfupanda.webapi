using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class FoundDao : BaseDao
    {
        public FoundDao(string _connStr) : base(_connStr)
        {
        }

        public bool AddFound(Found found)
        {
            if (found == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into found(openId, name, nickName, headpic, city, note, detailimage, createdAt, updatedAt) values(@openId, @name, @nickName, @headpic, @city, @note, @detailimage, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", found.openId));
                    cmd.Parameters.Add(new MySqlParameter("name", found.name));
                    cmd.Parameters.Add(new MySqlParameter("nickName", found.nickName));
                    cmd.Parameters.Add(new MySqlParameter("city", found.city));
                    cmd.Parameters.Add(new MySqlParameter("headpic", found.headpic));
                    cmd.Parameters.Add(new MySqlParameter("note", found.note));
                    cmd.Parameters.Add(new MySqlParameter("detailimage", found.detailimage));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool Update(Found found)
        {
            if (found == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update found set name=@name, headpic=@headpic, detailimage=@detailimage, note=@note, updatedAt=now() where id=@id", conn);
                    cmd.Parameters.Add(new MySqlParameter("id", found.id));
                    cmd.Parameters.Add(new MySqlParameter("name", found.name));
                    cmd.Parameters.Add(new MySqlParameter("headpic", found.headpic));
                    cmd.Parameters.Add(new MySqlParameter("detailimage", found.detailimage));
                    cmd.Parameters.Add(new MySqlParameter("note", found.note));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool DeleteById(string id)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update found set expiredAt=now() where id=@id", conn);
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

        public List<Found> GetList(string filter, int pageIndex = 1, int pageSize = 10)
        {
            List<Found> founds = new List<Found>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int skip = (pageIndex - 1) * pageSize;
                    MySqlCommand cmd = new MySqlCommand($"select * from found where expiredat is null {filter} limit {skip},{pageSize}", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Found found = new Found();
                        found.id = (int)sqlReader["id"];
                        found.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        found.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        found.nickName = sqlReader["nickName"] == DBNull.Value ? string.Empty : (string)sqlReader["nickName"];
                        found.city = sqlReader["city"] == DBNull.Value ? string.Empty : (string)sqlReader["city"];
                        found.headpic = sqlReader["headpic"] == DBNull.Value ? string.Empty : (string)sqlReader["headpic"];
                        found.note = sqlReader["note"] == DBNull.Value ? string.Empty : (string)sqlReader["note"];
                        found.detailimage = sqlReader["detailimage"] == DBNull.Value ? string.Empty : (string)sqlReader["detailimage"];
                        found.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];

                        founds.Add(found);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return founds;
        }

        public long GetListCount(string filter)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from found where expiredat is null {filter}", conn);
                    count = (Int64)cmd.ExecuteScalar();
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
