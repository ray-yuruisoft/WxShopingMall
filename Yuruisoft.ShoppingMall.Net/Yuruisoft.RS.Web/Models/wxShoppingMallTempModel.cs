using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yuruisoft.RS.Web.Models
{
    public class wxShoppingMallTempModel
    {      
        public class orderInfo//重新整合后的数据结构
        {
            public int waitForPayItemCount { get; set; }
            public int waitForConfirmItemCount { get; set; }
            public int waitForCommentItemCount { get; set; }
            public int waitForRepairItemCount { get; set; }
            public orderModel[] myOrders { get; set; }
            public class orderModel
            {
                public string orderNumber { get; set; }
                public string subTime { get; set; }
                public short orderStatus { get; set; }              
                public int allItemCount { get; set; }
                public string feeSum { get; set; }
                public string carriage { get; set; }
                public string deliveryCity { get; set; }
                public string deliveryAddress { get; set; }
                public string deliveryName { get; set; }
                public string deliveryPhoneNumber { get; set; }
                public class detailModel
                {
                    public class produceArrModel
                    {
                        public int id { get; set; }
                        public int itemCount { get; set; }
                        public string listTitle { get; set; }
                        public string listImageUrl { get; set; }
                        public string price { get; set; }
                    }
                    public int allItemCount { get; set; }
                    public int merchantId { get; set; }
                    public string merchantName { get; set; }
                    public string feeSum { get; set; }
                    public produceArrModel[] produceArr { get; set; }
                }
                public detailModel[] detail { get; set; }
                public class myOrders//原始客户端过来的数据结构
                {
                    public int allItemCount { get; set; }
                    public string feeSum { get; set; }
                    public string carriage { get; set; }
                    public string deliveryCity { get; set; }
                    public string deliveryAddress { get; set; }
                    public string deliveryName { get; set; }
                    public string deliveryPhoneNumber { get; set; }
                    
                    public detailModel[] detail { get; set; }
                }
            }
        }
    }
}



