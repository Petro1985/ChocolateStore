using System.ComponentModel.DataAnnotations;
using AdminUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Category;
using Services.Category;
using Services.Photo;
using Services.Product;

namespace AdminUI.Pages;

public class ProductsListModel : PageModel
{
    private readonly IProductService _productService;
    private readonly IPhotoService _photoService;

    public ProductsListModel(IProductService productService, IPhotoService photoService)
    {
        _productService = productService;
        _photoService = photoService;
    }

    [BindProperty(SupportsGet = true)] 
    public int CurrentPage { get; set; } = 1;
    public int Count { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    public List<ProductsInlineViewModel> ProductsList { get; set; } = [];

    #region UpdateProperties

    public Guid Id { get; init; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public Guid? MainPhotoId { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string? Composition { get; set; }

    public int? Weight { get; set; }

    public decimal? Width { get; set; }

    public decimal? Height { get; set; }
    
    [BindProperty]
    public IFormFile? PhotoFile { get; set; }

    #endregion
    
    public async Task OnGet([FromQuery] int pageSize = 10, [FromQuery] int page = 1)
    {
        var pagedProducts = await _productService
            .GetPagedProductsSortedByName(pageSize, page);

        PageSize = pagedProducts.PageSize;
        CurrentPage = pagedProducts.PageNumber;
        Count = pagedProducts.TotalCount; 
        
        ProductsList = pagedProducts.Items.Select(x => new ProductsInlineViewModel
            {
                Name = x.Name,
                Id = x.Id,
                MainPhotoId = x.MainPhotoId,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name,
                Composition = x.Composition,
                Description = x.Description,
                Price = x.Price,
                Weight = x.Weight,
                Width = x.Width,
                Height = x.Height,
            })
            .ToList();
    }
    
    public async Task<IActionResult> OnPost()
    {
        throw new NotImplementedException();
    }

}