using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;

namespace Yuruisoft.RS.BLL
{
    public partial class ExtensionAgentsService : BaseService<ExtensionAgents>, IExtensionAgentsService
    {
        #region 多条件搜索推广员信息
        public IQueryable<ExtensionAgents> LoadSearchAgentsInfo(Model.UserInfoParams.ExtensionAgentsFilter extensionAgentsfilter)
        {
            short deleteType = (short)DeleteEnumType.Normal;//标记 0正常，1逻辑，2物理
            var temp = this.DbSession.ExtensionAgentsDal.LoadEntities(c => c.DelFlag == deleteType);
            if (!string.IsNullOrEmpty(extensionAgentsfilter.LName)) //判断用户名是否为空
            {
                temp = temp.Where<ExtensionAgents>(u => u.LName.Contains(extensionAgentsfilter.LName));
            }
            if (!string.IsNullOrEmpty(extensionAgentsfilter.Remark))
            {
                temp = temp.Where<ExtensionAgents>(u => u.Remark.Contains(extensionAgentsfilter.Remark));
            }
            extensionAgentsfilter.TotalCount = temp.Count();
            return temp.OrderBy<ExtensionAgents, string>(u => u.Sort).Skip<ExtensionAgents>((extensionAgentsfilter.PageIndex - 1) * extensionAgentsfilter.PageSize).Take<ExtensionAgents>(extensionAgentsfilter.PageSize);
        }
        #endregion
        #region 批量删除
        public bool DeleteEntities(List<int> list)
        {
            var AgentInfoList = this.DbSession.ExtensionAgentsDal.LoadEntities(u => list.Contains(u.ID));
            if (AgentInfoList != null)
            {
                foreach (var AgentInfo in AgentInfoList)
                {
                    this.DbSession.ExtensionAgentsDal.DeleteEntity(AgentInfo);
                }
            }
            return this.DbSession.SaveChanges();//最后调用DBSession中的SaveChanges方法将数据一次性提交会数据库
        }
        #endregion
    }
}
