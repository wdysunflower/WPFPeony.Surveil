using System.Windows;
using DevExpress.Xpf.Core;

namespace WPFPeony.Surveil
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.ApplicationThemeName = Theme.MetropolisDark.Name;
            base.OnStartup(e);
        }
    }
}
