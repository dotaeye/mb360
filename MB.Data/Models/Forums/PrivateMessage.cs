using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    /// <summary>
    /// Represents a private message
    /// </summary>
    public partial class PrivateMessage : BaseEntity
    {
        /// <summary>
        /// Gets or sets the store identifier
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier who sent the message
        /// </summary>
        public int FromCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier who should receive the message
        /// </summary>
        public int ToCustomerId { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indivating whether message is read
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// Gets or sets a value indivating whether message is deleted by author
        /// </summary>
        public bool IsDeletedByAuthor { get; set; }

        /// <summary>
        /// Gets or sets a value indivating whether message is deleted by recipient
        /// </summary>
        public bool IsDeletedByRecipient { get; set; }

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
        /// Gets the customer who sent the message
        /// </summary>
        public virtual ApplicationUser FromCustomer { get; set; }

        /// <summary>
        /// Gets the customer who should receive the message
        /// </summary>
        public virtual ApplicationUser ToCustomer { get; set; }
    }
}
