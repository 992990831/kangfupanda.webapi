using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace kangfupanda.webapi.Util
{
    public class WeixinHelper
    {
        private const string AppId = "wxc10ff63ecf588c90";
        private const string AppSecret = "7289786fae8511cbe9e94a8aec93e125";

        /// <summary>
        /// 生成前端微信分享所需的签名等参数
        /// </summary>
        /// <returns></returns>
        public WX_Config_Response GenerateWXConfig()
        {
            //生成签名的时间戳
            TimeSpan ts = DateTime.Now - DateTime.Parse("1970-01-01 00:00:00");
            string timestamp = ts.TotalSeconds.ToString().Split('.')[0];
            //生成签名的随机串
            string nonceStr = "test";

            //微信access_token，用于获取微信jsapi_ticket
            string token = GetAccess_token(AppId, AppSecret);
            //微信jsapi_ticket
            string ticket = GetTicket(token);

            //当前网页的URL
            string pageurl = "http://app.kangfupanda.com/share.html";

            //对所有待签名参数按照字段名的ASCII 码从小到大排序（字典序）后，使用URL键值对的格式（即key1=value1&key2=value2…）拼接成字符串
            string str = "jsapi_ticket=" + ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + pageurl;
            //签名,使用SHA1生成
            string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1").ToLower();

            WX_Config_Response response = new WX_Config_Response() { appId = AppId, nonceStr = nonceStr, signature = signature, timestamp = timestamp };

            return response;
        }

        /// <summary>
        /// 获取微信jsapi_ticket
        /// </summary>
        /// <param name="token">access_token</param>
        /// <returns>jsapi_ticket</returns>
        private string GetTicket(string token)
        {
            string ticketUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token=" + token + "&type=jsapi";
            string jsonresult = HttpGet(ticketUrl, "UTF-8");
            WX_Ticket wxTicket = JsonDeserialize<WX_Ticket>(jsonresult);
            return wxTicket.ticket;
        }

        /// <summary>
        /// 获取微信access_token
        /// </summary>
        /// <param name="appid">公众号的应用ID</param>
        /// <param name="secret">公众号的应用密钥</param>
        /// <returns>access_token</returns>
        private string GetAccess_token(string appid, string secret)
        {
            string tokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
            string jsonresult = HttpGet(tokenUrl, "UTF-8");
            WX_Token wx = JsonDeserialize<WX_Token>(jsonresult);
            return wx.access_token;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="jsonString">JSON</param>
        /// <returns>实体类</returns>
        private T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// HttpGET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="encode">编码方式：GB2312/UTF-8</param>
        /// <returns>字符串</returns>
        private string HttpGet(string url, string encode)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=" + encode;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding(encode));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

    }

    /// <summary>
    /// 通过微信API获取access_token得到的JSON反序列化后的实体
    /// </summary>
    public class WX_Token
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }


    /// <summary>
    /// 通过微信API获取jsapi_ticket得到的JSON反序列化后的实体
    /// </summary>
    public class WX_Ticket
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public string expires_in { get; set; }
    }

    public class WX_Config_Response {
        public string appId { get; set; }
        public string timestamp { get; set; }
        public string nonceStr { get; set; }
        public string signature { get; set; }
    }
}