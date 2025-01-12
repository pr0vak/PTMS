using System.Net;
using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AuthController : BaseController
{
    private readonly UserManager<AppUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly JwtTokenGenerator jwtTokenGenerator;
    private readonly IValidator<RegisterDto> regValidator;
    private readonly IValidator<LoginDto> logValidator;
    private readonly ILogger<AuthController> logger;

    public AuthController(AppDbContext dbContext, UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager, JwtTokenGenerator jwtTokenGenerator,
        IValidator<RegisterDto> regValidator, IValidator<LoginDto> logValidator,
        ILogger<AuthController> logger) : base(dbContext)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.regValidator = regValidator;
        this.logValidator = logValidator;
        this.logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult<ServerResponse>> Register([FromBody] RegisterDto registerDto)
    {
        var validationResult = regValidator.Validate(registerDto);

        if (!validationResult.IsValid)
        {
            logger.LogWarning("", validationResult.Errors);
            return BadRequest(new ServerResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            });
        }

        return await RegisterService.Register(registerDto, dbContext, userManager);
    }

    [HttpPost]
    public async Task<ActionResult<ServerResponse>> Login([FromBody] LoginDto loginDto)
    {
        var validationResult = logValidator.Validate(loginDto);

        if (!validationResult.IsValid)
        {
            logger.LogWarning("", validationResult.Errors);
            return BadRequest(new ServerResponse
            {
                IsSuccess = false,
                StatusCode = HttpStatusCode.BadRequest,
                ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
            });
        }

        return await LoginService.Login(loginDto, dbContext, userManager, jwtTokenGenerator);
    }
}