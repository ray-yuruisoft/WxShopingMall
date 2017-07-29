using RouteStatisticsCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yuruisoft.RS.Web.Controllers
{
    public class RedirectController : Controller
    {
        //
        // GET: /Redirect/
        [HttpGet]
        public ActionResult Index(string strQuery)
        {//strQuery为推广员的ID
            if (strQuery != null)
            {
                string url;
                try { 
                    UrlCache cache = ExtendMethord.GetUrl();
                    url = cache.URLMap[strQuery];
                    ExtendMethord.OperateScoreCacheQueue.Enqueue(new OperateScoreCache(strQuery));//增加放到队列里去做
                }
                catch(Exception e)
                {
                    UrlCache cache = new UrlCache();
                    url = cache.URLMap[strQuery];
                    ExtendMethord.OperateScoreCacheQueue.Enqueue(new OperateScoreCache(strQuery));//增加放到队列里去做
                }
                return Redirect(url);
            }
            else
                return Content("链接错误！");
        }
    }
}
