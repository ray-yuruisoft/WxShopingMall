using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.wxShoppingMall
{
    public class wxShoppingMallDBFactory
    {
        #region DBContext工厂
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("wxShoppingMall_dbContext");
            if (dbContext == null)
            {
                //dbContext = SingleDBContextCache.GetDBContextSingle();
                dbContext = new wxShoppingMallEntities();
                CallContext.SetData("wxShoppingMall_dbContext", dbContext);
            }
            return dbContext;
        }
        #endregion
    }
    //上下文创建不能使用单例模式，会抛出如下警告：正在创建模型，此时不可使用上下文。如果在 OnModelCreating 方法内使用上下文或如果多个线程同时访问同一上下文实例，可能引发此异常。

    //public class SingleDBContextCache
    //{
    //    private volatile static DbContext DBContextCache = null;
    //    private static readonly object lockHelper = new object();
    //    public static DbContext GetDBContextSingle()
    //    {
    //        if (DBContextCache == null)
    //        {
    //            lock (lockHelper)
    //            {
    //                DBContextCache = new wxShoppingMallEntities();
    //            }
    //        }
    //        return DBContextCache;
    //    }
    //}
}
