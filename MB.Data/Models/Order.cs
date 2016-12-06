using System;
using SQ.Core.Data;
using SQ.Core.DTO;
using System.Collections.Generic;

namespace MB.Data.Models
{
    public partial class Order : BaseEntity
    {
        private ICollection<ShoppingCartItem> _shoppingCartItem;

        public Guid OrderGuid { get; set; }
        public string CustomerId { get; set; }

        public string WeChatSign { get; set; }
        public string PrePayId { get; set; }
        public string OutTradeNo { get; set; }
        public string TimeSpan { get; set; }
        public string NonceStr { get; set; }

        public int AddressId { get; set; }
        public bool PickUpInStore { get; set; }
        public int OrderStatusId { get; set; }
        public string PaymentMethodSystemName { get; set; }
        public string PaymentMethodDesction { get; set; }
        public decimal OrderTotalExclShipping { get; set; }
        public decimal OrderShipping { get; set; }
        public decimal OrderTotal { get; set; }
        public string CustomerIp { get; set; }
        public DateTime? PaidDate { get; set; }
        public string ShippingMethod { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual ApplicationUser Customer { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<ShoppingCartItem> ShoppingCartItems
        {
            get { return _shoppingCartItem ?? (_shoppingCartItem = new List<ShoppingCartItem>()); }
            protected set { _shoppingCartItem = value; }
        }

        public OrderStatus OrderStatus
        {
            get
            {
                return (OrderStatus)this.OrderStatusId;
            }
            set
            {
                this.OrderStatusId = (int)value;
            }
        }
    }
}
