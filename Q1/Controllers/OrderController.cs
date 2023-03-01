using Microsoft.AspNetCore.Mvc;
using Q1.Entity;
using Microsoft.EntityFrameworkCore;

namespace Q1.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private PRN_Sum22_B1Context _context;

        [HttpGet("{From}/{To}")]
        public IActionResult GetOrderByDate(string From, string To)
        {
            List<Object> items = new List<Object>();

            DateTime fromDate = DateTime.Parse(From);
            DateTime toDate = DateTime.Parse(To);
            _context = new PRN_Sum22_B1Context();
            foreach (var item in _context.Orders.Where(o => (o.OrderDate > fromDate) && (o.OrderDate < toDate)).Include(o => o.Customer).Include(o => o.Employee).ThenInclude(e => e.Department).ToList())
            {
                items.Add(new
                {
                    orderId = item.OrderId,
                    customerId = item.CustomerId,
                    customerName = item.Customer.CompanyName,
                    employeeId = item.EmployeeId,
                    employeeName = item.Employee.FirstName + " " + item.Employee.LastName,
                    employeeDepartmentId = item.Employee.DepartmentId,
                    employeeDepartmentName = item.Employee.Department.DepartmentName,
                    orderDate = item.OrderDate,
                    requiredDate = item.RequiredDate,
                    shippedDate = item.ShippedDate,
                    freight = item.Freight,
                    shipName = item.ShipName,
                    shipAddress = item.ShipAddress,
                    shipCity = item.ShipCity,
                    shipRegion = item.ShipRegion,
                    shipPostalCode = item.ShipPostalCode,
                    shipCountry = item.ShipCountry
                });
            }
            return Ok(items);
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            List<Object> items = new List<Object>();
            _context = new PRN_Sum22_B1Context();
            foreach (var item in _context.Orders.Include(o => o.Customer).Include(o => o.Employee).ThenInclude(e => e.Department).ToList())
            {
                items.Add(new
                {
                    orderId =  item.OrderId,
                    customerId = item.CustomerId,
                    customerName = item.Customer.CompanyName,
                    employeeId = item.EmployeeId,
                    employeeName = item.Employee.FirstName+" "+item.Employee.LastName,
                    employeeDepartmentId = item.Employee.DepartmentId,
                    employeeDepartmentName = item.Employee.Department.DepartmentName,
                    orderDate = item.OrderDate,
                    requiredDate = item.RequiredDate,
                    shippedDate = item.ShippedDate,
                    freight = item.Freight,
                    shipName = item.ShipName,
                    shipAddress = item.ShipAddress,
                    shipCity = item.ShipCity,
                    shipRegion = item.ShipRegion,
                    shipPostalCode = item.ShipPostalCode,
                    shipCountry = item.ShipCountry
                });
            }
            return Ok(items);
        }
    }
}
