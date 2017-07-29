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
using Yuruisoft.RS.IDAL;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.DAL
{
    public partial class UserInfoDal : Base_Dal<UserInfo>, IUserInfoDal
    {

    }
    public partial class ActionInfoDal : Base_Dal<ActionInfo>, IActionInfoDal
    {

    }
    public partial class DepartmentDal : Base_Dal<Department>, IDepartmentDal
    {

    }
    public partial class R_UserInfo_ActionInfoDal : Base_Dal<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoDal
    {

    }
    public partial class RoleInfoDal : Base_Dal<RoleInfo>, IRoleInfoDal
    {

    }
    public partial class RouteStatisticsLinksDal : Base_Dal<RouteStatisticsLinks>, IRouteStatisticsLinksDal
    {

    }
    public partial class ExtensionAgentsDal : Base_Dal<ExtensionAgents>, IExtensionAgentsDal
    {

    }
}