using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Web.Models;
using Yuruisoft.RS.Common;

namespace Yuruisoft.RS.Web.Controllers
{
    public class LogOnController : Controller
    {
        //
        // GET: /LogOn/
        private IUserInfoService userInfoService;
        public LogOnController(IUserInfoService _userInfoService) 
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
        {//验证码初次不显示功能，实现为加缓存，缓存每10分钟清空一次。如果加到数据库会增加压力
            string userName = Request["LoginCode"];
            if (SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache.ContainsKey(userName))
            {
                short FailCount = SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[userName];
                if (FailCount > 3)
                {
                    #region 需要验证码,成功登陆需重置验证码
                    string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
                    if (string.IsNullOrEmpty(validateCode))
                    {
                        return Content("no:验证码错误！:" + FailCount.ToString());
                    }
                    Session["validateCode"] = null;
                    string requestCode = Request["vCode"];
                    if (!requestCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return Content("no:验证码错误！:" + FailCount.ToString());
                    }
                    //string userName = Request["LoginCode"];
                    string userPwd = Request["LoginPwd"];
                    var userInfo = userInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();//对用户名密码进行过滤.
                    if (userInfo == null)
                    {
                        return Content("no:用户名或密码错误！:" + FailCount.ToString());
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
                        SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[userName] = 0;
                        return Content("ok:");
                    }
                    #endregion
                }
                else
                {
                    SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[userName]++;
                    #region 不需要验证码,成功登陆需重置验证码
                    string userPwd = Request["LoginPwd"];
                    var userInfo = userInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();//对用户名密码进行过滤.
                    if (userInfo == null)
                    {
                        return Content("no:用户名或密码错误！:" + FailCount.ToString());
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
                        SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[userName] = 0;
                        return Content("ok:");
                    }
                    #endregion
                }
            }
            else
            {
                SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[userName] = 1;
                #region 不需要验证码,成功登陆需重置验证码
                string userPwd = Request["LoginPwd"];
                var userInfo = userInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();//对用户名密码进行过滤.
                if (userInfo == null)
                {
                    return Content("no:用户名或密码错误！:" + "1");
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
                    SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[userName] = 0;
                    return Content("ok:");
                }
                #endregion
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
        [HttpPost]
        public ActionResult FindPwd()
        {
            string txtName = Request["txtName"];
            string txtMail = Request["txtMail"];
            if (txtName == null || txtMail == null)
            { return Content("no:"); }
            var userInfo_EMail = userInfoService.LoadEntities(u => u.UName == txtName).Select(c => c.UEmail).FirstOrDefault();
            if (userInfo_EMail == txtMail)
            {
                string Vcode = Guid.NewGuid().ToString("N");
                SingleFindPSWcache.GetFindPSWcache().findPSWcache[txtName] = Vcode;



                //message.Body = mail.Body;//邮件内容
                //message.BodyEncoding = mail.BodyEncoding;//邮件采用的编码
                //message.Subject = mail.Subject;//邮件标题
                //message.SubjectEncoding = mail.SubjectEncoding;//主题内容使用编码
                //message.IsBodyHtml = mail.IsBodyHtml;//邮件格式



                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();//两个类，别混了，要引入System.Net这个Assembly
                mailMsg.Subject = "Yuruisoft综合管理系统-密码找回服务";//发送邮件的标题
                mailMsg.SubjectEncoding = System.Text.Encoding.GetEncoding("GB2312");
                mailMsg.IsBodyHtml = true;//采用HTML格式

                StringBuilder sb = new StringBuilder();

                #region HTML代码A段
                sb.Append(@"<style type='text/css'>.ReadMsgBody{width:100%;background-color:#fff}.ExternalClass{width:100%;background-color:#fff}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div{line-height:100%}html{width:100%}body{-webkit-text-size-adjust:none;-ms-text-size-adjust:none;margin:0;padding:0}table{border-spacing:0;border-collapse:collapse;table-layout:fixed;margin:0 auto}table table table{table-layout:auto}img{display:block!important}table td{border-collapse:collapse}.yshortcuts a{border-bottom:none!important}a{color:#91c444;text-decoration:none}.textbutton a{font-family:'open sans',arial,sans-serif!important;color:#fff!important}.text-link a{color:#3b3b3b!important}@media only screen and (max-width:640px){body{width:auto!important}table[class='table600']{width:450px!important}table[class='table-inner']{width:90%!important}table[class='table3-3']{width:100%!important;text-align:center!important}}@media only screen and (max-width:479px){body{width:auto!important}table[class='table600']{width:290px!important}table[class='table-inner']{width:82%!important}table[class='table3-3']{width:100%!important;text-align:center!important}}</style><table width='100%' border='0' align='center' cellpadding='0' cellspacing='0' bgcolor='#494c50'><tr><td align='center' background='http://www.yurusoft.net/Content/Yuruisoft.RS.Images/bg.jpg' style='background-size:cover; background-position:top;'><table class='table600' width='600' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td height='60'></td></tr><tr><td align='center'><table style='border-top:3px solid #91c444; border-radius:4px;box-shadow: 0px 3px 0px #bdc3c7;' bgcolor='#FFFFFF' width='100%' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td align='center'><table width='550' align='center' class='table-inner' border='0' cellspacing='0' cellpadding='0'><tr><td height='15'></td></tr><tr><td><!-- logo --><table class='table3-3' width='50' border='0' align='left' cellpadding='0' cellspacing='0'><tr><td align='center' style='line-height:0px;'><img style='display:block; line-height:0px; font-size:0px; border:0px;' src='http://www.yurusoft.net/Content/Images/login/logo.png' width='50' height='26' alt='logo' /></td></tr></table><!-- end logo --><!--Space--><table width='1' height='15' border='0' cellpadding='0' cellspacing='0' align='left'><tr><td height='15' style='font-size: 0;line-height: 0;border-collapse: collapse;'><p style='padding-left: 24px;'>&nbsp;</p></td></tr></table><!--End Space--><!-- detail --><table align='right' class='table3-3' width='160' border='0' cellspacing='0' cellpadding='0'><tr><td align='center' style='font-family: 'Open Sans', Arial, sans-serif; font-size:13px; color:#7f8c8d; line-height:30px;'><span style='font-weight: bold; color:#91c444;'>联系QQ：</span>11082929 </td></tr></table><!-- end detail --></td></tr><tr><td height='15'></td></tr></table></td></tr></table></td></tr><tr><td height='25'></td></tr><tr><td align='center'><table align='center' bgcolor='#FFFFFF' style='box-shadow: 0px 3px 0px #bdc3c7; border-radius:4px;' width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td align='center'><table align='center' class='table-inner' width='500' border='0' cellspacing='0' cellpadding='0'><tr><td height='50'></td></tr><!-- title --><tr><td align='center' style='font-family: 'Open Sans', Arial, sans-serif; font-size:30px; color:#3b3b3b; font-weight: bold; '>Yuruisoft综合管理系统-密码找回服务</td></tr><!-- end title --><tr><td align='center'><table width='25' border='0' cellspacing='0' cellpadding='0'><tr><td height='20' style='border-bottom:2px solid #91c444;'></td></tr></table></td></tr><tr><td height='20'></td></tr><!-- content --><tr><td align='center' style='font-family: 'Open Sans', Arial, sans-serif; font-size:13px; color:#7f8c8d; line-height:30px;'>密码修改页面跳转链接：");
                #endregion
                sb.Append("http://" + Request.Url.Host.ToString() + "/UpdatePassWord/ChangePwd?UserName=" + txtName + "&Vcode=" + Vcode);
                #region HTML代码B段
                sb.Append(@"</td></tr><!-- end content --></table></td></tr><tr><td height='40'></td></tr><!-- button --><tr><td align='center' bgcolor='#ecf0f1'><table align='center' class='table-inner' width='550' border='0' cellspacing='0' cellpadding='0'><tr><td height='30'></td></tr><tr><td align='center'><table class='textbutton' align='center' bgcolor='#91c444' border='0' cellspacing='0' cellpadding='0' style=' border-radius:30px; box-shadow: 0px 2px 0px #dedfdf;'><tr><td height='55' align='center' style='font-family: 'Open Sans', Arial, sans-serif; font-size:16px; color:#7f8c8d; line-height:30px; font-weight: bold;padding-left: 25px;padding-right: 25px;'><a href='");
                #endregion
                sb.Append("http://" + Request.Url.Host.ToString() + "/UpdatePassWord/ChangePwd?UserName=" + txtName + "&Vcode=" + Vcode);
                #region HTML代码C段
                sb.Append(@"'>点我去修改密码</a>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td height='30'></td>
											</tr>
										</table>
									</td>
								</tr>
								<!-- end button -->
							</table>
						</td>
					</tr>
					<tr>
						<td height='25'></td>
					</tr>
					<tr>
						<td>
							<!-- left -->

							<table bgcolor='#ecf0f1' style='box-shadow:0 3px 0 #bdc3c7;border-radius:4px;' class='table3-3' align='left' width='183' border='0' cellspacing='0' cellpadding='0'>
								<tr>
									<td class='text-link' height='50' align='center' style='font-family:'Open Sans',Arial,sans-serif;font-size:14px;color:#3b3b3b;line-height:30px;padding-left:20px;padding-right:20px;'>
										<a href='#'>其他帮助服务</a>
									</td>
								</tr>
							</table>

							<!-- end left -->

							<!--Space-->

							<table width='1' height='25' border='0' cellpadding='0' cellspacing='0' align='left'>
								<tr>
									<td height='25' style='font-size:0;line-height:0;border-collapse:collapse;'>
										<p style='padding-left:24px;'>&nbsp;</p>
									</td>
								</tr>
							</table>

							<!--End Space-->

							<!-- middle -->

							<table bgcolor='#ecf0f1' style='box-shadow:0 3px 0 #bdc3c7;border-radius:4px;' class='table3-3' align='left' width='183' border='0' cellspacing='0' cellpadding='0'>
								<tr>
									<td class='text-link' height='50' align='center' style='font-family:'Open Sans',Arial,sans-serif;font-size:14px;color:#3b3b3b;line-height:30px;padding-left:20px;padding-right:20px;'>
										<a href='#'>下订单</a>
									</td>
								</tr>
							</table>

							<!-- end middle -->

							<!--Space-->

							<table width='1' height='25' border='0' cellpadding='0' cellspacing='0' align='left'>
								<tr>
									<td height='25' style='font-size:0;line-height:0;border-collapse:collapse;'>
										<p style='padding-left:24px;'>&nbsp;</p>
									</td>
								</tr>
							</table>

							<!--End Space-->

							<!-- right -->

							<table bgcolor='#ecf0f1' style='box-shadow:0 3px 0 #bdc3c7;border-radius:4px;' class='table3-3' align='right' width='183' border='0' cellspacing='0' cellpadding='0'>
								<tr>
									<td class='text-link' height='50' align='center' style='font-family:'Open Sans',Arial,sans-serif;font-size:14px;color:#3b3b3b;line-height:30px;padding-left:20px;padding-right:20px;'>
										<a href='#'>关于我们</a>
									</td>
								</tr>
							</table>

							<!-- end right -->
						</td>
					</tr>
					<tr>
						<td height='20'></td>
					</tr>
					<tr>
						<td>
							<!-- left -->

							<table align='left' class='table3-3' width='390' border='0' cellspacing='0' cellpadding='0'>
								<tr>
									<td style='font-family:'Open Sans',Arial,sans-serif;font-size:12px;color:#ffffff;line-height:30px;'>
										&copy 2017
										<a href='https://www.yuruisoft.com'>Yuruisoft</a>
										. All Rights Reserved.
									</td>
								</tr>
							</table>

							<!-- end left -->

							<!--Space-->

							<table width='1' height='25' border='0' cellpadding='0' cellspacing='0' align='left'>
								<tr>
									<td height='25' style='font-size:0;line-height:0;border-collapse:collapse;'>
										<p style='padding-left:24px;'>&nbsp;</p>
									</td>
								</tr>
							</table>

							<!--End Space-->

							<table align='right' class='table3-3' width='184' border='0' cellspacing='0' cellpadding='0'>
								<tr>
									<td align='center' class='textbutton' style='font-family:'Open Sans',Arial,sans-serif;font-size:12px;color:#ffffff;line-height:30px;'>
										<a href='https://www.yuruisoft.com'>Yuruisoft.com</a>
										<span>&nbsp;&nbsp;&nbsp;&nbsp;//&nbsp;&nbsp;&nbsp;&nbsp;</span>
										<a href='http://www.yurusoft.net'>Yurusoft.net</a>
									</td>
								</tr>
							</table>
						</td>
					</tr>

					<!-- option -->

					<tr>
						<td height='60'></td></tr></table></td></tr></table>");
                
                #endregion
                mailMsg.Body = sb.ToString();//发送邮件的内容 
                mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");

                string errMessage = null;
                EmailCommon.QQEmailSend(mailMsg,"", txtMail,out errMessage);





             







                //string errMessage = null;
                //EmailCommon.sendmail("11082929@qq.com", "裕睿软件@Yuruisoft.com", userInfo_EMail, txtName, "Yuruisoft综合系统密码找回服务", sb.ToString(), "", "smtp.qq.com", "11082929@qq.com", "nuhurazjakkacbch", out errMessage);







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
            return Redirect("/LogOn/Index");
        }
        #endregion

    }
}
