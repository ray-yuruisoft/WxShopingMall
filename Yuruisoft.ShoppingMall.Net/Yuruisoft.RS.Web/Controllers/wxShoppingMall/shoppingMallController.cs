using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
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
using Yuruisoft.RS.Model.wxShoppingMall;

using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using Yuruisoft.RS.Common;
using Yuruisoft.RS.Model.Enum;
using Yuruisoft.RS.Web.Models;





namespace Yuruisoft.RS.Web.Controllers.wxShoppingMall
{
    public class shoppingMallController : Controller
    {
        //
        // GET: /shoppingMall/

        [HttpPost]
        public ActionResult recommentListsGet(int takeNum)
        {

            if (!checkRequestHeader(Request)) { return Content("forbid!"); }

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
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }

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
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
            string version = System.Web.Configuration.WebConfigurationManager.AppSettings["version"].ToString();
            return Json(new { version = version });
        }

        [HttpPost]
        public ActionResult produceDetailGet(int id)
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
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


        [HttpPost]
        public ActionResult sessionCheck(string thirdSessionKey)
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }

            if (thirdSessionKey != null)
            {
                DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
                var result = Db.Set<wxShoppingMall_userInfo>().Any(c => c.thirdSessionKey == thirdSessionKey);
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
        public ActionResult userLogin(string code, string encryptedData, string iv, string raw, string signature)
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }

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

            string decodeEncryptedData = WxExtensionClass.DecodeUserInfo(raw, signature, encryptedData, iv, session_key);

            jo = (JObject)JsonConvert.DeserializeObject(decodeEncryptedData);
            #endregion
            #region 3、更新数据库（如果是新用户，插入到数据库，如果是老用户，更新session_key)
            wxShoppingMall_userInfo dataFromWx = new wxShoppingMall_userInfo();
            #region 必须检查更新的数据
            dataFromWx.openId = jo["openId"].ToString();                              //用户openId
            dataFromWx.sessionKey = session_key;                                      //微信返回session_key
            dataFromWx.encryptedData = decodeEncryptedData;                           //微信返回的EncryptedData解码后信息           
            dataFromWx.thirdSessionKey = Guid.NewGuid().ToString("N");                //自定义的ThirdSessionKey
            dataFromWx.modifiedOn = DateTime.Now;                                     //用户最近登陆时间
            #endregion

            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();

            var CurrentUser = Db.Set<wxShoppingMall_userInfo>().Where(c => c.openId == dataFromWx.openId).FirstOrDefault();
            if (CurrentUser == null)//判断为首次登陆
            {
                #region 首次登陆独有数据
                dataFromWx.subTime = DateTime.Now;                                         //首次登陆时间                           
                #endregion
                Db.Set<wxShoppingMall_userInfo>().Add(dataFromWx);
            }
            else
            {
                wxShoppingMall_userInfo CurrentUserInDB = Db.Set<wxShoppingMall_userInfo>().Find(CurrentUser.id, CurrentUser.openId);
                if (CurrentUserInDB == null)//缓存中查不到实体，先删除，后添加
                {
                    #region 缓存中查不到实体，先删除，后添加
                    Db.Set<wxShoppingMall_userInfo>().Remove(CurrentUser);
                    dataFromWx.subTime = DateTime.Now;                                     //首次登陆时间                  
                    Db.Set<wxShoppingMall_userInfo>().Add(dataFromWx);
                    #endregion
                }
                else//其余判断为二次登陆
                {
                    CurrentUserInDB.sessionKey = dataFromWx.sessionKey;
                    if (CurrentUserInDB.encryptedData != dataFromWx.encryptedData)
                    {
                        CurrentUserInDB.encryptedData = dataFromWx.encryptedData;
                    }
                    CurrentUserInDB.modifiedOn = dataFromWx.modifiedOn;
                    CurrentUserInDB.thirdSessionKey = dataFromWx.thirdSessionKey;
                }
            }
            if (Db.SaveChanges() > 0)
            {
                return Json(new { thirdSessionKey = dataFromWx.thirdSessionKey });
            }
            else
            {
                return Json(new { Update = false });
            }
            #endregion
        }


        [HttpPost]
        public ActionResult checkAccountRepeat(string account)
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
            var result = Db.Set<haowanFamilyAccountInfo>().Any(c => c.account == account);
            return Json(new { error = result });
        }

        [HttpPost]

        public ActionResult checkPhoneNumRepeat(string phoneNum)
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
            var temp = long.Parse(phoneNum);
            var result = Db.Set<haowanFamilyAccountInfo>().Any(c => c.phoneNumber == temp);
            return Json(new { error = result });
        }

        [HttpPost]
        public ActionResult accountAdd(string account, string password, string phoneNum, string email, string vCode)
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                return Json(new
                {
                    error = "VCODEWRONG"
                });
            }
            Session["validateCode"] = null;
            if (!vCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Json(new
                {
                    error = "VCODEWRONG"
                });
            }
            if (account != null && password != null && phoneNum != null)
            {
                haowanFamilyAccountInfo current = new haowanFamilyAccountInfo();
                current.account = account;
                current.email = email;
                current.modifiedOn = DateTime.Now;
                current.subtime = DateTime.Now;
                current.password = password;
                current.phoneNumber = long.Parse(phoneNum);
                DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
                Db.Set<haowanFamilyAccountInfo>().Add(current);
                if (Db.SaveChanges() > 0)
                {
                    var passwordMD5 = Common.WebCommon.Md5String(Common.WebCommon.Md5String(password));
                    return Json(new
                    {
                        account = account,
                        email = email,
                        phoneNumber = long.Parse(phoneNum),
                        password = passwordMD5,
                        error = false
                    });
                }
            }
            return Json(new { error = true });
        }

        [HttpPost]
        public ActionResult login(string name, string password, bool isEmail, bool isPhoneNum, string thirdSessionKey, string vCode)
        {//验证码初次不显示功能，实现为加缓存，缓存每10分钟清空一次。如果加到数据库会增加压力
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
            haowanFamilyAccountInfo result = new haowanFamilyAccountInfo();
            if (SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache.ContainsKey(thirdSessionKey))
            {
                short FailCount = SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[thirdSessionKey];
                if (FailCount > 3)
                {
                    #region 需要验证码,成功登陆需重置验证码
                    string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
                    if (string.IsNullOrEmpty(validateCode))
                    {
                        return Json(new
                        {
                            error = "VCODEWRONG",
                            failCount = FailCount
                        });
                    }
                    Session["validateCode"] = null;
                    if (!vCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return Json(new
                        {
                            error = "VCODEWRONG",
                            failCount = FailCount
                        });
                    }
                    #region 1、判断邮件名
                    if (isEmail)
                    {
                        result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.email == name).FirstOrDefault();
                    }
                    #endregion
                    #region 2、判断电话号码
                    if (isPhoneNum)
                    {
                        var temp = long.Parse(name);
                        result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.phoneNumber == temp).FirstOrDefault();
                    }
                    #endregion
                    #region 3、判断账户名
                    if ((!isEmail) && (!isPhoneNum))
                    {
                        result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.account == name).FirstOrDefault();
                    }
                    #endregion
                    if (result == null)
                    {
                        return Json(new
                        {
                            error = "NAMEWRONG",
                            failCount = FailCount
                        });
                    }
                    if (result.password != password)
                    {
                        return Json(new
                        {
                            error = "PASSWORDWRONG",
                            failCount = FailCount
                        });
                    }
                    SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[thirdSessionKey] = 0;
                    var passwordMD5 = Common.WebCommon.Md5String(Common.WebCommon.Md5String(password));
                    return Json(new
                    {
                        account = result.account,
                        email = result.email,
                        phoneNumber = result.phoneNumber,
                        password = passwordMD5
                    });
                    #endregion
                }
                else
                {
                    SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[thirdSessionKey]++;
                    #region 不需要验证码,成功登陆需重置验证码
                    #region 1、判断邮件名
                    if (isEmail)
                    {
                        result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.email == name).FirstOrDefault();
                    }
                    #endregion
                    #region 2、判断电话号码
                    if (isPhoneNum)
                    {
                        var temp = long.Parse(name);
                        result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.phoneNumber == temp).FirstOrDefault();
                    }
                    #endregion
                    #region 3、判断账户名
                    if ((!isEmail) && (!isPhoneNum))
                    {
                        result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.account == name).FirstOrDefault();
                    }
                    #endregion
                    if (result == null)
                    {
                        return Json(new
                        {
                            error = "NAMEWRONG",
                            failCount = FailCount
                        });
                    }
                    if (result.password != password)
                    {
                        return Json(new
                        {
                            error = "PASSWORDWRONG",
                            failCount = FailCount
                        });
                    }
                    SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[thirdSessionKey] = 0;
                    var passwordMD5 = Common.WebCommon.Md5String(Common.WebCommon.Md5String(password));
                    return Json(new
                    {
                        account = result.account,
                        email = result.email,
                        phoneNumber = result.phoneNumber,
                        password = passwordMD5
                    });
                    #endregion
                }
            }
            else//第一次登陆
            {
                SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[thirdSessionKey] = 1;
                #region 不需要验证码,成功登陆需重置验证码
                #region 1、判断邮件名
                if (isEmail)
                {
                    result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.email == name).FirstOrDefault();
                }
                #endregion
                #region 2、判断电话号码
                if (isPhoneNum)
                {
                    var temp = long.Parse(name);
                    result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.phoneNumber == temp).FirstOrDefault();
                }
                #endregion
                #region 3、判断账户名
                if ((!isEmail) && (!isPhoneNum))
                {
                    result = Db.Set<haowanFamilyAccountInfo>().Where(c => c.account == name).FirstOrDefault();
                }
                #endregion
                if (result == null)
                {
                    return Json(new
                    {
                        error = "NAMEWRONG",
                        failCount = 0
                    });
                }
                if (result.password != password)
                {
                    return Json(new
                    {
                        error = "PASSWORDWRONG",
                        failCount = 0
                    });
                }
                SingleLogOnVcodeCache.GetLogOnVcodeCache().LogOnCache[thirdSessionKey] = 0;
                var passwordMD5 = Common.WebCommon.Md5String(Common.WebCommon.Md5String(password));
                return Json(new
                {
                    account = result.account,
                    email = result.email,
                    phoneNumber = result.phoneNumber,
                    password = passwordMD5
                });
                #endregion
            }
        }


        [HttpPost]
        public ActionResult checkVCode(string vCode)
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
            DbContext Db = Yuruisoft.RS.Model.wxShoppingMall.wxShoppingMallDBFactory.CreateDbContext();
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                Session["validateCode"] = null;
                return Json(new
                {
                    error = true
                });
            }
            if (vCode.Equals(validateCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return Json(new
                {
                    error = false
                });
            }
            Session["validateCode"] = null;
            return Json(new
            {
                error = true
            });
        }

        #region 请求验证码.
        [HttpPost]
        public ActionResult validateCodeGet()
        {
            if (!checkRequestHeader(Request)) { return Content("forbid!"); }
            Common.ValidateCode validateCode = new Common.ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            string str = "data:image/png;base64," + Convert.ToBase64String(buffer);
            return Json(new
            {
                base64Image = str
            });
        }
        #endregion

        string domainGet()
        {
            if (bool.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["debug"]))
                return "http://" + Request.Url.Authority;
            else
                return "https://" + Request.Url.Authority;
        }
        bool checkRequestHeader(HttpRequestBase request)
        {
            if (request.Headers["haowanFamily"] != "www.haowanFamily.com")//请求头自定义
            {
                return false;
            }
            return true;
        }
        static class WxExtensionClass//扩展函数集，主要针对登录和微信支付的，可加密为动态库
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
