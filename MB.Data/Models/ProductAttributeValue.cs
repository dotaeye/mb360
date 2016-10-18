
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    [DTOIgnore]
    public class ProductAttributeValue : BaseEntity
    {

        public string Name { get; set; }

        public int ProductAttributeMappingId { get; set; }

        public decimal PriceAdjustment { get; set; }

        public string ImageUrl { get; set; }

        [DTO(false, true)]
        public string CreateUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [DTO(false, true)]
        public string LastUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> LastTime { get; set; }

        [DTO(false, true)]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets the product attribute mapping
        /// </summary>
        public virtual ProductAttributeMapping ProductAttributeMapping { get; set; }

    }

}
