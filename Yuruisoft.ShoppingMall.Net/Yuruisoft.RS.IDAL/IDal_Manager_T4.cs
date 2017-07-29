/*
 * 该文件为T4模板自动创建，请勿手动添加任何代码，否则会被刷新覆盖
 * 
 * 更多技术支持，请联系业务电话：15308202328  业务QQ：11082929
 * 
 * 更多业务请查看：www.yuruisoft.com(全球） www.yurusoft.net(国内专线)
 * 
 * 版权为裕睿软件@yuruisoft.com所持 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.IDAL
{
    public partial interface IUserInfoDal :IBaseDal<UserInfo>
    {
    }
    public partial interface IActionInfoDal :IBaseDal<ActionInfo>
    {
    }
    public partial interface IDepartmentDal :IBaseDal<Department>
    {
    }
    public partial interface IR_UserInfo_ActionInfoDal :IBaseDal<R_UserInfo_ActionInfo>
    {
    }
    public partial interface IRoleInfoDal :IBaseDal<RoleInfo>
    {
    }
    public partial interface IRouteStatisticsLinksDal :IBaseDal<RouteStatisticsLinks>
    {
    }
    public partial interface IExtensionAgentsDal :IBaseDal<ExtensionAgents>
    {
    }
}