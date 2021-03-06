﻿using kangfupanda.dataentity.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace kangfupanda.dataentity.DAO
{
    public class GraphicMessageDao : BaseDao
    {
        public GraphicMessageDao(string _connStr) : base(_connStr)
        {            
        }

        public ulong AddGraphicMessage(GraphicMessage msg)
        {
            if (msg == null)
                return 0;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into graphicmessage(name, text, poster, pic01, pic02, pic03, pic04, pic05, pic06, audio01, audio02, audio03, openid, author, wechatMediaId, wechatUrl, createdAt, updatedAt) values(@name, @text, @poster, @pic01, @pic02, @pic03, @pic04, @pic05, @pic06, @audio01, @audio02, @audio03, @openid, @author, @wechatMediaId, @wechatUrl, now(), now()); select @@identity;", conn);
                    cmd.Parameters.Add(new MySqlParameter("name", msg.name));
                    cmd.Parameters.Add(new MySqlParameter("text", msg.text));
                    cmd.Parameters.Add(new MySqlParameter("poster", msg.poster));
                    cmd.Parameters.Add(new MySqlParameter("pic01", msg.pic01));
                    cmd.Parameters.Add(new MySqlParameter("pic02", msg.pic02));
                    cmd.Parameters.Add(new MySqlParameter("pic03", msg.pic03));
                    cmd.Parameters.Add(new MySqlParameter("pic04", msg.pic04));
                    cmd.Parameters.Add(new MySqlParameter("pic05", msg.pic05));
                    cmd.Parameters.Add(new MySqlParameter("pic06", msg.pic06));
                    cmd.Parameters.Add(new MySqlParameter("audio01", msg.audio01));
                    cmd.Parameters.Add(new MySqlParameter("audio02", msg.audio02));
                    cmd.Parameters.Add(new MySqlParameter("audio03", msg.audio03));
                    cmd.Parameters.Add(new MySqlParameter("openid", msg.openId));
                    cmd.Parameters.Add(new MySqlParameter("author", msg.author));
                    cmd.Parameters.Add(new MySqlParameter("wechatMediaId", msg.wechatMediaId));
                    cmd.Parameters.Add(new MySqlParameter("wechatUrl", msg.wechatUrl));

                    ulong id = (ulong)cmd.ExecuteScalar();

                    return id;
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        public bool UpdateGraphicMessage(GraphicMessage msg)
        {
            if (msg == null)
                return false;

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update graphicmessage set name=@name, text=@text, poster=@poster, pic01=@pic01, pic02=@pic02, pic03=@pic03, pic04=@pic04, pic05=@pic05, pic06=@pic06, audio01=@audio01, audio02=@audio02, audio03=@audio03, openid=@openid, author=@author, updatedAt=now() where id=@id", conn);
                    cmd.Parameters.Add(new MySqlParameter("id", msg.id));
                    cmd.Parameters.Add(new MySqlParameter("name", msg.name));
                    cmd.Parameters.Add(new MySqlParameter("text", msg.text));
                    cmd.Parameters.Add(new MySqlParameter("poster", msg.poster));
                    cmd.Parameters.Add(new MySqlParameter("pic01", msg.pic01));
                    cmd.Parameters.Add(new MySqlParameter("pic02", msg.pic02));
                    cmd.Parameters.Add(new MySqlParameter("pic03", msg.pic03));
                    cmd.Parameters.Add(new MySqlParameter("pic04", msg.pic04));
                    cmd.Parameters.Add(new MySqlParameter("pic05", msg.pic05));
                    cmd.Parameters.Add(new MySqlParameter("pic06", msg.pic06));
                    cmd.Parameters.Add(new MySqlParameter("audio01", msg.audio01));
                    cmd.Parameters.Add(new MySqlParameter("audio02", msg.audio02));
                    cmd.Parameters.Add(new MySqlParameter("audio03", msg.audio03));
                    cmd.Parameters.Add(new MySqlParameter("openid", msg.openId));
                    cmd.Parameters.Add(new MySqlParameter("author", msg.author));

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
        /// 置顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool SetTop(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update graphicmessage set isTop=1 where id=@id", conn);
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

        /// <summary>
        /// 取消置顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UnsetTop(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update graphicmessage set isTop=0 where id=@id", conn);
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

        public bool DeleteById(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("update graphicmessage set expiredAt=now() where id=@id", conn);
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

        public List<GraphicMessage> GetList(string filter = "")
        {
            List<GraphicMessage> msgList = new List<GraphicMessage>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("select * from graphicmessage where expiredAt is null" + filter, conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        GraphicMessage msg = new GraphicMessage();
                        msg.id = (int)sqlReader["id"];
                        msg.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        msg.text = sqlReader["text"] == DBNull.Value ? string.Empty : (string)sqlReader["text"];
                        msg.poster = sqlReader["poster"] == DBNull.Value ? string.Empty : (string)sqlReader["poster"];
                        msg.pic01 = sqlReader["pic01"] == DBNull.Value ? string.Empty : (string)sqlReader["pic01"];
                        msg.pic02 = sqlReader["pic02"] == DBNull.Value ? string.Empty : (string)sqlReader["pic02"];
                        msg.pic03 = sqlReader["pic03"] == DBNull.Value ? string.Empty : (string)sqlReader["pic03"];
                        msg.pic04 = sqlReader["pic04"] == DBNull.Value ? string.Empty : (string)sqlReader["pic04"];
                        msg.pic05 = sqlReader["pic05"] == DBNull.Value ? string.Empty : (string)sqlReader["pic05"];
                        msg.pic06 = sqlReader["pic06"] == DBNull.Value ? string.Empty : (string)sqlReader["pic06"];
                        msg.audio01 = sqlReader["audio01"] == DBNull.Value ? string.Empty : (string)sqlReader["audio01"];
                        msg.audio02 = sqlReader["audio02"] == DBNull.Value ? string.Empty : (string)sqlReader["audio02"];
                        msg.audio03 = sqlReader["audio03"] == DBNull.Value ? string.Empty : (string)sqlReader["audio03"];
                        msg.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        msg.isTop = sqlReader["isTop"] == DBNull.Value ? false : (UInt64)sqlReader["isTop"] == 1;
                        msg.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        msg.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                        msgList.Add(msg);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return msgList;
        }

        public GraphicMessageExt GetById(string postId)
        {
            GraphicMessageExt msg = new GraphicMessageExt();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select g.*, u.headpic, (select count(1) from `like` where itemId=g.id and itemType='graphic' and expiredAt is null ) as likeCount,  (select count(1) from `comments` where comment_post_id=g.id and comment_post_type='graphic' and comment_audit_status=1 and expiredAt is null ) as commentCount from graphicmessage as g left join `user` as u on g.openId=u.openId  where g.expiredAt is null and g.id={postId} ", conn);

                    var sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        msg.id = (int)sqlReader["id"];
                        msg.postId = msg.id.ToString();
                        msg.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        msg.text = sqlReader["text"] == DBNull.Value ? string.Empty : (string)sqlReader["text"];
                        msg.poster = sqlReader["poster"] == DBNull.Value ? string.Empty : (string)sqlReader["poster"];
                        msg.pic01 = sqlReader["pic01"] == DBNull.Value ? string.Empty : (string)sqlReader["pic01"];
                        msg.pic02 = sqlReader["pic02"] == DBNull.Value ? string.Empty : (string)sqlReader["pic02"];
                        msg.pic03 = sqlReader["pic03"] == DBNull.Value ? string.Empty : (string)sqlReader["pic03"];
                        msg.pic04 = sqlReader["pic04"] == DBNull.Value ? string.Empty : (string)sqlReader["pic04"];
                        msg.pic05 = sqlReader["pic05"] == DBNull.Value ? string.Empty : (string)sqlReader["pic05"];
                        msg.pic06 = sqlReader["pic06"] == DBNull.Value ? string.Empty : (string)sqlReader["pic06"];
                        msg.audio01 = sqlReader["audio01"] == DBNull.Value ? string.Empty : (string)sqlReader["audio01"];
                        msg.audio02 = sqlReader["audio02"] == DBNull.Value ? string.Empty : (string)sqlReader["audio02"];
                        msg.audio03 = sqlReader["audio03"] == DBNull.Value ? string.Empty : (string)sqlReader["audio03"];
                        msg.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        msg.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        msg.authorHeadPic = sqlReader["headpic"] == DBNull.Value ? string.Empty : (string)sqlReader["headpic"];
                        msg.likeCount = sqlReader["likeCount"] == DBNull.Value ? 0 : (long)sqlReader["likeCount"];
                        msg.commentCount = sqlReader["commentCount"] == DBNull.Value ? 0 : (long)sqlReader["commentCount"];
                        msg.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return msg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="count"></param>
        /// <param name="endId">因为是按时间倒序，所以在SQL里面用小于</param>
        /// <returns></returns>
        public List<GraphicMessageExt> GetListExt(string filter = "", int count=10, int endId=0)
        {
            List<GraphicMessageExt> msgListExt = new List<GraphicMessageExt>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select g.*, u.headpic, (select count(1) from `like` where itemId=g.id and itemType='graphic' and expiredAt is null ) as likeCount,  (select count(1) from `comments` where comment_post_id=g.id and comment_post_type='graphic' and comment_audit_status=1 and expiredAt is null ) as commentCount from graphicmessage as g left join `user` as u on g.openId=u.openId  where g.expiredAt is null and g.id<{endId} {filter} order by g.id desc limit {count} ", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        GraphicMessageExt msg = new GraphicMessageExt();
                        msg.id = (int)sqlReader["id"];
                        msg.postId = msg.id.ToString();
                        msg.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        msg.text = sqlReader["text"] == DBNull.Value ? string.Empty : (string)sqlReader["text"];
                        msg.poster = sqlReader["poster"] == DBNull.Value ? string.Empty : (string)sqlReader["poster"];
                        msg.pic01 = sqlReader["pic01"] == DBNull.Value ? string.Empty : (string)sqlReader["pic01"];
                        msg.pic02 = sqlReader["pic02"] == DBNull.Value ? string.Empty : (string)sqlReader["pic02"];
                        msg.pic03 = sqlReader["pic03"] == DBNull.Value ? string.Empty : (string)sqlReader["pic03"];
                        msg.pic04 = sqlReader["pic04"] == DBNull.Value ? string.Empty : (string)sqlReader["pic04"];
                        msg.pic05 = sqlReader["pic05"] == DBNull.Value ? string.Empty : (string)sqlReader["pic05"];
                        msg.pic06 = sqlReader["pic06"] == DBNull.Value ? string.Empty : (string)sqlReader["pic06"];
                        msg.audio01 = sqlReader["audio01"] == DBNull.Value ? string.Empty : (string)sqlReader["audio01"];
                        msg.audio02 = sqlReader["audio02"] == DBNull.Value ? string.Empty : (string)sqlReader["audio02"];
                        msg.audio03 = sqlReader["audio03"] == DBNull.Value ? string.Empty : (string)sqlReader["audio03"];
                        msg.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        msg.isTop = sqlReader["isTop"] == DBNull.Value ? false : (UInt64)sqlReader["isTop"] == 1;
                        msg.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        msg.wechatUrl = sqlReader["wechatUrl"] == DBNull.Value ? string.Empty : (string)sqlReader["wechatUrl"];
                        msg.authorHeadPic = sqlReader["headpic"] == DBNull.Value ? string.Empty : (string)sqlReader["headpic"];
                        msg.likeCount = sqlReader["likeCount"] == DBNull.Value ? 0 : (long)sqlReader["likeCount"];
                        msg.commentCount = sqlReader["commentCount"] == DBNull.Value ? 0 : (long)sqlReader["commentCount"];
                        msg.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                        msgListExt.Add(msg);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return msgListExt;
        }

        public List<GraphicMessageExt> GetTopExt(string filter)
        {
            List<GraphicMessageExt> msgListExt = new List<GraphicMessageExt>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select g.*, u.headpic, (select count(1) from `like` where itemId=g.id and itemType='graphic' and expiredAt is null ) as likeCount,  (select count(1) from `comments` where comment_post_id=g.id and comment_post_type='graphic' and comment_audit_status=1 and expiredAt is null ) as commentCount from graphicmessage as g left join `user` as u on g.openId=u.openId  where g.expiredAt is null and g.isTop=1 {filter} order by g.isTop desc, g.id desc ", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        GraphicMessageExt msg = new GraphicMessageExt();
                        msg.id = (int)sqlReader["id"];
                        msg.postId = msg.id.ToString();
                        msg.name = sqlReader["name"] == DBNull.Value ? string.Empty : (string)sqlReader["name"];
                        msg.text = sqlReader["text"] == DBNull.Value ? string.Empty : (string)sqlReader["text"];
                        msg.poster = sqlReader["poster"] == DBNull.Value ? string.Empty : (string)sqlReader["poster"];
                        msg.pic01 = sqlReader["pic01"] == DBNull.Value ? string.Empty : (string)sqlReader["pic01"];
                        msg.pic02 = sqlReader["pic02"] == DBNull.Value ? string.Empty : (string)sqlReader["pic02"];
                        msg.pic03 = sqlReader["pic03"] == DBNull.Value ? string.Empty : (string)sqlReader["pic03"];
                        msg.pic04 = sqlReader["pic04"] == DBNull.Value ? string.Empty : (string)sqlReader["pic04"];
                        msg.pic05 = sqlReader["pic05"] == DBNull.Value ? string.Empty : (string)sqlReader["pic05"];
                        msg.pic06 = sqlReader["pic06"] == DBNull.Value ? string.Empty : (string)sqlReader["pic06"];
                        msg.audio01 = sqlReader["audio01"] == DBNull.Value ? string.Empty : (string)sqlReader["audio01"];
                        msg.audio02 = sqlReader["audio02"] == DBNull.Value ? string.Empty : (string)sqlReader["audio02"];
                        msg.audio03 = sqlReader["audio03"] == DBNull.Value ? string.Empty : (string)sqlReader["audio03"];
                        msg.openId = sqlReader["openId"] == DBNull.Value ? string.Empty : (string)sqlReader["openId"];
                        msg.isTop = sqlReader["isTop"] == DBNull.Value ? false : (UInt64)sqlReader["isTop"] == 1;
                        msg.author = sqlReader["author"] == DBNull.Value ? string.Empty : (string)sqlReader["author"];
                        msg.wechatUrl = sqlReader["wechatUrl"] == DBNull.Value ? string.Empty : (string)sqlReader["wechatUrl"];
                        msg.authorHeadPic = sqlReader["headpic"] == DBNull.Value ? string.Empty : (string)sqlReader["headpic"];
                        msg.likeCount = sqlReader["likeCount"] == DBNull.Value ? 0 : (long)sqlReader["likeCount"];
                        msg.commentCount = sqlReader["commentCount"] == DBNull.Value ? 0 : (long)sqlReader["commentCount"];
                        msg.createdAt = sqlReader["createdAt"] == DBNull.Value ? DateTime.MinValue : (DateTime)sqlReader["createdAt"];
                        msgListExt.Add(msg);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return msgListExt;
        }

        public List<int> GetAllIds()
        {
            List<int> msgIds = new List<int>();
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select id from graphicmessage ", conn);

                    var sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        int id = (int)sqlReader["id"];
                        msgIds.Add(id);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }

            return msgIds;
        }

        /// <summary>
        /// 微信MediaId是否已经存在
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public bool MediaIdExist(string mediaId)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand($"select count(1) from graphicmessage where expiredAt is null and wechatMediaId='{mediaId}' ", conn);

                    var result = cmd.ExecuteScalar();
                    Int64 count = (Int64)result;
                    if (count>0)
                    {
                        return true;
                    }

                    return false;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
