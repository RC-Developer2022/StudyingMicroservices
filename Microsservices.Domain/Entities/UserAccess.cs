using Microsservices.Domain.Enuns;

namespace Microsservices.Domain.Entities;
public class UserAccess : Entity
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string ConfirmPassword { get; private set; }
    public UserType UserType { get; private set; }
    public UserAccess(){}

    public UserAccess(string name, string email, string password, string confirmPassword, UserType userType, string createdBy, string editedBy) 
    {
        Name = name;
        Email = email;
        Password = password;
        ConfirmPassword = confirmPassword;
        UserType = userType;
        CreatedBy = createdBy;
        CreatedOn = DateTime.Now;
        EditedBy = editedBy;
        EditedOn = DateTime.Now;
    }
}

