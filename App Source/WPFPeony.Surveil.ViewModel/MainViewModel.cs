// ***********************************************************************
// <copyright file="MainViewModel.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-01-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-24-2014
// ***********************************************************************

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class MainViewModel.
    /// </summary>
    public class MainViewModel : UINavigateBase
    {
        #region Binding Property

        /// <summary>
        /// The _login oper
        /// </summary>
        private LoginManage _login;

        /// <summary>
        /// Gets the login oper.
        /// </summary>
        /// <value>The login oper.</value>
        public LoginManage Login
        {
            get { return _login ?? (_login = new LoginManage {ParentNavigate = this}); }
        }

        /// <summary>
        /// The _surveil oper
        /// </summary>
        private SurveilManage _surveil;

        /// <summary>
        /// Gets the surveil oper.
        /// </summary>
        /// <value>The surveil oper.</value>
        public SurveilManage Surveil
        {
            get { return _surveil ?? (_surveil = new SurveilManage {ParentNavigate = this}); }
        }

        #endregion

        #region Override

        /// <summary>
        /// Called when [view loaded].
        /// </summary>
        protected override void OnViewLoaded()
        {
            Navigate(UIViewNameHelper.RealTimeView);
        }

        #endregion
    }
}