using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public class Product : BaseEntity
    {

        private ICollection<ProductManufacturer> _productManufacturers;

        private ICollection<ProductStorageQuantity> _productStorageQuantity;

        private ICollection<ProductSpecificationAttribute> _productSpecificationAttributes;

        private ICollection<ProductAttributeMapping> _productAttributeMappings;
    



        public string Name { get; set; }

        public int CategoryId { get; set; }

        public string SKU { get; set; }

        public bool isAgreeActive { get; set; }

        public int Status { get; set; }

        public decimal Price { get; set; }

        public decimal VipPrice { get; set; }

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

        public virtual Category Category { get; set; }


        /// <summary>
        /// Gets or sets the collection of ProductManufacturer
        /// </summary>
        public virtual ICollection<ProductManufacturer> ProductManufacturers
        {
            get { return _productManufacturers ?? (_productManufacturers = new List<ProductManufacturer>()); }
            protected set { _productManufacturers = value; }
        }



        /// <summary>
        /// Gets or sets the collection of ProductStorageQuantity
        /// </summary>
        public virtual ICollection<ProductStorageQuantity> ProductStorageQuantity
        {
            get { return _productStorageQuantity ?? (_productStorageQuantity = new List<ProductStorageQuantity>()); }
            protected set { _productStorageQuantity = value; }
        }



        /// <summary>
        /// Gets or sets the product specification attribute
        /// </summary>
        public virtual ICollection<ProductSpecificationAttribute> ProductSpecificationAttributes
        {
            get { return _productSpecificationAttributes ?? (_productSpecificationAttributes = new List<ProductSpecificationAttribute>()); }
            protected set { _productSpecificationAttributes = value; }
        }

        /// <summary>
        /// Gets or sets the product attribute mappings
        /// </summary>
        public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings
        {
            get { return _productAttributeMappings ?? (_productAttributeMappings = new List<ProductAttributeMapping>()); }
            protected set { _productAttributeMappings = value; }
        }

    }
}
