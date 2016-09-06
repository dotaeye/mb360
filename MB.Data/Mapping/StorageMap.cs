using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;


namespace MB.Data.Mapping
{
    public class StorageMap : EntityTypeConfiguration<Storage>
    {
        public StorageMap()
        {
            this.HasKey(sao => sao.Id);

            this.Property(p => p.Name).IsRequired().HasMaxLength(200);

            this.Property(p => p.Location).IsRequired();
        }
    }
}
