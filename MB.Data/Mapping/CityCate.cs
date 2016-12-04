

using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public partial class CityCateMap : EntityTypeConfiguration<CityCate>
    {
        public CityCateMap()
        {
            this.HasKey(a => a.Code);

            this.Property(x => x.Name).IsRequired().HasMaxLength(50);

            this.Property(x => x.Code).IsRequired().HasMaxLength(50);

            this.Property(x => x.ParentCode).HasMaxLength(50);

            this.Property(x => x.ImageUrl).HasMaxLength(250);

            this.HasOptional(a => a.ParentCityCate).WithMany(x => x.ChildCityCate)

                .HasForeignKey(x => x.ParentCode).WillCascadeOnDelete(false);
        }
    }
}
