using System.ComponentModel.DataAnnotations;
using AdminUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.Category;
using Services.Category;
using Services.Photo;

namespace AdminUI.Pages;

public class CategoriesListModel : PageModel
{
    private readonly ICategoryService _categoryService;
    private readonly IPhotoService _photoService;

    public CategoriesListModel(ICategoryService categoryService, IPhotoService photoService)
    {
        _categoryService = categoryService;
        _photoService = photoService;
    }

    [BindProperty(SupportsGet = true)] 
    public int CurrentPage { get; set; } = 1;
    public int Count { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    public List<CategoryInlineViewModel> CategoriesList { get; set; } = [];

    #region UpdateProperties

    [BindProperty]
    public Guid CategoryId { get; set; }
    
    [BindProperty]
    [MaxLength(50)]
    [MinLength(5)]
    public string Name { get; set; }
    
    [BindProperty]
    public Guid MainPhotoId { get; set; }
    
    [BindProperty]
    public IFormFile? PhotoFile { get; set; }
    
    #endregion
    
    public async Task OnGet([FromQuery] int pageSize = 10, [FromQuery] int page = 1)
    {
        var pagedCategories = await _categoryService
            .GetPagedCategoriesSortedByName(pageSize, page);

        PageSize = pagedCategories.PageSize;
        CurrentPage = pagedCategories.PageNumber;
        Count = pagedCategories.TotalCount; 
        
        CategoriesList = pagedCategories.Items.Select(x => new CategoryInlineViewModel
            {
                Name = x.Name,
                Id = x.Id,
                MainPhotoId = x.MainPhotoId
            })
            .ToList();
    }
    
    public async Task<IActionResult> OnPost()
    {
        if (PhotoFile is not null)
        {
            await _photoService.TryDelete(MainPhotoId);
            await using var stream = PhotoFile.OpenReadStream();

            using var memoryStream = new MemoryStream();
            await stream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            MainPhotoId = await _photoService.AddPhoto(null, memoryStream.ToArray());
        }

        var category = new CategoryDto
        {
            Id = CategoryId,
            Name = Name!,
            MainPhotoId = MainPhotoId,
        };
        
        await _categoryService.UpdateCategory(category);

        return Redirect($"/Categories/Categories?page={CurrentPage}");
    }

}