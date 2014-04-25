// ***********************************************************************
// <copyright file="SurveilOperator.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-18-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-22-2014
// ***********************************************************************

using System.Threading;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class SurveilOperator.
    /// </summary>
    public class SurveilOperator : UINavigateBase
    {
        #region Binding Property

        /// <summary>
        /// The _real time oper
        /// </summary>
        private RealTimeOperator _realTimeOper;

        /// <summary>
        /// Gets the real time oper.
        /// </summary>
        /// <value>The real time oper.</value>
        public RealTimeOperator RealTimeOper
        {
            get { return _realTimeOper ?? (_realTimeOper = new RealTimeOperator()); }
        }

        /// <summary>
        /// The _play back oper
        /// </summary>
        private PlayBackOperator _playBackOper;

        /// <summary>
        /// Gets the play back oper.
        /// </summary>
        /// <value>The play back oper.</value>
        public PlayBackOperator PlayBackOper
        {
            get { return _playBackOper ?? (_playBackOper = new PlayBackOperator()); }
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

        #region ModuleExitCmd

        /// <summary>
        /// The _module exit command
        /// </summary>
        private ICommand _moduleExitCmd;

        /// <summary>
        /// Gets the module exit command.
        /// </summary>
        /// <value>The module exit command.</value>
        public ICommand ModuleExitCmd
        {
            get { return _moduleExitCmd ?? (_moduleExitCmd = new DelegateCommand(OnModuleExitCmd)); }
        }

        /// <summary>
        /// Called when [module exit command].
        /// </summary>
        private void OnModuleExitCmd()
        {
            BaseScreenService.ShowSplashScreen(UIViewNameHelper.WaitScreen);
            Thread.Sleep(1000);
            Navigate(UIViewNameHelper.SurveilView);
            BaseScreenService.HideSplashScreen();
        }

        #endregion

        #endregion
    }
}