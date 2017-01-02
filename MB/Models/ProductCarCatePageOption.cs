using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQ.Core.Data;

namespace MB.Models
{
    public class ProductCarCatePageOption
    {
        public int ProductId { get; set; }
        public int CarCateId { get; set; }
    }

    public class ProductCarCateModel
    {
        public int ProductId { get; set; }
        public List<int> Ids { get; set; }
    }
}