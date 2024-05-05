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
    private readonly ICategoryService _categoryService;

    public IReadOnlyCollection<CategoryDto> Categories { get; set; }

    public ProductsListModel(IProductService productService, IPhotoService photoService, ICategoryService categoryService)
    {
        _productService = productService;
        _photoService = photoService;
        _categoryService = categoryService;
    }

    [BindProperty(SupportsGet = true)] 
    public int CurrentPage { get; set; } = 1;
    [BindProperty(SupportsGet = true)] 
    public int PageSize { get; set; } = 10;

    public int Count { get; set; }
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    public List<ProductsInlineViewModel> ProductsList { get; set; } = [];

    #region UpdateProperties

    public Guid Id { get; init; }
    
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public Guid? MainPhotoId { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; }

    public string? Composition { get; set; }

    public int? Weight { get; set; }

    public decimal? Width { get; set; }

    public decimal? Height { get; set; }
    
    [BindProperty]
    public IFormFile? PhotoFile { get; set; }

    #endregion
    
    public async Task OnGet(/*Guid? categoryId, [FromQuery] int pageSize = 10, [FromQuery] int page = 1*/)
    {
        // Получаем список категорий для Select элемента
        Categories = await _categoryService.GetAllCategories();
            
        var pagedProducts = await _productService
            .GetPagedCategoryProductsSortedByName(CategoryId, PageSize, CurrentPage);

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