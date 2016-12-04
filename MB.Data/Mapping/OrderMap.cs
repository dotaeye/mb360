using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            this.HasKey(o => o.Id);
            this.Property(o => o.OrderTotal).HasPrecision(18, 8);
            this.Property(o => o.OrderTotalExclShipping).HasPrecision(18, 4);
            this.Property(o => o.OrderShipping).HasPrecision(18, 4);

            this.Property(o => o.PrePayId).HasMaxLength(250);
            this.Property(o => o.WeChatSign).HasMaxLength(350);

            this.Ignore(o => o.OrderStatus);

            this.HasRequired(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);


            //code below is commented because it causes some issues on big databases - http://www.nopcommerce.com/boards/t/11126/bug-version-20-command-confirm-takes-several-minutes-using-big-databases.aspx
            //this.HasRequired(o => o.BillingAddress).WithOptional().Map(x => x.MapKey("BillingAddressId")).WillCascadeOnDelete(false);
            //this.HasOptional(o => o.ShippingAddress).WithOptionalDependent().Map(x => x.MapKey("ShippingAddressId")).WillCascadeOnDelete(false);
            this.HasRequired(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .WillCascadeOnDelete(false);
        
        }
    }
}
