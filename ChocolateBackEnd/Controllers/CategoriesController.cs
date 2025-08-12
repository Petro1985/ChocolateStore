using Microsoft.AspNetCore.Mvc;
using ChocolateBackEnd.Auth;
using Microsoft.AspNetCore.Authorization;
using Models.Category;
using Services.Photo;
using Services.Product;

namespace ChocolateBackEnd.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly IPhotoService _photoService;
    private readonly IProductService _productService;

    public CategoriesController(IPhotoService photoService, IProductService productService)
    {
        _photoService = photoService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
    {
        var result = await _productService.GetAllCategories();
        await Task.Delay(1000);
        return Ok(result);
    }

    [HttpGet("{categoryId:guid}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDto>> GetCategory([FromRoute] Guid categoryId)
    {
        return Ok(await _productService.GetCategory(categoryId));
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpPost("Photos")]
    public async Task<IActionResult> AddCategoryPhoto(AddMainPhotoRequest request)
    {
        var photo = Convert.FromBase64String(request.PhotoBase64);
        var newPhotoId = await _photoService.AddPhoto(null, photo);
        await _productService.SetCategoryPhoto(request.EntityId, newPhotoId);
        return Ok(newPhotoId);
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpPost]
    public async Task<ActionResult<Guid>> AddCategory(CategoryDto category)
    {
        return Ok(await _productService.AddNewCategory(category));
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpPut]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryDto category)
    {
        await _productService.UpdateCategory(category);
        return Ok();
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] Guid categoryId)
    {
        await _productService.DeleteCategory(categoryId);
        return Ok();
    }
}