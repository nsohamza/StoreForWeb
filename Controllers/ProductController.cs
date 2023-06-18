using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Entities.Models;
using Store.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Store.Controllers
{
   
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        
        // GET api/products/search?name=keyword
        [HttpGet("search")]
        public IActionResult SearchProducts(string name)
        {
            try
            {
                var products = _context.Products
                // .Where(p => p.Name.Contains(name))
                    .Where(p => p.Name.ToLower().Contains(name.ToLower()))
                    .Select(p => new Productdto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        CategoryId = p.CategoryId
                    })
                    .ToList();

                return Ok(products);
            }
            catch
            {
                
                return StatusCode(500, "An error occurred while searching for products");
            }
        }


    }
}

