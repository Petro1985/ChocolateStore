﻿using ChocolateUI.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Models;

namespace ChocolateUI.Pages.DisplayProducts;

public class ProductsBase : ComponentBase
{
    [Inject] public IProductService ProductServ { get; set; }
    [Parameter] public Guid CategoryId { get; set; }
    
    public ICollection<ProductDTO>? Products { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = (await ProductServ.GetProductByCategory(CategoryId)).ToList();
    }
}