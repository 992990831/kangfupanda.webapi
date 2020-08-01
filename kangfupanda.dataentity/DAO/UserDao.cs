using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class UserDao : BaseDao
    {
        public UserDao(string _connStr) : base(_connStr)
        {
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
                        user.expertise = sqlReader["expertise"] == DBNull.Value ? string.Empty : (string)sqlReader["expertise"];
                        user.certificate = sqlReader["certificate"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate"];
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
                    MySqlCommand cmd = new MySqlCommand("insert into user(openId, nickName, province, city, phone, sex, headpic, usertype, note, expertise, detailimage, certificate, createdAt, updatedAt) values(@openId, @nickName, @province, @city, @phone, @sex, @headpic, @usertype, @note, @expertise, @detailimage, @certificate, now(), now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", user.openId));
                    cmd.Parameters.Add(new MySqlParameter("nickName", user.nickName));
                    cmd.Parameters.Add(new MySqlParameter("province", user.province));
                    cmd.Parameters.Add(new MySqlParameter("city", user.city));
                    cmd.Parameters.Add(new MySqlParameter("phone", user.phone));
                    cmd.Parameters.Add(new MySqlParameter("sex", user.sex));
                    cmd.Parameters.Add(new MySqlParameter("headpic", user.headpic));
                    cmd.Parameters.Add(new MySqlParameter("note", user.note));
                    cmd.Parameters.Add(new MySqlParameter("expertise", user.expertise));
                    cmd.Parameters.Add(new MySqlParameter("usertype", user.usertype));
                    cmd.Parameters.Add(new MySqlParameter("detailimage", user.detailimage));
                    cmd.Parameters.Add(new MySqlParameter("certificate", user.certificate));

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
                    MySqlCommand cmd = new MySqlCommand("update user set nickName=@nickName, province=@province, city=@city, phone=@phone, sex=@sex, headpic=@headpic, usertype=@usertype, note=@note, expertise=@expertise, detailimage=@detailimage, certificate=@certificate, updatedAt=now() where openId=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", user.openId));
                    cmd.Parameters.Add(new MySqlParameter("nickName", user.nickName));
                    cmd.Parameters.Add(new MySqlParameter("province", user.province));
                    cmd.Parameters.Add(new MySqlParameter("city", user.city));
                    cmd.Parameters.Add(new MySqlParameter("phone", user.phone));
                    cmd.Parameters.Add(new MySqlParameter("sex", user.sex));
                    cmd.Parameters.Add(new MySqlParameter("headpic", user.headpic));
                    cmd.Parameters.Add(new MySqlParameter("note", user.note));
                    cmd.Parameters.Add(new MySqlParameter("expertise", user.expertise));
                    cmd.Parameters.Add(new MySqlParameter("usertype", user.usertype));
                    cmd.Parameters.Add(new MySqlParameter("detailimage", user.detailimage));
                    cmd.Parameters.Add(new MySqlParameter("certificate", user.certificate));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool UpdateUserProfile(User user)
        {
            if (user == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update user set nickName=@nickName, city=@city, expertise=@expertise, note=@note, updatedAt=now() where openId=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", user.openId));
                    cmd.Parameters.Add(new MySqlParameter("nickName", user.nickName));
                    cmd.Parameters.Add(new MySqlParameter("city", user.city));
                    cmd.Parameters.Add(new MySqlParameter("expertise", user.expertise));
                    cmd.Parameters.Add(new MySqlParameter("note", user.note));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public List<User> GetList(int pageIndex = 1, int pageSize = 10) {
            var users = GetList(pageIndex, pageSize, string.Empty);

            return users;
        }

        public List<User> GetList(int pageIndex = 1, int pageSize = 10, string filter="", string orderBy="")
        {
            List<User> users = new List<User>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    int skip = (pageIndex - 1) * pageSize;

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select * from user where expiredat is null {filter} limit {skip},{pageSize} {orderBy}", conn);

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
                        user.expertise = sqlReader["expertise"] == DBNull.Value ? string.Empty : (string)sqlReader["expertise"];
                        user.verified = sqlReader["verified"] == DBNull.Value ? false : ((ulong)sqlReader["verified"] == 1);
                        user.displayinapp = sqlReader["displayinapp"] == DBNull.Value ? false : ((ulong)sqlReader["displayinapp"] == 1);
                        user.detailimage = sqlReader["detailimage"] == DBNull.Value ? string.Empty : (string)sqlReader["detailimage"];
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

        /// <summary>
        /// 按最后访问时间给用户排序
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<string> GetUserOpenIdListByApiLog(int pageIndex = 1, int pageSize = 10, string filter = "")
        {
            List<string> openIds = new List<string>();

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    int skip = (pageIndex - 1) * pageSize;

                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select u.openid, max(a.createdat) FROM demo.user as u left join demo.apilog as a on u.openid = a.openid"
                     + $" where 1=1 {filter} "
                     + " group by u.openid "
                     + " order by max(a.createdat) desc "
                     + $" limit {skip}, {pageSize}", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        openIds.Add(sqlReader["openid"].ToString());
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return openIds;
        }

        public Int64 GetListCount(string filter="")
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    var weekAgo = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from user where expiredat is null { filter }", conn);

                    var count = (Int64)cmd.ExecuteScalar();
                    return count;
                }
                finally
                {
                    conn.Close();
                }
            }
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

        /// <summary>
        /// 获取QR Code
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public string GetQRCode(string openId)
        {
            string qrcode = string.Empty;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select qrcode from user where openid=@openid and expiredat is null limit 1", conn);
                    cmd.Parameters.Add(new MySqlParameter("openid", openId));

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        qrcode = result.ToString();
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return qrcode;
        }

        public void UpdateQRCode(string qrCode, string openId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update user set qrcode=@qrCode where openId=@openId", conn);
                    cmd.Parameters.Add(new MySqlParameter("openId", openId));
                    cmd.Parameters.Add(new MySqlParameter("qrCode", qrCode));

                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
