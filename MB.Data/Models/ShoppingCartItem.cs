﻿using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;


namespace MB.Data.Models
{

    public class ShoppingCartItem : BaseEntity
    {

        /// <summary>
        /// 店铺拥有者ID
        /// </summary>
        public string OwnerId { get; set; }

        public int? OrderId { get; set; }

        public int? PackageId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public int StorageId { get; set; }

        /// <summary>
        /// Gets or sets the shopping cart type identifier
        /// </summary>
        public int ShoppingCartTypeId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the product attributes in XML format
        /// </summary>
        public string AttributesXml { get; set; }

        public string AttributesIds { get; set; }

        public decimal UnitPrice { get; set; }


        public decimal Price { get; set; }
        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }

        public int Status { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> CreateTime { get; set; }

  
        [DTO(false, true)]
        public Nullable<System.DateTime> LastTime { get; set; }

        [DTO(false, true)]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets the log type
        /// </summary>
        public ShoppingCartType ShoppingCartType
        {
            get
            {
                return (ShoppingCartType)this.ShoppingCartTypeId;
            }
            set
            {
                this.ShoppingCartTypeId = (int)value;
            }
        }

        /// <summary>
        /// Gets the log type
        /// </summary>
        public ShoppingCartStatus ShoppingCartStatus
        {
            get
            {
                return (ShoppingCartStatus)this.Status;
            }
            set
            {
                this.Status = (int)value;
            }
        }

        /// <summary>
        /// Gets or sets the product
        /// </summary>
        public virtual Product Product { get; set; }


        public virtual Order Order { get; set; }


        public virtual Package Package { get; set; }

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public virtual ApplicationUser Customer { get; set; }


    }
}
