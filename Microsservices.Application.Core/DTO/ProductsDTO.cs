using Microsservices.Domain.Entities;

namespace Microsservices.Application.Core.DTO;

public class ProductsDTO
{
    public string Name { get;  set; }
    public string CategoryId { get;  set; }
    public Category Category { get;  set; }
    public string Description { get;  set; }
    public bool HasStock { get;  set; }
    public bool Active { get;  set; } = true;
    public decimal Price { get;  set; }
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
