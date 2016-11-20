using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            this.HasKey(a => a.Id);

            this.HasRequired(a => a.CityCate)
                .WithMany()
                .HasForeignKey(a => a.CityId).WillCascadeOnDelete(false);

            this.HasRequired(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId).WillCascadeOnDelete(false);

        }

    }
}
