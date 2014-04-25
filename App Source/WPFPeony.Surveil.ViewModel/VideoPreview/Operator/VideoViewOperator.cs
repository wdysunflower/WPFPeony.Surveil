// ***********************************************************************
// <copyright file="VideoViewOperator.cs" company="Peony">
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

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;
using WPFPeony.Surveil.Custom;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class VideoViewManage.
    /// </summary>
    public class VideoViewOperator : OperatorBase
    {
        public bool IsFullScreen { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewOperator"/> class.
        /// </summary>
        public VideoViewOperator()
        {
            InitialData();
        }

        private void InitialData()
        {
            ObservableCollection<UIBindBase> layoutTypeCol = new ObservableCollection<UIBindBase>();
            UIBindBase view2 = new UIBindBase
            {
                RelationData = ViewLayoutTypes.Two,
                ControlViewKey = "View-Split",
                ControlToolTip = "平分视图"
            };
            layoutTypeCol.Add(view2);
            UIBindBase view4 = new UIBindBase
            {
                RelationData = ViewLayoutTypes.Four,
                ControlViewKey = "View-Medium-Icons",
                ControlToolTip = "四视图"
            };
            layoutTypeCol.Add(view4);
            UIBindBase view9 = new UIBindBase
            {
                RelationData = ViewLayoutTypes.Nine,
                ControlViewKey = "View-Small-Icons-01",
                ControlToolTip = "九视图"
            };
            layoutTypeCol.Add(view9);

            ObservableCol = layoutTypeCol;
            CurrentData = view4;
        }

        #region Binding Property

        /// <summary>
        /// The _layout type
        /// </summary>
        private ViewLayoutTypes _layoutType;

        /// <summary>
        /// Gets or sets the type of the layout.
        /// </summary>
        /// <value>The type of the layout.</value>
        public ViewLayoutTypes LayoutType
        {
            get { return _layoutType; }
            set { SetProperty(ref _layoutType, value, () => LayoutType); }
        }

        /// <summary>
        /// The _video win oper
        /// </summary>
        private VideoWinOperator _videoWinOper;

        /// <summary>
        /// Gets the video win oper.
        /// </summary>
        /// <value>The video win oper.</value>
        public VideoWinOperator VideoWinOper
        {
            get { return _videoWinOper ?? (_videoWinOper = new VideoWinOperator(this)); }
        }

        #endregion

        #region Binding Property

        protected override void OnCurrentDataChanged()
        {
            if (_currentData != null)
            {
                if (_videoWinOper != null)
                    foreach (UIBindBase bindBase in _videoWinOper.ObservableCol)
                    {
                        bindBase.ControlVis = Visibility.Visible;
                    }

                IsFullScreen = false;

                ViewLayoutTypes type = (ViewLayoutTypes)_currentData.RelationData;
                LayoutType = type;
            }
        }

        #endregion

        #region Public Method

        private ViewLayoutTypes _tempLayoutType;
        private UIBindBase _tempCurrentData;

        public void ExVideoWinDoubleClickCmd(VideoWin videoWin)
        {
            if (IsFullScreen)
            {
                IsFullScreen = false;
                foreach (UIBindBase bindBase in _videoWinOper.ObservableCol)
                {
                    bindBase.ControlVis = Visibility.Visible;
                }
                LayoutType = _tempLayoutType;
                CurrentData = _tempCurrentData;
            }
            else
            {
                IsFullScreen = true;
                foreach (UIBindBase bindBase in _videoWinOper.ObservableCol)
                {
                    if (bindBase != videoWin)
                        bindBase.ControlVis = Visibility.Hidden;
                }
                videoWin.ControlVis = Visibility.Visible;
                _tempLayoutType = LayoutType;
                _tempCurrentData = _currentData;

                CurrentData = null;
                LayoutType = ViewLayoutTypes.SpecialOne;
            }
        }

        #endregion
    }
}