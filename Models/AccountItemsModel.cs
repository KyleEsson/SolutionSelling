namespace SolutionSelling.Models
{
    public class AccountItemsModel
    {
        public List<Item> Items { get; set; }
        public List<Purchases> Purchase { get; set; }
        public List<TopBuyer> TopBuyers { get; set; } // Add this
    }

    public class TopBuyer
    {
        public string BuyerId { get; set; }
        public decimal TotalSpent { get; set; }
    }
}
