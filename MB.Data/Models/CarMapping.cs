

using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    [DTOIgnore]
    public class CarMapping : BaseEntity
    {

        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int CarCateId { get; set; }

        /// <summary>
        /// Gets or sets the product attribute identifier
        /// </summary>
        public string UserId { get; set; }

        public string CarCateList { get; set; }

        public string Name { get; set; }

        public bool Default { get; set; }

        public bool Delete { get; set; }

        public int DisplayOrder { get; set; }

        /// <summary>
        /// Gets the product attribute
        /// </summary>
        public virtual CarCate CarCate { get; set; }

        /// <summary>
        /// Gets the product
        /// </summary>
        public virtual ApplicationUser User { get; set; }

    }
}
