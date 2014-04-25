using System.Windows;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class VideoWin : UIBindBase
    {
        #region Binding Command

        #region DoubleClickCmd

        private ICommand _doubleClickCmd;

        public ICommand DoubleClickCmd
        {
            get { return _doubleClickCmd; }
            set { SetProperty(ref _doubleClickCmd, value, () => DoubleClickCmd); }
        }

        #endregion

        #region DragCmd

        private ICommand _dragCmd;

        public ICommand DragCmd
        {
            get { return _dragCmd ?? (_dragCmd = new DelegateCommand<object>(ExDragCmd)); }
        }

        private void ExDragCmd(object sender)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                VideoWin videoWin = element.DataContext as VideoWin;
                if (videoWin != null)
                {
                    DragDrop.DoDragDrop(element, videoWin, DragDropEffects.Move);
                }
            }
        }

        #endregion

        #region DragOverCmd

        private ICommand _dragOverCmd;

        public ICommand DragOverCmd
        {
            get { return _dragOverCmd ?? (_dragOverCmd = new DelegateCommand<DragEventArgs>(ExDragOverCmd)); }
        }

        private void ExDragOverCmd(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            var sorItem = e.Data.GetData(e.Data.GetFormats()[0]) as VideoWin;
            if (sorItem != null)
            {
                VideoWin dirItem = this;
                if (!dirItem.Equals(sorItem))
                    e.Effects = DragDropEffects.Move;
            }
            e.Handled = true;
        }

        #endregion

        #region DropCmd

        private ICommand _dropCmd;

        public ICommand DropCmd
        {
            get { return _dropCmd ?? (_dropCmd = new DelegateCommand<DragEventArgs>(ExDropCmd)); }
        }

        private void ExDropCmd(DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            var sorItem = e.Data.GetData(e.Data.GetFormats()[0]) as VideoWin;
            if (sorItem != null)
            {
                VideoWin dirItem = this;

                if (!dirItem.Equals(sorItem))
                {
                    e.Effects = DragDropEffects.Move;
                    if (VideoWinChangeEvent != null)
                        VideoWinChangeEvent(sorItem, dirItem);
                }

            }
            e.Handled = true;
        }

        #endregion

        #endregion

        public event VideoWinChangeHandler VideoWinChangeEvent;
    }
}