﻿using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace kangfupanda.dataentity.DAO
{
    public class ApiLogDao : BaseDao
    {
        public ApiLogDao(string _connStr) : base(_connStr)
        {
        }

        public bool Add(ApiLog log)
        {
            if (log == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into apilog(openid, nickname, ip, endpoint, client, createdAt) values(@openid, @nickname, @ip, @endpoint, @client, now())", conn);
                    cmd.Parameters.Add(new MySqlParameter("openid", log.openId));
                    cmd.Parameters.Add(new MySqlParameter("nickname", log.nickName));
                    cmd.Parameters.Add(new MySqlParameter("ip", log.ip));
                    cmd.Parameters.Add(new MySqlParameter("endpoint", log.endpoint));
                    cmd.Parameters.Add(new MySqlParameter("client", log.client));

                    cmd.ExecuteNonQuery();

                    return true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public VisitLog GetByOpenId(string openId)
        {
            VisitLog log = new VisitLog();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    var weekAgo = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT openid, nickname, max(createdat) as lastVisitedAt, count(1) as count"
                    + " FROM demo.apilog"
                    + $" where createdat > '{weekAgo}' and openid='{openId}'", conn);

                    var sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        log.openId = sqlReader["openid"] == DBNull.Value ? string.Empty : (string)sqlReader["openid"];
                        log.nickName = sqlReader["nickname"] == DBNull.Value ? string.Empty : (string)sqlReader["nickname"];
                        log.lastVisitedAt = sqlReader["lastVisitedAt"] == DBNull.Value ? string.Empty : ((DateTime)sqlReader["lastVisitedAt"]).ToString("yyyy-MM-dd HH:mm:ss");
                        log.visitCountLastWeek = sqlReader["count"] == DBNull.Value ? 0 : (long)sqlReader["count"];
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return log;
        }

        public List<VisitLog> GetList(int pageIndex = 1, int pageSize = 10)
        {
            List<VisitLog> logs = new List<VisitLog>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    int skip = (pageIndex - 1) * pageSize;
                    //MySqlCommand cmd = new MySqlCommand($"SELECT openid, nickname, max(createdat) as lastVisitedAt, count(1) as count"
                    MySqlCommand cmd = new MySqlCommand($"SELECT openid, nickname, max(createdat) as lastVisitedAt "
                    + " FROM demo.apilog"
                    + " where openid is not null group by openid, nickname"
                    + " order by max(createdat) desc "
                    + $" limit { skip},{pageSize} ", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        VisitLog log = new VisitLog();
                        log.openId = sqlReader["openid"] == DBNull.Value ? string.Empty : (string)sqlReader["openid"];
                        log.nickName = sqlReader["nickname"] == DBNull.Value ? string.Empty : (string)sqlReader["nickname"];
                        log.lastVisitedAt = sqlReader["lastVisitedAt"] == DBNull.Value ? string.Empty : ((DateTime)sqlReader["lastVisitedAt"]).ToString("yyyy-MM-dd HH:mm:ss");
                        //log.visitCountLastWeek = sqlReader["count"] == DBNull.Value ? 0 : (long)sqlReader["count"];

                        log.nickName = HttpUtility.UrlDecode(log.nickName);
                        logs.Add(log);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return logs;
        }

        public Int64 GetLastWeekCount(string openId)
        {
            Int64 count = 0;
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    var weekAgo = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    conn.Open();

                    //MySqlCommand cmd = new MySqlCommand($"SELECT openid, nickname, max(createdat) as lastVisitedAt, count(1) as count"
                    MySqlCommand cmd = new MySqlCommand($"SELECT count(1) as count"
                    + " FROM demo.apilog"
                    + $" where createdat > '{weekAgo}' and openid='{openId}'", conn);

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

        public string GetLastVisitedTime(string openId)
        {
            string lastVisitedAt = string.Empty;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    //MySqlCommand cmd = new MySqlCommand($"SELECT openid, nickname, max(createdat) as lastVisitedAt, count(1) as count"
                    MySqlCommand cmd = new MySqlCommand($"SELECT createdat as lastVisitedAt"
                    + " FROM demo.apilog"
                    + $" where openid = '{openId}' order by createdat desc  limit 1", conn);

                    var result = cmd.ExecuteScalar();

                    if(result!=null)
                    {
                        lastVisitedAt = ((DateTime)result).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                finally
                {
                    conn.Close();
                }

                return lastVisitedAt;
            }
        }

        public Int64 GetListCount()
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    //var weekAgo = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"SELECT count(1) from (select distinct openid, nickname"
                    + " FROM demo.apilog"
                    //+ $" where createdat > '{weekAgo}' and openid is not null) as a", conn);                
                    + $" where openid is not null) as a", conn);

                    var count = (Int64)cmd.ExecuteScalar();
                    return count;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
