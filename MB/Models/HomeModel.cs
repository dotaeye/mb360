using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MB.Data.DTO;


namespace MB.Models
{
    public class HomeModel
    {

        public HomeModel()
        {
            this.Banners = new List<BannerDTO>();
            this.HotCategories = new List<CategoryDTO>();
            this.HotManufacturers = new List<ManufacturerDTO>();
        }

        public List<BannerDTO> Banners { get; set; }

        public List<CategoryDTO> HotCategories { get; set; }

        public List<ManufacturerDTO> HotManufacturers { get; set; }




    }
}