using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(UserManager userManager) : ControllerBase
{
    private readonly UserManager _userManager = userManager;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _userManager.GetUsersAsync();
        return Ok(response);
    }
}
