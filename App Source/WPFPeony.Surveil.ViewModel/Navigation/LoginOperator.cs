// ***********************************************************************
// <copyright file="LoginOperator.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-17-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-18-2014
// ***********************************************************************

using System.Threading;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class LoginOperator.
    /// </summary>
    public class LoginOperator : UINavigateBase
    {
        #region Binding Property

        #endregion

        #region Bingding Command

        #region LoginCmd

        /// <summary>
        /// The _login command
        /// </summary>
        private ICommand _loginCmd;

        /// <summary>
        /// Gets the login command.
        /// </summary>
        /// <value>The login command.</value>
        public ICommand LoginCmd
        {
            get { return _loginCmd ?? (_loginCmd = new DelegateCommand(OnLoginCmd)); }
        }

        /// <summary>
        /// Called when [login command].
        /// </summary>
        private void OnLoginCmd()
        {
            WaitPrompt = "登录中...";
            ScreenService.ShowSplashScreenByData(UIViewNameHelper.LoginScreen, this);
            Thread.Sleep(1000);

            Navigate(UIViewNameHelper.SurveilView);
            BaseScreenService.HideSplashScreen();
        }

        #endregion

        #region CancelCmd

        /// <summary>
        /// The _cancel command
        /// </summary>
        private ICommand _cancelCmd;

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public ICommand CancelCmd
        {
            get { return _cancelCmd ?? (_cancelCmd = new DelegateCommand<DependencyObject>(OnCancelCmd)); }
        }

        /// <summary>
        /// Called when [cancel command].
        /// </summary>
        /// <param name="dependency">The dependency.</param>
        private void OnCancelCmd(DependencyObject dependency)
        {
            Window win = Window.GetWindow(dependency);
            if (win != null)
                win.Close();
        }

        #endregion

        #endregion
    }
}