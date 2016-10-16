using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public class ProductAttributeCategoryMapping : BaseEntity
    {

        public int CategoryId { get; set; }

        public int ProductAttributeId { get; set; }

  
        public virtual ProductAttribute ProductAttribute { get; set; }

        public virtual Category Category { get; set; }
    }
}
