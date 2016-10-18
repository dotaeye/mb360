
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;
namespace MB.Data.Models
{
    [DTOIgnore]
    public class ProductCarCate : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer identifier
        /// </summary>
        public int CarCateId { get; set; }

       
        /// <summary>
        /// Gets or sets the manufacturer
        /// </summary>
        public virtual CarCate CarCate { get; set; }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Product Product { get; set; }
    }
}

