using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;

namespace Yuruisoft.RS.Web.Controllers
{// Controller
    public class RoleInfoController : BaseController
    {
        //
        // GET: /RoleInfo/
        // 先Autofac注入
        private IRoleInfoService roleInfoService;
        public RoleInfoController(IRoleInfoService _roleInfoService)
        {
            roleInfoService = _roleInfoService;
        }

        public ActionResult Index()
        {
            return View();
        }
        #region 获取角色信息.
        public ActionResult GetRoleInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            int totalCount;
            short delFlag=(short)DeleteEnumType.Normal;
          var roleInfoList=roleInfoService.LoadPageEntities<int>(pageIndex,pageSize,out totalCount,r=>r.DelFlag==delFlag,r=>r.ID,true);
          var rows = from r in roleInfoList
                     select new { ID = r.ID, RoleName = r.RoleName, Sort = r.Sort, Remark=r.Remark,SubTime= r.SubTime };
          return Json(new {rows=rows,total=totalCount },JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 添加角色
        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.DelFlag = 0;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfoService.AddEntity(roleInfo);
            return Content("ok");
        }
        #endregion
        #region 角色删除
        public ActionResult DeleteRoleInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(int.Parse(id));
            }
            roleInfoService.DeleteEntities(list);
            return Content("ok");
        }

        #endregion
        #region 编辑角色信息
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
          ViewData.Model=roleInfoService.LoadEntities(r=>r.ID==id).FirstOrDefault();
          return View();
        }
        public ActionResult EditInfo(RoleInfo roleInfo)
        {
            roleInfoService.UpdateEntity(roleInfo);
            return Content("ok");
        }
        #endregion
    }
}
