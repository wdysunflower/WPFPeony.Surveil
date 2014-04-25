// ***********************************************************************
// <copyright file="SurveilOperator.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-18-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-25-2014
// ***********************************************************************

using System.Threading;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class SurveilManage.
    /// </summary>
    public class SurveilManage : UINavigateBase
    {
        #region Binding Property

        /// <summary>
        /// The _real time
        /// </summary>
        private RealTimeManage _realTime;

        /// <summary>
        /// Gets the real time.
        /// </summary>
        /// <value>The real time.</value>
        public RealTimeManage RealTime
        {
            get { return _realTime ?? (_realTime = new RealTimeManage {ParentNavigate = this}); }
        }

        /// <summary>
        /// The _play back
        /// </summary>
        private PlayBackManage _playBack;

        /// <summary>
        /// Gets the play back.
        /// </summary>
        /// <value>The play back.</value>
        public PlayBackManage PlayBack
        {
            get { return _playBack ?? (_playBack = new PlayBackManage {ParentNavigate = this}); }
        }

        #endregion

        #region Binding Command

        #region LogoutCmd

        /// <summary>
        /// The _logout command
        /// </summary>
        private ICommand _logoutCmd;

        /// <summary>
        /// Gets the logout command.
        /// </summary>
        /// <value>The logout command.</value>
        public ICommand LogoutCmd
        {
            get { return _logoutCmd ?? (_logoutCmd = new DelegateCommand(OnLogoutCmd)); }
        }

        /// <summary>
        /// Called when [logout command].
        /// </summary>
        private void OnLogoutCmd()
        {
            WaitPrompt = "注销中...";
            ScreenService.ShowSplashScreenByData(UIViewNameHelper.LoginScreen, this);
            Thread.Sleep(1000);

            Navigate(UIViewNameHelper.LoginView);
            BaseScreenService.HideSplashScreen();
        }

        #endregion

        #region ModuleChooseCmd

        /// <summary>
        /// The _module choose command
        /// </summary>
        private ICommand _moduleChooseCmd;

        /// <summary>
        /// Gets the module choose command.
        /// </summary>
        /// <value>The module choose command.</value>
        public ICommand ModuleChooseCmd
        {
            get
            {
                return _moduleChooseCmd ?? (_moduleChooseCmd = new DelegateCommand<DependencyObject>(OnModuleChooseCmd));
            }
        }

        /// <summary>
        /// Called when [module choose command].
        /// </summary>
        /// <param name="dependency">The dependency.</param>
        private void OnModuleChooseCmd(DependencyObject dependency)
        {
            BaseScreenService.ShowSplashScreen(UIViewNameHelper.WaitScreen);
            Thread.Sleep(1000);

            if ((string) dependency.GetValue(FrameworkElement.NameProperty) == "RealTime")
                Navigate(UIViewNameHelper.RealTimeView);
            if ((string) dependency.GetValue(FrameworkElement.NameProperty) == "PlayBack")
                Navigate(UIViewNameHelper.PlayBackView);

            BaseScreenService.HideSplashScreen();
        }

        #endregion

        #endregion
    }
}