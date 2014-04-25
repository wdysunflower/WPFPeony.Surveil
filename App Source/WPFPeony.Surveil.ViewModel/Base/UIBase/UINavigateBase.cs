// ***********************************************************************
// <copyright file="UINavigateBase.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-17-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-24-2014
// ***********************************************************************

using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class UINavigateBase.
    /// </summary>
    public class UINavigateBase : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the parent navigate.
        /// </summary>
        /// <value>The parent navigate.</value>
        public UINavigateBase ParentNavigate { get; set; }

        #region Member

        /// <summary>
        /// The _screen service
        /// </summary>
        private SplashScreenService _screenService;

        /// <summary>
        /// Gets the screen service.
        /// </summary>
        /// <value>The screen service.</value>
        protected SplashScreenService ScreenService
        {
            get { return _screenService ?? (_screenService = new SplashScreenService()); }
        }

        /// <summary>
        /// Gets the base screen service.
        /// </summary>
        /// <value>The base screen service.</value>
        protected ISplashScreenService BaseScreenService
        {
            get { return ScreenService; }
        }

        /// <summary>
        /// The _wait prompt
        /// </summary>
        private string _waitPrompt;

        /// <summary>
        /// Gets or sets the wait prompt.
        /// </summary>
        /// <value>The wait prompt.</value>
        public string WaitPrompt
        {
            get { return _waitPrompt; }
            set { SetProperty(ref _waitPrompt, value, () => WaitPrompt); }
        }

        #endregion

        #region Bingding Command

        #region OnViewLoadedCommand

        /// <summary>
        /// The _on view loaded command
        /// </summary>
        private ICommand _onViewLoadedCommand;

        /// <summary>
        /// Gets the on view loaded command.
        /// </summary>
        /// <value>The on view loaded command.</value>
        public ICommand OnViewLoadedCommand
        {
            get { return _onViewLoadedCommand ?? (_onViewLoadedCommand = new DelegateCommand(OnViewLoaded)); }
        }

        #endregion

        #region NavigateCommand

        /// <summary>
        /// The _navigate command
        /// </summary>
        private ICommand _navigateCommand;

        /// <summary>
        /// Gets the navigate command.
        /// </summary>
        /// <value>The navigate command.</value>
        public ICommand NavigateCommand
        {
            get { return _navigateCommand ?? (_navigateCommand = new DelegateCommand<string>(Navigate)); }
        }

        #endregion

        #region NavigateToHomeCommand

        /// <summary>
        /// The _navigate to home command
        /// </summary>
        private ICommand _navigateToHomeCommand;

        /// <summary>
        /// Gets the navigate to home command.
        /// </summary>
        /// <value>The navigate to home command.</value>
        public ICommand NavigateToHomeCommand
        {
            get
            {
                return _navigateToHomeCommand ??
                       (_navigateToHomeCommand = new DelegateCommand<int?>(id =>
                           NavigationService.Navigate("LoginCtl", id, this), id => id != null));
            }
        }

        #endregion

        #endregion

        #region Method

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        /// <value>The navigation service.</value>
        private INavigationService NavigationService
        {
            get { return ServiceContainer.GetService<INavigationService>(); }
        }

        /// <summary>
        /// Navigates the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        public void Navigate(string target)
        {
            INavigationService service = NavigationService;
            if (NavigationService == null)
            {
                UINavigateBase parent = ParentNavigate;
                while (service == null && parent != null)
                {
                    service = ParentNavigate.NavigationService;
                    parent = parent.ParentNavigate;
                }
            }
            if (service != null)
                service.Navigate(target, null, this);
        }

        /// <summary>
        /// Called when [view loaded].
        /// </summary>
        protected virtual void OnViewLoaded()
        {
        }

        #endregion
    }
}