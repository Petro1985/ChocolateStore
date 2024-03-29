﻿using ChocolateUI.Services;
using Microsoft.AspNetCore.Components;

namespace ChocolateUI.Pages.DisplayCategories;

public class DisplayCategoriesBase : ComponentBase
{

    [Inject] public IUserProfile UserProfile { get; set; }
    [Inject] public IFetchService FetchService { get; set; }
    [Inject] public ILogger<DisplayCategoriesBase> Logger { get; set; }
    
}