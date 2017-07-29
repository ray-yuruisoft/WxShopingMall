using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Web.Models;

namespace Yuruisoft.RS.Web.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        private IUserInfoService userInfoService;
        public LoginController(IUserInfoService _userInfoService)
        {
            userInfoService = _userInfoService;
        }

        public ActionResult Index()
        {
            CheckCookieInfo();//校验Cookie的信息
            return View();
        }
        #region 用户登录
        public ActionResult CheckLogin()
        {
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误!");
            }
            Session["validateCode"] = null;
            string requestCode = Request["vCode"];
            if (!requestCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
            var userInfo = userInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();//对用户名密码进行过滤.
            if (userInfo == null)
            {
                return Content("no:用户名或密码错误!");
            }
            else
            {
                Session["userInfo"] = userInfo;//低配版本直接存Session
                //string sessionId = Guid.NewGuid().ToString();//自己创建的SessionId,作为Memcache的key.
                //Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializerToString(userInfo));//将用户的信息存储到Memcache中。
                //Response.Cookies["sessionId"].Value = sessionId;//然后将自创的sessionId以Cookie的形式返回到浏览器，存储到浏览器端的内存中。
                //判断一下用户是否选择了记住我.
                if (!string.IsNullOrEmpty(Request["checkMe"]))
                {
                    HttpCookie cookie1 = new HttpCookie("cp1", userName);//用户名
                    HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.Md5String(Common.WebCommon.Md5String(userPwd)));//密码2次MD5加密，更安全
                    cookie1.Expires = DateTime.Now.AddDays(3);//记住多少天
                    cookie2.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                }
                return Content("ok:");
            }
        }
        #endregion
        #region 展示验证码.
        public ActionResult ValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion
        #region 找回密码.
        public ActionResult FindPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindPwd(FormCollection collection)
        {
            string txtName = Request["txtName"];
            string txtMail = Request["txtMail"];
            var userInfo_EMail = userInfoService.LoadEntities(u => u.UName == txtName).Select(c=>c.UEmail).FirstOrDefault();
            if (userInfo_EMail == txtMail)
            {
                string Vcode = Guid.NewGuid().ToString("N");
                SingleFindPSWcache.GetFindPSWcache().findPSWcache[txtName] = Vcode;

                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();//两个类，别混了，要引入System.Net这个Assembly
                mailMsg.From = new MailAddress("rayyewang01@163.com", "裕睿软件");//源邮件地址 。发件人地址.
                mailMsg.To.Add(new MailAddress("417853832@qq.com", "王睿"));//目的邮件地址。可以有多个收件人
                mailMsg.Subject = "找回密码的链接:";//发送邮件的标题 
                StringBuilder sb = new StringBuilder();
                sb.Append("找回密码的链接:");
                sb.Append("http;//" + Request.Url.Host.ToString() + ":4943/");
                sb.Append("公司裕睿");
                mailMsg.Body = sb.ToString();//发送邮件的内容 
                SmtpClient client = new SmtpClient("smtp.163.com");//smtp.163.com，smtp.qq.com.发件人的SMTP服务器的地址.
                client.Credentials = new NetworkCredential("rayyewang01@163.com", "wangrui1986");//发件人邮箱的用户和密码.
                client.Send(mailMsg);//排队发送邮件.

                return Content("ok:");
            }
            else
            {
                return Content("no:");
            }
        }
        #endregion
        #region 校验Cookie信息
        public void CheckCookieInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string cookieUserName = Request.Cookies["cp1"].Value;
                string cookieUserPwd = Request.Cookies["cp2"].Value;
                var userInfo = userInfoService.LoadEntities(u => u.UName == cookieUserName).FirstOrDefault();
                if (userInfo != null)
                {
                    //注意：要将用户密码加密以后写到用户表中，如果在添加是已经进行两次MD5运算，那么这里直接比较.
                    string md5Pwd = Common.WebCommon.Md5String(Common.WebCommon.Md5String(userInfo.UPwd));
                    if (md5Pwd == cookieUserPwd)
                    {
                        //string sessionId = Guid.NewGuid().ToString();//自己创建的SessionId,作为Memcache的key.
                        //Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializerToString(userInfo));//将用户的信息存储到Memcache中。
                        //Response.Cookies["sessionId"].Value = sessionId;//然后将自创的sessionId以Cookie的形式返回到浏览器，存储到浏览器端的内存中。
                        Session["userInfo"] = userInfo;//低配版本直接存Session
                        Response.Redirect("/Home/HomePage");
                    }
                }
                //删除Cookie.
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion
        #region 退出用户登录
        public ActionResult LoginOut()
        {
            if (Session["userInfo"] != null)
            {
                //string key = Request.Cookies["sessionId"].Value;
                //Common.MemcacheHelper.Delete(key);
                Session["userInfo"] = null;
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect("/Login/Index");
        }
        #endregion
    }
}
