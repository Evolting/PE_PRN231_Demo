using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Entity;

namespace Q1.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : Controller
    {
        private PRN_Sum22_B1Context _context;

        [HttpDelete("{CustomerId}")]
        public IActionResult Delete(string CustomerId)
        {
            _context = new PRN_Sum22_B1Context();
            Customer c = _context.Customers.FirstOrDefault(c => c.CustomerId == CustomerId);
            if(c == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    int orderCount = 0;
                    int orderDetailCount = 0;
                    int customerCount = 0;

                    List<Order> orders = _context.Orders.Include(o => o.OrderDetails).Where(o => o.CustomerId == CustomerId).ToList();
                    List<OrderDetail> orderDetails = orders.SelectMany(o => o.OrderDetails).ToList();

                    if(orderDetails.Count > 0)
                    {
                        _context.OrderDetails.RemoveRange(orderDetails);
                        orderDetailCount = orderDetails.Count;
                    }
                    if(orders.Count > 0)
                    {
                        _context.Orders.RemoveRange(orders);
                        orderCount = orders.Count;
                    }

                    _context.Customers.Remove(c);
                    customerCount = 1;

                    return Ok(new { 
                        customerDeletedCount = customerCount,
                        orderDeletedCount = orderCount,
                        orderDetailDeletedCount = orderDetailCount
                    });
                }
                catch (Exception)
                {
                    return Conflict("There was an unknown error when performing data deletion.");
                }
            }
        }
    }
}
