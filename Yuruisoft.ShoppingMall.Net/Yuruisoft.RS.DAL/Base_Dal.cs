using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.DAL
{
    public class Base_Dal<T>where T:class,new()
    {
        DbContext Db = DBContextFactory.CreateDbContext();//完成EF上下文的创建，缘由：在DBSession中会二次创建EF上下文
       // Yuruisoft_RS_DBEntities Db = new Yuruisoft_RS_DBEntities();
        #region 基本查询
        /// <summary>
        /// 基本查询方法的实现
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntities(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where<T>(whereLambda);
        }
        #endregion

        /// <summary>
        /// 无缓存查询，适合于查询，修改等同时操作后，无法保存的情况
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> LoadEntitiesAsNoTracking(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().Where<T>(whereLambda).AsNoTracking();
        }

        #region 基本查询分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="s">排序的约束</typeparam>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示记录数</param>
        /// <param name="totalCount">总条数</param>
        /// <param name="whereLambda">过滤条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">排序方式</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageEntities<s>(int pageIndex, int pageSize, out int totalCount, System.Linq.Expressions.Expression<Func<T, bool>> whereLambda, System.Linq.Expressions.Expression<Func<T, s>> orderbyLambda, bool isAsc)
        {
            var temp = Db.Set<T>().Where<T>(whereLambda);
            totalCount = temp.Count();
            if (isAsc)
            {
                temp = temp.OrderBy<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            else
            {
                temp = temp.OrderByDescending<T, s>(orderbyLambda).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            }
            return temp;
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool DeleteEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Deleted;
            // return Db.SaveChanges() > 0;
            return true;
        }
        #endregion
        #region 修改所有
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateEntity(T entity)
        {
            Db.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            //return Db.SaveChanges() > 0;




            //@刘剑_1989: 更新是这样的，
            //T existing = Context.Set<T>().Find
            //如果 existing == null， Context.Set<T>().Add(item)；
            //否则， 将 item 的值赋给 existing（不包括主键的值），
            //最后，Context.SaveChanges


            return true;
        }
        #endregion
        #region 增加
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T AddEntity(T entity)
        {
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;
        }
        #endregion       
    }    
}
