using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class VideoDao
    {
        private string connStr;
        public VideoDao(string _connStr)
        {
            this.connStr = _connStr;
        }

        public bool AddVideo(Video video)
        {
            if (video == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into video(name, author, posterUri, videoUri, duration, createdAt, updatedAt) values(@name, @author, @posterUri, @videoUri, @duration, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("name", video.name));
                    cmd.Parameters.Add(new MySqlParameter("author", video.author));
                    cmd.Parameters.Add(new MySqlParameter("posterUri", video.posterUri));
                    cmd.Parameters.Add(new MySqlParameter("videoUri", video.videoUri));
                    cmd.Parameters.Add(new MySqlParameter("duration", video.duration));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public Video GetVideo(int id)
        {
            Video video = new Video();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from video where id=@id", conn);
                    cmd.Parameters.Add(new MySqlParameter("id", id));

                    var sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        video.id = (int)sqlReader["id"];
                        video.name = (string)sqlReader["name"];
                        video.author = (string)sqlReader["author"];
                        video.posterUri = (string)sqlReader["posterUri"];
                        video.duration = (int)sqlReader["duration"];
                        video.videoUri = (string)sqlReader["videoUri"];
                        video.createdAt = (DateTime)sqlReader["createdAt"];
                        video.updatedAt = (sqlReader["updatedAt"] == DBNull.Value) ? null : (DateTime?)sqlReader["updatedAt"];
                    }
                }
                finally {
                    conn.Close();
                }
            }

            return video;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">当前页数，从1开始</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<Video> GetList(int pageIndex = 1, int pageSize = 50)
        {
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            List<Video> videos = new List<Video>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from video where expiredAt is null limit @rowNumber, @pageSize", conn);
                    cmd.Parameters.Add(new MySqlParameter("rowNumber", (pageIndex - 1) * pageSize));
                    cmd.Parameters.Add(new MySqlParameter("pageSize", pageSize));

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Video video = new Video();
                        video.id = (int)sqlReader["id"];
                        video.name = (string)sqlReader["name"];
                        video.author = (string)sqlReader["author"];
                        video.posterUri = (string)sqlReader["posterUri"];
                        video.duration = (int)sqlReader["duration"];
                        video.videoUri = (string)sqlReader["videoUri"];
                        video.createdAt = (DateTime)sqlReader["createdAt"];
                        video.updatedAt = (sqlReader["updatedAt"] == DBNull.Value) ? null : (DateTime?)sqlReader["updatedAt"];

                        videos.Add(video);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return videos;
        }

        public bool DeleteById(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update video set expiredAt=now() where id=@id", conn);
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

        public bool EditVideo(Video video)
        {
            if (video == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update video set name=@name, author=@author, posterUri=@posterUri, videoUri=@videoUri, duration=@duration, updatedAt= @updatedAt where id = @id", conn);
                    cmd.Parameters.Add(new MySqlParameter("name", video.name));
                    cmd.Parameters.Add(new MySqlParameter("author", video.author));
                    cmd.Parameters.Add(new MySqlParameter("posterUri", video.posterUri));
                    cmd.Parameters.Add(new MySqlParameter("videoUri", video.videoUri));
                    cmd.Parameters.Add(new MySqlParameter("duration", video.duration));
                    cmd.Parameters.Add(new MySqlParameter("updatedAt", DateTime.Now));
                    cmd.Parameters.Add(new MySqlParameter("id", video.id));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }
    }
}
