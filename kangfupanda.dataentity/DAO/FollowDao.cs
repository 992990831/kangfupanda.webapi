﻿using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class FollowDao : BaseDao
    {
        public FollowDao(string _connStr) : base(_connStr)
        {
        }

        public bool Add(Follow follow)
        {
            if (follow == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into `follow`(followee, follower, createdAt, updatedAt) values(@followee, @follower, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("followee", follow.followee));
                    cmd.Parameters.Add(new MySqlParameter("follower", follow.follower));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool GetFollowed(string followeeOpenId, string followerOpenId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select count(1) from `follow` where followee=@followee and follower=@follower and expiredat is null", conn);
                    cmd.Parameters.Add(new MySqlParameter("followee", followeeOpenId));
                    cmd.Parameters.Add(new MySqlParameter("follower", followerOpenId));

                    var count = (Int64)cmd.ExecuteScalar();

                    if (count > 0)
                        return true;
                }
                finally
                {
                    conn.Close();
                }
            }

            return false;
        }

        public bool UnFollow(int followId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update follow set expiredAt=now() where id=@id", conn);
                    cmd.Parameters.Add(new MySqlParameter("id", followId));

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
        /// 获取被关注人列表
        /// </summary>
        /// <param name="followerOpenId"></param>
        /// <returns></returns>
        public List<string> GetFolloweesList(string followerOpenId)
        {
            List<string> followers = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select distinct(followee) from `follow` where follower=@follower and expiredat is null", conn);
                    cmd.Parameters.Add(new MySqlParameter("follower", followerOpenId));
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        followers.Add(reader["followee"].ToString());
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return followers;
        }

        /// <summary>
        /// 获取粉丝数 (关注我的人)
        /// </summary>
        /// <param name="followeeOpenId"></param>
        /// <returns></returns>
        public Int64 GetFollowerCount(string followeeOpenId)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from `follow` where expiredat is null and followee=@followeeOpenId", conn);
                    cmd.Parameters.Add(new MySqlParameter("followeeOpenId", followeeOpenId));

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

        /// <summary>
        /// 获取关注数 (我关注的人)
        /// </summary>
        /// <param name="followerOpenId"></param>
        /// <returns></returns>
        public Int64 GetFolloweeCount(string followerOpenId)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from `follow` where expiredat is null and follower=@followerOpenId", conn);
                    cmd.Parameters.Add(new MySqlParameter("followerOpenId", followerOpenId));

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

        /// <summary>
        /// 后台使用，获取全部
        /// </summary>
        /// <param name="followerOpenId"></param>
        /// <returns></returns>
        public List<FollowEx> GetFollows(int pageIndex, int pageSize, string filter)
        {
            List<FollowEx> followers = new List<FollowEx>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int skip = (pageIndex - 1) * pageSize;

                    MySqlCommand cmd = new MySqlCommand($"select f.*, u1.nickname as followeeName, u2.nickname as followerName from `follow` as f left join `user` as u1 on f.followee=u1.openid left join `user` as u2 on f.follower=u2.openid where f.expiredat is null and u1.expiredat is null and u2.expiredat is null {filter} order by f.createdat desc limit {skip},{pageSize}", conn);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        followers.Add(
                        new FollowEx()
                        {
                            id = (int)reader["id"],
                            followee = reader["followee"] == DBNull.Value ? string.Empty : (string)reader["followee"],
                            follower = reader["followee"] == DBNull.Value ? string.Empty : (string)reader["followee"],
                            followeeName = reader["followeeName"] == DBNull.Value ? string.Empty : (string)reader["followeeName"],
                            followerName = reader["followerName"] == DBNull.Value ? string.Empty : (string)reader["followerName"],
                            createdAtStr = reader["createdat"] == DBNull.Value ? string.Empty : ((DateTime)reader["createdat"]).ToString("yyyy-MM-dd HH:mm:ss"),
                        });
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return followers;
        }

        public Int64 GetFollowsCount(string filter)
        {
            List<FollowEx> followers = new List<FollowEx>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from `follow` as f left join `user` as u1 on f.followee=u1.openid left join `user` as u2 on f.follower=u2.openid where  f.expiredat is null and u1.expiredat is null and u2.expiredat is null {filter} order by follower desc", conn);
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
