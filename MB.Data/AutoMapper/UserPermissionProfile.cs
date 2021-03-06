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
	public class UserPermissionProfile : BaseProfile
    {
        public UserPermissionProfile() : base("UserPermissionProfile")
        {
        }

		protected override void CreateMaps()
        {
			 
			CreateMap<UserPermission, UserPermissionDTO>()
;

			 
			CreateMap<UserPermissionDTO, UserPermission>()
					.ForMember(entity => entity.CreateUserId, o => o.Ignore())
					.ForMember(entity => entity.CreateTime, o => o.Ignore())
					.ForMember(entity => entity.LastUserId, o => o.Ignore())
					.ForMember(entity => entity.LastTime, o => o.Ignore())
					.ForMember(entity => entity.Deleted, o => o.Ignore())
					.ForMember(entity => entity.UserRoles, o => o.Ignore());
        }
    }
}
