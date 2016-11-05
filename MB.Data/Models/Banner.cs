
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;


namespace MB.Data.Models
{
    [DTOIgnore]
    public class  Banner : BaseEntity
    {
        public string Url { get; set; }

        public string ImageUrl { get; set; }

        public bool NativeRoute { get; set; }

        public int DisplayOrder { get; set; }

        public int Status { get; set; }

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
