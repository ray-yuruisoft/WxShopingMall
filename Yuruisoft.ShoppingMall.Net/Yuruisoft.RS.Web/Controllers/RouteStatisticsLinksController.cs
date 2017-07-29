using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;

namespace Yuruisoft.RS.Web.Controllers
{
    public class RouteStatisticsLinksController : Controller
    {
        //
        // GET: /RouteStatisticsLinks/

        private IRouteStatisticsLinksService routeStatisticsLinksService;
        public RouteStatisticsLinksController(IRouteStatisticsLinksService _routeStatisticsLinksService)
        {
            routeStatisticsLinksService = _routeStatisticsLinksService;
        }


        public ActionResult Index()
        {
            return View();
        }


        #region 获取链接表信息.
        public ActionResult GetLinksInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            int totalCount;
            short delFlag = (short)DeleteEnumType.Normal;
            var LinksInfoList = routeStatisticsLinksService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
            var rows = from r in LinksInfoList
                       select new { ID = r.ID, LName = r.LName, Url = r.Url, Remark = r.Remark, SubTime = r.SubTime, ModifiedOn = r.ModifiedOn };
            return Json(new { rows = rows, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 添加链接表信息.
        [HttpPost]
        public ActionResult AddLinksInfo(RouteStatisticsLinks LinksInfo)
        {
            if (ModelState.IsValid)
            {
                LinksInfo.DelFlag = 0;
                LinksInfo.SubTime = DateTime.Now;
                LinksInfo.ModifiedOn = DateTime.Now;
                routeStatisticsLinksService.AddEntity(LinksInfo);
                return Content("ok");
            }
            return Content("no");
        }
        #endregion
        #region 删除链接表信息.
        public ActionResult DeleteLinksInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(int.Parse(id));
            }
            routeStatisticsLinksService.DeleteEntities(list);
            return Content("ok");
        }
        #endregion
        //#region 编辑角色信息
        //public ActionResult ShowEditInfo()
        //{
        //    int id = int.Parse(Request["id"]);
        //    ViewData.Model = roleInfoService.LoadEntities(r => r.ID == id).FirstOrDefault();
        //    return View();
        //}
        //public ActionResult EditInfo(RoleInfo roleInfo)
        //{
        //    roleInfoService.UpdateEntity(roleInfo);
        //    return Content("ok");
        //}
        //#endregion



    }
}
