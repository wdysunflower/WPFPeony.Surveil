using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class LoginOperator : UINavigateBase
    {
        #region Bingding Command

        ICommand _loginCmd;
        public ICommand LoginCmd
        {
            get { return _loginCmd ?? (_loginCmd = new DelegateCommand(OnLoginCmd)); }
        }

        void OnLoginCmd()
        {
            Navigate(UIViewNameHelper.SurveilView);
        }

        #endregion
    }
}
