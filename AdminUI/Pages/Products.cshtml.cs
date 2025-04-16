using AdminUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Category;
using Models.Product;
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

    // public Guid ProductId { get; init; }
    //
    // public string Name { get; set; }
    //
    // public string Description { get; set; }
    //
    // public decimal Price { get; set; }

    [BindProperty]
    public ProductDto UpdateProduct { get; set; }
    
    // [BindProperty]
    // public Guid MainPhotoId { get; set; }

    [BindProperty(SupportsGet = true)]
    public Guid CategoryId { get; set; }

    // [BindProperty]
    // public string CategoryName { get; set; }
    //
    // [BindProperty]
    // public string? Composition { get; set; }
    //
    // [BindProperty]
    // public int? Weight { get; set; }
    //
    // [BindProperty]
    // public decimal? Width { get; set; }
    //
    // [BindProperty]
    // public decimal? Height { get; set; }
    
    [BindProperty]
    public IFormFile? PhotoFile { get; set; }

    #endregion
    
    public async Task OnGet()
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
                CategoryId = CategoryId,
                CategoryName = x.Category.Name,
                Composition = x.Composition,
                Description = x.Description,
                Price = x.Price,
                Weight = x.Weight,
                Width = x.Width,
                Height = x.Height,
                Photos = x.Photos.Select(y => y.Id).ToList(),
            })
            .ToList();
    }
    
    public async Task<IActionResult> OnPost()
    {

        if (PhotoFile is not null)
        {
            await _photoService.TryDelete(UpdateProduct.MainPhotoId);
            await using var stream = PhotoFile.OpenReadStream();

            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            UpdateProduct.MainPhotoId = await _photoService.AddPhoto(null, memoryStream.ToArray());
        }

        var product = new ProductDto
        {
            Id = UpdateProduct.Id,
            Name = UpdateProduct.Name,
            MainPhotoId = UpdateProduct.MainPhotoId,
            CategoryId = CategoryId,
            Composition = UpdateProduct.Composition,
            Width = UpdateProduct.Width,
            Height = UpdateProduct.Height,
            Description = UpdateProduct.Description,
            Price = UpdateProduct.Price,
            Weight = UpdateProduct.Weight,
        };
        
        await _productService.UpdateProduct(product);

        return Redirect($"/Products/Products?CategoryId={CategoryId}");
    }

}