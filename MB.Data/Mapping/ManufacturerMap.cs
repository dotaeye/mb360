
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public partial class ManufacturerMap : EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerMap()
        {
            this.HasKey(a => a.Id);
            this.Property(m => m.Name).IsRequired().HasMaxLength(200);
            this.Property(p => p.Description).HasMaxLength(400);
            this.Property(p => p.ImageUrl).HasMaxLength(800);
        }
    }
}
