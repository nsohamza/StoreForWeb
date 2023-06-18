using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Entities.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }

        public Category()
        {

        }
    }
}


