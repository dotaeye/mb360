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
            this.Banners = new List<BannerModel>();
            this.HotCategoires = new List<CategoryDTO>();
            this.HotManufacturers = new List<ManufacturerDTO>();
        }

        public List<BannerModel> Banners { get; set; }

        public List<CategoryDTO> HotCategoires { get; set; }

        public List<ManufacturerDTO> HotManufacturers { get; set; }

        public class BannerModel
        {
            public int Id { get; set; }
            public string Url { get; set; }
            public string ImageUrl { get; set; }
            public bool NativeRoute { get; set; }
        }


    }
}