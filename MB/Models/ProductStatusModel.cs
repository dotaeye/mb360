using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MB.Models
{
    public class ProductStatusModel
    {
        public int StatusType { get; set; }


        public ProductStatusType ProductStatusType
        {
            get
            {
                return (ProductStatusType)this.StatusType;
            }
            set
            {
                this.StatusType = (int)value;
            }
        }

        public List<int> Ids { get; set; }

        public bool Staus { get; set; }
    }

    public enum ProductStatusType
    {

        IsAgreeActive = 0,
        DeAgreeActive = 1,
        IsVipAlbum = 2,
        DeVipAlbum = 3,
        IsFeaturedProduct = 4,
        DeFeaturedProduct = 5,
        IsMatchAllCar = 6,
        DeMatchAllCar = 7,
        IsPublished = 8,
        DePublished = 9
    }
}