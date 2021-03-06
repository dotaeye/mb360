﻿
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    [DTOIgnore]
    public class CityCate : BaseEntity
    {
        public CityCate()
        {
            this.ChildCityCate = new HashSet<CityCate>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }

        public int DisplayOrder { get; set; }

        public string Description { get; set; }

        public int Level { get; set; }

        public string ImageUrl { get; set; }


        public string PinYin { get; set; }

        [DTO(false, true)]
        public bool Deleted { get; set; }

        public virtual CityCate ParentCityCate { get; set; }

        public virtual ICollection<CityCate> ChildCityCate { get; set; }

    }
}
