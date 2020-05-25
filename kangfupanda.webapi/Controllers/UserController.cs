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

        [Route("list")]
        public List<User> GetUserList() {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var users = dao.GetList();

            return users;
        }
    }
}
