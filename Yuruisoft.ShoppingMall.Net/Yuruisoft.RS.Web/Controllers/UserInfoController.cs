using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model.UserInfoParams;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;
using Yuruisoft.RS.DAL;

namespace Yuruisoft.RS.Web.Controllers
{// Controller
    public class UserInfoController : BaseController
    {
        private IUserInfoService userInfoService;
        private IRoleInfoService roleInfoService;
        private IActionInfoService actionInfoService;
        private IR_UserInfo_ActionInfoService r_UserInfo_ActionInfoService;
        public UserInfoController(IUserInfoService _userInfoService, IRoleInfoService _roleInfoService, IActionInfoService _actionInfoService, IR_UserInfo_ActionInfoService _r_UserInfo_ActionInfoService)
        {
            userInfoService = _userInfoService;
            roleInfoService = _roleInfoService;
            actionInfoService = _actionInfoService;
            r_UserInfo_ActionInfoService = _r_UserInfo_ActionInfoService;
        }
        public ActionResult Index()
        {
           
           return View();
        }
        #region 获取用户信息+多条件查询
        public ActionResult GetUserInfo()
        {
            int pageIndex = int.Parse(Request["page"]);//当前页码
            int pageSize = int.Parse(Request["rows"]);//当前每页显示记录数
            int totalCount = 0;
            short deleteType = (short)DeleteEnumType.Normal;//标记 0正常，1逻辑，2物理
            if (Request["DoTheSearch"] == "true")
            {
                string name = Request["name"];//接收用户名
                string remark = Request["remark"];//接收备注
                string email = Request["email"];//接收邮箱
                string phoneNumber = Request["phoneNumber"];//接收手机号码
                UserInfoFilter userInfoFilter = new UserInfoFilter()//构建用户搜索过滤的条件
                {
                    UName = name,
                    URemark = remark,
                    UEmail = email,
                    UPhoneNumber = phoneNumber,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };
                var userInfoList = userInfoService.LoadSearchUserInfo(userInfoFilter);
                var temp = from u in userInfoList
                           select new { ID = u.ID, UName = u.UName, UPwd = u.UPwd, TUPwd = u.TUPwd, UEmail = u.UEmail, UPhoneNumber = u.UPhoneNumber, Remark = u.Remark, SubTime = u.SubTime, ModifiedOn = u.ModifiedOn, DelFlag = u.DelFlag, Sort = u.Sort };
                return Json(new { rows = temp, total = userInfoFilter.TotalCount }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var userInfoList = userInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == deleteType, c => c.ID,true);
                var temp = from u in userInfoList
                           select new { ID = u.ID, UName = u.UName, UPwd = u.UPwd, TUPwd = u.TUPwd, UEmail = u.UEmail, UPhoneNumber = u.UPhoneNumber, Remark = u.Remark, SubTime = u.SubTime, ModifiedOn = u.ModifiedOn , DelFlag = u.DelFlag, Sort = u.Sort };

                return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region 删除用户信息
        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (var id in strIds)
            {
                list.Add(int.Parse(id));            
            }
            if (userInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }        
        }
        #endregion
        #region 添加用户信息
        [HttpPost]
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                userInfo.SubTime = DateTime.Now;
                userInfo.ModifiedOn = DateTime.Now;
                userInfo.Sort = "0";
                userInfo.DelFlag = 0;
                userInfoService.AddEntity(userInfo);
                return Content("ok");
            }
            return Content("no");
        }
        #endregion
        #region 修改数据
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            { 
                userInfo.ModifiedOn = DateTime.Now;
                if (userInfoService.UpdateEntity(userInfo))
                {
                    return Content("ok");
                }
                else
                {
                    return Content("no");
                }
            }
            return Content("no");
        }
        #endregion
        #region 展示修改的数据
         public ActionResult ShowEditInfo()
        {
           int id = int.Parse(Request["id"]);
           var userInfo= userInfoService.LoadEntities(u=>u.ID==id).FirstOrDefault();//获取要修改的数据.
           ViewData.Model = userInfo;
           return View();
        }
        #endregion
        #region 检查用户名重复
         /// <summary>  
         /// 检查用户名是否有重复  
         /// </summary>  
         /// <param name="userName">用户在页面(视图)表单中输入的UserName</param>  
         /// <returns>Json</returns>  
         public ActionResult CheckUserName(string UName)
         {
             return Json(userInfoService.CheckUserNameIsEx(UName), JsonRequestBehavior.AllowGet);
         }
         #endregion
        #region 为用户分配角色
         public ActionResult SetUserRoleInfo()
         {
             int id = int.Parse(Request["id"]);
             var userInfo = userInfoService.LoadEntities(c => c.ID == id).FirstOrDefault();//查询出当前用户信息
             ViewBag.UserInfo = userInfo;
             short DelFlag = (short)DeleteEnumType.Normal;
             ViewBag.AllRoles = roleInfoService.LoadEntities(r => r.DelFlag == DelFlag).ToList();//查出所有的角色信息
             ViewBag.ExtAllRoleIds = (from r in userInfo.RoleInfo
                                      select r.ID).ToList();//获取当前用户已经有的角色的编号
             return View();
         }
         [HttpPost]
         public ActionResult SetUserRoleInfo(FormCollection collection)
         {
             int userId = int.Parse(Request["userId"]);
             string[] AllKeys = Request.Form.AllKeys;//获取所有的表单的name属性的值.
             List<int> list = new List<int>();
             foreach (string key in AllKeys)
             {
                 if (key.StartsWith("cba_"))
                 {
                     string roleId = key.Replace("cba_","");
                     list.Add(int.Parse(roleId));
                 }
             }
             userInfoService.SetUserRole(userId, list);//给当前用户分配角色
             return Content("ok");
         }
         #endregion
        #region 为用户分配权限
         public ActionResult SetUserActionInfo()
         {
             int userId = int.Parse(Request["id"]);
             var userInfo = userInfoService.LoadEntities(u => u.ID == userId).FirstOrDefault();
             ViewData.Model = userInfo;
             ViewBag.UserInfo = userInfo;
             ViewBag.AllActions = actionInfoService.LoadEntities(a => a.DelFlag == 0).ToList();//找出所有的权限
             ViewBag.AllExtActions = userInfo.R_UserInfo_ActionInfo.ToList();//找出当前用户所有的权限（包含允许，禁止）
             return View();
         }
         public ActionResult SetActionForUser()
         {
             int userId = int.Parse(Request["userId"]);//用户编号
             int actionId = int.Parse(Request["actionId"]);//权限编号
             string value = Request["value"];//表示允许或拒绝。
             bool isPass = value == "true" ? true : false;
             if (userInfoService.SetUserAction(userId, actionId, isPass))
             {
                 return Content("ok");
             }
             else
             {
                 return Content("no");
             }
         }
         #endregion
        #region 删除用户的权限
         public ActionResult ClearActionUser()
         {
             int userId = int.Parse(Request["userId"]);
             int actionId = int.Parse(Request["actionId"]);
             var actionInfo = r_UserInfo_ActionInfoService.LoadEntities(r => r.UserInfoID == userId && r.ActionInfoID == actionId).FirstOrDefault();
             if (actionInfo != null)
             {
                 r_UserInfo_ActionInfoService.DeleteEntity(actionInfo);
             }
             return Content("ok");

         }
         #endregion
    }
}
