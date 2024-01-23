using Microsservices.Domain.Entities;

namespace Microsservices.Application.Core.DTO;

public class ProductsDTO
{
    public string Name { get; private set; }
    public string CategoryId { get; private set; }
    public Category Category { get; private set; }
    public string Description { get; private set; }
    public bool HasStock { get; private set; }
    public bool Active { get; private set; } = true;
    public decimal Price { get; private set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string EditedBy { get; set; }
    public DateTime EditedOn { get; set; }
    public ICollection<Order> Orders { get; set; }

    private ProductsDTO() { }

    public ProductsDTO(string name, Category category, string description, decimal price, bool hasStock, string createdBy)
    {
        Name = name;
        Category = category;
        Description = description;
        HasStock = hasStock;
        CreatedBy = createdBy;
        EditedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;
        Price = price;
    }
}
