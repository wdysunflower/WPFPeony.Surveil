using System.Collections.ObjectModel;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;
using WPFPeony.Surveil.Custom;

namespace WPFPeony.Surveil.ViewModel
{
    public class VideoViewOperator : ComplexData
    {
        public VideoViewOperator()
        {
            _layoutType = ViewLayoutTypes.Four;
            _layoutTypeCol = new ObservableCollection<UIDataBase>();
            UIDataBase view2 = new UIDataBase { RelationData = ViewLayoutTypes.Two, ControlViewKey = "View-Split", ControlToolTip = "平分视图" };
            _layoutTypeCol.Add(view2);
            UIDataBase view4 = new UIDataBase { RelationData = ViewLayoutTypes.Four, ControlViewKey = "View-Medium-Icons", ControlToolTip = "四视图" };
            _layoutTypeCol.Add(view4);
            UIDataBase view9 = new UIDataBase { RelationData = ViewLayoutTypes.Nine, ControlViewKey = "View-Small-Icons-01", ControlToolTip = "九视图" };
            _layoutTypeCol.Add(view9);
        }

        #region Binding Property

        private ViewLayoutTypes _layoutType;
        public ViewLayoutTypes LayoutType
        {
            get { return _layoutType; }
            set { SetProperty(ref _layoutType, value, () => LayoutType); }
        }

        private ObservableCollection<UIDataBase> _layoutTypeCol;
        public ObservableCollection<UIDataBase> LayoutTypeCol
        {
            get { return _layoutTypeCol; }
        }

        #endregion

        #region Binding Property

        private ICommand _selectionChangedCmd;
        public ICommand SelectionChangedCmd
        {
            get
            {
                return _selectionChangedCmd ?? (_selectionChangedCmd = new DelegateCommand<object>(ExSelectionChangedCmd));
            }
        }

        public void ExSelectionChangedCmd(object sender)
        {
            UIDataBase data = sender as UIDataBase;

            if (data != null)
            {
                ViewLayoutTypes type = (ViewLayoutTypes)data.RelationData;
                LayoutType = type;
            }
        }

        #endregion
    }
}
