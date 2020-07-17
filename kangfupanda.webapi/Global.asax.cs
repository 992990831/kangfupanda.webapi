using kangfupanda.webapi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace kangfupanda.webapi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //注册 log4net
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"bin\Log4Net.config"));

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //IDCache.UpdateRandomIDs();
            //Task.Factory.StartNew(() =>
            //{
            //    while(true)
            //    {
            //        IDCache.UpdateRandomIDs();
            //        Thread.Sleep(60 * 1000);
            //    }
                
            //});
        }

        protected void Application_BeginRequest()
        {
            //此处在Web.config已经有了，所以不用再定义
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //Response.Headers.Add("Access-Control-Allow-Methods", "PUT, POST, GET, DELETE, OPTIONS");
            //Response.Headers.Add("Access-Control-Allow-Headers", "x-requested-with,content-type");
            if (Request.HttpMethod == "OPTIONS")
            {
                //Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
                string allowHeaders = "Origin, No-Cache, X-Requested-With, If-Modified-Since, Pragma, Last-Modified, Cache-Control, Expires, Content-Type, X-E4M-With, Authorization";
                Response.Headers.Add("Access-Control-Allow-Headers", allowHeaders);
                Response.End();
            }
        }
    }
}
