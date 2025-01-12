using System.Net;
using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public static class LoginService
{
    public static async Task<ActionResult<ServerResponse>> Login(LoginDto loginDto, AppDbContext dbContext,
        UserManager<AppUser> userManager, JwtTokenGenerator tokenGenerator)
    {
        var userFromDb = await dbContext.Users
            .FirstOrDefaultAsync(u => u.NormalizedEmail.Equals(loginDto.Email.ToUpper()));

        if (userFromDb is null || !await userManager.CheckPasswordAsync(userFromDb, loginDto.Password))
        {
            return new BadRequestObjectResult(new ServerResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = { "Incorrect login and/or password" }
            });
        }

        var roles = await userManager.GetRolesAsync(userFromDb);
        var token = tokenGenerator.GenerateJwtToken(userFromDb, roles);

        return new OkObjectResult(new ServerResponse
        {
            StatusCode = HttpStatusCode.OK,
            Result = new LoginResponseDto
            {
                Email = userFromDb.Email,
                Token = token
            }
        });
    }
}