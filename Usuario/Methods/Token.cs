using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Usuario.Methods;

public class Token(IConfiguration configuration)
{

    public object Create(string role)
    {
        var key = Encoding.ASCII.GetBytes(configuration["Autenticacao:key"]);

        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
            new Claim(ClaimTypes.Role , role)
        }),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandle = new JwtSecurityTokenHandler();
        var token = tokenHandle.CreateToken(tokenConfig);
        var tokenString = tokenHandle.WriteToken(token);
        return new
        {
            token = tokenString
        };
    }
}
