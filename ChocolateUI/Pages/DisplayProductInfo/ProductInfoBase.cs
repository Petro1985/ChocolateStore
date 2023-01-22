using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;
using Models;
using Models.Product;

namespace ChocolateUI.Pages.DisplayProductInfo;

public class ProductInfoBase : ComponentBase
{
    [Inject] public IFetchService FetchServ { get; set; }
    [Parameter] public Guid ProductId { get; set; }
    
    public ProductDTO? Product { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Product = await FetchServ.GetProduct(ProductId);
        var category = await FetchServ.GetCategory(Product.CategoryId);
        Product.CategoryName = category.Name;
    }
}