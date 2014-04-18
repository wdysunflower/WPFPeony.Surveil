namespace WPFPeony.Surveil.ViewModel
{
    public class MainViewModel : UINavigateBase
    {
        #region Binding Property

        private LoginOperator _loginOper;

        public LoginOperator LoginOper
        {
            get { return _loginOper ?? (_loginOper = new LoginOperator { ParentNavigate = this }); }
        }

        private SurveilOperator _surveilOper;

        public SurveilOperator SurveilOper
        {
            get { return _surveilOper ?? (_surveilOper = new SurveilOperator { ParentNavigate = this }); }
        }

        #endregion

        #region Override

        protected override void OnViewLoaded()
        {
            Navigate(UIViewNameHelper.LoginView);
        }

        #endregion
    }
}
