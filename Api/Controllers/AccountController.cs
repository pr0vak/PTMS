using System.Net;
using Api.Data;
using Api.Models;
using Api.ModelsDto;
using Api.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AccountController : BaseController
{
    private readonly IAccountService accountService;
    private readonly IValidator<RegisterDto> regValidator;
    private readonly IValidator<LoginDto> logValidator;
    private readonly ILogger<AccountController> logger;

    public AccountController(IAccountService accountService, ILogger<AccountController> logger,
        IValidator<RegisterDto> regValidator, IValidator<LoginDto> logValidator)
    {
        this.accountService = accountService;
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

        return await accountService.Register(registerDto);
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

        return await accountService.Login(loginDto);
    }
}