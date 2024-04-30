using ChocolateData;
using ChocolateDomain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers;

[Route("[controller]")]
public class Category : Controller
{
    private readonly ApplicationDbContext _context;

    public Category(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] CategoryEntity category)
    {
        _context.Update(category);
        await _context.SaveChangesAsync();
        return Ok();
    }
    
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
}