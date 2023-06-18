using System;
using System.ComponentModel.DataAnnotations;


namespace Store.Entities.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }


        public virtual List<Order> Orders { get; set; }

        public User()
        {
        }
    }
}
