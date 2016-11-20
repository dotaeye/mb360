using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class ShoppingCartItemMap : EntityTypeConfiguration<ShoppingCartItem>
    {
        public ShoppingCartItemMap()
        {
            this.HasKey(sci => sci.Id);

            this.Ignore(sci => sci.ShoppingCartType);

            this.Property(p => p.OwnerId).HasMaxLength(128);

            this.HasRequired(sci => sci.Customer)
              .WithMany(c => c.ShoppingCartItems)
              .HasForeignKey(sci => sci.CustomerId);

            this.HasRequired(sci => sci.Product)
                .WithMany()
                .HasForeignKey(sci => sci.ProductId);
        }
    }
}
