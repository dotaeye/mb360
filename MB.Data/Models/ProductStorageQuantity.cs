
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace MB.Data.Models
{
    [DTOIgnore]
    public class ProductStorageQuantity : BaseEntity
    {
        public int ProductId { get; set; }

        public int StorageId { get; set; }

        public int Quantity { get; set; }

        [DTO(false, true)]
        public string CreateUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [DTO(false, true)]
        public string LastUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> LastTime { get; set; }

        public virtual Product Product { get; set; }

        public virtual Storage Storage { get; set; }
    }
}
