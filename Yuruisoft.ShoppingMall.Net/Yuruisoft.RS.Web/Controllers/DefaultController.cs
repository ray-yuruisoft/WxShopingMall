using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.Web.Models;

namespace Yuruisoft.RS.Web.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/

        public ActionResult Index()
        {
            //TODO:后期必须优化，放到缓存中去取
            string pathEnterFormFields = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Config/DefaultIndexView.Config/EnterFormFields.json");
            string pathportfolios = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Config/DefaultIndexView.Config/portfolios.json");

            string strjsonEnterFormFields = System.IO.File.ReadAllText(pathEnterFormFields, Encoding.Default);
            string strportfolios = System.IO.File.ReadAllText(pathportfolios, Encoding.Default);
            Portfolios[] portfolios_Temp = Newtonsoft.Json.JsonConvert.DeserializeObject<Portfolios[]>(strportfolios);
            EnterFormFields[] EnterFormFields_Temp = Newtonsoft.Json.JsonConvert.DeserializeObject<EnterFormFields[]>(strjsonEnterFormFields);

            ViewBag.portfolios = portfolios_Temp.Where(c => true);
            ViewBag.EnterFormFields = EnterFormFields_Temp.Where(c => true);
            return View();
        }

    }
}
