using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;
using Models;
using Models.Product;

namespace ChocolateUI.Pages.DisplayProductInfo;

public class ProductInfoBase : ComponentBase
{
    [Inject] public IProductService ProductServ { get; set; }
    [Parameter] public Guid ProductId { get; set; }
    
    public ProductDTO? Product { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Product = await ProductServ.GetProduct(ProductId);
    }
}