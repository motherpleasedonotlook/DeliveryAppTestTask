namespace DeliveryApp.Models;
using Microsoft.AspNetCore.Identity;

//класс модели пользователя (заказчика)
public class ApplicationUser : IdentityUser
{
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    //навигационное свойство к заказчику
    public virtual Customer Customer { get; set; }
}