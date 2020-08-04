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
                        user.certificate1 = sqlReader["certificate1"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate1"];
                        user.certificate2 = sqlReader["certificate2"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate2"];
                        user.certificate3 = sqlReader["certificate3"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate3"];
                        user.certificate4 = sqlReader["certificate4"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate4"];
                        user.certificate5 = sqlReader["certificate5"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate5"];
                        user.certificate6 = sqlReader["certificate6"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate6"];
                        user.certificate7 = sqlReader["certificate7"] == DBNull.Value ? string.Empty : (string)sqlReader["certificate7"];
                        user.certText = sqlReader["certtext"] == DBNull.Value ? string.Empty : (string)sqlReader["certtext"];
                        user.cert1Text = sqlReader["cert1text"] == DBNull.Value ? string.Empty : (string)sqlReader["cert1text"];
                        user.cert2Text = sqlReader["cert2text"] == DBNull.Value ? string.Empty : (string)sqlReader["cert2text"];
                        user.cert3Text = sqlReader["cert3text"] == DBNull.Value ? string.Empty : (string)sqlReader["cert3text"];
                        user.cert4Text = sqlReader["cert4text"] == DBNull.Value ? string.Empty : (string)sqlReader["cert4text"];
                        user.cert5Text = sqlReader["cert5text"] == DBNull.Value ? string.Empty : (string)sqlReader["cert5text"];
                        user.cert6Text = sqlReader["cert6text"] == DBNull.Value ? string.Empty : (string)sqlReader["cert6text"];
                        user.cert7Text = sqlReader["cert7text"] == DBNull.Value ? string.Empty : (string)sqlReader["cert7text"];

                        user.profilevideo = sqlReader["profilevideo"] == DBNull.Value ? string.Empty : (string)sqlReader["profilevideo"];
                        user.profilevideoposter = sqlReader["profilevideoposter"] == DBNull.Value ? string.Empty : (string)sqlReader["profilevideoposter"];

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
                    MySqlCommand cmd = new MySqlCommand("insert into user(openId, nickName, province, city, phone, sex, headpic, usertype, note, expertise, detailimage, certificate, certtext, certificate1, cert1text, certificate2, cert2text, certificate3, cert3text, certificate4, cert4text, certificate5, cert5text, certificate6, cert6text, certificate7, cert7text, profilevideo, profilevideoposter,  createdAt, updatedAt) values(@openId, @nickName, @province, @city, @phone, @sex, @headpic, @usertype, @note, @expertise, @detailimage, @certificate, @certtext, @certificate1, @cert1text, @certificate2, @cert2text, @certificate3, @cert3text, @certificate4, @cert4text, @certificate5, @cert5text, @certificate6, @cert6text, @certificate7, @cert7text, profilevideo, profilevideoposter, now(), now())", conn);
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
                    cmd.Parameters.Add(new MySqlParameter("certtext", user.certText));
                    cmd.Parameters.Add(new MySqlParameter("certificate1", user.certificate1));
                    cmd.Parameters.Add(new MySqlParameter("cert1text", user.cert1Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate2", user.certificate2));
                    cmd.Parameters.Add(new MySqlParameter("cert2text", user.cert2Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate3", user.certificate3));
                    cmd.Parameters.Add(new MySqlParameter("cert3text", user.cert3Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate4", user.certificate4));
                    cmd.Parameters.Add(new MySqlParameter("cert4text", user.cert4Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate5", user.certificate5));
                    cmd.Parameters.Add(new MySqlParameter("cert5text", user.cert5Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate6", user.certificate6));
                    cmd.Parameters.Add(new MySqlParameter("cert6text", user.cert6Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate7", user.certificate7));
                    cmd.Parameters.Add(new MySqlParameter("cert7text", user.cert7Text));

                    cmd.Parameters.Add(new MySqlParameter("profilevideo", user.profilevideo));
                    cmd.Parameters.Add(new MySqlParameter("profilevideoposter", user.profilevideoposter));

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
                    MySqlCommand cmd = new MySqlCommand("update user set nickName=@nickName, province=@province, city=@city, phone=@phone, sex=@sex, headpic=@headpic, usertype=@usertype, note=@note, expertise=@expertise, detailimage=@detailimage, certificate=@certificate, certtext=@certtext, certificate1=@certificate1, cert1text=@cert1text,certificate2=@certificate2, cert2text=@cert2text,certificate3=@certificate3, cert3text=@cert3text, certificate4=@certificate4, cert4text=@cert4text, certificate5=@certificate5, cert5text=@cert5text, certificate6=@certificate6,  cert6text=@cert6text, certificate7=@certificate7, cert7text=@cert7text, profilevideo=@profilevideo, profilevideoposter=@profilevideoposter, updatedAt=now() where openId=@openId", conn);
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
                    cmd.Parameters.Add(new MySqlParameter("certtext", user.certText));
                    cmd.Parameters.Add(new MySqlParameter("certificate1", user.certificate1));
                    cmd.Parameters.Add(new MySqlParameter("cert1text", user.cert1Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate2", user.certificate2));
                    cmd.Parameters.Add(new MySqlParameter("cert2text", user.cert2Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate3", user.certificate3));
                    cmd.Parameters.Add(new MySqlParameter("cert3text", user.cert3Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate4", user.certificate4));
                    cmd.Parameters.Add(new MySqlParameter("cert4text", user.cert4Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate5", user.certificate5));
                    cmd.Parameters.Add(new MySqlParameter("cert5text", user.cert5Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate6", user.certificate6));
                    cmd.Parameters.Add(new MySqlParameter("cert6text", user.cert6Text));
                    cmd.Parameters.Add(new MySqlParameter("certificate7", user.certificate7));
                    cmd.Parameters.Add(new MySqlParameter("cert7text", user.cert7Text));

                    cmd.Parameters.Add(new MySqlParameter("profilevideo", user.profilevideo));
                    cmd.Parameters.Add(new MySqlParameter("profilevideoposter", user.profilevideoposter));

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
