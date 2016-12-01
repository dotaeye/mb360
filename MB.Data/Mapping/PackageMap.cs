using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class PackageMap : EntityTypeConfiguration<Package>
    {
        public PackageMap()
        {
            this.HasKey(o => o.Id);
            this.Property(o => o.Amount).HasPrecision(18, 8);
            this.Property(o => o.Distance).HasPrecision(18, 4);
            this.Property(o => o.Weight).HasPrecision(18, 4);

            this.Ignore(o => o.PackageStatus);

            this.HasRequired(o => o.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderId);

            this.HasRequired(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .WillCascadeOnDelete(false);
        }
    }
}
