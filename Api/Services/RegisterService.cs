using System.Net;
using Api.Common;
using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public static class RegisterService
{
    public static async Task<ActionResult<ServerResponse>> Register(RegisterDto registerDto,
        AppDbContext dbContext, UserManager<AppUser> userManager)
    {
        var userFromDb = await dbContext.Users.FirstOrDefaultAsync(u =>
            u.NormalizedEmail.Equals(registerDto.Email.ToUpper()));

        if (userFromDb is not null)
        {
            return new BadRequestObjectResult(new ServerResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = { "This email already exists" }
            });
        }

        var newUser = new AppUser
        {
            UserName = registerDto.Email,
            Email = registerDto.Email,
            FullName = registerDto.FullName
        };

        var result = await userManager.CreateAsync(newUser, registerDto.Password);

        if (!result.Succeeded)
        {
            return new BadRequestObjectResult(new ServerResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = { "Ошибка регистрации" }
            });
        }

        var newRoleUser = SelectRole(registerDto);

        await userManager.AddToRoleAsync(newUser, newRoleUser);

        return new OkObjectResult(new ServerResponse
        {
            StatusCode = HttpStatusCode.OK,
            Result = "Registration successful"
        });
    }

    private static string SelectRole(RegisterDto registerRequestDto)
    {
        return registerRequestDto.Role.ToLower() switch
        {
            SharedData.Roles.Administrator => SharedData.Roles.Administrator,
            SharedData.Roles.ProjectManager => SharedData.Roles.ProjectManager,
            SharedData.Roles.TeamMember => SharedData.Roles.TeamMember,
            SharedData.Roles.Guest => SharedData.Roles.Guest,
            _ => SharedData.Roles.Guest
        };
    }
}