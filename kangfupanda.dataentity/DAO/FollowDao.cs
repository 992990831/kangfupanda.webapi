using kangfupanda.dataentity.Model;
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
                    
                    if(count > 0)
                        return true;
                }
                finally
                {
                    conn.Close();
                }
            }

            return false;
        }
    }
}
