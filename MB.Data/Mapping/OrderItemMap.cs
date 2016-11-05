using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap() {

            this.Property(orderItem => orderItem.Price).HasPrecision(18, 4);

            this.Property(orderItem => orderItem.UnitPrice).HasPrecision(18, 4);

            this.HasKey(orderItem => orderItem.Id);

            this.HasRequired(orderItem => orderItem.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId);

            this.HasRequired(orderItem => orderItem.Product)
                .WithMany()
                .HasForeignKey(orderItem => orderItem.ProductId);
        }
    }
}
