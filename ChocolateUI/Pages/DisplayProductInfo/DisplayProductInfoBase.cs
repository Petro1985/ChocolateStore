using Microsoft.AspNetCore.Components;
using Models;

namespace ChocolateUI.Pages.DisplayProductInfo;

public class DisplayProductInfoBase : ComponentBase
{
    [Parameter] public IEnumerable<Guid> Photos { get; set; }
}