using System;
using System.Collections.Generic;
using System.Linq;
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
            var controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            logger.Error("报错：" + controllerName + "/" + actionName, actionExecutedContext.Exception);
            base.OnException(actionExecutedContext);
        }
    }
}