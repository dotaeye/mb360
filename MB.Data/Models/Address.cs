
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;


namespace MB.Data.Models
{
   
    public class Address : BaseEntity
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public int CityId { get; set; }

        public virtual CityCate CityCate { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Detail { get; set; }

        public bool Default { get; set; }

        public string UserId { get; set; }

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

    }
}
