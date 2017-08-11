using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.Model.wxShoppingMall;

namespace Yuruisoft.RS.Web.Controllers.wxShoppingMall
{
    public class shoppingMallController : Controller
    {
        //
        // GET: /shoppingMall/

        [HttpPost]
        public ActionResult recommentListsGet(int takeNum)
        {
            if (Request.Headers["haowanFamily"] != "www.haowanFamily.com")//请求头自定义
            {
                return Content("forbid!");
            }
            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
            var lists = Db.Set<wxShoppingMall_produceInfo>().Where(c => true).OrderBy(c => c.sort).Take(takeNum);
            ArrayList results = new ArrayList();
            foreach (var item in lists)
            {
                results.Add(
                        new
                        {
                            id = item.id,
                            listImageUrl = domainGet() + item.listImageUrl,
                            listTitle = item.listTitle,
                            evaluationCount = item.evaluationCount,
                            evaluationPercent = item.evaluationPercent,
                            price = item.price,
                            unit = item.unit
                        }
                        );
            }

            return Json(results);
        }

        [HttpPost]
        public ActionResult searchKeyTreeGet()
        {
            if (Request.Headers["haowanFamily"] != "www.haowanFamily.com")//请求头自定义
            {
                return Content("forbid!");
            }
            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
            var lists = Db.Set<wxShoppingMall_produceInfo>().Where(c => true);

            ArrayList results = new ArrayList();
            foreach (var item in lists)
            {
                ArrayList temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayList>(item.listKeys);

                results.Add(
                        new
                        {
                            id = item.id,
                            listImageUrl = domainGet() + item.listImageUrl,
                            listTitle = item.listTitle,
                            listKeys = temp,
                            evaluationCount = item.evaluationCount,
                            evaluationPercent = String.Format("{0:N2}", item.evaluationPercent),
                            price = String.Format("{0:N2}", item.price),
                            unit = item.unit
                        }
                        );
            }

            string version = System.Web.Configuration.WebConfigurationManager.AppSettings["version"].ToString();
            return Json(new
            {
                searchKeyArrayVersion = version,
                searchKeyArray = results
            });
        }

        [HttpPost]
        public ActionResult verifyVersion()
        {
            if (Request.Headers["haowanFamily"] != "www.haowanFamily.com")//请求头自定义
            {
                return Content("forbid!");
            }
            string version = System.Web.Configuration.WebConfigurationManager.AppSettings["version"].ToString();
            return Json(new { version = version });
        }

        [HttpPost]
        public ActionResult produceDetailGet(int id)
        {
            if (Request.Headers["haowanFamily"] != "www.haowanFamily.com")//请求头自定义
            {
                return Content("forbid!");
            }
            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
            var finditem = Db.Set<wxShoppingMall_produceInfo>().Where(c => c.id == id).FirstOrDefault();
            var merchantName = Db.Set<wxShoppingMall_merchantInfo>().Where(c => c.id == finditem.merchantId).FirstOrDefault();
            if (finditem == null)
            {
                return Json(new
                {
                    error = true
                });
            }
            ArrayList tempBannerImages = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayList>(finditem.detailBannerImageUrl);
            var domain = domainGet();
            var detailBannerImageDic = finditem.detailBannerImageDic;

            for (var i = 0; i < tempBannerImages.Count; i++)
            {
                tempBannerImages[i] = domain + detailBannerImageDic + tempBannerImages[i].ToString();
            }

            ArrayList tempDetailTabInstructionImageUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<ArrayList>(finditem.detailTabInstructionImageUrl);
            for (var j = 0; j < tempDetailTabInstructionImageUrl.Count; j++)
            {
                tempDetailTabInstructionImageUrl[j] = domain + detailBannerImageDic + tempDetailTabInstructionImageUrl[j].ToString();
            }


            return Json(new
            {
                id = finditem.id,
                merchantId = finditem.merchantId,
                merchantName = merchantName.merchantName,
                listImageUrl = domain + finditem.listImageUrl,
                bannerImageUrl = tempBannerImages,
                detailTabInstructionImageUrl = tempDetailTabInstructionImageUrl,
                price = String.Format("{0:N2}", finditem.price),
                title = finditem.listTitle,
                unit = finditem.unit,
                error = false
            });
        }

        public string domainGet()
        {
            if (bool.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["debug"]))
                return "http://" + Request.Url.Authority;
            else
                return "https://" + Request.Url.Authority;
        }
    }




}
