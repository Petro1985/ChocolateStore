using Microsoft.AspNetCore.Components;
using Models;

namespace ChocolateUI.Pages.DisplayCategories;

public class DisplayCategoriesBase : ComponentBase
{
    [Parameter] public IEnumerable<CategoryDTO> Categories { get; set; }
}