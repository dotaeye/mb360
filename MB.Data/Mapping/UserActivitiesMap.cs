using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public partial class UserActivitiesMap : EntityTypeConfiguration<UserActivities>
    {
        public UserActivitiesMap()
        {
            this.HasKey(a => a.Id);

            this.Property(x => x.Type);

            this.Property(x => x.UserId).HasMaxLength(50);

            this.HasRequired(a => a.ApplicationUser).WithMany(x => x.UserActivities)

                .HasForeignKey(x => x.UserId).WillCascadeOnDelete(false);
        }
    }
}
