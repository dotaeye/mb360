using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;


namespace MB.Data.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(200);
            this.Property(p => p.SKU).HasMaxLength(400);
            this.Property(p => p.ImageUrl).HasMaxLength(800);
            this.Property(p => p.DetailUrl).HasMaxLength(800);
            this.Property(p => p.Description).HasMaxLength(800);
            this.Property(p => p.Price).HasPrecision(18, 4);
            this.Property(p => p.VipPrice).HasPrecision(18, 4);
            this.Property(p => p.UrgencyPrice).HasPrecision(18, 4);
        }
    }
}
