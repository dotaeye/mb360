using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;


namespace MB.Data.Mapping
{
    public class ProductAttributeValueMap : EntityTypeConfiguration<ProductAttributeValue>
    {
        public ProductAttributeValueMap()
        {
            this.HasKey(a => a.Id);
            this.Property(pav => pav.Name).IsRequired().HasMaxLength(400);
          
            this.Property(pav => pav.PriceAdjustment).HasPrecision(18, 4);
         
        }
    }
}
