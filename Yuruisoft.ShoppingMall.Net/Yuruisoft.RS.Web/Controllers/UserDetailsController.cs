using DynamicDal;
using DynamicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.Common;
using Yuruisoft.RS.DAL;
using System.Linq.Dynamic;

namespace Yuruisoft.RS.Web.Controllers
{
    public class UserDetailsController : Controller
    {
        //BaseDal dal = new BaseDal(CreateDbContext());
        // GET: /UserDetails/

        private BaseDal dal { get; set; }
        private Type runtimeModel{get;set;}
        public UserDetailsController()
        {
            BaseDal _dal = new BaseDal(DBContextFactory.CreateDbContext());
            dal = _dal;
            runtimeModel= _dal.GetRuntimeModelType(GetJsonDatas.GetJson(), 0);
        }

        public ActionResult Index()
        {
            ViewBag.Propertys = dal.GetRuntimeModelProperty(GetJsonDatas.GetJson(), 0);
            return View();
        }
        #region 获取用户信息
        public ActionResult GetUserDetails()
        {
            int pageIndex = int.Parse(Request["page"]);//当前页码
            int pageSize = int.Parse(Request["rows"]);//当前每页显示记录数
            int totalCount = dal.LoadEntities(runtimeModel).Where("id>0").Count();
            IQueryable userDetailsList = dal.LoadEntities(runtimeModel).Where("id>0").OrderBy("id").Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return Json(new { rows = userDetailsList, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 添加用户信息
        [HttpPost]
        public ActionResult AddUserDetails()
        {
            DynamicEntity userDetail = dal.GetRuntimeModel(runtimeModel);

            foreach (var item in dal.GetRuntimeModelProperty(GetJsonDatas.GetJson(), 0))
            {
                if (item.ValueType == "int")
                {
                    userDetail[item.PropertyName] = int.Parse(Request[item.PropertyName]);
                }
                else//TODO:这里还需要扩展几种类型
                {
                    userDetail[item.PropertyName] = Request[item.PropertyName];
                }
            }

            dal.AddEntity(userDetail, runtimeModel);//增加
            return Content("ok");
        }
        #endregion

    }
}
