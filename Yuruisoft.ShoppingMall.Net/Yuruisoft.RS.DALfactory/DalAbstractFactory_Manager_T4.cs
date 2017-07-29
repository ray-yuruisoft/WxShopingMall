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
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.DAL;

namespace Yuruisoft.RS.DALfactory
{
        /// <summary>
        /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
        /// </summary>
        public partial class DALAbstractFactory
        {
            public static UserInfoDal CreateUserInfoDal()
            {
                string fullClassName = DalNameSpace + ".UserInfoDal";//构建类的全名称
                return CreateInstance(fullClassName, DalAssembly) as UserInfoDal;
            }
        }
        /// <summary>
        /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
        /// </summary>
        public partial class DALAbstractFactory
        {
            public static ActionInfoDal CreateActionInfoDal()
            {
                string fullClassName = DalNameSpace + ".ActionInfoDal";//构建类的全名称
                return CreateInstance(fullClassName, DalAssembly) as ActionInfoDal;
            }
        }
        /// <summary>
        /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
        /// </summary>
        public partial class DALAbstractFactory
        {
            public static DepartmentDal CreateDepartmentDal()
            {
                string fullClassName = DalNameSpace + ".DepartmentDal";//构建类的全名称
                return CreateInstance(fullClassName, DalAssembly) as DepartmentDal;
            }
        }
        /// <summary>
        /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
        /// </summary>
        public partial class DALAbstractFactory
        {
            public static R_UserInfo_ActionInfoDal CreateR_UserInfo_ActionInfoDal()
            {
                string fullClassName = DalNameSpace + ".R_UserInfo_ActionInfoDal";//构建类的全名称
                return CreateInstance(fullClassName, DalAssembly) as R_UserInfo_ActionInfoDal;
            }
        }
        /// <summary>
        /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
        /// </summary>
        public partial class DALAbstractFactory
        {
            public static RoleInfoDal CreateRoleInfoDal()
            {
                string fullClassName = DalNameSpace + ".RoleInfoDal";//构建类的全名称
                return CreateInstance(fullClassName, DalAssembly) as RoleInfoDal;
            }
        }
        /// <summary>
        /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
        /// </summary>
        public partial class DALAbstractFactory
        {
            public static RouteStatisticsLinksDal CreateRouteStatisticsLinksDal()
            {
                string fullClassName = DalNameSpace + ".RouteStatisticsLinksDal";//构建类的全名称
                return CreateInstance(fullClassName, DalAssembly) as RouteStatisticsLinksDal;
            }
        }
        /// <summary>
        /// 抽象工厂和工厂都是一样的，解决对象的创建问题。只是创建的方法不一样，抽象工厂使用反射
        /// </summary>
        public partial class DALAbstractFactory
        {
            public static ExtensionAgentsDal CreateExtensionAgentsDal()
            {
                string fullClassName = DalNameSpace + ".ExtensionAgentsDal";//构建类的全名称
                return CreateInstance(fullClassName, DalAssembly) as ExtensionAgentsDal;
            }
        }
}