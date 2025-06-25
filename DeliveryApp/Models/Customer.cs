namespace DeliveryApp.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }  // id_заказчика

    //связь с пользователем
    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    
    //навигационные
    public virtual ICollection<Address> Addresses { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}