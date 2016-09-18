

using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;


namespace MB.Data.Mapping
{
    public class ProductCarCateMap : EntityTypeConfiguration<ProductCarCate>
    {
        public ProductCarCateMap()
        {
            this.HasKey(a => a.Id);
            this.HasRequired(pm => pm.CarCate)
                .WithMany()
                .HasForeignKey(pm => pm.CarCateId);

            this.HasRequired(pm => pm.Product)
                .WithMany(p => p.ProductCarCate)
                .HasForeignKey(pm => pm.ProductId);
        }
    }

}
