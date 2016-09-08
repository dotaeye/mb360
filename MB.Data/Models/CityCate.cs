
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public class CityCate : BaseEntity
    {
        public CityCate()
        {
            this.ChildCityCate = new HashSet<CityCate>();
        }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public int DisplayOrder { get; set; }

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

        public virtual CityCate ParentCityCate { get; set; }

        public virtual ICollection<CityCate> ChildCityCate { get; set; }

    }
}
