
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public partial class JobMap : EntityTypeConfiguration<Job>
    {
        public JobMap()
        {
            this.HasKey(a => a.Id);

            this.Property(x => x.Name).IsRequired().HasMaxLength(50);


            this.Property(x => x.CreateTime);

            this.Property(x => x.CreateUserId);

            this.Property(x => x.LastTime);

            this.Property(x => x.LastUserId);

            this.HasRequired(a => a.Department).WithMany(x => x.Jobs)

                .HasForeignKey(x => x.DepartmentId).WillCascadeOnDelete(false);
        }
    }
}
