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
    public class ShoppingCartItemProfile : BaseProfile
    {
        public ShoppingCartItemProfile() : base("ShoppingCartItemProfile")
        {
        }

        protected override void CreateMaps()
        {

            CreateMap<ShoppingCartItem, ShoppingCartItemDTO>()
;


            CreateMap<ShoppingCartItemDTO, ShoppingCartItem>()
                    .ForMember(entity => entity.CustomerId, o => o.Ignore())
                    .ForMember(entity => entity.Status, o => o.Ignore())
                    .ForMember(entity => entity.ShoppingCartType, o => o.Ignore())
                    .ForMember(entity => entity.CreateTime, o => o.Ignore())
                    .ForMember(entity => entity.LastTime, o => o.Ignore())
                    .ForMember(entity => entity.Deleted, o => o.Ignore())
                    .ForMember(entity => entity.Product, o => o.Ignore())
                    .ForMember(entity => entity.Customer, o => o.Ignore())
;
        }
    }
}
