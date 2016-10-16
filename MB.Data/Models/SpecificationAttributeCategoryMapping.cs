using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public class SpecificationAttributeCategoryMapping : BaseEntity
    {

        public int CategoryId { get; set; }

        public int SpecificationAttributeId { get; set; }

        public virtual SpecificationAttribute SpecificationAttribute { get; set; }

        public virtual Category Category { get; set; }
    }
}
