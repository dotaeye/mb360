using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;


namespace MB.Data.Mapping
{
    public class ProductStorageQuantityMap : EntityTypeConfiguration<ProductStorageQuantity>
    {
        public ProductStorageQuantityMap()
        {
            this.HasKey(a => a.Id);

            this.HasRequired(psa => psa.Storage)
                .WithMany()
                .HasForeignKey(psa => psa.StorageId);


            this.HasRequired(psa => psa.Product)
                .WithMany(p => p.ProductStorageQuantity)
                .HasForeignKey(psa => psa.ProductId);

        }
    }
}
