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
        app.MapPut("Forgot/Password", (ForgotPassword)).WithName(nameof(ForgotPassword)).WithOpenApi();
        app.MapPost("/register", UserCreate).WithName(nameof(UserCreate)).WithOpenApi();
        app.MapPost("connect/token", PostToken).WithName("TokenAccessApplication").WithOpenApi();
    }

    public static async Task<IResult> ForgotPassword([FromBody] PasswordForgot passwordForgot, [FromHeader] Guid Id, [FromServices]AppDbContext context) 
    {
        try
        {
            var user = await context.UserAccess.Where(u => u.Id.Equals(Id)).FirstOrDefaultAsync();
            if (user != null)
            {
                var  password = new PasswordCryptography(passwordForgot.Password).HashPassword(passwordForgot.Password);
                var confirmPassword = new PasswordCryptography(passwordForgot.ConfirmPassword).HashPassword(passwordForgot.ConfirmPassword);

                if (password.Equals(user.Password))
                    return Results.Problem("Sua senha não pode ser igual a anterior");

                if (password != user.Password) 
                {
                    if (confirmPassword.Equals(password)) 
                    {
                        user.Password = password;
                        user.ConfirmPassword = confirmPassword;
                        context.Update(user);
                        return Results.Ok( await context.SaveChangesAsync() > 0 );
                    }
                    return Results.BadRequest("imcompatíveis");
                }

            }
            return null;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public static async Task<IResult> UserCreate([FromBody]UserAccess userAccess, [FromServices] AppDbContext context) 
    {
        try
        {
            if (userAccess != null)
            {
                userAccess.Password =  new PasswordCryptography(userAccess.Password).HashPassword(userAccess.Password);
                userAccess.ConfirmPassword = new PasswordCryptography(userAccess.ConfirmPassword).HashPassword(userAccess.ConfirmPassword);

                if (userAccess.Password.Equals(userAccess.ConfirmPassword))
                {
                    await context.UserAccess.AddAsync(userAccess);
                    return Results.Ok(await context.SaveChangesAsync() > 0);
                }
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

            var user = await context.UserAccess.Where(u => u.Email.Equals(authentication.Email) && u.Password.Equals(new PasswordCryptography(authentication.Password).HashPassword(authentication.Password))).FirstOrDefaultAsync();
            if (user != null)
            {
                var verify = new PasswordCryptography(authentication.Password).VerifyPassword(authentication.Password, user.Password);
                if (authentication.Email == user.Email && verify)
                    if (user.UserType == Enuns.TipoUsuario.Client)
                        return Results.Ok(new Token(configuration).Create("Cliente"));
                    else
                        return Results.Ok(new Token(configuration).Create("Empregado"));
            }
            return Results.BadRequest();
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }
}
