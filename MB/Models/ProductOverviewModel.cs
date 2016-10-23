using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MB.Models
{
    public partial class ProductOverviewModel
    {
        public ProductOverviewModel()
        {
            SpecificationAttributeModels = new List<ProductSpecificationModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal VipPrice { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public double? Distance { get; set; }

        public bool MarkAsNew { get; set; }

        //specification attributes
        public IList<ProductSpecificationModel> SpecificationAttributeModels { get; set; }
      
    }
}