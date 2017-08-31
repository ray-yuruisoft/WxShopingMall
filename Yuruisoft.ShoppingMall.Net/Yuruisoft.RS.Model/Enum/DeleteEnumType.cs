using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.Enum
{
    public enum DeleteEnumType
    {
        /// <summary>
        /// 正常显示
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 逻辑删除
        /// </summary>
        LogicDelete = 1,
        /// <summary>
        /// 物理删除
        /// </summary>
        PhyicDelete = 2
    }
    public enum orderStatus
    {
        /// <summary>
        /// 待付款
        /// </summary>
        waitingForPay = 0,
        /// <summary>
        /// 待发货
        /// </summary>
        waitingForDeliver = 1,
        /// <summary>
        /// 待确认收货
        /// </summary>
        waitingForConfirm = 2,
        /// <summary>
        /// 待评价
        /// </summary>
        waitingForComment = 3,
        /// <summary>
        /// 待再次购买
        /// </summary>
        waitingForReBuy = 4,
        /// <summary>
        /// 退换售后 TODO：待卖家发货，买家收货，评价等
        /// </summary>
        waitingForRepair = 5
    }
}
