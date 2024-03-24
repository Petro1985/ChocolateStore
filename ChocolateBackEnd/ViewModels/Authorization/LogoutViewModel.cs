using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ChocolateBackEnd.ViewModels.Authorization;

public class LogoutViewModel
{
    [BindNever]
    public string RequestId { get; set; }
}
