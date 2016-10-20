using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MB.Data.Models
{
    public enum ProductStatus
    {

        /// <summary>
        /// 审核未通过
        /// </summary>
        /// 
        ValidFail = -1,

        /// <summary>
        /// 默认未审核
        /// </summary>
        Default = 0,
        /// <summary>
        /// 下架
        /// </summary>
        Offline = 1,
        /// <summary>
        /// 上架
        /// </summary>
        Published = 2,

    }
}
