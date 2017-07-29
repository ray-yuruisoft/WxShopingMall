using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DynamicModel;
using System.Configuration;
using System.Xml;
using Yuruisoft.RS.Common;


namespace Yuruisoft.RS.Model
{
    public partial class Yuruisoft_DBContext : DbContext
    {
        public Yuruisoft_DBContext(): base("name=Yuruisoft_RS_DBEntities")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //去掉创建数据表默认的复数形式

            /*************************************以下为必须添加内容*************************************/

            if(Convert.ToBoolean(ConfigurationManager.AppSettings["DynamicModelSwitch"]))//这里做一个开关，项目不涉及到的时候关闭，避免影响效率
            {
                Type[] runtimedata = SingletonForDymicModel.CreateInstance(GetJsonDatas.GetJson()).GetType();
                if (runtimedata != null)
                {
                    foreach (var item in runtimedata)
                    {
                        modelBuilder.RegisterEntityType(item);
                    }
                }
            }
            /*************************************以上为必须添加内容*************************************/

            base.OnModelCreating(modelBuilder);
        }           
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<ActionInfo> ActionInfo { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<R_UserInfo_ActionInfo> R_UserInfo_ActionInfo { get; set; }
        public DbSet<RoleInfo> RoleInfo { get; set; }

        public DbSet<RouteStatisticsLinks> RouteStatisticsLinks { get; set; }//来路统计链接表
        public DbSet<ExtensionAgents> ExtensionAgents { get; set; }//来路统计链接表
    }
}
