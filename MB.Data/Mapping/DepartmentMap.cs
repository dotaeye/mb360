﻿
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using MB.Data.Models;

namespace MB.Data.Mapping
{
    public partial class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            this.HasKey(a => a.Id);

            this.Property(x => x.Name).IsRequired().HasMaxLength(50);

            this.Property(x => x.Code).IsRequired().HasMaxLength(50);

            this.Property(x => x.ImageUrl).HasMaxLength(250);

            this.Property(x => x.CreateTime);

            this.Property(x => x.CreateUserId);

            this.Property(x => x.LastTime);

            this.Property(x => x.LastUserId);

            this.HasOptional(a => a.ParentDepartment).WithMany(x => x.ChildDepartments)

                .HasForeignKey(x => x.ParentId).WillCascadeOnDelete(false);
        }
    }
}
