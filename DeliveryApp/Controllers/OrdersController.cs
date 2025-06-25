using DeliveryApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DeliveryApp.Models;
using DeliveryApp.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Controllers
{
    public class OrdersController(AppDbContext context, UserManager<ApplicationUser> userManager)
        : Controller
    {
        public IActionResult Create()
        {
            return PartialView("_CreateOrderModal", new OrderCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_CreateOrderModal", model);
            }

            try
            {
                var user = await userManager.GetUserAsync(User);
                var customer = await context.Customers.FirstOrDefaultAsync(c => c.UserId == user.Id);

                if (customer == null)
                {
                    customer = new Customer { UserId = user.Id };
                    context.Customers.Add(customer);
                    await context.SaveChangesAsync();
                }
                
                var pickupDate = model.PickupDate.Kind == DateTimeKind.Unspecified 
                    ? DateTime.SpecifyKind(model.PickupDate, DateTimeKind.Utc)
                    : model.PickupDate.ToUniversalTime();

                // сздаем адреса
                var senderAddress = new Address
                {
                    CustomerId = customer.CustomerId,
                    City = model.SenderCity,
                    FullAddress = model.SenderAddress,
                    IsSender = true
                };

                var receiverAddress = new Address
                {
                    CustomerId = customer.CustomerId,
                    City = model.ReceiverCity,
                    FullAddress = model.ReceiverAddress,
                    IsSender = false
                };

                context.Addresses.AddRange(senderAddress, receiverAddress);
                await context.SaveChangesAsync();

                //создаем заказ
                var order = new Order
                {
                    CustomerId = customer.CustomerId,
                    SenderAddressId = senderAddress.AddressId,
                    ReceiverAddressId = receiverAddress.AddressId,
                    Weight = model.Weight,
                    PickupDate = pickupDate
                };

                context.Orders.Add(order);
                await context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Заказ успешно создан!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Ошибка при создании заказа: {ex.Message}";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}