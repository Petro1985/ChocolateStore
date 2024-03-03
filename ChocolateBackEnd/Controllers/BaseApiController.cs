using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateBackEnd.Controllers;

[ApiVersion("1.0")]
[ApiController]
[Route("/Api/v{version:apiVersion}/[controller]")]
public class BaseApiController : Controller
{
    
}