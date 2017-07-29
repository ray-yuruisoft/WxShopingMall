using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;


namespace Autofac_Research
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User { Id = 2, Name = "leepy" };
            var builder = new ContainerBuilder();
            //builder.RegisterType<DatabaseManager>();RegisterType泛型方法注册
            //DatabaseManager构造函数依赖于IDatabase，所以要类型转换下
            //方式一：通过直接注册实现构造函数注入
            //builder.RegisterType<SqlDatabase>().As<IDatabase>();
            //方式二：通过配置文件实现构造函数注入
            builder.RegisterModule(new ConfigurationSettingsReader("autofac"));
            builder.RegisterInstance(user).As<User>();
            //方式三：通过Register方法进行注册，方法内为表达式类型
            builder.Register(c => new DatabaseManager(c.Resolve<IDatabase>(),c.Resolve<User>()));//与前面RegisterType<>互斥
            using (var container = builder.Build())
            {
                var manager = container.Resolve<DatabaseManager>();
                manager.Search("select * from user");
                manager.Add("insert into user");
            }
            Console.ReadKey();
        }
    }
}
