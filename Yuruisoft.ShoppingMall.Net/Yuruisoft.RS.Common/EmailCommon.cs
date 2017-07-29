using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Common
{
    public class EmailCommon
    {
        /// <summary>
        /// 发送邮件函数
        /// </summary>
        /// <param name="sfrom">发送者邮箱</param>
        /// <param name="sfromer">发送人</param>
        /// <param name="sto">接受者邮箱</param>
        /// <param name="stoer">收件人</param>
        /// <param name="sSubject">主题</param>
        /// <param name="sBody">内容</param>
        /// <param name="sfile">附件地址</param>
        /// <param name="sSMTPHost">smtp服务器</param>
        /// <param name="sSMTPuser">smtp登录名</param>
        /// <param name="sSMTPpass">smtp密码</param>
        /// <param name="errMessage">错误信息返回字串</param>
        /// <returns></returns>
        public static bool sendmail(string sfrom, string sfromer, string sto, string stoer, string sSubject, string sBody, string sfile, string sSMTPHost, string sSMTPuser, string sSMTPpass, out string errMessage)
        {
            ////设置from和to地址
            MailAddress from = new MailAddress(sfrom, sfromer);
            MailAddress to = new MailAddress(sto, stoer);

            ////创建一个MailMessage对象
            MailMessage oMail = new MailMessage(from, to);

            //// 添加附件
            if (sfile != "")
            {
                oMail.Attachments.Add(new Attachment(sfile));
            }

            ////邮件标题
            oMail.Subject = sSubject;

            ////邮件内容
            oMail.Body = sBody;

            ////邮件格式
            oMail.IsBodyHtml = false;

            ////邮件采用的编码
            oMail.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");

            ////设置邮件的优先级为高
            oMail.Priority = MailPriority.High;

            ////发送邮件
            SmtpClient client = new SmtpClient();
            ////client.UseDefaultCredentials = false; 
            client.Host = sSMTPHost;
            client.Credentials = new NetworkCredential(sSMTPuser, sSMTPpass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(oMail);
                errMessage = null;
                return true;
            }
            catch (Exception err)
            {
                errMessage = err.Message.ToString();
                return false;
            }
            finally
            {
                ////释放资源
                oMail.Dispose();
            }
        }
        /// <summary>
        /// 为Yuruisoft.com定制QQ邮箱发送函数
        /// </summary>
        /// <param name="mail">邮件设置</param>
        /// <param name="sfile">附件地址</param>
        /// <param name="email">收件人的地址</param>
        /// <param name="errMessage">错误反馈</param>
        /// <returns></returns>
        public static bool QQEmailSend(MailMessage mail, string sfile, string email, out string errMessage)
        {
            SmtpClient client = new SmtpClient("smtp.qq.com");//配置SMTP客户端，这里是按QQ邮箱要求配置，初始化时设置了Host
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("11082929@qq.com", "nuhurazjakkacbch");//验证
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            #region 邮件设置
            //设置from和to地址
            MailAddress from = new MailAddress("11082929@qq.com", "裕睿软件yuruisoft.com", Encoding.UTF8);//初始化发件人  
            MailAddress to = new MailAddress(email, "", Encoding.UTF8);//初始化收件人
            //设置邮件内容,创建一个MailMessage对象
            MailMessage message = new MailMessage(from, to);
            // 添加附件
            if (sfile != "")
            {
                message.Attachments.Add(new Attachment(sfile));
            }
            message.Body = mail.Body;//邮件内容
            message.BodyEncoding = mail.BodyEncoding;//邮件采用的编码
            message.Subject = mail.Subject;//邮件标题
            message.SubjectEncoding = mail.SubjectEncoding;//主题内容使用编码
            message.IsBodyHtml = mail.IsBodyHtml;//邮件格式
            message.Priority = MailPriority.High;//设置邮件的优先级为高
            #endregion
            //发送邮件  
            try
            {
                client.Send(message);
                errMessage = null;
                return true;
            }
            catch (Exception err)
            {
                errMessage = err.Message.ToString();
                return false;
            }
            finally
            {
                ////释放资源
                message.Dispose();
            }
        }
    }
}
