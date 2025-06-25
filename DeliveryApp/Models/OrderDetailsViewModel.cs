namespace DeliveryApp.Models.ViewModels
{
    public class OrderDetailsViewModel
    {
        public string OrderNumber { get; set; }
        public string SenderCity { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverAddress { get; set; }
        public double Weight { get; set; }
        public DateTime PickupDate { get; set; }
    }
}