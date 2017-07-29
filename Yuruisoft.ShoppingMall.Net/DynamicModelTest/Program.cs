using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicModel;
using DynamicDal;
using System.Runtime.Remoting.Messaging;
using System.Linq.Dynamic;

namespace DynamicModelTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContext Db = new DynamicModelTestDB();
            Db.Database.CreateIfNotExists();

            string path = System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\DynamicModelTestConfig.json.txt";
            string strjson = System.IO.File.ReadAllText(path, Encoding.Default);//拿配置文件



            Type runtimeModels = SingletonForDymicModel.CreateInstance(strjson).GetType()[0];//将配置文件转换为实体类
            DynamicEntity En = Activator.CreateInstance(runtimeModels) as DynamicEntity;//将实体类实例化

            //实例化后通过键值赋值，详细查看DynamicEntity
            En["PersonID"] = 11212121;
            En["Name"] = "李四";
            En["Sex"] = "女";
            En["Age"] = 18;

            //这里需将DBContext的实例化暴露出来，方便混合静态实体模型
            BaseDal dal = new BaseDal(CreateDbContext());

            //分页查询
            int totalCount = dal.LoadEntities(runtimeModels).Count();
            int pageIndex = 1;
            int pageSize  = 2;
            dynamic results = dal.LoadEntities(runtimeModels).Where("id>0").OrderBy("Age").Skip((pageIndex - 1) * pageSize).Take(pageSize);
            foreach (var item in results)
            {
                Console.Write(item.Name);
            }

            //查询和删除
            dynamic result = dal.LoadEntities(runtimeModels).Where("id >6").OrderBy("Age");
            foreach (var item in result)
            {
                Console.Write(item.Name);
                //dal.DeleteEntity(item);
            }
            
            for (int i = 0; i <= 20; i++)
            {
                dal.AddEntity(En, runtimeModels);//增加
                CreateDbContext().SaveChanges();//最后统一保存
            }

            Console.ReadKey();

        }

        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("DynamicModelTest_dbContext");
            if (dbContext == null)
            {
                dbContext = new DynamicModelTestDB();
                CallContext.SetData("DynamicModelTest_dbContext", dbContext);
            }
            return dbContext;
        }
        public class DynamicModelTestDB : DbContext
        {
            public DynamicModelTestDB()
                : base("name=DynamicModelTest")
            {
            }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //去掉创建数据表默认的复数形式
                /*************************************以下为必须添加内容*************************************/

                string path = System.AppDomain.CurrentDomain.BaseDirectory + @"..\..\DynamicModelTestConfig.json.txt";
                string strjson = System.IO.File.ReadAllText(path, Encoding.Default);//拿配置文件

                Type[] runtimedata = SingletonForDymicModel.CreateInstance(strjson).GetType();
                if (runtimedata != null)
                {
                    foreach (var item in runtimedata)
                    {
                        modelBuilder.RegisterEntityType(item);
                    }
                }
                /*************************************以上为必须添加内容*************************************/
                base.OnModelCreating(modelBuilder);
            }
        }

    }
}
