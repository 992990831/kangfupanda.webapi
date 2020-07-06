using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace kangfupanda.webapi.Common
{
    public class Utils
    {
        public static Bitmap GetImageFromBase64(string base64)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bitmap = new Bitmap(ms);
            return bitmap;
        }

        public static string ConvertBitmap2Base64(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            //不要用jpeg，会丢失信息
            //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] arr = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(arr, 0, (int)ms.Length);
            ms.Close();
            return Convert.ToBase64String(arr);
        }
    }
}