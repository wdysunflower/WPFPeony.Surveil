// ***********************************************************************
// <copyright file="UIDataBase.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-18-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-25-2014
// ***********************************************************************

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using DevExpress.Xpf.Mvvm;
using WPFPeony.Surveil.Util;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class UIBindBase.
    /// </summary>
    public class UIBindBase : BindableBase
    {
        #region 数据

        /// <summary>
        /// Gets or sets the relation data.
        /// </summary>
        /// <value>The relation data.</value>
        public object RelationData { get; set; }

        #endregion

        #region 界面展现

        #region ControlName

        /// <summary>
        /// 显示名称
        /// </summary>
        [NonSerialized] private string _controlName;

        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        /// <value>The name of the control.</value>
        [XmlIgnore]
        public string ControlName
        {
            get
            {
                return !String.IsNullOrEmpty(_controlNameKey)
                    ? ResourceCom.GetString(_controlNameKey)
                    : _controlName;
            }
            set
            {
                _controlNameKey = null;
                SetProperty(ref _controlName, value, () => ControlName);
            }
        }

        /// <summary>
        /// The _control name key
        /// </summary>
        [NonSerialized] private string _controlNameKey;

        /// <summary>
        /// Gets or sets the control name key.
        /// </summary>
        /// <value>The control name key.</value>
        [XmlIgnore]
        public string ControlNameKey
        {
            get { return _controlNameKey; }
            set { _controlNameKey = value; }
        }

        /// <summary>
        /// The _control name temporary key
        /// </summary>
        [NonSerialized] private string _controlNameTempKey;

        /// <summary>
        /// Gets or sets the control name temporary key.
        /// </summary>
        /// <value>The control name temporary key.</value>
        [XmlIgnore]
        public string ControlNameTempKey
        {
            get { return _controlNameTempKey; }
            set
            {
                _controlNameTempKey = value;
                _controlNameKey = value;
            }
        }

        #endregion

        #region ControlImage

        /// <summary>
        /// 图片
        /// </summary>
        [NonSerialized] private ImageSource _controlImage;

        /// <summary>
        /// Gets or sets the control image.
        /// </summary>
        /// <value>The control image.</value>
        [XmlIgnore]
        public ImageSource ControlImage
        {
            get
            {
                return !String.IsNullOrEmpty(_controlImageKey)
                    ? ResourceCom.GetImageSource(_controlImageKey)
                    : _controlImage;
            }
            set
            {
                _controlImageKey = null;
                SetProperty(ref _controlImage, value, () => ControlImage);
            }
        }

        /// <summary>
        /// The _control image key
        /// </summary>
        [NonSerialized] private string _controlImageKey;

        /// <summary>
        /// Gets or sets the control image key.
        /// </summary>
        /// <value>The control image key.</value>
        [XmlIgnore]
        public string ControlImageKey
        {
            get { return _controlImageKey; }
            set { _controlImageKey = value; }
        }

        #endregion

        #region ControlView

        /// <summary>
        /// 展现
        /// </summary>
        [NonSerialized] private Viewbox _controlView;

        /// <summary>
        /// Gets or sets the control view.
        /// </summary>
        /// <value>The control view.</value>
        [XmlIgnore]
        public Viewbox ControlView
        {
            get
            {
                return !String.IsNullOrEmpty(_controlViewKey)
                    ? ResourceCom.GetViewbox(_controlViewKey)
                    : _controlView;
            }
            set
            {
                _controlViewKey = null;
                SetProperty(ref _controlView, value, () => ControlView);
            }
        }

        /// <summary>
        /// The _control view key
        /// </summary>
        [NonSerialized] private string _controlViewKey;

        /// <summary>
        /// Gets or sets the control view key.
        /// </summary>
        /// <value>The control view key.</value>
        [XmlIgnore]
        public string ControlViewKey
        {
            get { return _controlViewKey; }
            set { _controlViewKey = value; }
        }

        #endregion

        #region ControlToolTip

        /// <summary>
        /// 显示标签
        /// </summary>
        [NonSerialized] private string _controlToolTip;

        /// <summary>
        /// Gets or sets the control tool tip.
        /// </summary>
        /// <value>The control tool tip.</value>
        [XmlIgnore]
        public string ControlToolTip
        {
            get
            {
                return !String.IsNullOrEmpty(_controlToolTipKey)
                    ? ResourceCom.GetString(_controlToolTipKey)
                    : _controlToolTip;
            }
            set
            {
                _controlToolTipKey = null;
                SetProperty(ref _controlToolTip, value, () => ControlToolTip);
            }
        }

        /// <summary>
        /// The _control tool tip key
        /// </summary>
        [NonSerialized] private string _controlToolTipKey;

        /// <summary>
        /// Gets or sets the control tool tip key.
        /// </summary>
        /// <value>The control tool tip key.</value>
        [XmlIgnore]
        public string ControlToolTipKey
        {
            get { return _controlToolTipKey; }
            set { _controlToolTipKey = value; }
        }

        /// <summary>
        /// The _control tool tip temporary key
        /// </summary>
        [NonSerialized] private string _controlToolTipTempKey;

        /// <summary>
        /// Gets or sets the control tool tip temporary key.
        /// </summary>
        /// <value>The control tool tip temporary key.</value>
        [XmlIgnore]
        public string ControlToolTipTempKey
        {
            get { return _controlToolTipTempKey; }
            set
            {
                _controlToolTipTempKey = value;
                _controlToolTipKey = value;
            }
        }

        #endregion

        #region ContextMenuData

        /// <summary>
        /// 右键菜单数据
        /// </summary>
        [NonSerialized] private ObservableCollection<BindableBase> _contextMenuData;

        /// <summary>
        /// Gets or sets the context menu data.
        /// </summary>
        /// <value>The context menu data.</value>
        [XmlIgnore]
        public ObservableCollection<BindableBase> ContextMenuData
        {
            get { return _contextMenuData ?? (_contextMenuData = new ObservableCollection<BindableBase>()); }
            set { SetProperty(ref _contextMenuData, value, () => ContextMenuData); }
        }

        #endregion

        #endregion

        #region 界面控制

        /// <summary>
        /// 显示属性
        /// </summary>
        [NonSerialized] private Visibility _controlVis;

        /// <summary>
        /// Gets or sets the control vis.
        /// </summary>
        /// <value>The control vis.</value>
        [XmlIgnore]
        public Visibility ControlVis
        {
            get { return _controlVis; }
            set { SetProperty(ref _controlVis, value, () => ControlVis); }
        }

        /// <summary>
        /// 边界间距
        /// </summary>
        [NonSerialized] private Thickness _margin;

        /// <summary>
        /// Gets or sets the margin.
        /// </summary>
        /// <value>The margin.</value>
        [XmlIgnore]
        public Thickness Margin
        {
            get { return _margin; }
            set { SetProperty(ref _margin, value, () => Margin); }
        }

        /// <summary>
        /// 是否可用（当前项）
        /// </summary>
        [NonSerialized] private bool _isEnabled = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value, () => IsEnabled); }
        }

        /// <summary>
        /// 是否被选中（当前项）
        /// </summary>
        [NonSerialized] private bool _isSelected;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value><c>true</c> if this instance is selected; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value, () => IsSelected, OnIsSelectedChanged); }
        }

        /// <summary>
        /// Occurs when [is selected changed].
        /// </summary>
        public event Action<object> IsSelectedChanged;

        /// <summary>
        /// Called when [is selected changed].
        /// </summary>
        protected virtual void OnIsSelectedChanged()
        {
            if (IsSelectedChanged != null)
                IsSelectedChanged(this);
        }

        /// <summary>
        /// 是否选择
        /// </summary>
        [NonSerialized] private bool _isChecked;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is checked.
        /// </summary>
        /// <value><c>true</c> if this instance is checked; otherwise, <c>false</c>.</value>
        [XmlIgnore]
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value, () => IsChecked, OnIsCheckedChanged); }
        }

        /// <summary>
        /// Occurs when [is checked changed].
        /// </summary>
        public event EventHandler IsCheckedChanged;

        /// <summary>
        /// Called when [is checked changed].
        /// </summary>
        protected virtual void OnIsCheckedChanged()
        {
            if (IsChecked)
            {
                if (!string.IsNullOrEmpty(ControlNameTempKey))
                    ControlName = ResourceCom.GetString("Un" + ControlNameTempKey);
                if (!string.IsNullOrEmpty(ControlToolTipTempKey))
                    ControlToolTip = ResourceCom.GetString("Un" + ControlToolTipTempKey);
            }
            else
            {
                if (!string.IsNullOrEmpty(ControlNameTempKey))
                    ControlName = ResourceCom.GetString(ControlNameTempKey);
                if (!string.IsNullOrEmpty(ControlToolTipTempKey))
                    ControlToolTip = ResourceCom.GetString(ControlToolTipTempKey);
            }

            if (IsCheckedChanged != null)
                IsCheckedChanged(this, new EventArgs());
        }

        #endregion

        #region ControlCommand

        /// <summary>
        /// 命令
        /// </summary>
        [NonSerialized] private ICommand _controlCmd;

        /// <summary>
        /// Gets or sets the control command.
        /// </summary>
        /// <value>The control command.</value>
        [XmlIgnore]
        public ICommand ControlCmd
        {
            get { return _controlCmd; }
            set { SetProperty(ref _controlCmd, value, () => ControlCmd); }
        }

        #endregion
    }
}