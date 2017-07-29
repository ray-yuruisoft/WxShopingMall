using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.IDAL
{
    public interface IBaseDal<T>where T:class,new()
    {
        #region 基本查询接口
        IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);
        #endregion


        IQueryable<T> LoadEntitiesAsNoTracking(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda);


        #region 基本查询分页接口
        IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc);
        #endregion
        #region 删除接口
        bool DeleteEntity(T entity);
        #endregion
        #region 修改接口
        bool UpdateEntity(T entity);
        #endregion
        #region 增加接口
        T AddEntity(T entity);
        #endregion        
    }
}
