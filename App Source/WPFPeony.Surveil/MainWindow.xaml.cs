using System;
using System.Windows.Forms;
using DevExpress.Xpf.Core;
using WPFPeony.Surveil.ViewModel;

namespace WPFPeony.Surveil
{
    public partial class MainWindow : DXWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (this.DataContext != null)
            {
                MainViewModel viewModel = (MainViewModel)DataContext;
                viewModel.RegisterHotKey();
            }
        }
    }
}