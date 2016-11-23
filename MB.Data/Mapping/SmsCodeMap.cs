
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public class SmsCodeMap : EntityTypeConfiguration<SmsCode>
    {
        public SmsCodeMap()
        {
            this.HasKey(a => a.Id);

            this.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(50);

            this.Property(x => x.Code).IsRequired().HasMaxLength(20);

            this.Property(x => x.CreateTime).IsRequired();

            this.Property(x => x.ExpireTime).IsRequired();

            this.Ignore(x => x.CodeType);

        }
    }
}
