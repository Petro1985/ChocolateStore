using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;
using Models;
using Models.Category;
using Models.Product;

namespace ChocolateUI.Pages.DisplayProducts;

public class ProductsBase : ComponentBase
{
    [Inject] public IProductService ProductServ { get; set; }
    [Inject] public IUserProfile UserProfile { get; set; }
    [Parameter] public Guid CategoryId { get; set; }
    
    public ICollection<ProductDTO>? Products { get; set; }
    public CategoryDTO? Category { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = (await ProductServ.GetProductByCategory(CategoryId)).ToList();
        Category = await ProductServ.GetCategory(CategoryId);
    }
}