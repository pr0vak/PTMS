using Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]/[Action]")]
public class BaseController : ControllerBase
{
    protected readonly AppDbContext dbContext;

    public BaseController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
}