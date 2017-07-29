using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuruisoft.RS.Model.Enum
{
    public enum Notifystatus
    {
        /// <summary>
        /// 下单
        /// </summary>
        PlaceOrder = 0,
        /// <summary>
        /// 支付但未发送
        /// </summary>
        PayNoSend = 1,
        /// <summary>
        /// 支付但未发送
        /// </summary>
        PayAndSend = 2
    }
}
