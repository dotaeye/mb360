using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MB.Models
{
    public class ProductCarCateItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CarCateId { get; set; }

        public string CarName { get; set; }

        public bool Selected { get; set; }
    }
}