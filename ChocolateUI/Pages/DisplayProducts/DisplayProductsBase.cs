using Microsoft.AspNetCore.Components;
using Models;

namespace ChocolateUI.Pages.DisplayProducts;

public class DisplayProductsBase : ComponentBase
{
    [Parameter] public IEnumerable<ProductDTO> Products { get; set; }
}