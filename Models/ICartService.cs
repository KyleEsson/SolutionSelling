namespace SolutionSelling.Models
{
    public interface ICartService
    {
        void Add(Item item, int quantity);
        void Remove(string productId);
        void Clear();
        Cart Get();
    }
}
