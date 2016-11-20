using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MB.Models
{
    public class ProductDetailModel
    {
        public ProductDetailModel()
        {
            ProductAttributes = new List<ProductAttributeModel>();
            ProductSpecifications = new List<ProductSpecificationModel>();
            ProductStorages = new List<ProductStorageModel>();
        }

        public string OwnerId { get; set; }

        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string SKU { get; set; }
        public Boolean isAgreeActive { get; set; }

        public int ManufacturerId { get; set; }

        public bool IsFeaturedProduct { get; set; }

        public int Status { get; set; }
        public Decimal Price { get; set; }
        public Decimal VipPrice { get; set; }
        public string ImageUrl { get; set; }
        public decimal UrgencyPrice { get; set; }
        public string Description { get; set; }
        public string DetailUrl { get; set; }
        public int Id { get; set; }

        public IList<ProductAttributeModel> ProductAttributes { get; set; }

        public IList<ProductSpecificationModel> ProductSpecifications { get; set; }

        public IList<ProductStorageModel> ProductStorages { get; set; }

    }

    public partial class ProductStorageModel
    {

        public int ProductId { get; set; }
        public int StorageId { get; set; }
        public int Quantity { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
    }



    public partial class ProductAttributeModel
    {
        public ProductAttributeModel()
        {
            Values = new List<ProductAttributeValueModel>();
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ProductAttributeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public IList<ProductAttributeValueModel> Values { get; set; }

    }

    public partial class ProductAttributeValueModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal PriceAdjustment { get; set; }

        public bool IsPreSelected { get; set; }

        public string ImageUrl { get; set; }
    }

    public partial class ProductSpecificationModel
    {

        /// <summary>
        /// Specificartion attribute ID
        /// </summary>
        public int SpecificationAttributeId { get; set; }

        /// <summary>
        /// Specificartion attribute name
        /// </summary>
        public string SpecificationAttributeName { get; set; }

        /// <summary>
        /// Option value
        /// this value is already HTML encoded
        /// </summary>
        public string Value { get; set; }

    }
}