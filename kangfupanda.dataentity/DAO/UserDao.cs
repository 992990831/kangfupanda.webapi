using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class UserDao
    {
        private string connStr;
        public UserDao(string _connStr)
        {
            this.connStr = _connStr;
        }

        public User GetUser(string openId)
        {
            User user = new User();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from user where openid=@openid and expiredat is null limit 1", conn);
                    cmd.Parameters.Add(new MySqlParameter("openid", openId));

                    var sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        user.id = (int)sqlReader["id"];
                        user.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        user.nickName = sqlReader["nickName"] == DBNull.Value ? string.Empty : (string)sqlReader["nickName"];
                        user.province = sqlReader["province"] == DBNull.Value ? string.Empty : (string)sqlReader["province"];
                        user.city = sqlReader["city"] == DBNull.Value ? string.Empty : (string)sqlReader["city"];
                        user.phone = sqlReader["phone"] == DBNull.Value ? string.Empty : (string)sqlReader["phone"];
                        user.sex = sqlReader["sex"] == DBNull.Value ? string.Empty : (string)sqlReader["sex"];
                        user.headpic = sqlReader["headpic"] == DBNull.Value ? string.Empty : (string)sqlReader["headpic"];
                        user.usertype = sqlReader["usertype"] == DBNull.Value ? string.Empty : (string)sqlReader["usertype"];
                        user.note = sqlReader["note"] == DBNull.Value ? string.Empty : (string)sqlReader["note"];
                        user.verified = sqlReader["verified"] == DBNull.Value ? false : ((ulong)sqlReader["verified"] == 1);
                        user.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return user;
        }

        public bool AddUser(User user)
        {
            if (user == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into user(openId, nickName, province, city, phone, sex, headpic, usertype, note, createdAt, updatedAt) values(@openId, @nickName, @province, @city, @phone, @sex, @headpic, @usertype, @note, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", user.openId));
                    cmd.Parameters.Add(new MySqlParameter("nickName", user.nickName));
                    cmd.Parameters.Add(new MySqlParameter("province", user.province));
                    cmd.Parameters.Add(new MySqlParameter("city", user.city));
                    cmd.Parameters.Add(new MySqlParameter("phone", user.phone));
                    cmd.Parameters.Add(new MySqlParameter("sex", user.sex));
                    cmd.Parameters.Add(new MySqlParameter("headpic", user.headpic));
                    cmd.Parameters.Add(new MySqlParameter("note", user.note));
                    cmd.Parameters.Add(new MySqlParameter("usertype", user.usertype));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool UpdateUser(User user)
        {
            if (user == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update user set nickName=@nickName, province=@province, city=@city, phone=@phone, sex=@sex, headpic=@headpic, usertype=@usertype, note=@note, updatedAt=now() where openId=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", user.openId));
                    cmd.Parameters.Add(new MySqlParameter("nickName", user.nickName));
                    cmd.Parameters.Add(new MySqlParameter("province", user.province));
                    cmd.Parameters.Add(new MySqlParameter("city", user.city));
                    cmd.Parameters.Add(new MySqlParameter("phone", user.phone));
                    cmd.Parameters.Add(new MySqlParameter("sex", user.sex));
                    cmd.Parameters.Add(new MySqlParameter("headpic", user.headpic));
                    cmd.Parameters.Add(new MySqlParameter("note", user.note));
                    cmd.Parameters.Add(new MySqlParameter("usertype", user.usertype));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public List<User> GetList() {
            var users = GetList(string.Empty);

            return users;
        }

        public List<User> GetList(string filter)
        {
            List<User> users = new List<User>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select * from user where expiredat is null {filter}", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        User user = new User();
                        user.id = (int)sqlReader["id"];
                        user.openId = (string)sqlReader["openId"];
                        user.nickName = sqlReader["nickName"] == DBNull.Value ? string.Empty : (string)sqlReader["nickName"];
                        user.province = sqlReader["province"] == DBNull.Value ? string.Empty : (string)sqlReader["province"];
                        user.city = sqlReader["city"] == DBNull.Value ? string.Empty : (string)sqlReader["city"];
                        user.phone = sqlReader["phone"] == DBNull.Value ? string.Empty : (string)sqlReader["phone"];
                        user.sex = sqlReader["sex"] == DBNull.Value ? string.Empty : (string)sqlReader["sex"];
                        user.headpic = sqlReader["headpic"] == DBNull.Value ? string.Empty : (string)sqlReader["headpic"];
                        user.usertype = sqlReader["usertype"] == DBNull.Value ? string.Empty : (string)sqlReader["usertype"];
                        user.note = sqlReader["note"] == DBNull.Value ? string.Empty : (string)sqlReader["note"];
                        user.verified = sqlReader["verified"] == DBNull.Value ? false : ((ulong)sqlReader["verified"] == 1);
                        user.displayinapp = sqlReader["displayinapp"] == DBNull.Value ? false : ((ulong)sqlReader["displayinapp"] == 1);
                        user.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];

                        users.Add(user);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return users;
        }

        public bool DeleteById(string openId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update user set expiredAt=now() where openId=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", openId));

                    cmd.ExecuteNonQuery();
                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public bool VerifyUser(string openId, bool isVerified)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update user set verified=@verify where openId=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", openId));
                    cmd.Parameters.Add(new MySqlParameter("verify", isVerified));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }


        public bool DisplayUser(string openId, bool isDisplay)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update user set displayinapp=@displayinapp where openId=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", openId));
                    cmd.Parameters.Add(new MySqlParameter("displayinapp", isDisplay));

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
