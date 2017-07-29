using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Yuruisoft.RS.Common;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;

namespace Yuruisoft.RS.Web.Controllers.mathTool
{
    public class MathtoolController : Controller
    {
        //
        // GET: /Mathtool/

        private DbContext Db;
        public MathtoolController()//构造注入
        {
            Db = MathtoolDBFactory.CreateDbContext();
        }
        [HttpPost]
        public ActionResult Searchdeal(string Searchdata)//微信小程序公式页面处理方法
        {
            if (Searchdata != null)
            {
                if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
                {
                    return Content("forbid!");
                }
                #region 准确查询
               ArrayList results = new ArrayList();
               var result_a = Db.Set<math_level_data>().Where(c => (c.first_level == Searchdata) || (c.second_level == Searchdata) || (c.third_level == Searchdata) || (c.fourth_level == Searchdata) || (c.fifth_level == Searchdata) || (c.full_key == Searchdata)).OrderBy(c => c.order_all);
               foreach (var item in result_a)
               {
                  string[] Array = item.url.Replace("_url_", "#").Split('#');
                  results.Add(new//TODO:这里为了测试暂时用http而不用https
                  {
                      image_url = "http://" + Request.Url.Host.ToString() + ":4943/Mathtool/images/formula/" + Array[0] + "/" + item.url
                  });
               }
                #endregion
                #region 模糊查询
               if (results.Count == 0)
               {
                  var result_b = Db.Set<math_level_data>().Where(c => c.first_level.Contains(Searchdata) || c.second_level.Contains(Searchdata) || c.third_level.Contains(Searchdata) || c.fourth_level.Contains(Searchdata) || c.fifth_level.Contains(Searchdata) || c.full_key.Contains(Searchdata)).OrderBy(c => c.order_all);
                   foreach (var item in result_b)
                   {
                       string[] Array = item.url.Replace("_url_", "#").Split('#');
                       results.Add(new
                       {
                           image_url = "https://" + Request.Url.Host.ToString() + "/Mathtool/images/formula/" + Array[0] + item.url
                       });
                   }
               }
               #endregion
               if (results.Count > 0)
               {
                   return Json(new { results = results });
               }
               else
               {
                   return Json(new { error = true });
               }
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public ActionResult SearchKeydeal(string Searchkey)//微信小程序搜索关键字提醒处理方法
        {
            if (Searchkey != null)
            {
                if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
                {
                    return Content("forbid!");
                }
                ArrayList Search_data = new ArrayList();
                IQueryable<math_key_and_level> result;
                try//高并发场景，有可能出错
                {
                     result = Db.Set<math_key_and_level>().Where(c => c.keys.Contains(Searchkey)).OrderBy(c=>c.level);
                }
                catch (Exception)
                {
                    return Json(new { error = "NOTFOUND" });
                }
                int key_id = 0 ;
                foreach (var item in result)
                { 
                    key_id++;
                    Search_data.Add(new {
                        key_id= key_id,
                        key = item.keys
                    });
                }
                if (Search_data.Count > 0) {
                    return Json(new
                    {
                        Search_data = Search_data
                    });
                }
                return Json(new { error = "NOTFOUND" });
            }
            return Json(new { error = "NOTFOUND" });
        }
        [HttpPost]
        public ActionResult Abilitydeal(string id)//能力提升数据获取
        {
            if (id != null)
            {
                if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
                {
                    return Content("forbid!");
                }

                int idInDB = int.Parse(id);
                var result = Db.Set<math_AbilityContent>().Where(c => c.id == idInDB).FirstOrDefault();
                return Json(new
                {
                    txt_data = result.AbContent.Replace(@"\n","\n"),
                    url_gongshi = "http://" + Request.Url.Host.ToString() + ":4943/Mathtool/images/NLTS_XXSJ/NLTS_XXSJ_gongshi_" + result.id + ".gif",
                    url_daizhi_left = "http://" + Request.Url.Host.ToString() + ":4943/Mathtool/images/NLTS_XXSJ/NLTS_XXSJ_daizhi_left_" + result.id + ".gif",
                    url_daizhi_right = "http://" + Request.Url.Host.ToString() + ":4943/Mathtool/images/NLTS_XXSJ/NLTS_XXSJ_daizhi_right_" + result.id + ".gif"
                });
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public ActionResult Verificationdeal(string yuruisoft_session)//验证Third_session是否正确
        {
            if (yuruisoft_session != null)
            {
                if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
                {
                    return Content("forbid!");
                }
              var result = Db.Set<math_user_info>().Any(c => c.yuruisoft_session_key == yuruisoft_session);//确定是否存在
              if (result)
              {
                  return Json(new { exist = true });
              }
              else
              {
                  return Json(new { exist = false });
              }
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public ActionResult AbilityScoredeal(string score,string yuruisoft_session)//网络对战积分功能处理方法
        {
            if (yuruisoft_session != null)
            {
                if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
                {
                    return Content("forbid!");
                }
                var result = Db.Set<math_user_info>().Any(c => c.yuruisoft_session_key == yuruisoft_session);//确定是否存在
                if (result)
                {
                    var currentUser = Db.Set<math_user_info>().Where(c => c.yuruisoft_session_key == yuruisoft_session).FirstOrDefault();
                    math_user_info Temp = Db.Set<math_user_info>().Find(currentUser.openId);
                    if (Temp.math_score != null)
                    {
                        if(score == "1")
                            Temp.math_score = (int.Parse(Temp.math_score) + 1).ToString();
                        else
                            Temp.math_score = (int.Parse(Temp.math_score) - 1).ToString();
                    }
                    else
                    { 
                        if(score == "1")
                            Temp.math_score = "1";
                        else
                            Temp.math_score = "-1";
                    }

                    if (Db.SaveChanges() > 0)
                        return Json(new { UpdateDB = true });
                    else
                        return Json(new { UpdateDB = false });
                }
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public ActionResult ScoreQuery(string yuruisoft_session)//查询网络对战得分
        {
            if (yuruisoft_session != null)
            {
                if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
                {
                    return Content("forbid!");
                }
                var result = Db.Set<math_user_info>().Where(c => c.yuruisoft_session_key == yuruisoft_session).FirstOrDefault();
                if (result != null)
                {
                    if (result.math_score == null)
                        return Json(new { score = 0 });
                    else
                        return Json(new { score = result.math_score });
                }
                else
                {
                    return Json(new { error = false });
                }
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public ActionResult FeedBackDeal(string yuruisoft_session, string Feed_back_words, string phoneNum, string QQ, string Email)//记录留言
        {
            if (yuruisoft_session != null)
            {
                if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
                {
                    return Content("forbid!");
                }
                var result = Db.Set<math_user_info>().Where(c => c.yuruisoft_session_key == yuruisoft_session).FirstOrDefault();
                if (result != null)
                {
                    math_FeedBackInfo currentFeedBack = new math_FeedBackInfo();
                    currentFeedBack.openId = result.openId;
                    currentFeedBack.nickName = result.nickName;
                    currentFeedBack.qq = long.Parse(QQ);
                    currentFeedBack.phoneNum = long.Parse(phoneNum);
                    currentFeedBack.Email = Email;
                    currentFeedBack.FeedBackWords = Feed_back_words;
                    currentFeedBack.SubTime = DateTime.Now;

                    Db.Set<math_FeedBackInfo>().Add(currentFeedBack);
                    if (Db.SaveChanges() > 0)
                    {
                        return Json(new { updateDB = true });
                    }
                    else
                    {
                        return Json(new { updateDB = false });
                    }
                }
                else
                {
                    return Json(new { error = false });
                }
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public ActionResult SourceBuyList(int page, int pageSize, string search_LIKE_title)//资源购买页面清单
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            var Results = Db.Set<math_SourceBuy>().Where(c => true).OrderBy(c => c.Sort).Skip<math_SourceBuy>((page - 1) * pageSize).Take<math_SourceBuy>(pageSize);
            ArrayList results = new ArrayList();
            foreach (var item in Results)
            {
                results.Add(
                    new
                    {
                        id = item.id,
                        SBTitleImage =  item.SBTitleImage,
                        SBTitle = item.SBTitle,
                        ModiyTime = item.ModiyTime.ToString()
                    }
                    );
            }
            return Json(results);
        }
        [HttpPost]
        public ActionResult SourceBuyFindOne(int id)//资源购买页面单项展示
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            var Results = Db.Set<math_SourceBuy>().Where(c => c.id == id).FirstOrDefault();

            return Json(
                    new {
                        id = Results.id,
                        SBTitle = Results.SBTitle,
                        SBContent = Results.SBContent.Replace(@"\n","\n"),
                        SBTitleImage = Results.SBTitleImage,
                        ModiyTime = Results.ModiyTime.ToString(),
                        TotalFee = Results.TotalFee,
                        SurportDetail = Results.SurportDetail,
                        SurportTitle = Results.SurportTitle,
                        WxBody =  Results.WxBody
                    }
                );
        }
        [HttpPost]
        public ActionResult SourceBuyPay(int id,string yuruisoft_session,string Email)//资源购买微信支付
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }

            if (yuruisoft_session != null)
            {
                var CurrentPay = Db.Set<math_SourceBuy>().Where(c => c.id == id).FirstOrDefault();
                if (CurrentPay != null)
                {
                    var userinfo = Db.Set<math_user_info>().Where(c => c.yuruisoft_session_key == yuruisoft_session).FirstOrDefault();
                    //获取配置文件
                    string key = System.Web.Configuration.WebConfigurationManager.AppSettings["wx_key"].ToString();
                    string wx_mch_id = System.Web.Configuration.WebConfigurationManager.AppSettings["wx_mch_id"].ToString();
                    string notify_url = "https://" + Request.Url.Host.ToString() + "Mathtool/SourceBuyNotify/";
                    //"http://lifark.com/notify_url.aspx"
                    var results = WxExtensionClass.Wxpay(CurrentPay.WxBody, CurrentPay.TotalFee.ToString(), notify_url, userinfo.appid, wx_mch_id, userinfo.openId, key);

                    if (results != null)
                    {
                        math_SourceBuyOrderstatus order = new math_SourceBuyOrderstatus();
                        order.orderName = results["wx_out_trade_no"];
                        order.orderStatus = (int)Notifystatus.PlaceOrder;
                        order.sourceBuyId = CurrentPay.id;
                        order.email = Email;
                        Db.Set<math_SourceBuyOrderstatus>().Add(order);
                        if (Db.SaveChanges() > 0)
                        {
                            return Json(results);
                        }
                    }
                }
            }
            return Json(new
            {
                error = true
            });
        }
        [HttpPost]
        public ActionResult SourceBuySendEmail(int id,string orderName)//客户端收到回调确认支付成功后，请求发送邮件
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            var SendInfo = Db.Set<math_SourceBuyOrderstatus>().Where(c => c.orderName == orderName).FirstOrDefault();
            if (SendInfo.orderStatus == (int)Notifystatus.PlaceOrder)
            {
                math_SourceBuyOrderstatus Temp = Db.Set<math_SourceBuyOrderstatus>().Find(SendInfo.id);
                Temp.orderStatus = (int)Notifystatus.PayNoSend;
                if (Db.SaveChanges() > 0)
                { 
                  #region 发送邮件代码
                    var currentSource = Db.Set<math_SourceBuy>().Where(c => c.id == id).FirstOrDefault();//附件查询
                    string path = System.Web.HttpContext.Current.Server.MapPath("/App_Data/SourceBuy/" + currentSource.SourceUrl);//附件地址
                    System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();//两个类，别混了，要引入System.Net这个Assembly
                    mailMsg.Subject = "裕睿数学小工具资源购买邮件";//发送邮件的标题
                    mailMsg.SubjectEncoding = System.Text.Encoding.GetEncoding("GB2312");
                    mailMsg.IsBodyHtml = true;//采用HTML格式

                    StringBuilder sb = new StringBuilder();

                    sb.Append("您要的资源已添加到附件，敬请下载！");

                    mailMsg.Body = sb.ToString();//发送邮件的内容 
                    mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");

                    string errMessage = null;
                    if (EmailCommon.QQEmailSend(mailMsg, path, Temp.email, out errMessage))
                    {
                        return Json(new
                        {
                            error = false
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            error = true
                        });
                    }
                    #endregion
                }
            }
            return Json(new
            {
                error = true
            });
        }
        [HttpPost]
        public ActionResult SourceBuyTestEmail(string Email)//客户端测试邮件是否能正常到达用户
        {
            System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.Subject = "裕睿数学小工具测试邮件";//发送邮件的标题
            mailMsg.SubjectEncoding = System.Text.Encoding.GetEncoding("GB2312");
            mailMsg.IsBodyHtml = true;//采用HTML格式

            StringBuilder sb = new StringBuilder();

            sb.Append("这是测试邮件，若能收到此邮件，代表能正常收到邮件");

            mailMsg.Body = sb.ToString();//发送邮件的内容 
            mailMsg.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");

            string errMessage = null;
            if (EmailCommon.QQEmailSend(mailMsg, "", Email, out errMessage))
            {
                return Json(new
                {
                    error = false
                });
            }
            else {
                return Json(new
                {
                    error = true
                });
            }
        }


        public ActionResult Test()//模仿微信服务端做测试！
        {
            #region 发post请求   

            var request = (HttpWebRequest)WebRequest.Create("http://localhost:4943/Mathtool/SourceBuyNotify");

            var postData ="<xml><appid><![CDATA[wx2421b1c4370ec43b]]></appid><attach><![CDATA[支付测试]]></attach><bank_type><![CDATA[CFT]]></bank_type><fee_type><![CDATA[CNY]]></fee_type><is_subscribe><![CDATA[Y]]></is_subscribe><mch_id><![CDATA[10000100]]></mch_id><nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str><openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid><out_trade_no><![CDATA[1409811653]]></out_trade_no><result_code><![CDATA[SUCCESS]]></result_code><return_code><![CDATA[SUCCESS]]></return_code><sign><![CDATA[B552ED6B279343CB493C5DD0D78AB241]]></sign><sub_mch_id><![CDATA[10000100]]></sub_mch_id><time_end><![CDATA[20140903131540]]></time_end><total_fee>1</total_fee><trade_type><![CDATA[JSAPI]]></trade_type><transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id></xml>";

            var data = Encoding.UTF8.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            #endregion

            return Content("发送成功！");
        }

        /*
          回调确认分三步走：
         * 1、请求SourceBuyPay函数后，插入用户订单信息，状态为下单
         * 2、微信小程序收到签名后，用户确认，待微信服务器回调以后，状态置为已支付未收到回调（微信小程序支付操作成功以后，直接发邮件。防止微信支付成功不回调的坑）
         * 3、收到微信支付成功的回调以后，状态置为已支付已收到回调。（对任何其他的回调直接回复）
         */
        [HttpPost]
        public ActionResult SourceBuyNotify()//回调确认函数
        {
            //签名正确   处理订单操作逻辑
            String xmlData = getPostStr();//获取请求数据
            if (xmlData == "")
            {
               // Log.Error(this.GetType().ToString(), "xmlData返回为空");
                return Content("xmlData返回为空");
            }
            else
            {
               // Log.Info(this.GetType().ToString(), "xmlData返回不为空");
                #region 1、成功状态码
                var dic = new Dictionary<string, string>
                {
                    {"return_code", "SUCCESS"},
                    {"return_msg","OK"}
                };
                var sb = new StringBuilder();
                sb.Append("<xml>");
                foreach (var d in dic)
                {
                    sb.Append("<" + d.Key + ">" + d.Value + "</" + d.Key + ">");
                }
                sb.Append("</xml>");
                #endregion
                #region 2、本地数据库错误状态码
                var dicDBF = new Dictionary<string, string>
                                            {
                                                {"return_code", "FAIL"},
                                                {"return_msg","本地数据库错误"}
                                            };
                var sbDBF = new StringBuilder();
                sbDBF.Append("<xml>");
                foreach (var d in dicDBF)
                {
                    sbDBF.Append("<" + d.Key + ">" + d.Value + "</" + d.Key + ">");
                }
                sbDBF.Append("</xml>");
                #endregion

                //把数据重新返回给客户端

                DataSet ds = new DataSet();
                StringReader stram = new StringReader(xmlData);
                XmlTextReader datareader = new XmlTextReader(stram);
                ds.ReadXml(datareader);
                if (ds.Tables[0].Rows[0]["return_code"].ToString() == "SUCCESS")
                {
                    #region 字段初始化
                    string wx_appid = "";//微信开放平台审核通过的应用APPID
                    string wx_mch_id = "";//微信支付分配的商户号

                    string wx_nonce_str = "";// 	随机字符串，不长于32位
                    string wx_sign = "";//签名，详见签名算法
                    string wx_result_code = "";//SUCCESS/FAIL

                    string wx_return_code = "";
                    string wx_openid = "";//用户在商户appid下的唯一标识
                    string wx_is_subscribe = "";//用户是否关注公众账号，Y-关注，N-未关注，仅在公众账号类型支付有效
                    string wx_trade_type = "";// 	APP
                    string wx_bank_type = "";// 	银行类型，采用字符串类型的银行标识，银行类型见银行列表
                    string wx_fee_type = "";// 	货币类型，符合ISO4217标准的三位字母代码，默认人民币：CNY，其他值列表详见货币类型


                    string wx_transaction_id = "";//微信支付订单号
                    string wx_out_trade_no = "";//商户系统的订单号，与请求一致。
                    string wx_time_end = "";// 	支付完成时间，格式为yyyyMMddHHmmss，如2009年12月25日9点10分10秒表示为20091225091010。其他详见时间规则
                    int wx_total_fee = -1;// 	订单总金额，单位为分
                    int wx_cash_fee = -1;//现金支付金额订单现金支付金额，详见支付金额
                    #endregion
                    #region  数据解析
                    //列 是否存在
                    string signstr = "";//需要前面的字符串
                    //wx_appid
                    if (ds.Tables[0].Columns.Contains("appid"))
                    {
                        wx_appid = ds.Tables[0].Rows[0]["appid"].ToString();
                        if (!string.IsNullOrEmpty(wx_appid))
                        {
                            signstr += "appid=" + wx_appid;
                        }
                    }
                    //wx_bank_type
                    if (ds.Tables[0].Columns.Contains("bank_type"))
                    {
                        wx_bank_type = ds.Tables[0].Rows[0]["bank_type"].ToString();
                        if (!string.IsNullOrEmpty(wx_bank_type))
                        {
                            signstr += "&bank_type=" + wx_bank_type;
                        }
                    }
                    //wx_cash_fee
                    if (ds.Tables[0].Columns.Contains("cash_fee"))
                    {
                        wx_cash_fee = Convert.ToInt32(ds.Tables[0].Rows[0]["cash_fee"].ToString());

                        signstr += "&cash_fee=" + wx_cash_fee;
                    }

                    //wx_fee_type
                    if (ds.Tables[0].Columns.Contains("fee_type"))
                    {
                        wx_fee_type = ds.Tables[0].Rows[0]["fee_type"].ToString();
                        if (!string.IsNullOrEmpty(wx_fee_type))
                        {
                            signstr += "&fee_type=" + wx_fee_type;
                        }
                    }

                    //wx_is_subscribe
                    if (ds.Tables[0].Columns.Contains("is_subscribe"))
                    {
                        wx_is_subscribe = ds.Tables[0].Rows[0]["is_subscribe"].ToString();
                        if (!string.IsNullOrEmpty(wx_is_subscribe))
                        {
                            signstr += "&is_subscribe=" + wx_is_subscribe;
                        }
                    }

                    //wx_mch_id
                    if (ds.Tables[0].Columns.Contains("mch_id"))
                    {
                        wx_mch_id = ds.Tables[0].Rows[0]["mch_id"].ToString();
                        if (!string.IsNullOrEmpty(wx_mch_id))
                        {
                            signstr += "&mch_id=" + wx_mch_id;
                        }
                    }

                    //wx_nonce_str
                    if (ds.Tables[0].Columns.Contains("nonce_str"))
                    {
                        wx_nonce_str = ds.Tables[0].Rows[0]["nonce_str"].ToString();
                        if (!string.IsNullOrEmpty(wx_nonce_str))
                        {
                            signstr += "&nonce_str=" + wx_nonce_str;
                        }
                    }

                    //wx_openid
                    if (ds.Tables[0].Columns.Contains("openid"))
                    {
                        wx_openid = ds.Tables[0].Rows[0]["openid"].ToString();
                        if (!string.IsNullOrEmpty(wx_openid))
                        {
                            signstr += "&openid=" + wx_openid;
                        }
                    }

                    //wx_out_trade_no
                    if (ds.Tables[0].Columns.Contains("out_trade_no"))
                    {
                        wx_out_trade_no = ds.Tables[0].Rows[0]["out_trade_no"].ToString();
                        if (!string.IsNullOrEmpty(wx_out_trade_no))
                        {
                            signstr += "&out_trade_no=" + wx_out_trade_no;
                        }
                    }

                    //wx_result_code 
                    if (ds.Tables[0].Columns.Contains("result_code"))
                    {
                        wx_result_code = ds.Tables[0].Rows[0]["result_code"].ToString();
                        if (!string.IsNullOrEmpty(wx_result_code))
                        {
                            signstr += "&result_code=" + wx_result_code;
                        }
                    }

                    //wx_result_code 
                    if (ds.Tables[0].Columns.Contains("return_code"))
                    {
                        wx_return_code = ds.Tables[0].Rows[0]["return_code"].ToString();
                        if (!string.IsNullOrEmpty(wx_return_code))
                        {
                            signstr += "&return_code=" + wx_return_code;
                        }
                    }

                    //wx_sign 
                    if (ds.Tables[0].Columns.Contains("sign"))
                    {
                        wx_sign = ds.Tables[0].Rows[0]["sign"].ToString();
                        if (!string.IsNullOrEmpty(wx_sign))
                        {
                            signstr += "&sign=" + wx_sign;
                        }
                    }

                    //wx_time_end
                    if (ds.Tables[0].Columns.Contains("time_end"))
                    {
                        wx_time_end = ds.Tables[0].Rows[0]["time_end"].ToString();
                        if (!string.IsNullOrEmpty(wx_time_end))
                        {
                            signstr += "&time_end=" + wx_time_end;
                        }
                    }

                    //wx_total_fee
                    if (ds.Tables[0].Columns.Contains("total_fee"))
                    {
                        wx_total_fee = Convert.ToInt32(ds.Tables[0].Rows[0]["total_fee"].ToString());

                        signstr += "&total_fee=" + wx_total_fee;
                    }

                    //wx_trade_type
                    if (ds.Tables[0].Columns.Contains("trade_type"))
                    {
                        wx_trade_type = ds.Tables[0].Rows[0]["trade_type"].ToString();
                        if (!string.IsNullOrEmpty(wx_trade_type))
                        {
                            signstr += "&trade_type=" + wx_trade_type;
                        }
                    }

                    //wx_transaction_id
                    if (ds.Tables[0].Columns.Contains("transaction_id"))
                    {
                        wx_transaction_id = ds.Tables[0].Rows[0]["transaction_id"].ToString();
                        if (!string.IsNullOrEmpty(wx_transaction_id))
                        {
                            signstr += "&transaction_id=" + wx_transaction_id;
                        }
                    }
                    #endregion

                    //追加key 密钥
                    signstr += "&key=" + System.Web.Configuration.WebConfigurationManager.AppSettings["wx_key"].ToString();
                    //商户订单号
                    string orderStrwhere = "ordernumber='" + wx_out_trade_no + "'";

                    #region MD5加密
                                        // Log.Info(this.GetType().ToString(), "开始验证MD5");
                    //MD5加密
                    //sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sign, "MD5").ToUpper(); //过时
                    MD5 md5 = MD5.Create();
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(signstr);
                    byte[] md5Buffer = md5.ComputeHash(buffer);
                    StringBuilder sb_Second = new StringBuilder();
                    foreach (byte b in md5Buffer)
                    {
                        sb_Second.Append(b.ToString("x2"));
                    }
                    #endregion
                    if (wx_sign == sb_Second.ToString().ToUpper())
                    {
                       // Log.Info(this.GetType().ToString(), "MD5正确");
                        //签名正确   处理订单操作逻辑
                        if (Db.Set<math_SourceBuyOrderstatus>().Any(c => c.orderName == ds.Tables[0].Rows[0]["wx_out_trade_no"].ToString()))
                        {
                            var CurrentStatus = Db.Set<math_SourceBuyOrderstatus>().Where(c => c.orderName == ds.Tables[0].Rows[0]["wx_out_trade_no"].ToString()).FirstOrDefault();
                            if (CurrentStatus.orderStatus == (int)Notifystatus.PayAndSend)//重复广播的情况
                            {
                                return Content(sb.ToString());
                            }

                            math_SourceBuyOrderstatus Temp = Db.Set<math_SourceBuyOrderstatus>().Find(CurrentStatus.id);
                            Temp.orderStatus = (int)Notifystatus.PayAndSend;
                            if (Db.SaveChanges() == 0)//数据库出错
                            {
                                return Content(sbDBF.ToString());//返回错误信息
                            }
                        }
                        else//数据库出错
                        {
                            return Content(sbDBF.ToString());//返回错误信息
                        }
                    }
                    else//MD5验证失败
                    {
                        #region 3、MD5错误状态码
                        var dicMD5 = new Dictionary<string, string>
                                            {
                                                {"return_code", "FAIL"},
                                                {"return_msg","签名失败"}
                                            };
                        var sbMD5 = new StringBuilder();
                        sbDBF.Append("<xml>");
                        foreach (var d in dicMD5)
                        {
                            sbMD5.Append("<" + d.Key + ">" + d.Value + "</" + d.Key + ">");
                        }
                        sbMD5.Append("</xml>");
                        #endregion
                        return Content(sbMD5.ToString());//返回错误信息
                        //追加备注信息
                       // Log.Error(this.GetType().ToString(), "MD5错误");
                    }
                }
                else//return_code不是成功的情况
                {
                    // 返回信息，如非空，为错误原因  签名失败 参数格式校验错误
                    string return_msg = ds.Tables[0].Rows[0]["return_msg"].ToString();
                    #region 4、状态码直接返回
                    var dicDir = new Dictionary<string, string>
                                            {
                                                {"return_code", "FAIL"},
                                                {"return_msg",return_msg}
                                            };
                    var sbDir = new StringBuilder();
                    sbDir.Append("<xml>");
                    foreach (var d in dicDir)
                    {
                        sbDir.Append("<" + d.Key + ">" + d.Value + "</" + d.Key + ">");
                    }
                    sbDir.Append("</xml>");
                    #endregion
                    return Content(sbDir.ToString());
                }
                return Content(sb.ToString());
            }
        }

        //获得Post过来的数据
        public string getPostStr()
        {
            Int32 intLen = Convert.ToInt32(Request.InputStream.Length);
            byte[] b = new byte[intLen];
            Request.InputStream.Read(b, 0, intLen);
            return System.Text.Encoding.UTF8.GetString(b);
        }

        /*
         微信小程序登陆流程：
         1、code由wx.login(OBJECT)获取，其余均由wx.getUserInfo(OBJECT)获取.参数含义,见参数说明.
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">用户允许登录后，回调内容会带上 code（有效期五分钟），开发者需要将 code 发送到开发者服务器后台，使用code 换取 session_key api，将 code 换成 openid 和 session_key</param>
        /// <param name="encryptedData">包括敏感数据在内的完整用户信息的加密数据,详细见加密数据解密算法.https://mp.weixin.qq.com/debug/wxadoc/dev/api/signature.html</param>
        /// <param name="iv">加密算法的初始向量，详细见加密数据解密算法.https://mp.weixin.qq.com/debug/wxadoc/dev/api/signature.html</param>
        /// <param name="raw">不包括敏感信息的原始数据字符串，用于计算签名.</param>
        /// <param name="signature">使用 sha1( rawData + sessionkey ) 得到字符串，用于校验用户信息，参考文档 signature.https://mp.weixin.qq.com/debug/wxadoc/dev/api/signature.html</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logindeal(string code,string encryptedData,string iv,string raw,string signature)//微信登陆处理
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            if (code == null || encryptedData == null || iv == null || raw == null || signature == null)
            {
                return Json(new { error = true });
            }

            #region 1、发送code到微信服务器换取session_key和openid
            var request = (HttpWebRequest)WebRequest.Create("https://api.weixin.qq.com/sns/jscode2session?appid=wxa8474342da670622&secret=bf3c4a8a4eb44793b33709ac223d8cc7&js_code=" + code + "&grant_type=authorization_code");
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            string jsonText = responseString;//此处为json序列化
            JObject jo = JObject.Parse(jsonText);
            string session_key = jo["session_key"].ToString();//得到key
            string openid = jo["openid"].ToString();          //得到id
            #endregion
            #region 2、解码获取用户信息，需要第一步的session_key
            //解码！！想要获取unionid就必须要把小程序和公众号绑定到同一开发平台才能获取到，暂时预留！！！！
            jo = (JObject)JsonConvert.DeserializeObject(WxExtensionClass.DecodeUserInfo(raw, signature, encryptedData, iv, session_key));
            #endregion
            #region 3、更新数据库（如果是新用户，插入到数据库，如果是老用户，更新session_key)
            math_user_info DataFromwx = new math_user_info();
            #region 二次登陆需要的数据
            DataFromwx.openId = jo["openId"].ToString();                               //用户openId
            DataFromwx.session_key = session_key;                                      //微信返回session_key
            DataFromwx.nickName = jo["nickName"].ToString();                           //用户昵称
            DataFromwx.gender = jo["gender"].ToString();                               //用户性别
            DataFromwx.language = jo["language"].ToString();                           //用户语言
            DataFromwx.city = jo["city"].ToString();                                   //用户城市
            DataFromwx.province = jo["province"].ToString();                           //用户省份
            DataFromwx.country = jo["country"].ToString();                             //用户国家
            DataFromwx.avatarUrl = jo["avatarUrl"].ToString();                         //用户头像地址
            DataFromwx.watermark_timestamp = jo["watermark"]["timestamp"].ToString();  //微信返回水印时间戳
            if (jo.Property("unionId") != null)//这里预留字段，必须公众平台和微信小程序合一了才会有这个值
                DataFromwx.unionId = jo["unionId"].ToString();
            DataFromwx.yuruisoft_session_key = Guid.NewGuid().ToString("N");           //自定义的ThirdSessionKey
            DataFromwx.ModifiedOn = DateTime.Now;                                      //用户最近登陆时间
            #endregion
            var CurrentUser = Db.Set<math_user_info>().Where(c => c.openId == DataFromwx.openId).FirstOrDefault();
            if (CurrentUser == null)//判断为首次登陆
            {
                #region 首次登陆独有数据
                DataFromwx.SubTime = DateTime.Now;                                         //首次登陆时间
                DataFromwx.appid = jo["watermark"]["appid"].ToString();
                int? Max = Db.Set<math_user_info>().Where(c => true).Max(c => c.Sort);     //序列
                if (Max == null)
                {
                    DataFromwx.Sort = 1;
                }
                else
                {
                    DataFromwx.Sort = Max + 1;
                }
                #endregion
                Db.Set<math_user_info>().Add(DataFromwx);
            }
            else
            {
                math_user_info CurrentUserInDB = Db.Set<math_user_info>().Find(CurrentUser.openId);
                if (CurrentUserInDB == null)//异常，作为首次登陆
                {
                    #region 首次登陆独有数据
                    DataFromwx.openId = jo["openId"].ToString();                               //用户openId
                    DataFromwx.SubTime = DateTime.Now;                                         //首次登陆时间
                    DataFromwx.appid = jo["watermark"]["appid"].ToString();
                    int? Max = Db.Set<math_user_info>().Where(c => true).Max(c => c.Sort);     //序列
                    if (Max == null)
                    {
                        DataFromwx.Sort = 1;
                    }
                    else
                    {
                        DataFromwx.Sort = Max + 1;
                    }
                    #endregion
                    Db.Set<math_user_info>().Add(DataFromwx);
                }
                else//其余判断为二次登陆
                {
                    CurrentUserInDB.session_key = DataFromwx.session_key;
                    CurrentUserInDB.nickName = DataFromwx.nickName;//其他值可能用户会改变
                    CurrentUserInDB.gender = DataFromwx.gender;
                    CurrentUserInDB.language = DataFromwx.language;
                    CurrentUserInDB.city = DataFromwx.city;
                    CurrentUserInDB.province = DataFromwx.province;
                    CurrentUserInDB.country = DataFromwx.country;
                    CurrentUserInDB.avatarUrl = DataFromwx.avatarUrl;
                    CurrentUserInDB.watermark_timestamp = DataFromwx.watermark_timestamp;

                    CurrentUserInDB.ModifiedOn = DataFromwx.ModifiedOn;
                    CurrentUserInDB.yuruisoft_session_key = DataFromwx.yuruisoft_session_key;
                }
            }
              if (Db.SaveChanges() > 0)
              {
                  return Json(new { rd_session = DataFromwx.yuruisoft_session_key });
              }
              else
              {
                  return Json(new { Update = false });
              }
            #endregion
        }
        /// <summary>
        /// 微信支付开发流程 https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=7_3&index=1 
        /// </summary>
        /// <param name="yuruisoft_seesion"></param>
        /// <param name="total_fee"></param>
        /// <param name="wx_body"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Wxpaydeal(string yuruisoft_seesion,string total_fee,string wx_body)//TODO：回调未处理，微信支付处理
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            if (yuruisoft_seesion != null && total_fee != null && wx_body != null)
            {
                var userinfo = Db.Set<math_user_info>().Where(c => c.yuruisoft_session_key == yuruisoft_seesion).FirstOrDefault();
                //获取配置文件
                string key = System.Web.Configuration.WebConfigurationManager.AppSettings["wx_key"].ToString();
                string wx_mch_id = System.Web.Configuration.WebConfigurationManager.AppSettings["wx_mch_id"].ToString();
                return Json(WxExtensionClass.Wxpay(wx_body, total_fee, "http://lifark.com/notify_url.aspx", userinfo.appid, wx_mch_id, userinfo.openId, key));
            }
            return Json(new { error = true });
        }
        public static class WxExtensionClass//扩展函数集，主要针对登录和微信支付的，可加密为动态库
        {
            /// <summary>
            /// 微信支付处理函数
            /// </summary>
            /// <param name="wx_bodyCode">商品详情</param>
            /// <param name="total_fee">商品价格</param>
            /// <param name="wx_notify_url">回调网址</param>
            /// <param name="wx_appid">商户appid</param>
            /// <param name="wx_mch_id">商户mchid</param>
            /// <param name="openId">用户openId</param>
            /// <returns>回调Json</returns>
            public static Dictionary<string, string> Wxpay(string wx_bodyCode, string total_fee, string wx_notify_url, string wx_appid, string wx_mch_id, string openId, string wx_key)//下单网站，注意必填参数 https://pay.weixin.qq.com/wiki/doc/api/wxa/wxa_api.php?chapter=9_1
            {
                #region 1、支付数据格式化
                string wx_nonce_str = GetRandomString(20);//随机字符串，微信只支持不长于32位
                byte[] buffer = Encoding.UTF8.GetBytes(wx_bodyCode); //将商品详情转换为字节数组
                string wx_body = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                string wx_out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmss") + GetRandomString(10);//商户系统内部的订单号,32个字符内、可包含字母, 其他说明见商户订单号
                string wx_total_fee = total_fee;//订单总金额，单位为分
                string wx_spbill_create_ip = GetWebClientIp();//客户端IP
                string wx_trade_type = "JSAPI";//支付类型


                /**测试***测试环境使用IP*/

                 //wx_spbill_create_ip = "182.149.203.40";
                /**测试强制赋值*/


                var dic = new Dictionary<string, string>
                {
                    {"appid",wx_appid},                         //商户appid
                    {"body",wx_body},                           //商品详情
                    {"mch_id",wx_mch_id},                       //商户mchid
                    {"nonce_str",wx_nonce_str},                 //随机字符串，微信只支持不长于32位
                    {"notify_url",wx_notify_url},               //异步通知的地址，不能带参数
                    {"openid",openId},                          //用户openId
                    {"out_trade_no",wx_out_trade_no},           //商户系统内部的订单号,32个字符内、可包含字母, 其他说明见商户订单号
                    {"spbill_create_ip",wx_spbill_create_ip},   //客户端IP
                    {"total_fee",wx_total_fee},                 //商品价格
                    {"trade_type",wx_trade_type}                //支付类型
                };
                dic.Add("sign", GetSignString(dic, wx_key));            //字典信息签名
                #endregion
                #region 2、请求XML，第一次签名，统一下单步骤
                var sb = new StringBuilder();
                sb.Append("<xml>");
                foreach (var d in dic)
                {
                    sb.Append("<" + d.Key + ">" + d.Value + "</" + d.Key + ">");
                }
                sb.Append("</xml>");
                var xml = new XmlDocument();
                //CookieCollection coo = new CookieCollection();
                Encoding en = Encoding.GetEncoding("UTF-8");
                HttpWebResponse response = CreatePostHttpResponse("https://api.mch.weixin.qq.com/pay/unifiedorder", sb.ToString(), en);
                #endregion
                #region 3、XML换回值处理，返回JSON，第二次签名，由小程序端确认订单
                //打印返回值
                Stream stream = response.GetResponseStream();//获取响应的字符串流
                StreamReader sr = new StreamReader(stream);//创建一个stream读取流
                string html = sr.ReadToEnd();//从头读到尾，放到字符串html

                DataSet ds = new DataSet();
                StringReader stram = new StringReader(html);
                XmlTextReader reader = new XmlTextReader(stram);
                ds.ReadXml(reader);
                string return_code = ds.Tables[0].Rows[0]["return_code"].ToString();
                if (return_code.ToUpper() == "SUCCESS")
                {
                    //通信成功
                    string result_code = ds.Tables[0].Rows[0]["result_code"].ToString();//业务结果
                    if (result_code.ToUpper() == "SUCCESS")
                    {
                        var res = new Dictionary<string, string>
                        {
                            {"appId", wx_appid},
                            {"nonceStr", ds.Tables[0].Rows[0]["nonce_str"].ToString()},
                            {"package", "prepay_id="+ds.Tables[0].Rows[0]["prepay_id"].ToString()},
                            {"signType", "MD5"},
                            {"timeStamp", GetTimeStamp()},
                        };
                        //在服务器上签名           
                        res.Add("paySign", GetSignString(res, wx_key));
                        res.Add("wx_out_trade_no", wx_out_trade_no);
                        //发JSON回微信小程序
                        return res;
                    }
                }
                #endregion
                return null;
            }
            /// <summary>
            /// 微信小程序字典序列签名
            /// </summary>
            /// <param name="dic">字典序</param>
            /// <param name="key">商户平台 API安全里面设置的KEY  32位长度</param>
            /// <returns></returns>
            public static string GetSignString(Dictionary<string, string> dic, string key)//微信小程序字典序列签名
            {
                //
                //排序
                dic = dic.OrderBy(d => d.Key).ToDictionary(d => d.Key, d => d.Value);
                //连接字段
                var sign = dic.Aggregate("", (current, d) => current + (d.Key + "=" + d.Value + "&"));
                sign += "key=" + key;

                //MD5
                //sign = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sign, "MD5").ToUpper(); //过时
                //return sign;
                MD5 md5 = MD5.Create();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sign);
                byte[] md5Buffer = md5.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in md5Buffer)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString().ToUpper();

            }
            /// <summary>
            /// 微信小程序,登录使用解码函数
            /// </summary>
            /// <param name="raw"></param>
            /// <param name="signature"></param>
            /// <param name="encryptedData"></param>
            /// <param name="iv"></param>
            /// <param name="session_key"></param>
            /// <returns></returns>
            public static string DecodeUserInfo(string raw, string signature, string encryptedData, string iv, string session_key)//微信小程序,登录使用解码函数
            {
                byte[] iv2 = Convert.FromBase64String(iv);
                if (string.IsNullOrEmpty(encryptedData)) return "";
                Byte[] toEncryptArray = Convert.FromBase64String(encryptedData);
                System.Security.Cryptography.RijndaelManaged rm = new System.Security.Cryptography.RijndaelManaged
                {
                    Key = Convert.FromBase64String(session_key),
                    IV = iv2,
                    Mode = System.Security.Cryptography.CipherMode.CBC,
                    Padding = System.Security.Cryptography.PaddingMode.PKCS7
                };
                System.Security.Cryptography.ICryptoTransform cTransform = rm.CreateDecryptor();
                Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                return Encoding.UTF8.GetString(resultArray);
            }
            /// <summary>
            /// 从字符串里随机得到，规定个数的字符串.
            /// </summary>
            /// <param name="CodeCount"></param>
            /// <returns></returns>
            public static string GetRandomString(int CodeCount)//从字符串里随机得到，规定个数的字符串
            {
                string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                string[] allCharArray = allChar.Split(',');
                string RandomCode = "";
                int temp = -1;
                Random rand = new Random();
                for (int i = 0; i < CodeCount; i++)
                {
                    if (temp != -1)
                    {
                        rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                    }
                    int t = rand.Next(allCharArray.Length - 1);
                    while (temp == t)
                    {
                        t = rand.Next(allCharArray.Length - 1);
                    }
                    temp = t;
                    RandomCode += allCharArray[t];
                }

                return RandomCode;
            }
            /// <summary>
            /// 时间戳从1970年1月1日00:00:00至今的秒数,即当前的时间
            /// </summary>
            /// <returns></returns>
            public static string GetTimeStamp()//时间戳从1970年1月1日00:00:00至今的秒数,即当前的时间
            {
                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                return Convert.ToInt64(ts.TotalSeconds).ToString();
            }
            public static string GetWebClientIp()//获取客户端IP
            {
                string userIP = "IP";
                try
                {
                    if (System.Web.HttpContext.Current == null || System.Web.HttpContext.Current.Request == null || System.Web.HttpContext.Current.Request.ServerVariables == null)
                        return "";

                    string CustomerIP = "";

                    //CDN加速后取到的IP
                    CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                    if (!string.IsNullOrEmpty(CustomerIP))
                    {
                        return CustomerIP;
                    }

                    CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                    if (!String.IsNullOrEmpty(CustomerIP))
                        return CustomerIP;

                    if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                    {
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (CustomerIP == null)
                            CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                    else
                    {
                        CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                    }

                    if (string.Compare(CustomerIP, "unknown", true) == 0)
                        return System.Web.HttpContext.Current.Request.UserHostAddress;
                    return CustomerIP;
                }
                catch { }

                return userIP;
            }
            private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true; //总是接受
            }
            /// <summary>
            /// https的请求
            /// </summary>
            /// <param name="url"></param>
            /// <param name="datas"></param>
            /// <param name="charset"></param>
            /// <returns></returns>
            public static HttpWebResponse CreatePostHttpResponse(string url, string datas, Encoding charset)//https的请求
            {
                HttpWebRequest request = null;
                //HTTPSQ请求
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                StringBuilder buffer = new StringBuilder();
                buffer.AppendFormat(datas);
                byte[] data = charset.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                return request.GetResponse() as HttpWebResponse;
            }
        }
    }
}
