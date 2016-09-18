
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace MB.Data.Models
{
    public class Storage : BaseEntity
    {

        public string Name { get; set; }

        public string Address { get; set; }

        public DbGeography Location { get; set; }

        public int StorageType { get; set; }

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
