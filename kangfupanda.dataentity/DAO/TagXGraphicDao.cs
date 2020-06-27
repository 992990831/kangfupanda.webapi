using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class TagXGraphicDao : BaseDao
    {
        public TagXGraphicDao(string _connStr) : base(_connStr)
        {

        }

        public bool AddTagXGraphic(TagXGraphic tag)
        {
            if (tag == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into tagxgraphic(tagid, tagtext, graphicid, createdAt, updatedAt) values(@tagid, @tagtext, @graphicid, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("tagid", tag.tagid));
                    cmd.Parameters.Add(new MySqlParameter("tagtext", tag.tagtext));
                    cmd.Parameters.Add(new MySqlParameter("graphicid", tag.graphicid));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public List<TagXGraphic> GetList(int graphicId)
        {
            List<TagXGraphic> tags = new List<TagXGraphic>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select * from tagxgraphic where expiredat is null order by createdat asc", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        TagXGraphic tag = new TagXGraphic();
                        tag.id = (int)sqlReader["id"];
                        tag.tagtext = (string)sqlReader["tagtext"];
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
    }
}
