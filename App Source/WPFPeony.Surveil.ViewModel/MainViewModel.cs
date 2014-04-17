namespace WPFPeony.Surveil.ViewModel
{
    public class MainViewModel : UINavigateBase
    {
        private SkinOperator _skinOper;

        public SkinOperator SkinOper
        {
            get { return _skinOper ?? (_skinOper = new SkinOperator()); }
        }

        private LoginOperator _loginOper;

        public LoginOperator LoginOper
        {
            get { return _loginOper ?? (_loginOper = new LoginOperator { ParentNavigate = this }); }
        }

        protected override void OnViewLoaded()
        {
            Navigate(UIViewNameHelper.LoginView);
        }
    }
}
