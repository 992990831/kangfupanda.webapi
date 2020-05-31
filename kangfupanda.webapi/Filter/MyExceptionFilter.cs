using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace kangfupanda.webapi.Filter
{
    /// <summary>
    /// 只能处理未捕获的异常
    /// </summary>
    public class MyExceptionFilter: ExceptionFilterAttribute
    {
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MyExceptionFilter));
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            HttpRequestMessage request = actionExecutedContext.Request;
            var controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            string content = request.Content.ReadAsStringAsync().Result;
            string arguments = JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments);
            string msg1 = "报错：" + controllerName + "/" + actionName;
            string msg2 = "-requestContent: " + content + "-requestArguments: " + arguments;
            logger.Error(msg1 + msg2, actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}