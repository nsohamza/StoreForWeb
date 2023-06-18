using Microsoft.AspNetCore.Mvc;
using Store.Entities.Models;
using System;

namespace Store.Controllers
{
    [Route("api/seed")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly StoreContext _context;

        public SeedController(StoreContext context)
        {
            _context = context;
        }

        // POST api/seed
        [HttpPost]
        public IActionResult SeedData()
        {
           /* // Find the order items to delete
            var orderItemsToDelete = _context.OrderItems.Where(item => item.Id >= 11 && item.Id <= 14);

            // Remove the order items from the context
            _context.OrderItems.RemoveRange(orderItemsToDelete);

            // Save the changes to the database
            _context.SaveChanges();*/

            
            // Create categories
            var shoesCategory = new Category { Name = "Shoes" };
            var clothesCategory = new Category { Name = "Clothes" };

            // Create products
            var runningShoes = new Product { Name = "Running Shoes", Description = "Comfortable running shoes", Price = 79.99m, Category = shoesCategory };
            var casualShoes = new Product { Name = "Casual Shoes", Description = "Stylish casual shoes", Price = 59.99m, Category = shoesCategory };
            var tShirt = new Product { Name = "T-Shirt", Description = "Cotton t-shirt", Price = 19.99m, Category = clothesCategory };
            var dressShirt = new Product { Name = "Dress Shirt", Description = "Formal dress shirt", Price = 39.99m, Category = clothesCategory };

            // Create users
            var johnUser = new User { Username = "john", Password = "password" };
            var janeUser = new User { Username = "jane", Password = "password" };

            // Create orders
            var order1 = new Order { OrderDate = DateTime.Now, User = johnUser };
            var order2 = new Order { OrderDate = DateTime.Now, User = janeUser };

            // Create order items
            var orderItem1 = new OrderItem { Products = runningShoes, Quantity = 1, Price = runningShoes.Price, Orders = order1 };
            var orderItem2 = new OrderItem { Products = tShirt, Quantity = 2, Price = tShirt.Price, Orders = order2 };

            // Add data to the context
            _context.Categories.AddRange(shoesCategory, clothesCategory);
            _context.Products.AddRange(runningShoes, casualShoes, tShirt, dressShirt);
            _context.Users.AddRange(johnUser, janeUser);
            _context.Orders.AddRange(order1, order2);
            _context.OrderItems.AddRange(orderItem1, orderItem2);

            // Save changes to the database
            _context.SaveChanges();

            return Ok("Data seeded successfully.");
        }
    }
}
