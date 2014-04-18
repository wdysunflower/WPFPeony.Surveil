using System.Threading;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class SurveilOperator : UINavigateBase
    {
        private Window _parenWindow;

        #region Binding Property

        private RealTimeOperator _realTimeOper;

        public RealTimeOperator RealTimeOper
        {
            get { return _realTimeOper ?? (_realTimeOper = new RealTimeOperator()); }
        }

        private PlayBackOperator _playBackOper;

        public PlayBackOperator PlayBackOper
        {
            get { return _playBackOper ?? (_playBackOper = new PlayBackOperator()); }
        }

        #endregion

        #region Binding Command

        #region LogoutCmd

        ICommand _logoutCmd;
        public ICommand LogoutCmd
        {
            get { return _logoutCmd ?? (_logoutCmd = new DelegateCommand(OnLogoutCmd)); }
        }

        void OnLogoutCmd()
        {
            WaitPrompt = "注销中...";
            ScreenService.ShowSplashScreenByData(UIViewNameHelper.LoginScreen, this);
            Thread.Sleep(1000);

            Navigate(UIViewNameHelper.LoginView);
            BaseScreenService.HideSplashScreen();
        }

        #endregion

        #region ModuleChooseCmd

        ICommand _moduleChooseCmd;
        public ICommand ModuleChooseCmd
        {
            get { return _moduleChooseCmd ?? (_moduleChooseCmd = new DelegateCommand<DependencyObject>(OnModuleChooseCmd)); }
        }

        void OnModuleChooseCmd(DependencyObject dependency)
        {
            BaseScreenService.ShowSplashScreen(UIViewNameHelper.WaitScreen);
            Thread.Sleep(1000);
            
            if ((string)dependency.GetValue(FrameworkElement.NameProperty) == "RealTime")
                Navigate(UIViewNameHelper.RealTimeView);
            if ((string)dependency.GetValue(FrameworkElement.NameProperty) == "PlayBack")
                Navigate(UIViewNameHelper.PlayBackView);

            BaseScreenService.HideSplashScreen();
           
            Window win = Window.GetWindow(dependency);
            if (win != null)
            {
                win.WindowState = WindowState.Maximized;
                _parenWindow = win;
            }
        }

        #endregion

        #region ModuleExitCmd

        ICommand _moduleExitCmd;
        public ICommand ModuleExitCmd
        {
            get { return _moduleExitCmd ?? (_moduleExitCmd = new DelegateCommand(OnModuleExitCmd)); }
        }

        void OnModuleExitCmd()
        {
            if (_parenWindow != null)
                _parenWindow.WindowState = WindowState.Normal;

            BaseScreenService.ShowSplashScreen(UIViewNameHelper.WaitScreen);
            Thread.Sleep(1000);
            Navigate(UIViewNameHelper.SurveilView);
            BaseScreenService.HideSplashScreen();
        }

        #endregion

        #endregion
    }
}
