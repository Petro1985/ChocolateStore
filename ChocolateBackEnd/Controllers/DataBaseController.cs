using ChocolateBackEnd.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateBackEnd.Controllers;

[ApiController]
public class DataBaseController : ControllerBase
{
    [Authorize(Policy = Policies.Admin)]
    [HttpPost("/DateBase/MakeBackup")]
    public IActionResult MakeBackup()
    {
        
        return Ok();
    }
}