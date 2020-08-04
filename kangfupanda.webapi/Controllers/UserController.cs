using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using kangfupanda.webapi.Models;
using kangfupanda.webapi.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;

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

        /// <summary>
        /// 前端查看个人主页
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [Route("{openId}")]
        [HttpGet]
        public UserProfile GetUser(string openId)
        {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var user = dao.GetUser(openId);
            var userProfile = JsonConvert.DeserializeObject<UserProfile>(JsonConvert.SerializeObject(user));

            userProfile.certs = new List<DoctorCert>();
            if (!string.IsNullOrEmpty(userProfile.certificate) || !string.IsNullOrEmpty(userProfile.certText))
            {
                userProfile.certs.Add(new DoctorCert() { cert=userProfile.certificate, text= userProfile.certText });
            }

            if (!string.IsNullOrEmpty(userProfile.certificate1) || !string.IsNullOrEmpty(userProfile.cert1Text))
            {
                userProfile.certs.Add(new DoctorCert() { cert = userProfile.certificate1, text = userProfile.cert1Text });
            }

            if (!string.IsNullOrEmpty(userProfile.certificate2) || !string.IsNullOrEmpty(userProfile.cert2Text))
            {
                userProfile.certs.Add(new DoctorCert() { cert = userProfile.certificate2, text = userProfile.cert2Text });
            }

            if (!string.IsNullOrEmpty(userProfile.certificate3) || !string.IsNullOrEmpty(userProfile.cert3Text))
            {
                userProfile.certs.Add(new DoctorCert() { cert = userProfile.certificate3, text = userProfile.cert3Text });
            }

            if (!string.IsNullOrEmpty(userProfile.certificate4) || !string.IsNullOrEmpty(userProfile.cert4Text))
            {
                userProfile.certs.Add(new DoctorCert() { cert = userProfile.certificate4, text = userProfile.cert4Text });
            }

            if (!string.IsNullOrEmpty(userProfile.certificate5) || !string.IsNullOrEmpty(userProfile.cert5Text))
            {
                userProfile.certs.Add(new DoctorCert() { cert = userProfile.certificate5, text = userProfile.cert5Text });
            }

            if (!string.IsNullOrEmpty(userProfile.certificate6) || !string.IsNullOrEmpty(userProfile.cert6Text))
            {
                userProfile.certs.Add(new DoctorCert() { cert = userProfile.certificate6, text = userProfile.cert6Text });
            }

            if (!string.IsNullOrEmpty(userProfile.certificate7) || !string.IsNullOrEmpty(userProfile.cert7Text))
            {
                userProfile.certs.Add(new DoctorCert() { cert = userProfile.certificate7, text = userProfile.cert7Text });
            }


            var likeDao = new LikeDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var followDao = new FollowDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            userProfile.likeCount = likeDao.GetLikeCount(openId);
            userProfile.fansCount = followDao.GetFollowerCount(openId);
            userProfile.followeeCount = followDao.GetFolloweeCount(openId);

            return userProfile;
        }

        /// <summary>
        /// 后台的用户下拉列表数据源
        /// </summary>
        /// <returns></returns>
        [Route("list")]
        public List<User> GetUsersList()
        {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var users = dao.GetList(1, int.MaxValue);

            return users;
        }

        /// <summary>
        /// 给后台admin portal使用的
        /// </summary>
        /// <returns></returns>
        [Route("admin/list/doctor")]
        public UserList GetAdminDoctorList(int pageIndex = 1, int pageSize = 10, string order="") {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);

            var userOpenIds = dao.GetUserOpenIdListByApiLog(pageIndex, pageSize, "and usertype is not null and usertype!='普通用户'");

            var count = dao.GetListCount("and usertype is not null and usertype!='普通用户'");

            var usersExt = new List<UserExt>();
            var apiLogDao = new ApiLogDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            userOpenIds.ForEach(openId =>
            {
                if (!string.IsNullOrEmpty(openId))
                {
                    var user = dao.GetUser(openId);

                    //var log = apiLogDao.GetByOpenId(user.openId);
                    UserExt userExt = new UserExt();
                    userExt.id = user.id;
                    userExt.openId = user.openId;
                    userExt.nickName = user.nickName;
                    userExt.province = user.province;
                    userExt.city = user.city;
                    userExt.phone = user.phone;
                    userExt.sex = user.sex;
                    userExt.headpic = user.headpic;
                    userExt.usertype = user.usertype;
                    userExt.note = user.note;
                    userExt.expertise = user.expertise;
                    userExt.verified = user.verified;
                    userExt.displayinapp = user.displayinapp;
                    userExt.detailimage = user.detailimage;

                    #region 证书
                    userExt.certificate = user.certificate;
                    userExt.certText = user.certText;
                    userExt.certificate1 = user.certificate1;
                    userExt.cert1Text = user.cert1Text;
                    userExt.certificate2 = user.certificate2;
                    userExt.cert2Text = user.cert2Text;
                    userExt.certificate3 = user.certificate3;
                    userExt.cert3Text = user.cert3Text;
                    userExt.certificate4 = user.certificate4;
                    userExt.cert4Text = user.cert4Text;
                    userExt.certificate5 = user.certificate5;
                    userExt.cert5Text = user.cert5Text;
                    userExt.certificate6 = user.certificate6;
                    userExt.cert6Text = user.cert6Text;
                    userExt.certificate7 = user.certificate7;
                    userExt.cert7Text = user.cert7Text;

                    userExt.profilevideo = user.profilevideo;
                    userExt.profilevideoposter = user.profilevideoposter;

                    #endregion

                    userExt.createdAt = user.createdAt;

                    userExt.lastVisitedAt = apiLogDao.GetLastVisitedTime(user.openId); //log.lastVisitedAt;
                    userExt.visitCountLastWeek = apiLogDao.GetLastWeekCount(user.openId); //log.visitCountLastWeek;

                    usersExt.Add(userExt);
                }                
            });

            if (order == "descend")
            {
                usersExt = usersExt.OrderByDescending(user => user.lastVisitedAt).ToList();
            }
            else if (order == "ascend")
            {
                usersExt = usersExt.OrderBy(user => user.lastVisitedAt).ToList();
            }

            return new UserList() { list= usersExt, count=count };
        }

        /// <summary>
        /// 给后台admin portal使用的
        /// </summary>
        /// <returns></returns>
        [Route("admin/list/user")]
        public UserList GetAdminUserList(int pageIndex = 1, int pageSize = 10, string order = "")
        {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var userOpenIds = dao.GetUserOpenIdListByApiLog(pageIndex, pageSize, "and (usertype='普通用户' or usertype is null) ");
            var count = dao.GetListCount("and (usertype='普通用户' or usertype is null) ");

            var usersExt = new List<UserExt>();
            var apiLogDao = new ApiLogDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            userOpenIds.ForEach(openId =>
            {
                if (!string.IsNullOrEmpty(openId))
                {
                    var user = dao.GetUser(openId);

                    var log = apiLogDao.GetByOpenId(user.openId);
                    UserExt userExt = new UserExt();
                    userExt.id = user.id;
                    userExt.openId = user.openId;
                    userExt.nickName = user.nickName;
                    userExt.province = user.province;
                    userExt.city = user.city;
                    userExt.phone = user.phone;
                    userExt.sex = user.sex;
                    userExt.headpic = user.headpic;
                    userExt.usertype = user.usertype;
                    userExt.note = user.note;
                    userExt.expertise = user.expertise;
                    userExt.verified = user.verified;
                    userExt.displayinapp = user.displayinapp;
                    userExt.detailimage = user.detailimage;
                    userExt.createdAt = user.createdAt;

                    userExt.lastVisitedAt = apiLogDao.GetLastVisitedTime(user.openId); //log.lastVisitedAt;
                    userExt.visitCountLastWeek = apiLogDao.GetLastWeekCount(user.openId); //log.visitCountLastWeek;

                    usersExt.Add(userExt);
                }

            });

            if (order == "descend")
            {
                usersExt = usersExt.OrderByDescending(user => user.lastVisitedAt).ToList();
            }
            else if (order == "ascend")
            {
                usersExt = usersExt.OrderBy(user => user.lastVisitedAt).ToList();
            }

            return new UserList() { list = usersExt, count = count };
        }


        /// <summary>
        /// 给前台App使用
        /// </summary>
        /// <returns></returns>
        [Route("doctor/list")]
        public List<User> GetDoctorList()
        {
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var users = dao.GetList(1, int.MaxValue, " and displayinapp=1");

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

            var responseEntity = new ResponseEntity<string>(true, "更新成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).UpdateUser(userDTO);

            return response;
        }

        [Route("profile")]
        [HttpPost]
        public ResponseEntity<string> UpdateUserProfile(User userDTO)
        {
            ResponseEntity<string> response = new ResponseEntity<string>();

            var responseEntity = new ResponseEntity<string>(true, "更新成功", string.Empty);
            (new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"])).UpdateUserProfile(userDTO);

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

        /// <summary>
        /// 获取用户的小程序二维码, 如果不存在就新建一个
        /// </summary>
        [Route("mini/qrcode")]
        [HttpGet]
        public ResponseEntity<string> GetMiniQRCode(string openId)
        {
            ResponseEntity<string> response = new ResponseEntity<string>(true, "小程序QR Code", string.Empty);
            var dao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            var userDao = new UserDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
            string QRCode = dao.GetQRCode(openId);

            if(!string.IsNullOrEmpty(QRCode))
            {
                response.Data = QRCode;
                return response;
            }
            else
            {
                var weixinHelper = new WeixinHelper();
                string token = weixinHelper.GetMiniToken();

                var path = $"pages/index/index?url={ "profile/doctor/" + openId }";
                var newQRCode = weixinHelper.GenerateMiniQR(token, path);
                dao.UpdateQRCode(newQRCode, openId);

                response.Data = newQRCode;
                return response;
            }

        }
    }

    public class UserList
    {
        public List<UserExt> list { get; set; }
        public Int64 count { get; set; }
    }
}
