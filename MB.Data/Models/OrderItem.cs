
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;


namespace MB.Data.Models
{
    public partial class OrderItem : BaseEntity
    {

        /// <summary>
        /// Gets or sets the order item identifier
        /// </summary>
        public Guid OrderItemGuid { get; set; }

        /// <summary>
        /// Gets or sets the order identifier
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the product identifie
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }

      
        public decimal UnitPrice { get; set; }

    
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the attribute description
        /// </summary>
        public string AttributeDescription { get; set; }

        /// <summary>
        /// Gets or sets the product attributes in XML format
        /// </summary>
        public string AttributesXml { get; set; }


        public string TrackingNumber { get; set; }


        public bool Deleted { get; set; }
        /// <summary>
        /// Gets or sets the shipped date and time
        /// </summary>
        public DateTime? ShippedTime { get; set; }

        /// <summary>
        /// Gets or sets the delivery date and time
        /// </summary>
        public DateTime? DeliveryTime { get; set; }

        /// <summary>
        /// Gets the order
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual Product Product { get; set; }

     
    }
}
