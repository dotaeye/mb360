﻿
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    [DTOIgnore]
    public class CarCate : BaseEntity
    {
        public CarCate()
        {
            this.ChildCarCate = new HashSet<CarCate>();
        }

        public string Name { get; set; }

        public int? ParentId { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public int Level { get; set; }

        public string PinYin { get; set; }

        public string ImageUrl { get; set; }

        public int DisplayOrder { get; set; }

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

        public virtual CarCate ParentCarCate { get; set; }

        public virtual ICollection<CarCate> ChildCarCate { get; set; }

    }
}
