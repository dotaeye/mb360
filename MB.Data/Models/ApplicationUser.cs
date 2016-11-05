using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using SQ.Core.DTO;

namespace MB.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<UserActivities> _userActivities;
        private ICollection<ShoppingCartItem> _shoppingCartItems;
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }


        public string Avatar { get; set; }

        public bool Sex { get; set; }

        public string PhonePrefix { get; set; }

        public string OpenId { get; set; }

        public int? OpenTypeId { get; set; }

        public int UserRoleId { get; set; }

        public int JobId { get; set; }

        public bool Deleted { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> LastLoginTime { get; set; }

        [DTO(false, true)]
        public string CreateUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [DTO(false, true)]
        public string LastUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> LastTime { get; set; }


        public virtual UserRole UserRole { get; set; }

        public virtual Job Job { get; set; }

        public virtual ICollection<UserActivities> UserActivities
        {
            get { return _userActivities ?? (_userActivities = new List<UserActivities>()); }
            protected set { _userActivities = value; }
        }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return _shoppingCartItems ?? (_shoppingCartItems = new List<ShoppingCartItem>()); }
            protected set { _shoppingCartItems = value; }
        }

    }
}
