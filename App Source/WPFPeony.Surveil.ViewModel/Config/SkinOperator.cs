using System;
using System.Windows;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public enum SkinTypes
    {
        MetropolisLight,
        MetropolisDark,
    }

    public class SkinOperator : ObjectControl
    {
        public SkinOperator()
        {
            InitData();
        }

        protected override sealed void InitData()
        {
            foreach (var skinname in Enum.GetNames(typeof(SkinTypes)))
            {
                var control = new UIControlBase
                {
                    ControlHandle = skinname,
                    ControlToolTipKey = skinname + "Tip",
                    ControlImageKey = skinname + "_Source",
                };
                control.ControlCmd = new DelegateCommand<UIControlBase>(param => SkinChanged(control));

                if (skinname == SkinTypes.MetropolisDark.ToString())
                    control.IsSelected = true;
                ObjectControlS.Add(control);
            }
            base.InitData();
        }

        #region SetSkin

        private void SkinChanged(object sender)
        {
            var control = sender as UIControlBase;
            if (control != null &&
                ThemeManager.ActualApplicationThemeName != control.ControlHandle)
            {
                Application.Current.Dispatcher.BeginInvoke(
                    new Action(() => ThemeManager.ApplicationThemeName = control.ControlHandle));
                control.IsSelected = true;
            }
        }

        #endregion
    }
}