using Api.Models;
using Api.ModelsDto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IAccountService
{
    Task<ActionResult<ServerResponse>> Register(RegisterDto registerDto);
    Task<ActionResult<ServerResponse>> Login(LoginDto loginDto);
}