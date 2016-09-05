
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    [DTOIgnore]
    public partial class Department : BaseEntity
    {
        public Department()
        {
            this.Jobs = new HashSet<Job>();
            this.ChildDepartments = new HashSet<Department>();
        }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Code { get; set; }

        [DTO(false, true)]
        public string CreateUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> CreateTime { get; set; }

        [DTO(false, true)]
        public string LastUserId { get; set; }

        [DTO(false, true)]
        public Nullable<System.DateTime> LastTime { get; set; }

        [DTO(false, true)]
        public bool Deleted { get; set; }

        public virtual Department ParentDepartment { get; set; }

        public virtual ICollection<Department> ChildDepartments { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

    }
}
