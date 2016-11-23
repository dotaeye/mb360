
using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;


namespace MB.Data.Models
{

    public class SmsCode : BaseEntity
    {
        public string PhoneNumber { get; set; }

        public string Code { get; set; }

        public int CodeTypeId { get; set; }

        public CodeType CodeType
        {
            get
            {
                return (CodeType)this.CodeTypeId;
            }
            set
            {
                this.CodeTypeId = (int)value;
            }
        }

        [DTO(false, true)]
        public Nullable<System.DateTime> CreateTime { get; set; }


        [DTO(false, true)]
        public Nullable<System.DateTime> ExpireTime { get; set; }
    }
}
