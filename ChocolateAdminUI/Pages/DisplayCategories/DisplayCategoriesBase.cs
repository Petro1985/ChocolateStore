using ChocolateAdminUI.Services;
using Microsoft.AspNetCore.Components;

namespace ChocolateAdminUI.Pages.DisplayCategories;

public class DisplayCategoriesBase : ComponentBase
{

    [Inject] public IUserProfile UserProfile { get; set; }
    [Inject] public IFetchService FetchService { get; set; }
    [Inject] public ILogger<DisplayCategoriesBase> Logger { get; set; }
    
}