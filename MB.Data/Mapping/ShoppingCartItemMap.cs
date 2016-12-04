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

            this.Property(orderItem => orderItem.Price).HasPrecision(18, 4);

            this.Property(orderItem => orderItem.UnitPrice).HasPrecision(18, 4);

            this.Property(x => x.ImageUrl).HasMaxLength(250);

            this.Property(x => x.Name).HasMaxLength(250);

            this.Ignore(sci => sci.ShoppingCartType);

            this.Ignore(sci => sci.ShoppingCartStatus);

            this.Property(p => p.OwnerId).HasMaxLength(128);

            this.HasRequired(sci => sci.Customer)
              .WithMany(c => c.ShoppingCartItems)
              .HasForeignKey(sci => sci.CustomerId);

            this.HasOptional(sci => sci.Order)
              .WithMany(c => c.ShoppingCartItems)
              .HasForeignKey(sci => sci.OrderId);

            this.HasOptional(sci => sci.Package)
              .WithMany(c => c.ShoppingCartItems)
              .HasForeignKey(sci => sci.PackageId);

            this.HasRequired(sci => sci.Product)
              .WithMany()
              .HasForeignKey(sci => sci.ProductId);
        }
    }
}
