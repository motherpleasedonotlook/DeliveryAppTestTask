using DeliveryApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Models;
using DeliveryApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Controllers
{
    [Authorize]
    public class AccountController(UserManager<ApplicationUser> userManager, AppDbContext context)
        : Controller
    {
        public async Task<IActionResult> Personal()
        {
            var user = await userManager.GetUserAsync(User);
            var customer = await context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.SenderAddress)
                .Include(c => c.Orders)
                    .ThenInclude(o => o.ReceiverAddress)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);

            var model = new PersonalAccountViewModel
            {
                UserName = user.UserName,
                Orders = customer?.Orders.Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderNumber = o.OrderNumber,
                    CreationDate = o.CreationDate,
                    Route = $"{o.SenderAddress.City} - {o.ReceiverAddress.City}"
                }).ToList() ?? []
            };

            return View(model);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await context.Orders
                .Include(o => o.SenderAddress)
                .Include(o => o.ReceiverAddress)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            var model = new OrderDetailsViewModel
            {
                OrderNumber = order.OrderNumber,
                SenderCity = order.SenderAddress.City,
                SenderAddress = order.SenderAddress.FullAddress,
                ReceiverCity = order.ReceiverAddress.City,
                ReceiverAddress = order.ReceiverAddress.FullAddress,
                Weight = order.Weight,
                PickupDate = order.PickupDate
            };

            return PartialView("_OrderDetailsModal", model);
        }
    }
}