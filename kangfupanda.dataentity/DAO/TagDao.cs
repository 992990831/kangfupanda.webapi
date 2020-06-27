using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class TagDao : BaseDao
    {
        public TagDao(string _connStr) : base(_connStr)
        {

        }

        public bool AddTag(Tag tag)
        {
            if (tag == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into tag(text, createdAt, updatedAt) values(@text, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("text", tag.text));
                   
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
                    MySqlCommand cmd = new MySqlCommand("update tag set expiredAt=now() where id=@id", conn);
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

        public bool Update(Tag tag)
        {
            if (tag == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update tag set text=@text, updatedAt=now() where id=@id", conn);
                    cmd.Parameters.Add(new MySqlParameter("id", tag.id));
                    cmd.Parameters.Add(new MySqlParameter("text", tag.text));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<Tag> GetList(string filter, int pageIndex = 1, int pageSize = 10)
        {
            List<Tag> tags = new List<Tag>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int skip = (pageIndex - 1) * pageSize;
                    MySqlCommand cmd = new MySqlCommand($"select * from tag where expiredat is null {filter} limit {skip},{pageSize}", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Tag tag = new Tag();
                        tag.id = (int)sqlReader["id"];
                        tag.text = (string)sqlReader["text"];
                        tag.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];

                        tags.Add(tag);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return tags;
        }

        public long GetListCount(string filter)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from tag where expiredat is null {filter}", conn);
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
