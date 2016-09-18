﻿
// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using AutoMapper;
using SQ.Core.AutoMapper;
using MB.Data.Models;
using MB.Data.DTO;

namespace MB.Data.AutoMapper
{
    public class ProductCarCateProfile : BaseProfile
    {
        public ProductCarCateProfile() : base("ProductCarCateProfile")
        {
        }

        protected override void CreateMaps()
        {

            CreateMap<ProductCarCate, ProductCarCateDTO>()
;


            CreateMap<ProductCarCateDTO, ProductCarCate>()
                    .ForMember(entity => entity.CarCate, o => o.Ignore())
                    .ForMember(entity => entity.Product, o => o.Ignore())
;
        }
    }
}