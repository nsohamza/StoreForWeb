using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Entities.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }


        public int userId { get; set; }



        public User User { get; set; }

        public virtual List<OrderItem> OrderItems { get; set; }

        public Order()
        {
        }
    }
}