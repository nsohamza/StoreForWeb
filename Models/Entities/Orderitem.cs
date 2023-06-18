using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace Store.Entities.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public virtual Product Products { get; set; }

        public virtual Order Orders { get; set; }

        public OrderItem()
        {
        }
    }
}
