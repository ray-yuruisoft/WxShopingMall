using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Yuruisoft.RS.Common
{
    public class GetJsonDatas
    {
        public static string GetJson()
        {
            #region 拿Json数据（含异常处理）
            // string path = System.AppDomain.CurrentDomain.BaseDirectory + @"App_Data\Yuruisoft.RS.Config\runtimemodelconfig.json.txt";
            string path = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Config/Yuruisoft.RS.Config/runtimemodelconfig.json.txt");
            string strjson = null;
            try
            {
                strjson = System.IO.File.ReadAllText(path, Encoding.Default);
                if (ConfigurationManager.AppSettings["JsonConfigPath"].ToString() == "")
                {
                    // ConfigurationManager.AppSettings.Add("JsonConfigPath", strjson); 只读占用
                    XmlDocument webconfigDoc = new XmlDocument();
                    string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"/web.config";
                    //设置节的xml路径
                    string xPath = "/configuration/appSettings/add[@key='?']";
                    //加载web.config文件    
                    webconfigDoc.Load(filePath);
                    //找到要修改的节点    
                    XmlNode passkey = webconfigDoc.SelectSingleNode(xPath.Replace("?", "JsonConfigPath"));
                    //设置节点的值    
                    passkey.Attributes["value"].InnerText = path;
                    //保存设置    
                    webconfigDoc.Save(filePath);
                }
            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ArgumentException))
                    try
                    {
                        strjson = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["JsonConfigPath"].ToString());
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
            }
            #endregion
            return strjson;
        }
    }
}
