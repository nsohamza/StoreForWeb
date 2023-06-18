using System;
using Store.Models.DTO;

namespace Store.Models
{
    public class OrderDTO
    {
        public DateTime OrderDate { get; set; }
        public int userId { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }

}

