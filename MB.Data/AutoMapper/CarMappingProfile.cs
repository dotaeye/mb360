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
    public class CarMappingProfile : BaseProfile
    {
        public CarMappingProfile() : base("CarMappingProfile")
        {
        }

        protected override void CreateMaps()
        {

            CreateMap<CarMapping, CarMappingDTO>()
;


            CreateMap<CarMappingDTO, CarMapping>()
                    .ForMember(entity => entity.User, o => o.Ignore())
                    .ForMember(entity => entity.CarCate, o => o.Ignore())
                    .ForMember(entity => entity.Delete, o => o.Ignore())
;
        }
    }
}
