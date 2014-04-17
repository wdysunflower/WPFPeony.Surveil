using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UIThemeOperator _uiThemeOper;

        public UIThemeOperator UIThemeOper
        {
            get { return _uiThemeOper ?? (_uiThemeOper = new UIThemeOperator()); }
        }
    }
}
