using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using System.Reflection;
using System.Threading;
using yuruisoft.oa.Web.Models;
using log4net;
using Yuruisoft.RS.Setup;
using Yuruisoft.RS.Model.RuntimeModel;
using RouteStatisticsCache;
using System.Configuration;

namespace Yuruisoft.RS.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DBInitialize.CreatDefaultTableData();//首次启动程序会因为去创建DBContext中的Dynamic模型而费时间，但是启动起来了就OK了。

            //这里做一个开关，项目不涉及到的时候关闭，避免影响效率
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["DynamicModelSwitch"]))
            {
                EnterPageInitialize.CreateInstance(EnterPageInitialize.ConfigProviderCreate());//启动报名页面修改的单例对象，提上同理。
            }

            //ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();
            //var container = builder.Build();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            var builder = new ContainerBuilder();
            var assembly = Assembly.GetExecutingAssembly();
            var BllService = System.Reflection.Assembly.Load("Yuruisoft.RS.BLL");//类似于RegisterType的形式注册，被注册类型可以不是直接引用，但是必须被加载
            builder.RegisterAssemblyTypes(BllService, BllService).AsImplementedInterfaces();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            #region 线程与异步管理
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("/App_Data/Config/Log4net.Config/log4net.Config.xml")));//读取配置信息
            //string fileLogPath = Server.MapPath("App_Data/Log/");
            //开启一个线程，扫描日志队列
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)
                {
                    if (MyExceptionAttribute.exceptonQueue.Count() > 0)//判断是否有数据
                    {
                        Exception ex = MyExceptionAttribute.exceptonQueue.Dequeue();//出队
                        if (ex != null)
                        {
                            //string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            //File.AppendAllText(fileLogPath + fileName, ex.ToString(), Encoding.Default);//将异常写到文件中，用追加
                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(ex);//将异常信息写到磁盘上
                        }
                        else
                        {
                            Thread.Sleep(3000);//如果队列中没有数据，让当前线程休息，避免造成CPU空转
                        }
                    }
                    else
                    {//这里可以自定义
                        Thread.Sleep(3000);//如果队列中没有数据，让当前线程休息，避免造成CPU空转                    
                    }
                }
            });

            ThreadPool.QueueUserWorkItem((a) =>
            {//这里是更新键值对的操作
                while (true)
                {
                    if (ExtendMethord.OperateScoreCacheQueue.Count() > 0)
                    {
                        OperateScoreCache op = ExtendMethord.OperateScoreCacheQueue.Dequeue();
                        if (op != null)
                        {
                            op.ScoreCacheAddOne();
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);
                    }
                }
            });
            ThreadPool.QueueUserWorkItem((a) =>
            {//这里是更新键值对的操作
                while (true)
                {
                    ExtendMethord.UpdateDB();
                    Thread.Sleep(3000);
                }
            });
            #endregion
        }
    }
}