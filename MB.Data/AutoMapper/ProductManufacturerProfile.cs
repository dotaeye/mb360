﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using AutoMapper;
using SQ.Core.AutoMapper;
using MB.Data.Models;
using MB.Data.DTO;

namespace MB.Data.AutoMapper
{
	public class ProductManufacturerProfile : BaseProfile
    {
        public ProductManufacturerProfile() : base("ProductManufacturerProfile")
        {
        }

		protected override void CreateMaps()
        {
			 
			CreateMap<ProductManufacturer, ProductManufacturerDTO>()
;

			 
			CreateMap<ProductManufacturerDTO, ProductManufacturer>()
					.ForMember(entity => entity.Manufacturer, o => o.Ignore())
					.ForMember(entity => entity.Product, o => o.Ignore())
;
        }
    }
}