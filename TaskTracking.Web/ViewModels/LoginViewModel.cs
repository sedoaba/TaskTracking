using TaskTracking.Web.Services;

namespace TaskTracking.Web.ViewModels
{
    public class LoginViewModel : BaseViewModel, ILoginViewModel
    {
        private readonly ITrackingHttpClient _trackingHttpClient;

        public LoginViewModel(ITrackingHttpClient trackingHttpClient)
        {
            _trackingHttpClient = trackingHttpClient;
        }
    }
}
