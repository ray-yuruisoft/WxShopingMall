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
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;

namespace Yuruisoft.RS.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        /// <summary>
        /// 多态实现，让基类可以调用MODEL，增强代码复用性
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.UserInfoDal;
        }
    }
    public partial class ActionInfoService : BaseService<ActionInfo>, IActionInfoService
    {
        /// <summary>
        /// 多态实现，让基类可以调用MODEL，增强代码复用性
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ActionInfoDal;
        }
    }
    public partial class DepartmentService : BaseService<Department>, IDepartmentService
    {
        /// <summary>
        /// 多态实现，让基类可以调用MODEL，增强代码复用性
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.DepartmentDal;
        }
    }
    public partial class R_UserInfo_ActionInfoService : BaseService<R_UserInfo_ActionInfo>, IR_UserInfo_ActionInfoService
    {
        /// <summary>
        /// 多态实现，让基类可以调用MODEL，增强代码复用性
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.R_UserInfo_ActionInfoDal;
        }
    }
    public partial class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        /// <summary>
        /// 多态实现，让基类可以调用MODEL，增强代码复用性
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RoleInfoDal;
        }
    }
    public partial class RouteStatisticsLinksService : BaseService<RouteStatisticsLinks>, IRouteStatisticsLinksService
    {
        /// <summary>
        /// 多态实现，让基类可以调用MODEL，增强代码复用性
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.RouteStatisticsLinksDal;
        }
    }
    public partial class ExtensionAgentsService : BaseService<ExtensionAgents>, IExtensionAgentsService
    {
        /// <summary>
        /// 多态实现，让基类可以调用MODEL，增强代码复用性
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = this.DbSession.ExtensionAgentsDal;
        }
    }
}