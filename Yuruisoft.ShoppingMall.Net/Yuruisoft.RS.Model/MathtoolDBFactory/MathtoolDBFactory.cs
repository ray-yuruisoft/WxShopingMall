using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model
{
    public class MathtoolDBFactory
    {
        #region DBContext工厂
        public static DbContext CreateDbContext()
        {
            DbContext dbContext = (DbContext)CallContext.GetData("Mathtool_dbContext");
            if (dbContext == null)
            {
                dbContext = SingleMathtoolDBContextCache.GetDBContextSingle();
                CallContext.SetData("Mathtool_dbContext", dbContext);
            }
            return dbContext;
        }
        #endregion
    }
    public class SingleMathtoolDBContextCache
    {
        private volatile static DbContext DBContextCache = null;
        private static readonly object lockHelper = new object();
        public static DbContext GetDBContextSingle()
        {
            if (DBContextCache == null)
            {
                lock (lockHelper)
                {
                    DBContextCache = new MathtoolDBEntities();
                }
            }
            return DBContextCache;
        }
    }
}
