using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        [Route("register")]
        [HttpPost]
        /// <summary>
        /// 前端获取用户的微信信息，再发送到后端接口
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ResponseEntity<string> RegisterUser(User userDTO) {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var user = dao.GetUser(userDTO.openId);
            if (user == null || user.id == 0)
            {
                bool success = dao.AddUser(userDTO);

                if (success)
                {
                    response = new ResponseEntity<string>(true, "添加用户成功", "添加用户成功");
                }
                else {
                    response = new ResponseEntity<string>(true, "添加用户失败", "添加用户失败");
                }
            }
            else
            {
                response = new ResponseEntity<string>(true, "用户已存在", "用户已存在");
            }

            return response;
        }

        [Route("{openId}")]
        [HttpGet]
        public User GetUser(string openId)
        {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var user = dao.GetUser(openId);

            return user;
        }

        /// <summary>
        /// 给后台admin portal使用的
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        public List<User> GetUserList() {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var users = dao.GetList();

            return users;
        }

        /// <summary>
        /// 给前台App使用
        /// </summary>
        /// <returns></returns>
        [Route("doctor/list")]
        public List<User> GetDoctorList()
        {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var users = dao.GetList(" and displayinapp=1");

            return users;
        }

        /// <summary>
        /// 通过Admin portal手动添加，openID自动生成
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        [Route("add")]
        public ResponseEntity<string> AddUser(User userDTO)
        {
            userDTO.openId = Guid.NewGuid().ToString();
            userDTO.headpic = ConfigurationManager.AppSettings["UploadUrl"] + userDTO.headpic;

            ResponseEntity<string> response = new ResponseEntity<string>();

            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            bool success = dao.AddUser(userDTO);
            if (success)
            {
                response = new ResponseEntity<string>(true, "添加用户成功", "添加用户成功");
            }
            else
            {
                response = new ResponseEntity<string>(true, "添加用户失败", "添加用户失败");
            }

            return response;
        }

        [Route("update")]
        [HttpPost]
        public ResponseEntity<string> UpdateUser(User userDTO)
        {
            userDTO.headpic = ConfigurationManager.AppSettings["UploadUrl"] + userDTO.headpic;

            ResponseEntity<string> response = new ResponseEntity<string>();

            var responseEntity = new ResponseEntity<string>(true, "删除成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).UpdateUser(userDTO);

            return response;
        }

        [Route("delete")]
        [HttpDelete]
        public ResponseEntity<string> DeleteUser(string openId)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "删除成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).DeleteById(openId);

            return response;
        }

        [Route("verify")]
        [HttpGet]
        public ResponseEntity<string> VerifyUser(string openId)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "认证成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).VerifyUser(openId, true);

            return response;
        }

        [Route("unverify")]
        [HttpGet]
        public ResponseEntity<string> UnVerifyUser(string openId)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "取消认证成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).VerifyUser(openId, false);

            return response;
        }

        [Route("display")]
        [HttpGet]
        public ResponseEntity<string> DisplayUserInApp(string openId)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "显示用户成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).DisplayUser(openId, true);

            return response;
        }

        [Route("hide")]
        [HttpGet]
        public ResponseEntity<string> HideUserFromApp(string openId)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "取消显示成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).DisplayUser(openId, false);

            return response;
        }
    }
}
