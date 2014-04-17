using System;
using System.Windows;
using DevExpress.Xpf.Core;

namespace WPFPeony.Surveil
{
    /// <summary>
    /// Interaction logic for WaitingScreen.xaml
    /// </summary>
    public partial class WaitingScreen : Window, ISplashScreen
    {
        public WaitingScreen()
        {
            InitializeComponent();
            this.board.Completed += OnAnimationCompleted;
        }

        #region ISplashScreen
        public void Progress(double value)
        {
            progressBar.Value = value;
        }
        public void CloseSplashScreen()
        {
            this.board.Begin(this);
        }
        public void SetProgressState(bool isIndeterminate)
        {
            progressBar.IsIndeterminate = isIndeterminate;
        }
        #endregion

        #region Event Handlers
        void OnAnimationCompleted(object sender, EventArgs e)
        {
            this.board.Completed -= OnAnimationCompleted;
            this.Close();
        }
        #endregion
    }
}
