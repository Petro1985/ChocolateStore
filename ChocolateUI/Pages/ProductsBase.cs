using ChocolateUI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Models;

namespace ChocolateUI.Pages;

public class ProductsBase : ComponentBase
{
    [Inject] public IProductService ProductServ { get; set; }

    public IEnumerable<ProductDTO>? Products { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = await ProductServ.GetItems();
    }
}