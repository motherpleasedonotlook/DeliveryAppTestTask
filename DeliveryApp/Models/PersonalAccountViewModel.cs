namespace DeliveryApp.Models.ViewModels
{
    public class PersonalAccountViewModel
    {
        public string UserName { get; set; }
        public List<OrderViewModel> Orders { get; set; }
    }

    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public string Route { get; set; }
    }
}