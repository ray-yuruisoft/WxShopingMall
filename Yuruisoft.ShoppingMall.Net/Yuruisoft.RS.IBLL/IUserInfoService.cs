using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.UserInfoParams;

namespace Yuruisoft.RS.IBLL
{
    public partial interface IUserInfoService : IBaseService<UserInfo>
    {
        /// <summary>
        /// 批量删除用户信息
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool DeleteEntities(List<int> list);
        /// <summary>
        /// 多条件搜索用户信息
        /// </summary>
        /// <param name="userInfoFilter"></param>
        /// <returns></returns>
        IQueryable<UserInfo> LoadSearchUserInfo(UserInfoFilter userInfoFilter);
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        bool CheckUserNameIsEx(string username);
        /// <summary>
        /// 给用户分配角色信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="RoleIdList">角色编号列表</param>
        /// <returns></returns>
        bool SetUserRole(int userId, List<int> RoleIdList);
        /// <summary>
        /// 设置用户的权限
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="actionId">权限编号</param>
        /// <param name="value">设置值(true:表示允许，false:拒绝)</param>
        /// <returns></returns>
        bool SetUserAction(int userId, int actionId, bool value);
    }
}
