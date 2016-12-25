using SQ.Core.Data;

namespace MB.Models
{
    public class OrderPageOption : AntPageOption
    {
        public int OrderStatusId { get; set; }

        public string OrderNo { get; set; }
    }
}