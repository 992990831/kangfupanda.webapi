using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Common;
using kangfupanda.webapi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    [RoutePrefix("message")]
    public class MessageController : ApiController
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MessageController));

        /// <summary>
        /// 后台管理网站使用
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        public List<GraphicMessage> GetMessageList()
        {
            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var messages = dao.GetList(" order by isTop desc, id desc ");
           
            //messages.ForEach(msg =>
            //{
            //    if (!string.IsNullOrEmpty(msg.poster))
            //    {
            //        Bitmap bitmap = new Bitmap(ConfigurationManager.AppSettings["UploadFolderPath"] + msg.poster);
            //        msg.poster = Utils.ConvertBitmap2Base64(bitmap);
            //    }
            //});

            return messages;
        }

        [Route("{id}")]
        public GraphicMessage GetGraphicById(int id)
        {
            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var msg = dao.GetById(id.ToString());

            return msg;
        }

        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("settop/{id}")]
        [HttpGet]
        public ResponseEntity<bool> SetTop(int id)
        {
            ResponseEntity<bool> response = new ResponseEntity<bool>();
            try
            {
                var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
                dao.SetTop(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return response;
        }

        /// <summary>
        /// 设置置顶
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("unsettop/{id}")]
        [HttpGet]
        public ResponseEntity<bool> UnsetTop(int id)
        {
            ResponseEntity<bool> response = new ResponseEntity<bool>();
            try
            {
                var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
                dao.UnsetTop(id);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            return response;
        }

        [Route("add")]
        public ResponseEntity<long> AddGraphicMessage(GraphicMessage msg)
        {
            //msg.pic01 = string.IsNullOrEmpty(msg.pic01) ? string.Empty : ConfigurationManager.AppSettings["UploadUrl"] + msg.pic01;
            //msg.pic02 = string.IsNullOrEmpty(msg.pic02) ? string.Empty : ConfigurationManager.AppSettings["UploadUrl"] + msg.pic02;
            //msg.pic03 = string.IsNullOrEmpty(msg.pic03) ? string.Empty : ConfigurationManager.AppSettings["UploadUrl"] + msg.pic03;
            //msg.pic04 = string.IsNullOrEmpty(msg.pic04) ? string.Empty : ConfigurationManager.AppSettings["UploadUrl"] + msg.pic04;
            //msg.pic05 = string.IsNullOrEmpty(msg.pic05) ? string.Empty : ConfigurationManager.AppSettings["UploadUrl"] + msg.pic05;
            //msg.pic06 = string.IsNullOrEmpty(msg.pic06) ? string.Empty : ConfigurationManager.AppSettings["UploadUrl"] + msg.pic06;

            if (!string.IsNullOrEmpty(msg.poster) && msg.poster.IndexOf("base64,") > -1)
            {
                try
                {
                    var img = Utils.GetImageFromBase64(msg.poster.Substring(msg.poster.IndexOf("base64,")+7));
                    string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".jpg";
                    string fullPath = ConfigurationManager.AppSettings["UploadFolderPath"] + fileName;
                    img.Save(fullPath);

                    msg.poster = fileName;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
            

            ResponseEntity<long> response = new ResponseEntity<long>();

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = true;
            long id = 0; 
            if (msg.id > 0)
            {
                dao.UpdateGraphicMessage(msg);
                id = msg.id;
            }
            else
            {
                id=  (long)dao.AddGraphicMessage(msg);
            }
            

            if (success)
            {
                response = new ResponseEntity<long>(true, "添加图文成功", id);
            }
            else
            {
                response = new ResponseEntity<long>(true, "添加图文失败", id);
            }

            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public ResponseEntity<string> DeleteGraphicMessage(int id)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.DeleteById(id);

            if (success)
            {
                response = new ResponseEntity<string>(true, "删除图文成功", "删除图文成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "删除图文失败", "删除图文失败");
            }

            return response;
        }

        /// <summary>
        /// 前端使用
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [Route("list/my")]
        [HttpGet]
        public List<GraphicMessageExt> GetMyMessageList(string openId)
        {
            var dao = new GraphicMessageDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var messages = dao.GetListExt(filter: $" and g.openid='{openId}'", count: int.MaxValue, endId: int.MaxValue);

            messages.ForEach(msg => {
                msg.itemType = "graphic";
            });

            //messages.ForEach(msg =>
            //{
            //    if (!string.IsNullOrEmpty(msg.poster))
            //    {
            //        Bitmap bitmap = new Bitmap(ConfigurationManager.AppSettings["UploadFolderPath"] + msg.poster);
            //        msg.poster = Utils.ConvertBitmap2Base64(bitmap);
            //    }
            //});

            return messages;
        }
    }
}
