using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace yuruisoft.oa.Web.Models
{
    
    public class MyExceptionAttribute:HandleErrorAttribute
    {
        /// <summary>
        /// 控制器方法中出现异常，会调用改方法捕获异常。
        /// </summary>
        /// <param name="filterContext"></param>
        public static Queue<Exception> exceptonQueue = new Queue<Exception>();
        public override void OnException(ExceptionContext filterContext)
        {
            exceptonQueue.Enqueue(filterContext.Exception);//将捕获的异常信息写到队列中
            filterContext.HttpContext.Response.Redirect("/Error.html");
            base.OnException(filterContext);
        }
    }
}