using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Web.Models;

namespace Yuruisoft.RS.Web.Controllers
{
    public class UpdatePassWordController : Controller
    {
        //
        // GET: /UpdatePassWord/

        private IUserInfoService userInfoService;
        public UpdatePassWordController(IUserInfoService _userInfoService) { userInfoService = _userInfoService; }


        public ActionResult ChangePwd(string UserName, string Vcode)
        {
            if (UserName == null || Vcode == null)
            {
                return Redirect("/LogOn/Index");
            }
            Dictionary<string,string> TempDIC = SingleFindPSWcache.GetFindPSWcache().findPSWcache;
            if (!TempDIC.ContainsKey(UserName) || TempDIC[UserName] == "")
            {
                return Redirect("/LogOn/Index");
            }
            if (Vcode == TempDIC[UserName])
            {
                ViewBag.Vcode = Vcode;
                ViewBag.UserName = UserName;
            }
            else
            {
                return Redirect("/Error.html");
            }

            return View();
        }
        [HttpPost]
        public ActionResult ChangePwd()
        {
            if (Request["Vcode"] == SingleFindPSWcache.GetFindPSWcache().findPSWcache[Request["UserName"]])
            {
                string userName = Request["UserName"];
                var userInfoM = userInfoService.LoadEntities(c => c.UName == userName).FirstOrDefault();
                userInfoM.UPwd = Request["PSW"];
                userInfoM.TUPwd = Request["PSWA"];
                userInfoM.ModifiedOn = DateTime.Now;
                if (userInfoService.UpdateEntity(userInfoM))
                {//缓存清空，保证链接只能一次有效
                    SingleFindPSWcache.GetFindPSWcache().findPSWcache[Request["UserName"]] = "";
                    return Content("ok:");
                }
                else
                    return Content("no:");
            }
            else
            {
                return Redirect("/Error.html");
            }
        }
    }
}
