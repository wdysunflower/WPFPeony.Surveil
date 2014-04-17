using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// 数据推送
    /// </summary>
    public class DataChangedArgs : EventArgs
    {
        public BindableBase OldValue;
        public BindableBase NewValue;
    }

    /// <summary>
    /// 数据推动委托
    /// </summary>
    /// <param name="sender">发起者</param>
    /// <param name="args">数据推送数据</param>
    public delegate void DataChangedHandler(object sender, DataChangedArgs args);

    /// <summary>
    /// 实体对象
    /// </summary>
    public abstract class ObjectControl : BindableBase
    {
        #region Property

        /// <summary>
        /// 唯一识别实体的字符串
        /// </summary>
        public string HandleString { get; set; }

        /// <summary>
        /// 父对象
        /// </summary>
        public ObjectControl ParentObject { get; set; }

        /// <summary>
        /// 管理对象
        /// </summary>
        public OperatorControl ParentOper { get; set; }

        /// <summary>
        /// 界面操作对象
        /// </summary>
        public UINavigateBase ParentNavigate { get; set; }

        #endregion

        #region Binding Property

        /// <summary>
        /// 当前对象
        /// </summary>
        private BindableBase _currentObject;
        public BindableBase CurrentObject
        {
            get { return _currentObject; }
            set
            {
                BindableBase oldvalue = _currentObject;
                SetProperty(ref _currentObject, value, () => CurrentObject);
                OnCurrentObjectChanged(oldvalue, _currentObject);
            }
        }

        /// <summary>
        /// 对象集合
        /// </summary>
        private ObservableCollection<BindableBase> _objectControlS;
        public ObservableCollection<BindableBase> ObjectControlS
        {
            get { return _objectControlS ?? (_objectControlS = new ObservableCollection<BindableBase>()); }
            set { SetProperty(ref _objectControlS, value, () => ObjectControlS); }
        }

        /// <summary>
        /// 选中的对象集合
        /// </summary>
        private ObservableCollection<BindableBase> _selectedObjectControlS;
        public ObservableCollection<BindableBase> SelectedObjectControlS
        {
            get { return _selectedObjectControlS ?? (_selectedObjectControlS = new ObservableCollection<BindableBase>()); }
            set { SetProperty(ref _selectedObjectControlS, value, () => SelectedObjectControlS); }
        }

        /// <summary>
        /// 展开/折叠
        /// </summary>
        private bool _isExpanded;
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {

                if (_isExpanded && ParentObject != null && !ParentObject.IsExpanded)
                {
                    ParentObject.IsExpanded = true;
                }

                SetProperty(ref _isExpanded, value, () => IsExpanded);
            }
        }

        #endregion

        #region Command

        #region MouseDoubleClickCmd

        private ICommand _mouseDoubleClickCmd;
        public ICommand MouseDoubleClickCmd
        {
            get
            {
                return _mouseDoubleClickCmd ??
                       (_mouseDoubleClickCmd = new DelegateCommand(ExMouseDoubleClickCmd));
            }
        }

        protected virtual void ExMouseDoubleClickCmd() { }

        #endregion

        #endregion

        #region Changed Event

        #region CurrentObjectChanged

        public event DataChangedHandler CurrentObjectChanged;

        protected virtual void OnCurrentObjectChanged(BindableBase oldvalue, BindableBase newvalue)
        {
            DataChangedArgs args = new DataChangedArgs { OldValue = oldvalue, NewValue = newvalue };
            if (CurrentObjectChanged != null)
                CurrentObjectChanged(this, args);
        }

        #endregion

        #endregion

        #region Private Method

        /// <summary>
        /// 初始化对象相关信息
        /// </summary>
        protected virtual void InitData()
        {
            if (ObjectControlS.Count > 0 && _currentObject == null)
                CurrentObject = ObjectControlS[0];
        }

        public virtual void CleanData() { }

        #endregion

        #region Public Method

        /// <summary>
        /// 删除项前的选中项处理
        /// </summary>
        /// <param name="data"></param>
        public void SetCurrentSimpleDel(BindableBase data = null)
        {
            if (ObjectControlS.Count == 1)
                return;

            BindableBase manageData = data ?? _currentObject;

            if (manageData != null)
            {
                int index = ObjectControlS.IndexOf(manageData);
                CurrentObject = index == ObjectControlS.Count - 1 ?
                    ObjectControlS[index - 1] : ObjectControlS[index + 1];
            }
        }

        #endregion
    }
}