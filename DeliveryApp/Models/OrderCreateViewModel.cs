using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Models.ViewModels
{
    public class OrderCreateViewModel
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string SenderCity { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string SenderAddress { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string ReceiverCity { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public string ReceiverAddress { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Range(0.1, double.MaxValue, ErrorMessage = "Вес должен быть больше 0")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime PickupDate { get; set; } = DateTime.UtcNow.Date;
    }
}