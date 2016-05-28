namespace FoodOrderingBuddy.Models
{
    public class CartSummaryViewModel
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public decimal Total { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}