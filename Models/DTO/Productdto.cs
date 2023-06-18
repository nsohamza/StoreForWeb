using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Models.DTO
{
	public class Productdto
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Productdto()
		{
		}
	}
}

