﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using MB.Data.Models;

namespace MB.Data.DTO
{
    public class ShoppingCartItemDTO
    {

        public string OwnerId { get; set; }
        public int StorageId { get; set; }
        public int? OrderId { get; set; }

        public int? PackageId { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int ShoppingCartTypeId { get; set; }
        public int ProductId { get; set; }
        public string AttributesXml { get; set; }
        public string AttributesIds { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }

        public int Status { get; set; }
    }
}
