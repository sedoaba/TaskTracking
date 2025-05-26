namespace TaskTracking.Web.ViewModels
{
    public class BaseViewModel
    {
		private bool _isBusy;

		public bool IsBusy
		{
			get { return _isBusy; }
			set { _isBusy = value; }
		}

	}
}
