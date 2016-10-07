using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    /// <summary>
    /// Represents a forum
    /// </summary>
    public partial class Forum : BaseEntity
    {
        /// <summary>
        /// Gets or sets the forum group identifier
        /// </summary>
        public int ForumGroupId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the number of topics
        /// </summary>
        public int NumTopics { get; set; }

        /// <summary>
        /// Gets or sets the number of posts
        /// </summary>
        public int NumPosts { get; set; }

        /// <summary>
        /// Gets or sets the last topic identifier
        /// </summary>
        public int LastTopicId { get; set; }

        /// <summary>
        /// Gets or sets the last post identifier
        /// </summary>
        public int LastPostId { get; set; }

        /// <summary>
        /// Gets or sets the last post customer identifier
        /// </summary>
        public int LastPostCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the last post date and time
        /// </summary>
        public DateTime? LastPostTime { get; set; }

        /// <summary>
        /// Gets or sets the display order
        /// </summary>
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

        /// <summary>
        /// Gets the ForumGroup
        /// </summary>
        public virtual ForumGroup ForumGroup { get; set; }
    }
}
