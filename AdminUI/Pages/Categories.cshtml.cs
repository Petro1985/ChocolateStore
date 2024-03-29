using AdminUI.ViewModels;
using ChocolateData.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdminUI.Pages;

public class Categories : PageModel {
    private readonly ICategoryRepository _categoryRepository;

    public Categories(ICategoryRepository categoryRepository) {
        _categoryRepository = categoryRepository;
    }

    [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
    public int Count { get; set; }
    public int PageSize { get; set; } = 10;
    public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
    public List<CategoryInlineViewModel> CategoriesList { get; set; } = [];

    public async Task OnGet() {
        var categories = await _categoryRepository.GetPagedCategoriesSortedByName(10, 0);

        CategoriesList = categories.Select(x => new CategoryInlineViewModel {
                Name = x.Name,
                Id = x.Id,
                MainPhotoId = x.MainPhotoId
            })
            .ToList();
    }
}