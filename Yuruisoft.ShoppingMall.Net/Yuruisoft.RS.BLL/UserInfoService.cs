using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;
using Yuruisoft.RS.Model.Enum;

namespace Yuruisoft.RS.BLL
{
    public partial class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        #region 批量删除
        public bool DeleteEntities(List<int> list)
        {
            var userInfoList = this.DbSession.UserInfoDal.LoadEntities(u => list.Contains(u.ID));
            if (userInfoList != null)
            {
                foreach (var userInfo in userInfoList)
                {
                    this.DbSession.UserInfoDal.DeleteEntity(userInfo);
                }
            }
            return this.DbSession.SaveChanges();//最后调用DBSession中的SaveChanges方法将数据一次性提交会数据库
        }
        #endregion
        #region 多条件搜索用户信息
        public IQueryable<UserInfo> LoadSearchUserInfo(Model.UserInfoParams.UserInfoFilter userInfoFilter)
        {
            short deleteType = (short)DeleteEnumType.Normal;//标记 0正常，1逻辑，2物理
            var temp = this.DbSession.UserInfoDal.LoadEntities(c => c.DelFlag == deleteType);
            if (!string.IsNullOrEmpty(userInfoFilter.UName)) //判断用户名是否为空
            {
                temp = temp.Where<UserInfo>(u => u.UName.Contains(userInfoFilter.UName));
            }
            if (!string.IsNullOrEmpty(userInfoFilter.URemark))
            {
                temp = temp.Where<UserInfo>(u => u.Remark.Contains(userInfoFilter.URemark));
            }
            if (!string.IsNullOrEmpty(userInfoFilter.UEmail))
            {
                temp = temp.Where<UserInfo>(u => u.UEmail.Contains(userInfoFilter.UEmail));
            }
            if (!string.IsNullOrEmpty(userInfoFilter.UPhoneNumber))
            {
                temp = temp.Where<UserInfo>(u => u.UPhoneNumber.ToString().Contains(userInfoFilter.UPhoneNumber));
            }
            userInfoFilter.TotalCount = temp.Count();
            return temp.OrderBy<UserInfo, string>(u => u.Sort).Skip<UserInfo>((userInfoFilter.PageIndex - 1) * userInfoFilter.PageSize).Take<UserInfo>(userInfoFilter.PageSize);
        }
        #endregion
        #region 检查用户名是否存在
        public bool CheckUserNameIsEx(string UName)
        {
            bool result = false;
            var query = this.DbSession.UserInfoDal.LoadEntities(u => u.UName == UName);
            if (query == null || !query.Any())
            {
                result = true;
            }
            return result;
        }
        #endregion
        #region 为用户分配角色
        public bool SetUserRole(int userId, List<int> RoleIdList)
        {
            var userInfo = this.DbSession.UserInfoDal.LoadEntities(u => u.ID == userId).FirstOrDefault();
            if (userInfo != null)
            {
                if(userInfo.RoleInfo != null)
                    userInfo.RoleInfo.Clear();//删除当前用户已经有的角色.
                foreach (int roleId in RoleIdList)
                {
                    var roleInfo = this.DbSession.RoleInfoDal.LoadEntities(r => r.ID == roleId).FirstOrDefault();
                    //insert into UserInfoRoleInfo(UserId,RoleId) values(userId,roleId)
                    //SqlHelper.ExecuteNoneQuery(sql,new sqlpara);
                    userInfo.RoleInfo.Add(roleInfo);//根据RoleIdList集合中存储的角色编号，获取角色信息，然后给当前用户添加.
                }
            }
            return this.DbSession.SaveChanges();
        }
        #endregion
        #region 给用户设置权限
        public bool SetUserAction(int userId, int actionId, bool value)
        {
            //根据传过来的用户编号与权限编号查询R_UserInfo_ActionInfoDal中是否有该记录
            var actionInfo = this.DbSession.R_UserInfo_ActionInfoDal.LoadEntities(r => r.UserInfoID == userId && r.ActionInfoID == actionId).FirstOrDefault();
            //如果没有就添加
            if (actionInfo == null)
            {
                R_UserInfo_ActionInfo r_UserInfo_ActionInfo = new R_UserInfo_ActionInfo();
                r_UserInfo_ActionInfo.IsPass = value;
                r_UserInfo_ActionInfo.ActionInfoID = actionId;
                r_UserInfo_ActionInfo.UserInfoID = userId;
                this.DbSession.R_UserInfo_ActionInfoDal.AddEntity(r_UserInfo_ActionInfo);
                //return this.DbSession.SaveChanges();
            }
            else//如果就修改
            {
                if (actionInfo.IsPass != value)
                {
                    actionInfo.IsPass = value;
                    //return  this.DbSession.SaveChanges();
                }
            }
            return this.DbSession.SaveChanges();
        }
        #endregion
    }
}
