using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.Model.EnterPagePortfolioModal;
using Yuruisoft.RS.Model.RuntimeModel;
using Yuruisoft.RS.Setup;

namespace Yuruisoft.RS.Web.Controllers
{
    public class EnterController : Controller
    {
        //
        // GET: /Enter/

        public ActionResult Index()
        {
            ConfigDataCache Cache = EnterPageInitialize.CreateInstance(EnterPageInitialize.ConfigProviderCreate());
            #region 动态字段初始化
            ViewBag.Title = Cache.Map_ConfigData["Title"];
            ViewBag.MenuNameForPhoneSize = Cache.Map_ConfigData["MenuNameForPhoneSize"];
            ViewBag.EnterNoticName = Cache.Map_ConfigData["EnterNoticName"];
            ViewBag.FirstNav = Cache.Map_ConfigData["FirstNav"]; 
            ViewBag.SecondNav = Cache.Map_ConfigData["SecondNav"];
            ViewBag.ThirdNav = Cache.Map_ConfigData["ThirdNav"];
            ViewBag.EnterNoticContentTitle = Cache.Map_ConfigData["EnterNoticContentTitle"];
            ViewBag.EnterNoticContent = Cache.Map_ConfigData["EnterNoticContent"];
            ViewBag.FooterFirstTitle = Cache.Map_ConfigData["FooterFirstTitle"];
            ViewBag.FooterFirstContent = Cache.Map_ConfigData["FooterFirstContent"];
            ViewBag.TableDownloadContent_left = Cache.Map_ConfigData["TableDownloadContent_left"];
            ViewBag.TableDownloadContent_right = Cache.Map_ConfigData["TableDownloadContent_right"];
            ViewBag.FirstNav_Title = Cache.Map_ConfigData["FirstNav_Title"];
            ViewBag.SecondNav_Title = Cache.Map_ConfigData["SecondNav_Title"];
            ViewBag.ThirdNav_Title = Cache.Map_ConfigData["ThirdNav_Title"];
            ViewBag.TableDownloadFileName = Cache.Map_ConfigData["TableDownloadFileName"];
            ViewBag.TableDownloadLink = Cache.Map_ConfigData["TableDownloadLink"];
            ViewBag.FourthNav = Cache.Map_ConfigData["FourthNav"];
            #endregion
            #region portfolios初始化
            int portfolioModalSum =  Convert.ToInt32(Cache.Map_ConfigData["portfolioModalSum"]);
            portfolioModal portfolio = new portfolioModal();
            portfolioModal[] portfolios = new portfolioModal[portfolioModalSum];
            for (int i = 1; i <= portfolioModalSum; i++)
            {
                portfolio.Name = "portfolioModal" + i.ToString();
                portfolio.ImgSrc = Cache.Map_ConfigData["portfolioImgSrc" + i.ToString()];
                portfolio.TitleInside= Cache.Map_ConfigData["portfolioTitleInside" + i.ToString()];
                portfolio.ImgSrcInside = Cache.Map_ConfigData["portfolioImgSrcInside" + i.ToString()];
                portfolio.paragraphInside = Cache.Map_ConfigData["portfolioparagraphInside" + i.ToString()];
                portfolio.LinkNameInside = Cache.Map_ConfigData["portfolioLinkNameInside" + i.ToString()];
                portfolio.LinkContentInside = Cache.Map_ConfigData["portfolioLinkContentInside" + i.ToString()];
                portfolio.LinkInside = Cache.Map_ConfigData["portfolioLinkInside" + i.ToString()];
                portfolios[i - 1] = DeepCopy<portfolioModal>(portfolio);//这里必须深拷贝
            }
            ViewBag.portfolios = portfolios;
            #endregion
            #region EnterForm初始化
            int EnterFormFieldSum =  Convert.ToInt32(Cache.Map_ConfigData["EnterFormFieldSum"]);
            EnterFormFieldModul EnterFormField = new EnterFormFieldModul();
            EnterFormFieldModul[] EnterFormFields = new EnterFormFieldModul[EnterFormFieldSum];
            for (int a = 1; a <= EnterFormFieldSum; a++)
            {
                EnterFormField.LabelName = "EnterFormField" + a.ToString();
                EnterFormField.TypeName = Cache.Map_ConfigData["EnterFormFieldTypeName" + a.ToString()];
                EnterFormField.PlaceholderString = Cache.Map_ConfigData["EnterFormFieldPlaceholderString" + a.ToString()];
                EnterFormField.Attribute = Cache.Map_ConfigData["EnterFormAttribute" + a.ToString()];
                EnterFormField.DisplayName = Cache.Map_ConfigData["EnterFormDisplayName" + a.ToString()];
                EnterFormFields[a - 1] = DeepCopy<EnterFormFieldModul>(EnterFormField);//这里必须深拷贝
            }
            ViewBag.EnterFormFields = EnterFormFields;
            #endregion
                return View();
        }

        /// <summary>
        /// 深拷贝，利用二进制序列化和反序列化实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T obj)
         {
             object retval;
             using (MemoryStream ms = new MemoryStream())
             {
                 BinaryFormatter bf = new BinaryFormatter();
                 //序列化成流
                 bf.Serialize(ms, obj);
                 ms.Seek(0, SeekOrigin.Begin);
                 //反序列化成对象
                 retval = bf.Deserialize(ms);
                 ms.Close();
             }
             return (T)retval;
         }

    }
}
