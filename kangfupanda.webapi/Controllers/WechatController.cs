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
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WechatController));

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

        [HttpGet]
        [Route("access_token")]
        /// <summary>
        /// 获取缓存的access token
        /// </summary>
        /// <returns></returns>
        public string GetAccessToken()
        {
            var wxHelper = new WeixinHelper();
            var access_token = wxHelper.GetCachedToken();

            return access_token;
        }

        [HttpGet]
        [Route("list/news")]
        public string GetNewsList(int offset = 0, int count=10 )
        {
            var wxHelper = new WeixinHelper();
            var access_token = wxHelper.GetCachedToken();
            string postUrl = $"https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={access_token}";
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";

            string options = $"{{\"type\":\"news\", \"offset\":{offset}, \"count\":{count}}}";
            byte[] payload = Encoding.UTF8.GetBytes(options);
            request.ContentLength = payload.Length;

            Stream writer = request.GetRequestStream();
            writer.Write(payload, 0, payload.Length);
            writer.Close();

            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            List<byte> bytes = new List<byte>();
            int temp = stream.ReadByte();
            while (temp != -1)
            {
                bytes.Add((byte)temp);
                temp = stream.ReadByte();
            }
            byte[] result = bytes.ToArray();
            string newsListStr = System.Text.Encoding.UTF8.GetString(result);

            return newsListStr;
        }

        [HttpGet]
        [Route("image")]
        public string GetNewsList(string url)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "get";
            request.ContentType = "application/json;charset=UTF-8";

            //string options = $"{{\"type\":\"news\", \"offset\":{offset}, \"count\":{count}}}";
            //byte[] payload = Encoding.UTF8.GetBytes(options);
            //request.ContentLength = payload.Length;

            //Stream writer = request.GetRequestStream();
            //writer.Write(payload, 0, payload.Length);
            //writer.Close();

            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            List<byte> bytes = new List<byte>();
            int temp = stream.ReadByte();
            while (temp != -1)
            {
                bytes.Add((byte)temp);
                temp = stream.ReadByte();
            }
            byte[] result = bytes.ToArray();
            string imgBase64 = Convert.ToBase64String(result);

            return imgBase64;
        }

    }

    public class wxRequest
    {
        public string url { get; set; }
    }
}
