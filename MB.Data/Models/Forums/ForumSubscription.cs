using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    /// <summary>
    /// Represents a forum subscription item
    /// </summary>
    public partial class ForumSubscription : BaseEntity
    {
        /// <summary>
        /// Gets or sets the forum subscription identifier
        /// </summary>
        public Guid SubscriptionGuid { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the forum identifier
        /// </summary>
        public int ForumId { get; set; }

        /// <summary>
        /// Gets or sets the topic identifier
        /// </summary>
        public int TopicId { get; set; }


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

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual ApplicationUser Customer { get; set; }
    }
}
