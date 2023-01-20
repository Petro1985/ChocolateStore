using Microsoft.AspNetCore.Components;
using Models;
using Models.Category;

namespace ChocolateUI.Pages.DisplayCategories;

public class DisplayCategoriesBase : ComponentBase
{
    [Parameter] public IEnumerable<CategoryDTO> Categories { get; set; }
}