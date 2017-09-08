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
        /// 退换售后，待审核||待审核
        /// </summary>
        waitingForRepairConfirm = 5,


        /// <summary>
        /// 退换售后，审核不通过，待用户确认||审核不通过
        /// </summary>
        waitingForRepairNoPassUserConfirm = 6,


        /// <summary>
        /// 退换售后，审核通过，待用户发货||商家退换,待买家发货
        /// </summary>
        waitingForRepairPassExchangeUserDeliver = 7,


        /// <summary>
        /// 退换售后，审核通过，待商家收货||待商家收货
        /// </summary>
        waitingForRepairPassMerchantReceive = 8,


        /// <summary>
        /// 用户申请，待好万家介入||商家退款待买家发货
        /// </summary>
        waitingForRepairPassRefundUserDeliver = 9,


        /// <summary>
        /// 退换售后，审核通过，待商家退款||待退款
        /// </summary>
        waitingForRepairPassRefund = 10,


        /// <summary>
        /// 退换售后，审核通过，待商家退款||待好万家介入
        /// </summary>
        waitingForhaowanFamilyDeal = 11
    }
}
