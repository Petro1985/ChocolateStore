using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;
using Models.Category;
using Models.Product;

namespace ChocolateUI.Pages.DisplayProducts;

public class ProductsBase : ComponentBase
{
    [Inject] public IFetchService FetchService { get; set; }
    [Inject] public IUserProfile UserProfile { get; set; }
    [Parameter] public Guid CategoryId { get; set; }
    
    public Dictionary<Guid, ProductDTO>? Products { get; set; }
    public CategoryDTO? Category { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = (await FetchService.GetProductByCategory(CategoryId)).ToDictionary(x => x.Id);
        Category = await FetchService.GetCategory(CategoryId);
    }
}