namespace Microsservices.Domain.Entities;
public class Order : Entity
{
    public string ClientId { get; private set; }
    public List<Products> Products { get; private set; }
    public decimal Total { get; private set; }
    public string DeliveryAddress { get; private set; }
    private Order() { }

    public Order(string clientId, string clientName, List<Products> products, string deliveryAddress)
    {
        ClientId = clientId;
        Products = products;
        CreatedBy = clientName;
        EditedBy = clientName;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Total = 0;
        Products.ForEach(o =>
        {
            Total += o.Price;
        });
    }
}
