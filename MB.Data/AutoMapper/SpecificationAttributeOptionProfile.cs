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
	public class SpecificationAttributeOptionProfile : BaseProfile
    {
        public SpecificationAttributeOptionProfile() : base("SpecificationAttributeOptionProfile")
        {
        }

		protected override void CreateMaps()
        {
			 
			CreateMap<SpecificationAttributeOption, SpecificationAttributeOptionDTO>()
;

			 
			CreateMap<SpecificationAttributeOptionDTO, SpecificationAttributeOption>()
					.ForMember(entity => entity.SpecificationAttribute, o => o.Ignore())
;
        }
    }
}
