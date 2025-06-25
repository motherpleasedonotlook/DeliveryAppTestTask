using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryApp.Models;

using System.ComponentModel.DataAnnotations;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int AddressId { get; set; }

    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(100)]
    public string City { get; set; } // город

    [Required]
    public string FullAddress { get; set; } // остальной адрес

    [Required]
    public bool IsSender { get; set; }  //true - отправитель, false - получатель

    //навигационное свойство
    public virtual Customer Customer { get; set; }
}