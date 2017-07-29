using DynamicModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace DynamicDal
{
    public class BaseDal
    {   //需构造注入DbContext,作用是使用者更加灵活
        private DbContext Db { get; set; }
        public BaseDal(DbContext _Db)
        {
            Db = _Db;
        }
        #region 提供查询的动态实体
        /// <summary>
        /// 提供查询的动态实体
        /// </summary>
        /// <param name="runtimeModel">动态类的类型</param>
        /// <returns>返回DbSet,需配合DynamicLinq来查询</returns>
        public DbSet LoadEntities(Type runtimeModel)
        {
            return Db.Set(runtimeModel);
        }
        #endregion
        #region 增加
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="entity">动态类类型实例化后的实体</param>
        /// <param name="runtimeModel">动态类的类型</param>
        /// <returns>动态类类型实例化后的实体</returns>
        public DynamicEntity AddEntity(DynamicEntity entity, Type runtimeModel)
        {
            Db.Set(runtimeModel).Add(entity);
            Db.SaveChanges();
            return entity;
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">动态类类型实例化后的实体</param>
        /// <returns>返回true</returns>
        public bool DeleteEntity(DynamicEntity entity)
        {
            Db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            //return Db.SaveChanges() > 0;
            return true;
        }
        #endregion
        #region 修改所有
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">动态类类型实例化后的实体</param>
        /// <returns>返回true</returns>
        public bool UpdateEntity(DynamicEntity entity)
        {
            Db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            //return Db.SaveChanges() > 0;

            return true;
        }
        #endregion


        /// <summary>
        /// 将配置文件转换为实体类
        /// </summary>
        /// <param name="strjson">配置文件</param>
        /// <param name="ID">实体类型的ID</param>
        /// <returns></returns>
        public Type GetRuntimeModelType(string strjson,int ID)
        {
            return SingletonForDymicModel.CreateInstance(strjson).GetType()[ID];
        }

        /// <summary>
        /// 获取配置信息中字段名称
        /// </summary>
        /// <param name="strjson"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IEnumerable<RuntimeModelMeta.ModelPropertyMeta> GetRuntimeModelProperty(string strjson, int ID)
        {
             return SingletonForDymicModel.CreateInstance(strjson).GetRuntimeModelMeta()[ID].ModelProperties.Where(c => true);
        }

        /// <summary>
        /// 将实体类实例化
        /// </summary>
        /// <param name="runtimeModelType">实体类类型</param>
        /// <returns></returns>
        public DynamicEntity GetRuntimeModel(Type runtimeModelType)
        {
            return Activator.CreateInstance(runtimeModelType) as DynamicEntity;
        }
    }
}
