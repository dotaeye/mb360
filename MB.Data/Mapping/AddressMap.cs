using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            this.HasKey(a => a.Id);

            this.Property(x => x.CityCode).HasMaxLength(50);
            this.Property(x => x.CityCodeList).HasMaxLength(150);
            this.Property(x => x.Province).HasMaxLength(50);
            this.Property(x => x.Area).HasMaxLength(50);
            this.Property(x => x.County).HasMaxLength(50);

            this.HasRequired(a => a.CityCate)
                .WithMany()
                .HasForeignKey(a => a.CityCode).WillCascadeOnDelete(false);

            this.HasRequired(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId).WillCascadeOnDelete(false);

        }

    }
}
