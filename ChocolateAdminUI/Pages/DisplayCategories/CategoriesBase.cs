using ChocolateAdminUI.Services;
using Microsoft.AspNetCore.Components;
using Models.Category;

namespace ChocolateAdminUI.Pages.DisplayCategories;

public class CategoryBase : ComponentBase
{
    [Inject] public IFetchService FetchServ { get; set; }
    [Inject] public IUserProfile UserProfile { get; set; }
    [Inject] public ILogger<CategoryBase> Logger { get; set; }

    public Dictionary<Guid, CategoryDTO>? Categories { get; set; }

    public bool IsAddingNew { get; set; }
    protected override async Task OnInitializedAsync()
    {
        await GetCategories();
    }

    protected async Task GetCategories()
    {
        var categoriesCollection = await FetchServ.GetCategories();
        Categories = categoriesCollection.ToDictionary(x => x.Id);
    }
}