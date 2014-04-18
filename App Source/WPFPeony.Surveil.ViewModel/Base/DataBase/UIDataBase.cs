using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using DevExpress.Xpf.Mvvm;
using WPFPeony.Surveil.Util;

namespace WPFPeony.Surveil.ViewModel
{
    public class UIDataBase : BindableBase
    {
        public string ControlHandle { get; set; }

        #region 界面展现

        #region ControlName

        /// <summary>
        /// 显示名称
        /// </summary>
        [NonSerialized]
        private string _controlName;
        [XmlIgnore]
        public string ControlName
        {
            get
            {
                return !String.IsNullOrEmpty(_controlNameKey) ?
                    ResourceCom.GetString(_controlNameKey) : _controlName;
            }
            set
            {
                _controlNameKey = null;
                SetProperty(ref _controlName, value, () => ControlName);
            }
        }

        [NonSerialized]
        private string _controlNameKey;
        [XmlIgnore]
        public string ControlNameKey
        {
            get { return _controlNameKey; }
            set { _controlNameKey = value; }
        }

        [NonSerialized]
        private string _controlNameTempKey;
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
        [NonSerialized]
        private ImageSource _controlImage;
        [XmlIgnore]
        public ImageSource ControlImage
        {
            get
            {
                return !String.IsNullOrEmpty(_controlImageKey) ?
                    ResourceCom.GetImageSource(_controlImageKey) : _controlImage;
            }
            set
            {
                _controlImageKey = null;
                SetProperty(ref _controlImage, value, () => ControlImage);
            }
        }

        [NonSerialized]
        private string _controlImageKey;
        [XmlIgnore]
        public string ControlImageKey
        {
            get { return _controlImageKey; }
            set { _controlImageKey = value; }
        }

        #endregion

        #region ControlToolTip

        /// <summary>
        /// 显示标签
        /// </summary>
        [NonSerialized]
        private string _controlToolTip;
        [XmlIgnore]
        public string ControlToolTip
        {
            get
            {
                return !String.IsNullOrEmpty(_controlToolTipKey) ?
                    ResourceCom.GetString(_controlToolTipKey) : _controlToolTip;
            }
            set
            {
                _controlToolTipKey = null;
                SetProperty(ref _controlToolTip, value, () => ControlToolTip);
            }
        }

        [NonSerialized]
        private string _controlToolTipKey;
        [XmlIgnore]
        public string ControlToolTipKey
        {
            get { return _controlToolTipKey; }
            set { _controlToolTipKey = value; }
        }

        [NonSerialized]
        private string _controlToolTipTempKey;
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
        [NonSerialized]
        private ObservableCollection<BindableBase> _contextMenuData;
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
        [NonSerialized]
        private Visibility _controlVis;
        [XmlIgnore]
        public Visibility ControlVis
        {
            get { return _controlVis; }
            set { SetProperty(ref _controlVis, value, () => ControlVis); }
        }

        /// <summary>
        /// 边界间距
        /// </summary>
        [NonSerialized]
        private Thickness _margin;
        [XmlIgnore]
        public Thickness Margin
        {
            get { return _margin; }
            set { SetProperty(ref _margin, value, () => Margin); }
        }

        /// <summary>
        /// 是否可用（当前项）
        /// </summary>
        [NonSerialized]
        private bool _isEnabled = true;
        [XmlIgnore]
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value, () => IsEnabled); }
        }

        /// <summary>
        /// 是否被选中（当前项）
        /// </summary>
        [NonSerialized]
        private bool _isSelected;
        [XmlIgnore]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value, () => IsSelected, OnIsSelectedChanged); }
        }

        public event Action<object> IsSelectedChanged;
        protected virtual void OnIsSelectedChanged()
        {
            if (IsSelectedChanged != null)
                IsSelectedChanged(this);
        }

        /// <summary>
        /// 是否选择
        /// </summary>
        [NonSerialized]
        private bool _isChecked;
        [XmlIgnore]
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value, () => IsChecked, OnIsCheckedChanged); }
        }

        public event EventHandler IsCheckedChanged;
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
        [NonSerialized]
        private ICommand _controlCmd;
        [XmlIgnore]
        public ICommand ControlCmd
        {
            get { return _controlCmd; }
            set { SetProperty(ref _controlCmd, value, () => ControlCmd); }
        }

        #endregion
    }
}
