using Microsoft.AspNetCore.Mvc;
using Usuario.Entities;
using Usuario.Methods;
using Usuario.Record;

namespace Usuario.Endpoints;

public static class MapAuth
{
    private static IConfiguration configuration;

    public static void MapTokenAuth(this WebApplication app)
    {
        app.MapPost("connect/token", PostToken).WithName("TokenAccess").WithOpenApi();
    }

    public static async Task<IResult> PostToken( Autenticacao autenticacao, [FromServices] IConfiguration configuration)
    {
        try
        {
            var usuario = new UsuarioAccess();
            if (autenticacao.Usuario == usuario.Email && autenticacao.Senha == usuario.Password)
                if (usuario.UserType == Enuns.TipoUsuario.Client)
                    return Results.Ok(new Token(configuration).Create("Cliente"));
                else
                    return Results.Ok(new Token(configuration).Create("Empregado"));

            return Results.BadRequest();
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }
}
