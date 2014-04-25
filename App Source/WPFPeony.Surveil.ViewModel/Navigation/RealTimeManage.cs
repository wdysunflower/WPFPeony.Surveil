﻿// ***********************************************************************
// <copyright file="RealTimeOperator.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-22-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-24-2014
// ***********************************************************************

using System.Threading;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class RealTimeManage.
    /// </summary>
    public class RealTimeManage : UINavigateBase
    {
        #region Binding Property

        /// <summary>
        /// The _camera oper
        /// </summary>
        private CameraOperator _cameraOper;

        /// <summary>
        /// Gets the camera oper.
        /// </summary>
        /// <value>The camera oper.</value>
        public CameraOperator CameraOper
        {
            get { return _cameraOper ?? (_cameraOper = new CameraOperator(DataUIModes.TreeView)); }
        }

        /// <summary>
        /// The _video view
        /// </summary>
        private VideoViewManage _videoView;

        /// <summary>
        /// Gets the video view.
        /// </summary>
        /// <value>The video view.</value>
        public VideoViewManage VideoView
        {
            get { return _videoView ?? (_videoView = new VideoViewManage()); }
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
    }
}