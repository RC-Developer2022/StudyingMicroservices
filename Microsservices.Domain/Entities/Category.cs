namespace Microsservices.Domain.Entities;
public class Category : Entity
{
    public string Name { get; private set; }
    public bool Active { get; private set; }


    public Category(string name, string createdBy, string editedBy)
    {
        Name = name;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;
        Active = true;
    }
}
