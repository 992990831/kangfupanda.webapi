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
        private static System.Web.Caching.Cache tokenCache = HttpRuntime.Cache;
        private const string tokenCacheName = "token";

        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(WeixinHelper));

        //一健点评(服务号)：wx496e5a01291ad836
        //一健点评(小程序): wx0bfa43c76629368d
        //快熊康复：wxc10ff63ecf588c90
        public const string AppId = "wxc10ff63ecf588c90";

        /// <summary>
        /// 小程序的AppId
        /// </summary>
        public const string MiniProgramAppId = "wx0bfa43c76629368d";

        //一健点评：5a544217123aca4505f8a680de415a35
        //一健点评(小程序): 8db4aeac6f14921b8a7087daeba81f7a
        //快熊康复：7289786fae8511cbe9e94a8aec93e125
        public const string AppSecret = "7289786fae8511cbe9e94a8aec93e125";

        /// <summary>
        /// 小程序的AppSecret
        /// </summary>
        public const string MiniProgramAppSecret = "8db4aeac6f14921b8a7087daeba81f7a";

        /// <summary>
        /// 生成前端微信分享所需的签名等参数
        /// </summary>
        /// <returns></returns>
        public WX_Config_Response GenerateWXConfig(string pageUrl)
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
            //string pageUrl = "http://app.kangfupanda.com/share.html";

            //对所有待签名参数按照字段名的ASCII 码从小到大排序（字典序）后，使用URL键值对的格式（即key1=value1&key2=value2…）拼接成字符串
            string str = "jsapi_ticket=" + ticket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + pageUrl;
            //签名,使用SHA1生成
            string signature = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1").ToLower();

            WX_Config_Response response = new WX_Config_Response() { appId = AppId, nonceStr = nonceStr, signature = signature, timestamp = timestamp };

            return response;
        }

        /// <summary>
        /// 微信小程序的token
        /// </summary>
        /// <returns></returns>
        public string GetMiniToken()
        {
            string tokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + MiniProgramAppId + "&secret=" + MiniProgramAppSecret;
            string jsonresult = HttpGet(tokenUrl, "UTF-8");
            WX_Token wx = JsonDeserialize<WX_Token>(jsonresult);
            return wx.access_token;
        }

        public string GenerateMiniQR(string accessToken, string path)
        {
            string postUrl = "https://api.weixin.qq.com/wxa/getwxacode?access_token=" + accessToken;
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";

            string options = $"{{\"path\":\"{path}\"}}";
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
            string base64 = Convert.ToBase64String(result);//将byte[]转为base64

            return base64;
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
            if(tokenCache[tokenCacheName] != null)
            {
                logger.Info($"get wechat token from cache: {tokenCache[tokenCacheName].ToString()}");
                return tokenCache[tokenCacheName].ToString();
            }

            string tokenUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + appid + "&secret=" + secret;
            string jsonresult = HttpGet(tokenUrl, "UTF-8");
            WX_Token wx = JsonDeserialize<WX_Token>(jsonresult);
            tokenCache.Insert(tokenCacheName, wx.access_token, null, System.DateTime.Now.AddSeconds(1800), TimeSpan.Zero);
            logger.Info($"get wechat token from api: {wx.access_token}");
            return wx.access_token;
        }

        public string GetCachedToken()
        {
            string access_token = GetAccess_token(AppId, AppSecret);
            //if (tokenCache[tokenCacheName] != null)
            //{
            //    logger.Info($"get wechat token from cache: {tokenCache[tokenCacheName].ToString()}");
            //    return tokenCache[tokenCacheName].ToString();
            //}

            return access_token;
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