
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class CarMappingMap : EntityTypeConfiguration<CarMapping>
    {
        public CarMappingMap()
        {
            this.HasKey(a => a.Id);

            this.Property(a => a.CarCateList).HasMaxLength(100);

            this.Property(a => a.Name).HasMaxLength(100);

            this.HasRequired(pm => pm.User)
                .WithMany()
                .HasForeignKey(pm => pm.UserId);

            this.HasRequired(pm => pm.CarCate)
                .WithMany()
                .HasForeignKey(pm => pm.CarCateId);
        }
    }

}
