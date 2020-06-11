using kangfupanda.dataentity.Model;
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
                    MySqlCommand cmd = new MySqlCommand("insert into video(name, author, openId, posterUri, videoUri, duration, createdAt) values(@name, @author, @openId, @posterUri, @videoUri, @duration, now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("name", video.name));
                    cmd.Parameters.Add(new MySqlParameter("author", video.author));
                    cmd.Parameters.Add(new MySqlParameter("openId", video.openId));
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
                        video.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        video.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        video.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        video.posterUri = sqlReader["posterUri"] == DBNull.Value ? string.Empty : (string)sqlReader["posterUri"];
                        video.duration = sqlReader["duration"] == DBNull.Value ? 0 : (int)sqlReader["duration"];
                        video.videoUri = sqlReader["videoUri"] == DBNull.Value ? string.Empty : (string)sqlReader["videoUri"];
                        video.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
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
                        video.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
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

        public List<VideoExt> GetListExt(string filter = "")
        {
            List<VideoExt> videosExt = new List<VideoExt>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select v.*, u.headpic, (select count(1) from `like` where itemId=v.id and itemType='video' and expiredAt is null ) as likeCount, (select count(1) from `comments` where comment_post_id=v.id and comment_post_type='video' and comment_audit_status=1 and expiredAt is null ) as commentCount from video as v left join `user` as u on v.openId=u.openId  where v.expiredAt is null " + filter, conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        VideoExt videoExt = new VideoExt();
                        videoExt.id = (int)sqlReader["id"];
                        videoExt.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        videoExt.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        videoExt.authorHeadPic = sqlReader["headpic"] == DBNull.Value ? string.Empty : (string)sqlReader["headpic"];
                        videoExt.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        videoExt.posterUri = sqlReader["posterUri"] == DBNull.Value ? string.Empty : (string)sqlReader["posterUri"];
                        videoExt.duration = sqlReader["duration"] == DBNull.Value ? 0 : (int)sqlReader["duration"];
                        videoExt.videoUri = sqlReader["videoUri"] == DBNull.Value ? string.Empty : (string)sqlReader["videoUri"];
                        videoExt.likeCount = sqlReader["likeCount"] == DBNull.Value ? 0 : (long)sqlReader["likeCount"];
                        videoExt.commentCount = sqlReader["commentCount"] == DBNull.Value ? 0 : (long)sqlReader["commentCount"];
                        videoExt.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];

                        videosExt.Add(videoExt);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return videosExt;
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
