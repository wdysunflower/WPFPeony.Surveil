// ***********************************************************************
// <copyright file="PlayBackOperator.cs" company="Peony">
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

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class PlayBackOperator.
    /// </summary>
    public class PlayBackOperator
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
            get { return _cameraOper ?? (_cameraOper = new CameraOperator(DataUIModes.TreeListView)); }
        }

        /// <summary>
        /// The _video view oper
        /// </summary>
        private VideoViewOperator _videoViewOper;

        /// <summary>
        /// Gets the video view oper.
        /// </summary>
        /// <value>The video view oper.</value>
        public VideoViewOperator VideoViewOper
        {
            get { return _videoViewOper ?? (_videoViewOper = new VideoViewOperator()); }
        }

        #endregion
    }
}