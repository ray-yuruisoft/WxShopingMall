using RouteStatisticsCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;
using Yuruisoft.RS.Model.UserInfoParams;

namespace Yuruisoft.RS.Web.Controllers
{
    public class ExtensionAgentsController : Controller
    {
        //
        // GET: /ExtensionAgents/

        private IExtensionAgentsService extensionAgentsService;
        private IRouteStatisticsLinksService routeStatisticsLinksService;
        public ExtensionAgentsController(IExtensionAgentsService _extensionAgentsService, IRouteStatisticsLinksService _routeStatisticsLinksService)
        {
            extensionAgentsService = _extensionAgentsService;
            routeStatisticsLinksService = _routeStatisticsLinksService;
        }

        public ActionResult Index()
        {
            var rp = routeStatisticsLinksService.LoadEntities(c => true);
            ViewData["UrlName"] = rp.Select(a => new SelectListItem
                                    {
                                        Text = a.LName,
                                        Value = a.ID+"," + a.LName
                                    });
            return View();
        }

        #region 获取用户信息+多条件查询
        public ActionResult GetAgentInfo()
        {
            int pageIndex = int.Parse(Request["page"]);//当前页码
            int pageSize = int.Parse(Request["rows"]);//当前每页显示记录数
            int totalCount = 0;
            short deleteType = (short)DeleteEnumType.Normal;//标记 0正常，1逻辑，2物理
            if (Request["DoTheSearch"] == "true")
            {
                string name = Request["name"];//接收用户名
                string remark = Request["remark"];//接收备注

                ExtensionAgentsFilter agentInfoFilter = new ExtensionAgentsFilter()//构建用户搜索过滤的条件
                {
                    LName = name,
                    Remark = remark,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = totalCount
                };
                var agentInfoList = extensionAgentsService.LoadSearchAgentsInfo(agentInfoFilter);
                var temp = from u in agentInfoList
                           select new { ID = u.ID, LName = u.LName,UrlName = u.UrlName ,ExtensionUrl = u.ExtensionUrl, ExtensionScore = u.ExtensionScore, Remark = u.Remark, SubTime = u.SubTime, ModifiedOn = u.ModifiedOn, DelFlag = u.DelFlag, Sort = u.Sort };
                return Json(new { rows = temp, total = agentInfoFilter.TotalCount }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var agentInfoList = extensionAgentsService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.DelFlag == deleteType, c => c.ID, true);
                var temp = from u in agentInfoList
                           select new { ID = u.ID, LName = u.LName, UrlName = u.UrlName, ExtensionUrl = u.ExtensionUrl, ExtensionScore = u.ExtensionScore, Remark = u.Remark, SubTime = u.SubTime, ModifiedOn = u.ModifiedOn, DelFlag = u.DelFlag, Sort = u.Sort };

                return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region 添加用户信息
        [HttpPost]
        public ActionResult AddAgentInfo(ExtensionAgents extensionAgents)
        {
                extensionAgents.SubTime = DateTime.Now;
                extensionAgents.ModifiedOn = DateTime.Now;
                extensionAgents.Sort = "0";
                extensionAgents.DelFlag = 0;
                extensionAgents.ExtensionScore = 0;
                if (extensionAgents.UrlName == null)
                    return Content("no");
                string[] temp = extensionAgents.UrlName.Split(',');
                if (temp.Length != 2)
                { return Content("no"); }
                else
                {
                    extensionAgents.GUID = Guid.NewGuid().ToString("N");
                    extensionAgents.RouteStatisticsLinks_ID = int.Parse(temp[0]);
                    extensionAgents.ExtensionUrl = ExtendMethord.ExUrlCreate(extensionAgents.GUID);
                    extensionAgents.ExtensionScore = 0;
                    extensionAgents.UrlName = temp[1];
                    extensionAgentsService.AddEntity(extensionAgents);
                    Dictionary<string, string> Dic = new Dictionary<string, string> { { extensionAgents.GUID, routeStatisticsLinksService.LoadEntities(c => c.ID == extensionAgents.RouteStatisticsLinks_ID).Select(c => c.Url).FirstOrDefault() } };
                    if (ExtendMethord.GetUrl().URLMap != null)
                    {
                        ExtendMethord.GetUrl().URLMap = Dic;// 更新内存值，这里的等号相当于添加
                    }
                }
                return Content("ok");
        }
        #endregion
        #region 删除用户信息
        public ActionResult DeleteAgentInfo()
        {
            string strId = Request["strId"];
            string[] strIds = strId.Split(',');
            List<int> list = new List<int>();
            foreach (var id in strIds)
            {
                list.Add(int.Parse(id));
            }
            if (extensionAgentsService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

    }
}
