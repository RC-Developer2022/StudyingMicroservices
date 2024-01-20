using Microsoft.AspNetCore.Mvc;
using AccessApplication.Entities;
using AccessApplication.Methods;
using AccessApplication.Record;
using AccessApplication.Context;
using Microsoft.EntityFrameworkCore;

namespace AccessApplication.Endpoints;

public static class MapAuth
{
    public static void MapAccess(this WebApplication app)
    {
        app.MapPost("/register", UserCreate).WithName(nameof(UserCreate)).WithOpenApi();
        app.MapPost("connect/token", PostToken).WithName("TokenAccessApplication").WithOpenApi();
    }

    public static async Task<IResult> UserCreate([FromBody]UserAccess userAccess, [FromServices] AppDbContext context) 
    {
        try
        {
            if (userAccess != null)
            {
                await context.UserAccess.AddAsync(userAccess);
                return Results.Ok(await context.SaveChangesAsync() > 0);
            }
            return Results.BadRequest();
        }
        catch (Exception)
        {

            return Results.NotFound();
        }
    }

    public static async Task<IResult> PostToken([FromBody] Authentication authentication, [FromServices] IConfiguration configuration, AppDbContext context)
    {
        try
        {
            var user = await context.UserAccess.Where(u => u.Email.Equals(authentication.Email) && u.Password.Equals(authentication.Password)).FirstOrDefaultAsync();

            if (authentication.Email == user.Email && authentication.Password == user.Password)
                if (user.UserType == Enuns.TipoUsuario.Client)
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
