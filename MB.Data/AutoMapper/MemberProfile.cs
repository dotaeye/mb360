using AutoMapper;
using SQ.Core.AutoMapper;
using MB.Data.Models;
using MB.Data.DTO;

namespace MB.Data.AutoMapper
{
    public class MemberProfile : BaseProfile
    {
        public MemberProfile() : base("MemberProfile")
        {
        }

        protected override void CreateMaps()
        {

            CreateMap<ApplicationUser, MemberDTO>()
                .ForMember(dto => dto.Password, entity => entity.Ignore());


            CreateMap<MemberDTO, ApplicationUser>()
                    .ForMember(entity => entity.CreateUserId, o => o.Ignore())
                    .ForMember(entity => entity.LastUserId, o => o.Ignore())
                    .ForMember(entity => entity.LastTime, o => o.Ignore())
                    .ForMember(entity => entity.OpenId, o => o.Ignore())
                    .ForMember(entity => entity.OpenTypeId, o => o.Ignore())
                    .ForMember(entity => entity.Deleted, o => o.Ignore())
                    .ForMember(entity => entity.PasswordHash, o => o.Ignore())
                    .ForMember(entity => entity.SecurityStamp, o => o.Ignore())
                    .ForMember(entity => entity.TwoFactorEnabled, o => o.Ignore())
                    .ForMember(entity => entity.LockoutEndDateUtc, o => o.Ignore())
                    .ForMember(entity => entity.LockoutEnabled, o => o.Ignore())
                    .ForMember(entity => entity.AccessFailedCount, o => o.Ignore())
                    .ForMember(entity => entity.UserActivities, o => o.Ignore())
                    .ForMember(entity => entity.UserRole, o => o.Ignore())
                    .ForMember(entity => entity.Job, o => o.Ignore())
                    .ForMember(entity => entity.Roles, o => o.Ignore())
                    .ForMember(entity => entity.Claims, o => o.Ignore())
                    .ForMember(entity => entity.Logins, o => o.Ignore())
                             ;
        }

    }
}
