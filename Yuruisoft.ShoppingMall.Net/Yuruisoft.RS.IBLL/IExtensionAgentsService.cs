using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.IBLL
{
    public partial interface IExtensionAgentsService : IBaseService<ExtensionAgents>
    {
        /// <summary>
        /// 多条件搜索推广员信息
        /// </summary>
        /// <param name="extensionAgentsfilter"></param>
        /// <returns></returns>
        IQueryable<ExtensionAgents> LoadSearchAgentsInfo(Model.UserInfoParams.ExtensionAgentsFilter extensionAgentsfilter);

        /// <summary>
        /// 批量删除用户信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);

    }
}
