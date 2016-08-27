using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public partial class UserPermissionMap : EntityTypeConfiguration<UserPermission>
    {
        public UserPermissionMap()
        {
            this.HasKey(a => a.Id);

            this.Property(x => x.Name).IsRequired().HasMaxLength(50);

            this.Property(x => x.Controller).IsRequired().HasMaxLength(50);

            this.Property(x => x.Action).IsRequired().HasMaxLength(50);

            this.Property(x => x.Group).IsRequired().HasMaxLength(50);

            this.Property(x => x.IsApi);

            this.Property(x => x.CreateTime);

            this.Property(x => x.CreateUserId);

            this.Property(x => x.LastTime);

            this.Property(x => x.LastUserId);

            this.HasMany(a => a.UserRoles).WithMany(x => x.UserPermissions).Map
                (
                    m =>
                    {
                        m.MapLeftKey("PermissionId");
                        m.MapRightKey("RoleId");
                        m.ToTable("UserRole_Permissions");
                    }
                ); 
        }
    }
}
