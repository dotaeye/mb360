using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MB.Data.DTO;

namespace MB.Models
{
    public partial class CategoryModel 
    {
        public CategoryModel()
        {
           
            FeaturedProducts = new List<ProductOverviewModel>();
            Products = new List<ProductOverviewModel>();
            PagingFilteringContext = new CatalogPagingFilteringModel();
            SubCategories = new List<CategoryDTO>();
          
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }


        public string ImageUrl { get; set; }

        public CatalogPagingFilteringModel PagingFilteringContext { get; set; }

        public IList<CategoryDTO> SubCategories { get; set; }

        public IList<ProductOverviewModel> FeaturedProducts { get; set; }
        public IList<ProductOverviewModel> Products { get; set; }

    }
}