using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MB.Helpers;
using MB.Data.Service;
using MB.Data.DTO;
using MB.Data.Models;

namespace MB.Models
{
    public class OrderListModal
    {
        public int Status { get; set; }
        public List<OrderDTO> Orders { get; set; }
        public int TotalCount { get; set; }
    }
}