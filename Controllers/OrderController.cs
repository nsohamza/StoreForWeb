using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Entities.Models;
using Store.Models;
using Store.Models.DTO;
using System;
using System.Linq;
using System.Security.Claims;

namespace Store.Controllers
{
    
    [Route("api/orders")]
    [ApiController]

    // Requires authentication for all methods in this controller
    public class OrderController : ControllerBase
    {
        private readonly StoreContext _context;

        public OrderController(StoreContext context)
        {
            _context = context;
        }

      
             [HttpPost]
             [Authorize] // Require authentication (token) for this endpoint
             public IActionResult CreateOrder(OrderDTO orderDTO)
             {
                 try
                 {
                    

                     var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                     if (userIdClaim == null)
                     {
                         return BadRequest("User ID claim not found."); // Handle the case where the user ID claim is not present
                     }

                     var userId = int.Parse(userIdClaim.Value);

                     // Check if the provided user ID matches the authenticated user's ID
                     if (orderDTO.userId != userId)
                     {
                         return Unauthorized("Access denied: The order doesn't belong to the authenticated user.");
                     }

                     // Create a new Order instance
                     var order = new Order
                     {
                         OrderDate = DateTime.Now,
                         userId = userId,
                         OrderItems = new List<OrderItem>() // Create an empty list to hold order items
                     };
                     // Create a new Order instance


                     // Iterate through each item in the order DTO and create the corresponding OrderItem
                     foreach (var item in orderDTO.OrderItems)
                     {
                         // Validate the order item properties
                         if (item.Quantity <= 0)
                         {
                             return BadRequest("Invalid quantity");
                         }

                         // Create the order item
                         var orderItem = new OrderItem
                         {
                             Quantity = item.Quantity,
                             Price = item.Price,
                             ProductId = item.ProductId
                             // Set any other properties of the order item as needed
                         };

                         // Add the order item to the order's collection of order items
                         order.OrderItems.Add(orderItem);
                     }

                     // Save the order to the database
                     _context.Orders.Add(order);
                     _context.SaveChanges();

                     return Ok();
                 }
                 catch
                 {
                     // Handle the exception and return an appropriate response
                     return StatusCode(500, "An error occurred while creating the order");
                 }
             }

        }
    }



