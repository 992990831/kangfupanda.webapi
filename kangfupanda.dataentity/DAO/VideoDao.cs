﻿using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class VideoDao : BaseDao
    {
        public VideoDao(string _connStr) : base(_connStr)
        {
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
                    MySqlCommand cmd = new MySqlCommand("insert into video(name, author, posterUri, videoUri, duration, createdAt) values(@name, @author, @posterUri, @videoUri, @duration, now())", conn);
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
                    }
                }
                finally {
                    conn.Close();
                }
            }

            return video;
        }

        public List<Video> GetList(string filter="")
        {
            List<Video> videos = new List<Video>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from video where expiredAt is null" + filter, conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Video video = new Video();
                        video.id = (int)sqlReader["id"];
                        video.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        video.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        video.posterUri = sqlReader["posterUri"] == DBNull.Value ? string.Empty : (string)sqlReader["posterUri"];
                        video.duration = sqlReader["duration"] == DBNull.Value ? 0 : (int)sqlReader["duration"];
                        video.videoUri = sqlReader["videoUri"] == DBNull.Value ? string.Empty : (string)sqlReader["videoUri"];
                        video.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];

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
    }
}
