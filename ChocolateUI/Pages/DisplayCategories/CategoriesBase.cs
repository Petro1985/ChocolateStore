using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;
using Models;
using Models.Category;

namespace ChocolateUI.Pages.DisplayCategories;

public class CategoryBase : ComponentBase
{
    [Inject] public IProductService ProductServ { get; set; }
    [Inject] public IUserProfile UserProfile { get; set; }

    public IEnumerable<CategoryDTO>? Categories { get; set; }

    public bool IsAddingNew { get; set; }
    protected override async Task OnInitializedAsync()
    {
        Categories = await ProductServ.GetCategories();
    }

    protected void AddNewCategory()
    {
        Console.WriteLine("Clicked");
        //ProductServ.CreateNewCategory();
        
    }
}