using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQ.Core.Data;

namespace MB.Models
{
    public class ProductPageOption : AntPageOption
    {
        public int CategoryId { get; set; }

        public int ManufacturerId { get; set; }

        public string Name { get; set; }
    }
}