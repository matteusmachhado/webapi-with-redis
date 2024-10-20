using Project.Domain.Interfaces;

namespace Project.Domain.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        private Product() { }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        public void Update(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
    }
}
