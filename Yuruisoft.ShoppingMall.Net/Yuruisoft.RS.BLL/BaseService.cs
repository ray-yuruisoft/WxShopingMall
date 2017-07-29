using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.DALfactory;
using Yuruisoft.RS.IDAL;

namespace Yuruisoft.RS.BLL
{
    public abstract class BaseService<T> where T:class,new()
    {
        public IDBSession DbSession//实现IDBSession接口
        {
            get { return DBSessionFactory.CreateDbSession(); }
        }

        /// <summary>
        /// 这里用到了多态，让基类也可以调用确定MODEL，而不是泛型
        /// </summary>
        public IDAL.IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();
        public BaseService()
        {
            SetCurrentDal();//子类必须要实现该抽象方法
        }        
        #region 基本查询，返回给Session
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDal.LoadEntities(whereLambda);
        }
        #endregion


        #region 无缓存查询，适合于查询，修改等同时操作后，无法保存的情况
        public IQueryable<T> LoadEntitiesAsNoTracking(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return this.CurrentDal.LoadEntitiesAsNoTracking(whereLambda);
        }
        #endregion



        #region 分页查询，返回给Session
         public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            return this.CurrentDal.LoadPageEntities<s>(pageIndex, pageSize, out totalCount, whereLambda, orderbyLambda, isAsc);
        }
        #endregion
        #region 删除，返回给Session
         public bool DeleteEntity(T entity)
        {
            this.CurrentDal.DeleteEntity(entity);
            return this.DbSession.SaveChanges();
        }
         #endregion
        #region 修改，返回给Session
          public bool UpdateEntity(T entity)
        {
            this.CurrentDal.UpdateEntity(entity);
            return this.DbSession.SaveChanges();
        }
         #endregion
        #region 增加，返回给Session
        public T AddEntity(T entity)
        {
            this.CurrentDal.AddEntity(entity);
            this.DbSession.SaveChanges();
            return entity;
        }
          #endregion        
    }
}
