using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    /// <summary>
    /// Represents a forum post vote
    /// </summary>
    public partial class ForumPostVote : BaseEntity
    {
        /// <summary>
        /// Gets or sets the forum post identifier
        /// </summary>
        public int ForumPostId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this vote is up or is down
        /// </summary>
        public bool IsUp { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets the post
        /// </summary>
        public virtual ForumPost ForumPost { get; set; }
    }
}
