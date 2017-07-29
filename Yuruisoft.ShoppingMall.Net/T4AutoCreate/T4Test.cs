using Yuruisoft.RS.Model;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace T4AutoCreate
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
}