using ChocolateUI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Models;

namespace ChocolateUI.Pages.DisplayCategories;

public class CategoryBase : ComponentBase
{
    [Inject] public IProductService ProductServ { get; set; }

    public IEnumerable<CategoryDTO>? Categories { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Categories = await ProductServ.GetCategories();
    }
}