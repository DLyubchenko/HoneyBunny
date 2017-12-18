
namespace HoneyBunny.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }

        public Product() { }
        public Product(string name, byte[] image, string description, double price, int categoryId)
        {
            Name = name;
            Image = image;
            Description = description;
            Price = price;
            CategoryId = categoryId;
        }
    }
}