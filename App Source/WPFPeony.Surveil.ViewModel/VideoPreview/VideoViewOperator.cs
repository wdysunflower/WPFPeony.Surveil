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
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;
using WPFPeony.Surveil.Custom;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class VideoViewOperator.
    /// </summary>
    public class VideoViewOperator : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoViewOperator"/> class.
        /// </summary>
        public VideoViewOperator()
        {
            _layoutType = ViewLayoutTypes.Four;
            _layoutTypeCol = new ObservableCollection<UIDataBase>();
            UIDataBase view2 = new UIDataBase
            {
                RelationData = ViewLayoutTypes.Two,
                ControlViewKey = "View-Split",
                ControlToolTip = "平分视图"
            };
            _layoutTypeCol.Add(view2);
            UIDataBase view4 = new UIDataBase
            {
                RelationData = ViewLayoutTypes.Four,
                ControlViewKey = "View-Medium-Icons",
                ControlToolTip = "四视图"
            };
            _layoutTypeCol.Add(view4);
            UIDataBase view9 = new UIDataBase
            {
                RelationData = ViewLayoutTypes.Nine,
                ControlViewKey = "View-Small-Icons-01",
                ControlToolTip = "九视图"
            };
            _layoutTypeCol.Add(view9);
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
        /// The _layout type col
        /// </summary>
        private readonly ObservableCollection<UIDataBase> _layoutTypeCol;

        /// <summary>
        /// Gets the layout type col.
        /// </summary>
        /// <value>The layout type col.</value>
        public ObservableCollection<UIDataBase> LayoutTypeCol
        {
            get { return _layoutTypeCol; }
        }

        #endregion

        #region Binding Property

        /// <summary>
        /// The _selection changed command
        /// </summary>
        private ICommand _selectionChangedCmd;

        /// <summary>
        /// Gets the selection changed command.
        /// </summary>
        /// <value>The selection changed command.</value>
        public ICommand SelectionChangedCmd
        {
            get
            {
                return _selectionChangedCmd ??
                       (_selectionChangedCmd = new DelegateCommand<object>(ExSelectionChangedCmd));
            }
        }

        /// <summary>
        /// Exes the selection changed command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        public void ExSelectionChangedCmd(object sender)
        {
            UIDataBase data = sender as UIDataBase;

            if (data != null)
            {
                ViewLayoutTypes type = (ViewLayoutTypes) data.RelationData;
                LayoutType = type;
            }
        }

        #endregion
    }
}