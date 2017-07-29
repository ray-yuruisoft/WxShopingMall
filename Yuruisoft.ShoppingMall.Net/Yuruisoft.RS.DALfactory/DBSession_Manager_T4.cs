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
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.DAL;
using Yuruisoft.RS.IDAL;

namespace Yuruisoft.RS.DALfactory
{
    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        private IUserInfoDal _UserInfo_Dal;
        public IUserInfoDal UserInfoDal
        {
            get
            {
                if (_UserInfo_Dal == null)
                {
                    _UserInfo_Dal = DALAbstractFactory.CreateUserInfoDal();//通过反射获取到动态数据，正式解耦
                }
                return _UserInfo_Dal;
            }
            set
            {
                _UserInfo_Dal = value;
            }
        }
    }    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        private IActionInfoDal _ActionInfo_Dal;
        public IActionInfoDal ActionInfoDal
        {
            get
            {
                if (_ActionInfo_Dal == null)
                {
                    _ActionInfo_Dal = DALAbstractFactory.CreateActionInfoDal();//通过反射获取到动态数据，正式解耦
                }
                return _ActionInfo_Dal;
            }
            set
            {
                _ActionInfo_Dal = value;
            }
        }
    }    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        private IDepartmentDal _Department_Dal;
        public IDepartmentDal DepartmentDal
        {
            get
            {
                if (_Department_Dal == null)
                {
                    _Department_Dal = DALAbstractFactory.CreateDepartmentDal();//通过反射获取到动态数据，正式解耦
                }
                return _Department_Dal;
            }
            set
            {
                _Department_Dal = value;
            }
        }
    }    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        private IR_UserInfo_ActionInfoDal _R_UserInfo_ActionInfo_Dal;
        public IR_UserInfo_ActionInfoDal R_UserInfo_ActionInfoDal
        {
            get
            {
                if (_R_UserInfo_ActionInfo_Dal == null)
                {
                    _R_UserInfo_ActionInfo_Dal = DALAbstractFactory.CreateR_UserInfo_ActionInfoDal();//通过反射获取到动态数据，正式解耦
                }
                return _R_UserInfo_ActionInfo_Dal;
            }
            set
            {
                _R_UserInfo_ActionInfo_Dal = value;
            }
        }
    }    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        private IRoleInfoDal _RoleInfo_Dal;
        public IRoleInfoDal RoleInfoDal
        {
            get
            {
                if (_RoleInfo_Dal == null)
                {
                    _RoleInfo_Dal = DALAbstractFactory.CreateRoleInfoDal();//通过反射获取到动态数据，正式解耦
                }
                return _RoleInfo_Dal;
            }
            set
            {
                _RoleInfo_Dal = value;
            }
        }
    }    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        private IRouteStatisticsLinksDal _RouteStatisticsLinks_Dal;
        public IRouteStatisticsLinksDal RouteStatisticsLinksDal
        {
            get
            {
                if (_RouteStatisticsLinks_Dal == null)
                {
                    _RouteStatisticsLinks_Dal = DALAbstractFactory.CreateRouteStatisticsLinksDal();//通过反射获取到动态数据，正式解耦
                }
                return _RouteStatisticsLinks_Dal;
            }
            set
            {
                _RouteStatisticsLinks_Dal = value;
            }
        }
    }    /// <summary>
    /// DBsesion:数据会话层，负责数据操作类实例的创建。业务层调用数据会话层，获取相应的数据操作类实例
    /// </summary>
    public partial class DBSession : IDBSession
    {
        private IExtensionAgentsDal _ExtensionAgents_Dal;
        public IExtensionAgentsDal ExtensionAgentsDal
        {
            get
            {
                if (_ExtensionAgents_Dal == null)
                {
                    _ExtensionAgents_Dal = DALAbstractFactory.CreateExtensionAgentsDal();//通过反射获取到动态数据，正式解耦
                }
                return _ExtensionAgents_Dal;
            }
            set
            {
                _ExtensionAgents_Dal = value;
            }
        }
    }}