using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuruisoft.RS.IBLL;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.BLL
{
    public partial class RouteStatisticsLinksService : BaseService<RouteStatisticsLinks>, IRouteStatisticsLinksService
    {
        #region 批量删除
        public bool DeleteEntities(List<int> list)
        {
            var LinksInfoList = this.DbSession.RouteStatisticsLinksDal.LoadEntities(u => list.Contains(u.ID));
            if (LinksInfoList != null)
            {
                foreach (var LinksInfo in LinksInfoList)
                {
                    this.DbSession.RouteStatisticsLinksDal.DeleteEntity(LinksInfo);
                }
            }
            return this.DbSession.SaveChanges();//最后调用DBSession中的SaveChanges方法将数据一次性提交会数据库
        }
        #endregion
    }
}
