using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymBuddyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WorkoutsController : ControllerBase
{
    [Authorize]
    [HttpGet("protected")]
    public IActionResult GetSecretData()
    {
        return Ok("This is a protected route!");
    }
}