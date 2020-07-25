using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string access_token = "35_EwDiuIMKB2ZVwEKC0KYGGceJZ25yvhFVAB6YgmMazLUBFNC0XCssbDQq_3yJ8M9O8kgs5vJGKSVppH0oW9sJs4iyc5L6iS-htqGfX3UlFM9bOVBVDaFp3Ct4IlfYW_GhPrZ0euiUOqaNym3UDWSaACAWPL"; //这里写你调用token的方法
          

            string postUrl = "https://api.weixin.qq.com/wxa/getwxacode?access_token=" + access_token;
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";

            string options = "{\"path\":\"pages/index/index\"}";
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

            File.WriteAllText(@"C:\Users\Administrator\Desktop\1.txt", base64);

            Console.ReadLine();
        }

    }
}
