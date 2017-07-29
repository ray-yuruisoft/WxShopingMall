using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.Web.Controllers
{
    public class BaseController : Controller
    {
        // GET: /Base/
        //执行控制器方法之前先执行该方法,过滤器的创建
        //获取userInfo的值
        protected UserInfo LoginUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)//动作执行前
        {
            bool isExt = false;
            if (Session["userInfo"] != null)
            {
                    LoginUser = Session["userInfo"] as UserInfo;
                    isExt = true;
                    if (LoginUser.UName == "admin")//超级权限，留的后门
                    {
                        return;
                    }
                    //完成权限过滤.
                    string requestUrl = Request.Url.AbsolutePath.ToLower();//获取URL地址.
                    string requestHttpMethod = Request.HttpMethod;

                    IBLL.IUserInfoService userInfoService = new BLL.UserInfoService();//暂时没有注入
                    IBLL.IActionInfoService actionInfoService = new BLL.ActionInfoService();

                    var currentAction = actionInfoService.LoadEntities(a => a.Url == requestUrl && a.HttpMethod == requestHttpMethod).FirstOrDefault();//根据URL地址与请求方式找出具体的权限.
                    if (currentAction == null)
                    {
                        Response.Redirect("/Error.html");
                        return;
                    }
                    //通过1号线进行校验.
                    var currentUserInfo = userInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();//登录用户
                    var actions = currentUserInfo.R_UserInfo_ActionInfo.Where(r => r.ActionInfoID == currentAction.ID).FirstOrDefault();//判断登录用户是否有权限
                    if (actions != null)
                    {
                        if (actions.IsPass == true)
                        {
                            return;
                        }
                        else
                        {
                            Response.Redirect("/Error.html");
                            return;
                        }
                    }
                    //走2号线校验.
                    var currentUserRoles = currentUserInfo.RoleInfo;
                    var currentUserActions = from a in currentUserRoles
                                             select a.ActionInfo;
                    var count = (from a in currentUserActions
                                 from b in a
                                 where b.ID == currentAction.ID
                                 select b).Count();
                    if (count < 1)
                    {
                        Response.Redirect("/Error.html");
                        return;
                    }
                    //走3条线.

            }
            if (!isExt)//用户没有登录
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}
