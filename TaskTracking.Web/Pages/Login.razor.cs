using Microsoft.AspNetCore.Components;
using TaskTracking.Web.ViewModels;

namespace TaskTracking.Web.Pages
{
    public partial class Login
    {
        [Inject]
        public ILoginViewModel LoginViewModel { get; set; }
    }
}
