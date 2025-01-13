using System.Net;
using Api.Common;
using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext dbContext;
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly JwtTokenGenerator jwtTokenGenerator;

    public AccountService(AppDbContext dbContext, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager, JwtTokenGenerator jwtTokenGenerator)
    {
        this.dbContext = dbContext;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ActionResult<ServerResponse>> Register(RegisterDto registerDto)
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

    public async Task<ActionResult<ServerResponse>> Login(LoginDto loginDto)
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
        var token = jwtTokenGenerator.GenerateJwtToken(userFromDb, roles);

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