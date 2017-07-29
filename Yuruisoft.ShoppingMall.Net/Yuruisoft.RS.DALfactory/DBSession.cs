using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.DAL;
using Yuruisoft.RS.IDAL;

namespace Yuruisoft.RS.DALfactory
{
    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        public DbContext Db
        {
            get { return DBContextFactory.CreateDbContext(); }//完成EF上下文创建
        }
        /// <summary>
        ///  一个业务中可能涉及到多张表的操作，希望将多张表的操作，先追加到EF上下文中，然后再一次性更新就OK 了
        /// </summary>
        /// <returns></returns>
        public bool SaveChanges()
        {
            return Db.SaveChanges() > 0;
        }
    }
}
