using AccessApplication.Abstract;
using AccessApplication.Enuns;

namespace AccessApplication.Entities;

public class UserAccess : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public TipoUsuario UserType { get; set; }
    public UserAccess()
    {
        
    }
}
