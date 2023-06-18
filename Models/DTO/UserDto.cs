using System;
using System.ComponentModel.DataAnnotations;

namespace Store.Models.DTO
{
	public class UserDto
	{
            [Required]
            [StringLength(50)]
            public string Username { get; set; }

            [Required]
            [StringLength(100)]
            public string Password { get; set; }
        
    }

}


