using Usuario.Enuns;

namespace Usuario.Entities;

public class UsuarioAccess
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public TipoUsuario UserType { get; set; }
    public UsuarioAccess()
    {
        
    }
}
