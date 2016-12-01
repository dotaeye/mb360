using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public partial class Package : BaseEntity
    {
        public int AddressId { get; set; }

        public string LogisticCompany { get; set; }

        public string LogisticCode { get; set; }

        public decimal Amount { get; set; }

        public decimal Weight { get; set; }

        public decimal Distance { get; set; }

        public int OrderId { get; set; }

        public int Status { get; set; }

        private ICollection<ShoppingCartItem> _shoppingCartItem;

        public virtual Address Address { get; set; }

        public virtual Order Order { get; set; }

        /// <summary>
        /// Gets or sets order items
        /// </summary>
        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return _shoppingCartItem ?? (_shoppingCartItem = new List<ShoppingCartItem>()); }
            protected set { _shoppingCartItem = value; }
        }


        /// <summary>
        /// Gets or sets the PackageStatus
        /// </summary>
        public PackageStatus PackageStatus
        {
            get
            {
                return (PackageStatus)this.Status;
            }
            set
            {
                this.Status = (int)value;
            }
        }


    }
}