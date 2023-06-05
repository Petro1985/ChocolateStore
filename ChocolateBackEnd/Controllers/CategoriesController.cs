using Microsoft.AspNetCore.Mvc;
using ChocolateBackEnd.Auth;
using Microsoft.AspNetCore.Authorization;
using Models.Category;
using Services.Photo;
using Services.Product;

namespace ChocolateBackEnd.Controllers;

[ApiController]
[Route("Categories")]
public class CategoriesController : Controller
{
    private readonly IPhotoService _photoService;
    private readonly IProductService _productService;

    public CategoriesController(IPhotoService photoService, IProductService productService)
    {
        _photoService = photoService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        return Ok(await _productService.GetAllCategories());
    }

    [HttpGet("{categoryId:guid}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> GetCategory([FromRoute] Guid categoryId)
    {
        return Ok(await _productService.GetCategory(categoryId));
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpPost("Photo")]
    public async Task<IActionResult> AddCategoryPhoto(AddMainPhotoRequest request)
    {
        var photo = Convert.FromBase64String(request.PhotoBase64);
        var newPhotoId = await _photoService.AddPhoto(null, photo);
        await _productService.SetCategoryPhoto(request.EntityId, newPhotoId);
        return Ok(newPhotoId);
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpPost]
    public async Task<ActionResult<Guid>> AddCategory(CategoryDTO category)
    {
        return Ok(await _productService.AddNewCategory(category));
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDTO category)
    {
        await _productService.UpdateCategory(category);
        return Ok();
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
    {
        await _productService.DeleteCategory(categoryId);
        return Ok();
    }
}