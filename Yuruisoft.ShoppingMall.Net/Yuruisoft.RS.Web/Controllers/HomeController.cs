using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model.ActionEqualityCompare;
using Yuruisoft.RS.Model.Enum;

namespace Yuruisoft.RS.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IUserInfoService userInfoService;
        public HomeController(IUserInfoService _userInfoService)
        {
            userInfoService = _userInfoService;
        }


        public ActionResult Index()
        {
            if (LoginUser != null)
                ViewBag.userName = LoginUser.UName;
            return View();
        }
        public ActionResult HomePage()
        {
            if (LoginUser != null)
            {
                ViewBag.userName = LoginUser.UName;
            }
            return View();
        }


        #region 找出用户对应的菜单
        public ActionResult GetMenuItems()
        {
            //1.查询用户已经有的角色.
            var userRoles = LoginUser.RoleInfo;
            //2.找出对应的权限.
            short menuType = (short)ActionTypeEnum.MenuActionType;
            var userMenuItem = (from r in userRoles
                                from a in r.ActionInfo
                                where a.ActionTypeEnum == menuType
                                select a).ToList();//6
            //3.找出用户特有的权限.
            var userActions = LoginUser.R_UserInfo_ActionInfo.ToList();
            //4.找出userActions允许的权限.
            var isPassUserActions = from a in userActions
                                    where a.IsPass == true && a.ActionInfo.ActionTypeEnum == menuType
                                    select a;

            var isPassActions = (from a in isPassUserActions
                                 select a.ActionInfo).ToList();

            userMenuItem.AddRange(isPassActions);//合并两个集合.
            //找出禁止权限.
            var isNotPassUserActions = (from a in userActions
                                        where a.IsPass == false
                                        select a.ActionInfoID).ToList();
            //完成禁用权限的过滤
            userMenuItem = userMenuItem.Where(a => !isNotPassUserActions.Contains(a.ID)).ToList();

            //去掉重复的,无效果
            userMenuItem.Distinct(new ActionEqualCompare());
            JsonResult jsonResult = null;
            try
            {
                var result = from u in userMenuItem
                             select new
                             {
                                 icon = u.MenuIcon,
                                 title = u.ActionInfoName,
                                 url = u.Url
                             };
                jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            }
            catch
            {
            }
            return jsonResult;
        }
        #endregion
    }
}
