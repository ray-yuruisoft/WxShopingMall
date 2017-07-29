using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.BLL
{
    public partial class ActionInfoService :BaseService<ActionInfo>, IActionInfoService
    {
        /// <summary>
        /// 给权限分配角色
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool SetActionRoleInfo(int actionId, List<int> list)
        {
            var actionInfo = this.DbSession.ActionInfoDal.LoadEntities(a=>a.ID==actionId).FirstOrDefault();
            if (actionInfo != null)
            {
                actionInfo.RoleInfo.Clear();
                foreach (int roleId in list)
                {
                   var roleInfo= this.DbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                   actionInfo.RoleInfo.Add(roleInfo);
                }
            }
         return   this.DbSession.SaveChanges();
        }
        /// <summary>
        /// 删除权限信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool DeleteEntities(List<int> list)
        {
            var actionInfos = this.DbSession.ActionInfoDal.LoadEntities(c => list.Contains(c.ID));
            foreach (var actionInfo in actionInfos)
            {
                this.DbSession.ActionInfoDal.DeleteEntity(actionInfo);
            }
            return this.DbSession.SaveChanges();
        }
    }
}
