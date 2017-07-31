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
                dbContext = SingleDBContextCache.GetDBContextSingle();
                CallContext.SetData("wxShoppingMall_dbContext", dbContext);
            }
            return dbContext;
        }
        #endregion
    }
    public class SingleDBContextCache
    {
        private volatile static DbContext DBContextCache = null;
        private static readonly object lockHelper = new object();
        public static DbContext GetDBContextSingle()
        {
            if (DBContextCache == null)
            {
                lock (lockHelper)
                {
                    DBContextCache = new wxShoppingMallEntities();
                }
            }
            return DBContextCache;
        }
    }
}
