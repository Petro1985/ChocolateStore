using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net;
using AdminUI.ViewModels;
using ChocolateDomain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Category;
using Services.Models;
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
    public string SearchString { get; set; }
    
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
    
    public async Task OnGet()
    {
        Expression<Func<CategoryEntity, bool>>? criteria = null;

        if (!string.IsNullOrWhiteSpace(SearchString))
        {
            criteria = (x) => EF.Functions.Like(x.Name.ToLower(), $"%{SearchString.ToLower()}%");
        }
        
        var pagedCategories = await _categoryService
            .GetPagedCategoriesSortedByName(PageSize, CurrentPage, criteria);
        // Если на текущей странице нет элементов, но при этом в результате что-то есть -> переходим на первую страницу
        if (pagedCategories is { TotalCount: > 0, Items.Count: 0 }) 
        {
            pagedCategories = await _categoryService
                .GetPagedCategoriesSortedByName(PageSize, 1, criteria);
        }

        Count = pagedCategories.TotalCount; 
        PageSize = pagedCategories.PageSize;
        CurrentPage = pagedCategories.PageNumber;
        
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

        return Redirect($"/Categories/Categories?page={CurrentPage}&SearchString={WebUtility.UrlEncode(SearchString)}");
    }

}