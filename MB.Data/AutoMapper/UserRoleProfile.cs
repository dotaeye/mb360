﻿// <autogenerated>
//   This file was generated by T4 code generator Entry.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using AutoMapper;
using SQ.Core.AutoMapper;
using MB.Data.Models;
using MB.Data.DTO;
using System.Collections.Generic;
using System.Linq;

namespace MB.Data.AutoMapper
{
    public class UserRoleProfile : BaseProfile
    {
        public UserRoleProfile() : base("UserRoleProfile")
        {
        }

        protected override void CreateMaps()
        {

            CreateMap<UserRole, UserRoleDTO>()
                .ForMember(dto => dto.Permission, e => e.MapFrom(src => src.UserPermissions.Select(x => x.Id).ToList()));


            CreateMap<UserRoleDTO, UserRole>()
                    .ForMember(entity => entity.CreateUserId, o => o.Ignore())
                    .ForMember(entity => entity.CreateTime, o => o.Ignore())
                    .ForMember(entity => entity.LastUserId, o => o.Ignore())
                    .ForMember(entity => entity.LastTime, o => o.Ignore())
                    .ForMember(entity => entity.Deleted, o => o.Ignore())
                    .ForMember(entity => entity.UserPermissions, o => o.Ignore())
                    .ForMember(entity => entity.ApplicationUsers, o => o.Ignore())
;
        }
    }
}
