using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.Data.Models
{
    public enum OrderStatus
    {

        /// <summary>
        /// 删除的订单
        /// </summary>
        Deleted = -1,

        /// <summary>
        /// 买家主动取消
        /// </summary>
        Cancelled = 1,

        /// <summary>
        /// 未支付
        /// </summary>
        NotPay = 10,

        /// <summary>
        /// 买家已付款,等待卖家发货
        /// </summary>
        Paied = 20,

        /// <summary>
        /// 卖家已发货，买家等待收货
        /// </summary>
        Shiped = 30,

        /// <summary>
        /// 买家已确认收货
        /// </summary>
        Completed = 40

    }
}
