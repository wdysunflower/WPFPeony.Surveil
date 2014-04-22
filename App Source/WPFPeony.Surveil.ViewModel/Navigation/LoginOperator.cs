using System.Threading;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class LoginOperator : UINavigateBase
    {
        #region Binding Property


        #endregion

        #region Bingding Command

        #region LoginCmd

        ICommand _loginCmd;
        public ICommand LoginCmd
        {
            get { return _loginCmd ?? (_loginCmd = new DelegateCommand(OnLoginCmd)); }
        }

        void OnLoginCmd()
        {
            WaitPrompt = "登录中...";
            ScreenService.ShowSplashScreenByData(UIViewNameHelper.LoginScreen, this);
            Thread.Sleep(1000);

            Navigate(UIViewNameHelper.SurveilView);
            BaseScreenService.HideSplashScreen();
        }

        #endregion

        #region CancleCmd

        ICommand _cancleCmd;
        public ICommand CancleCmd
        {
            get { return _cancleCmd ?? (_cancleCmd = new DelegateCommand<DependencyObject>(OnCancleCmd)); }
        }

        void OnCancleCmd(DependencyObject dependency)
        {
            Window win = Window.GetWindow(dependency);
            if (win != null)
                win.Close();
        }

        #endregion

        #endregion
    }
}
