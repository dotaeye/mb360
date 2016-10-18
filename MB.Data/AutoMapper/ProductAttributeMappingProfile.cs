﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using AutoMapper;
using SQ.Core.AutoMapper;
using MB.Data.Models;
using MB.Data.DTO;
using System.Linq;
using System.Data.Entity;

namespace MB.Data.AutoMapper
{
    public class ProductAttributeMappingProfile : BaseProfile
    {
        public ProductAttributeMappingProfile() : base("ProductAttributeMappingProfile")
        {
        }

        protected override void CreateMaps()
        {

            CreateMap<ProductAttributeMapping, ProductAttributeMappingDTO>()
                .ForMember(dto => dto.ValueCount, entity => entity.MapFrom(ety => ety.ProductAttributeValues.Count(x=>!x.Deleted)));


            CreateMap<ProductAttributeMappingDTO, ProductAttributeMapping>()

                    .ForMember(entity => entity.ProductAttribute, o => o.Ignore())
                    .ForMember(entity => entity.Product, o => o.Ignore())
                    .ForMember(entity => entity.ProductAttributeValues, o => o.Ignore());
        }
    }
}
