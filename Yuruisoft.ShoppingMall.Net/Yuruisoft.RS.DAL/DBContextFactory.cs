using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.DAL
{
    public class DBContextFactory
    {
        #region DBContext工厂
        /// <summary>
        /// 使用CallContext 保证在一次请求过程中只创建一次EF上下文实例
        ///
        /// </summary>
        /// <returns></returns>
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("Static_dbContext");
            if (dbContext == null)
            {
                dbContext = SingleYuruisoft_DBContextCache.GetDBContextSingle();
                CallContext.SetData("Static_dbContext", dbContext);
            }
            return dbContext;
        }
        #endregion
    }
    //这里使用单例的目的，因为发现页面访问时候，数据槽缓存不上，使用数据槽也可以防止单例访问量太大崩溃而影响数据构建
    public class SingleYuruisoft_DBContextCache
    {
        private volatile static DbContext DBContextCache = null;
        private static readonly object lockHelper = new object();
        /// <summary>
        /// 获取上下文的单例
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDBContextSingle()
        {
            if (DBContextCache == null)
            {
                lock (lockHelper)
                {
                    DBContextCache = new Yuruisoft_DBContext();
                }
            }
            return DBContextCache;
        }
    }

}
