using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Routing;

namespace kangfupanda.webapi.Filter
{
    public class MyActionTimeFilter : ActionFilterAttribute
    {
        private Stopwatch timerAction;
        static readonly log4net.ILog logger = log4net.LogManager.GetLogger(typeof(MyActionTimeFilter));
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            timerAction = new Stopwatch();
            timerAction.Start();
            string controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string actionName = actionContext.ActionDescriptor.ActionName;
            string msg = "执行Action前:" + "请求地址：" + controllerName + "/" + actionName;
            logger.Debug(msg);
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            timerAction.Stop();
            string controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
            string msg = "执行Action后:" + "请求地址：" + controllerName + "/" + actionName + "   耗时：" + timerAction.ElapsedMilliseconds + "毫秒";
            logger.Debug(msg);

            base.OnActionExecuted(actionExecutedContext);
        }

    }
}