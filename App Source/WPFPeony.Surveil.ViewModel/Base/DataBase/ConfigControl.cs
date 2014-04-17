using System;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// 设置类型
    /// </summary>
    public enum ConfigStatusTypes
    {
        Empty,
        Add,
        Edit
    }

    /// <summary>
    /// 设置相关的封装
    /// </summary>
    public abstract class ConfigControl : ObjectControl
    {
        #region Member

        /// <summary>
        /// 设置类型
        /// </summary>
        public ConfigStatusTypes ConfigType { get; set; }

        /// <summary>
        /// 配置是否成功（一般确认保存后改值为True）
        /// </summary>
        public bool IsConfigSucceed { get; protected set; }

        #endregion

        #region Command

        #region AddCommand

        protected ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new DelegateCommand(ExAddCommand)); }
        }

        protected virtual void ExAddCommand()
        {
        }

        #endregion

        #region EditCommand

        protected ICommand _editCommand;
        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new DelegateCommand(ExEditCommand)); }
        }

        protected virtual void ExEditCommand()
        {
        }

        #endregion

        #region DelCommand

        protected ICommand _delCommand;
        public ICommand DelCommand
        {
            get { return _delCommand ?? (_delCommand = new DelegateCommand(ExDelCommand)); }
        }

        protected virtual void ExDelCommand()
        {
            
        }

        #endregion

        #region OKCommand

        protected ICommand _okCommand;
        public ICommand OKCommand
        {
            get { return _okCommand ?? (_okCommand = new DelegateCommand(ExOKCommand)); }
        }

        protected virtual void ExOKCommand()
        {
            IsConfigSucceed = true;
            OnConfigClose();
        }

        #endregion

        #region CancleCommand

        protected ICommand _cancleCommand;
        public ICommand CancleCommand
        {
            get { return _cancleCommand ?? (_cancleCommand = new DelegateCommand(ExCancleCommand)); }
        }

        protected virtual void ExCancleCommand()
        {
            IsConfigSucceed = false;
            OnConfigClose();
        }

        #endregion

        #region RefreshCommand

        protected ICommand _refreshCommand;

        protected ConfigControl()
        {
            IsConfigSucceed = false;
        }

        public ICommand RefreshCommand
        {
            get { return _refreshCommand ?? (_refreshCommand = new DelegateCommand(ExRefreshCommand)); }
        }

        protected virtual void ExRefreshCommand()
        {
            RaisePropertyChanged();
        }

        #endregion

        #endregion

        #region Notify

        public event EventHandler ConfigClose;
        protected void OnConfigClose()
        {
            ConfigType = ConfigStatusTypes.Empty;
            if (ConfigClose != null)
                ConfigClose(this, new EventArgs());
        }

        /// <summary>
        /// 对象更新处理
        /// </summary>
        public event EventHandler ObjectNotified;
        public void OnObjectNotified()
        {
            if (ObjectNotified != null)
                ObjectNotified(this, new EventArgs());
        }

        /// <summary>
        /// 编辑通知处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void EditObjectNotified(object sender, EventArgs e)
        {
            RaisePropertyChanged();
        }

        #endregion
    }
}