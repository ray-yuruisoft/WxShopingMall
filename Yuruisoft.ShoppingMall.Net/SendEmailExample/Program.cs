using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Common;

namespace SendEmailExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string from = "rayyewang01@163.com";//发送者邮箱
            string fromer = "裕睿软件@Yuruisoft.com";//发送人
            string to = "417853832@qq.com";//接受者邮箱
            string toer = "小王";//收件人
            string Subject = "这只是个测试！";//主题
            string file = @"C:\Users\Administrator\Desktop\02_asp.net_mvc_框架EasyUI快速开发框架通用权限管理.rar";//附件
            string Body = "这只是个测试！";//内容
            string SMTPHost = "smtp.163.com";
            string SMTPuser = "rayyewang01@163.com";
            string SMTPpass = "wangrui1986";
            string errMessage = null;
            if (!EmailCommon.sendmail(from, fromer, to, toer, Subject, Body, file, SMTPHost, SMTPuser, SMTPpass, out errMessage))
                Console.Write("{0}", errMessage);
            else
                Console.Write("发送成功！");
            Console.ReadKey();
        }
    }
}
