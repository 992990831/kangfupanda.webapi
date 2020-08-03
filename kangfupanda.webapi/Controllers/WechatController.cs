using kangfupanda.webapi.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace kangfupanda.webapi.Controllers
{
    [RoutePrefix("wechat")]
    public class WechatController : ApiController
    {
        [Route("user")]
        [HttpGet]
        public string GetWeChatUser(string code)
        {
            string accessTokenStr = getAccessToken(code);
            var accessTokenObj = JObject.Parse(accessTokenStr);

            if (accessTokenObj.Property("access_token") == null || accessTokenObj.Property("openid") == null)
            {
                return string.Empty;
            }

            string userInfo = getUserInfo(accessTokenObj["access_token"].Value<string>(), accessTokenObj["openid"].Value<string>());

            return userInfo;
        }

        string getAccessToken(string code)
        {
            string url = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={WeixinHelper.AppId}&secret={WeixinHelper.AppSecret}&code={code}&grant_type=authorization_code";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.ContentType = "application/json";
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.UTF8;

                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                string strResult = streamReader.ReadToEnd();

                return strResult;
            }
            catch (WebException webEx)
            {
                Stream ReceiveStream = webEx.Response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");

                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                string str = readStream.ReadToEnd();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                request.Abort();
            }

        }

        string getUserInfo(string accessToken, string openId)
        {
            string url = $"https://api.weixin.qq.com/sns/userinfo?access_token={accessToken}&openid={openId}&lang=zh_CN";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                request.ContentType = "application/json";
                request.Method = "GET";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.UTF8;

                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                string strResult = streamReader.ReadToEnd();

                return strResult;
            }
            catch (WebException webEx)
            {
                Stream ReceiveStream = webEx.Response.GetResponseStream();
                Encoding encode = Encoding.GetEncoding("utf-8");

                StreamReader readStream = new StreamReader(ReceiveStream, encode);
                string str = readStream.ReadToEnd();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                request.Abort();
            }
        }

        [HttpPost]
        [Route("config")]
        /// <summary>
        /// 生成微信分享所需的config
        /// </summary>
        /// <returns></returns>
        public WX_Config_Response getConfig([FromBody]wxRequest request)
        {
            var wxHelper = new WeixinHelper();
            var response = wxHelper.GenerateWXConfig(request.url);

            return response;
        }

        public class wxRequest { 
            public string url { get; set; }
        }
    }
}
