using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class UINavigateBase : ViewModelBase
    {
        public UINavigateBase ParentNavigate { get; set; }

        #region Member

        private SplashScreenService _screenService;
        protected SplashScreenService ScreenService
        {
            get { return _screenService ?? (_screenService = new SplashScreenService()); }
        }

        protected ISplashScreenService BaseScreenService
        {
            get { return ScreenService; }
        }

        private string _waitPrompt;
        public string WaitPrompt
        {
            get { return _waitPrompt; }
            set { SetProperty(ref _waitPrompt, value, () => WaitPrompt); }
        }

        #endregion

        #region Bingding Command

        ICommand _onViewLoadedCommand;
        public ICommand OnViewLoadedCommand
        {
            get { return _onViewLoadedCommand ?? (_onViewLoadedCommand = new DelegateCommand(OnViewLoaded)); }
        }

        ICommand _navigateCommand;
        public ICommand NavigateCommand
        {
            get { return _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate)); }
        }

        ICommand _navigateToHomeCommand;
        public ICommand NavigateToHomeCommand
        {
            get
            {
                return _navigateToHomeCommand ??
                       (_navigateToHomeCommand = new DelegateCommand<int?>(id =>
                           NavigationService.Navigate("LoginCtl", id, this), id => id != null));
            }
        }

        #endregion

        #region Method

        private INavigationService NavigationService { get { return ServiceContainer.GetService<INavigationService>(); } }

        public void Navigate(string target)
        {
            INavigationService service = NavigationService;
            if (NavigationService == null)
            {
                UINavigateBase parent = ParentNavigate;
                while (service == null && parent != null)
                {
                    service = ParentNavigate.NavigationService;
                    parent = parent.ParentNavigate;
                }
            }
            if (service != null)
                service.Navigate(target, null, this);
        }

        protected virtual void OnViewLoaded() { }

        #endregion
    }
}
