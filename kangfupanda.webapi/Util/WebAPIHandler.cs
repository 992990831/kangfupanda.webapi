using kangfupanda.dataentity.DAO;
using kangfupanda.dataentity.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace kangfupanda.webapi.Util
{
    public class WebAPIHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                ApiLog log = new ApiLog();
                if (request.Headers.Contains("openid"))
                {
                    log.openId = request.Headers.GetValues("openid").FirstOrDefault();
                }
                if (request.Headers.Contains("nickname"))
                {
                    log.nickName = request.Headers.GetValues("nickname").FirstOrDefault();
                }

                log.endpoint = request.RequestUri.ToString();

                log.ip = GetClientIpAddress(request);
                log.client = request.Headers.UserAgent.ToString();

                var dao = new ApiLogDao(ConfigurationManager.AppSettings["mysqlConnStr"]);
                dao.Add(log);
            }
            catch { 
            
            }
            

            return base.SendAsync(request, cancellationToken);
        }

        public string GetClientIpAddress(HttpRequestMessage request)
        {
            // Web-hosting. Needs reference to System.Web.dll
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                dynamic ctx = request.Properties["MS_HttpContext"];
                if (ctx != null)
                {
                    return ctx.Request.UserHostAddress;
                }
            }

            // Self-hosting. Needs reference to System.ServiceModel.dll. 
            if (request.Properties.ContainsKey("System.ServiceModel.Channels.RemoteEndpointMessageProperty"))
            {
                dynamic remoteEndpoint = request.Properties["System.ServiceModel.Channels.RemoteEndpointMessageProperty"];
                if (remoteEndpoint != null)
                {
                    return remoteEndpoint.Address;
                }
            }

            // Self-hosting using Owin. Needs reference to Microsoft.Owin.dll. 
            if (request.Properties.ContainsKey("MS_OwinContext"))
            {
                dynamic owinContext = request.Properties["MS_OwinContext"];
                if (owinContext != null)
                {
                    return owinContext.Request.RemoteIpAddress;
                }
            }

            return null;
        }
    }
}