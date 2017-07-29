using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.DAL;
using Yuruisoft.RS.Model;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;
using System.Configuration;
using System.Xml;

namespace Yuruisoft.RS.Setup
{
    public class DBInitialize
    {
        public static bool CreatDefaultTableData()
        {
            //防止自动迁移
            //System.Data.Entity.Database.SetInitializer<Yuruisoft.RS.Model.Yuruisoft_DBContext>(null);
            //System.Data.Entity.Database.SetInitializer<Yuruisoft.RS.Model.RuntimeDBContext>(null);
            //设置自动迁移
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.MigrateDatabaseToLatestVersion<Yuruisoft.RS.Model.Yuruisoft_DBContext, Yuruisoft.RS.Model.Migrations.Configuration>());
            //当自动迁移时，就drop
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Yuruisoft.RS.Model.RuntimeDBContext>());
            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Yuruisoft.RS.Model.Yuruisoft_DBContext>());

            // false 异动，true 没有异动
            //if (FirstDBcontext.Database.Exists())
            //{
            //    var isChanged = DBContextFactory.CreateDbContext().Database.CompatibleWithModel(false);
            //}

            //以上为保留代码，后期动态类型会有有用


            DbContext FirstDBcontext = DBContextFactory.CreateDbContext();
            //CodeFirst首次创建数据库,首次创建DBContext,数据库不存在情况下
            if (FirstDBcontext.Database.CreateIfNotExists())
            {
                #region 读文件处理（含异常处理）
                string path = System.Web.HttpContext.Current.Server.MapPath("/App_Data/Config/Yuruisoft.RS.Config/DBInitialize.sql");
                string strjson = null;
                try
                {
                    strjson = System.IO.File.ReadAllText(path, Encoding.Default);
                    if (ConfigurationManager.AppSettings["DBInitializePath"].ToString() == "")
                    {
                        // ConfigurationManager.AppSettings.Add("JsonConfigPath", strjson); 只读占用
                        XmlDocument webconfigDoc = new XmlDocument();
                        string filePath = System.AppDomain.CurrentDomain.BaseDirectory + @"/web.config";
                        //设置节的xml路径
                        string xPath = "/configuration/appSettings/add[@key='?']";
                        //加载web.config文件    
                        webconfigDoc.Load(filePath);
                        //找到要修改的节点    
                        XmlNode passkey = webconfigDoc.SelectSingleNode(xPath.Replace("?", "DBInitializePath"));
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
                            strjson = System.IO.File.ReadAllText(ConfigurationManager.AppSettings["DBInitializePath"].ToString());
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                }
                #endregion
                FirstDBcontext.Database.ExecuteSqlCommandAsync(strjson, new SqlParameter("DateTime_now", DateTime.Now.ToString()));
                return true;
            }
            return false;
        }
    }
}
