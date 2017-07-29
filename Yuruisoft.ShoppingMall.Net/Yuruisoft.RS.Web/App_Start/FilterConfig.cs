using System.Web;
using System.Web.Mvc;

namespace Yuruisoft.RS.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new yuruisoft.oa.Web.Models.MyExceptionAttribute());//自定义的异常处理过滤器
        }
    }
}