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
	public class OrderProfile : BaseProfile
    {
        public OrderProfile() : base("OrderProfile")
        {
        }

		protected override void CreateMaps()
        {
			 
			CreateMap<Order, OrderDTO>()
;

			 
			CreateMap<OrderDTO, Order>()
					.ForMember(entity => entity.Customer, o => o.Ignore())
					.ForMember(entity => entity.Address, o => o.Ignore())
					.ForMember(entity => entity.OrderItems, o => o.Ignore())
;
        }
    }
}
