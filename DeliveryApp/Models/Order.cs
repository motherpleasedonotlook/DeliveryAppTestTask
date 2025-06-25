using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderId { get; set; }

    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    [Required]
    [ForeignKey("SenderAddress")]
    public int SenderAddressId { get; set; }

    [Required]
    [ForeignKey("ReceiverAddress")]
    public int ReceiverAddressId { get; set; }

    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Вес должен быть больше 0")]
    public double Weight { get; set; }

    [Required]
    public DateTime PickupDate { get; set; }

    [Required]
    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    //навигационные свойства
    public virtual Customer Customer { get; set; }
    public virtual Address SenderAddress { get; set; }
    public virtual Address ReceiverAddress { get; set; }

    // вычисляем номер заказа
    public string OrderNumber => $"ORD-{CreationDate:yyyyMMdd}-{OrderId:D5}";
}