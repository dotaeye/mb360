
using System;
using MB.Data.Models;


namespace MB.Data.DTO
{
    public class BannerDTO
    {
    
        public int Id { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public bool NativeRoute { get; set; }

        public int DisplayOrder { get; set; }

        public int Status { get; set; }
    }
}

