using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class ComplaintDao : BaseDao
    {
        public ComplaintDao(string _connStr) : base(_connStr)
        {
        }

        public bool AddComplaint(Complaint complaint)
        {
            if (complaint == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into complain(itemid, itemtype, title, phone, complain, status, createdAt,updatedAt) " +
                        "values(@itemid, @itemtype, @title, @phone, @complain, @status,now(),now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("itemid", complaint.itemId));
                    cmd.Parameters.Add(new MySqlParameter("itemtype", complaint.itemType));
                    cmd.Parameters.Add(new MySqlParameter("title", complaint.title));
                    cmd.Parameters.Add(new MySqlParameter("phone", complaint.phone));
                    cmd.Parameters.Add(new MySqlParameter("complain", complaint.complain));
                    cmd.Parameters.Add(new MySqlParameter("status", 0));
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
