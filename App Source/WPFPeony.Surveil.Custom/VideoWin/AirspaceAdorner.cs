using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFPeony.Surveil.Custom
{
    /// <summary>
    /// 通过承载Window解决跨“空域”问题的装饰器
    /// </summary>
    public class AirspaceAdorner : Adorner
    {
        #region Member

        private readonly AdornerLayer _parentLayer;

        private Window _parentWindow;
        private Window _contentWindow;

        #endregion

        public AirspaceAdorner(UIElement adornedElement, AdornerLayer parentLayer)
            : base(adornedElement)
        {
            _parentLayer = parentLayer;
            InitialData();
        }

        private void InitialData()
        {
            //呈现内容的窗体
            _contentWindow = new Window
            {
                Background = Brushes.Transparent,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                ShowInTaskbar = false,
                Focusable = false
            };

            //绑定内容
            var contentBinding = new Binding
            {
                Mode = BindingMode.TwoWay,
                Source = AdornedElement,
                Path = new PropertyPath(DataContextProperty)
            };
            _contentWindow.SetBinding(DataContextProperty, contentBinding);

            _parentLayer.Add(this);

            this.Loaded += Win32Decorator_Loaded;
            AdornedElement.LayoutUpdated += ParentLayoutUpdated;
        }

        #region WinChild

        public object WinChild
        {
            get
            {
                if (_contentWindow != null)
                    return _contentWindow.Content;
                return null;
            }
            set
            {
                if (_contentWindow != null)
                    _contentWindow.Content = value;
            }
        }

        #endregion

        private void Win32Decorator_Loaded(object sender, RoutedEventArgs e)
        {
            _parentWindow = GetParentWindow(this);
            _contentWindow.Owner = _parentWindow;

            SetChildWinProperty();
            _contentWindow.Show();
        }

        private void ParentLayoutUpdated(object sender, EventArgs e)
        {
            SetChildWinProperty();
        }

        private Window GetParentWindow(DependencyObject dependency)
        {
            Window parent = Window.GetWindow(dependency);
            if (parent == null)
                throw new ApplicationException("A window parent could not be found for " + dependency);

            return parent;
        }

        private void SetChildWinProperty()
        {
            FrameworkElement element = AdornedElement as FrameworkElement;
            if (element != null)
            {
                //高宽
                _contentWindow.Width = element.ActualWidth;
                _contentWindow.Height = element.ActualHeight;

                //位置
                Point elementPoint = new Point(0, 0);
                Point parentPoint = element.PointToScreen(elementPoint);
                _contentWindow.Left = parentPoint.X;
                _contentWindow.Top = parentPoint.Y;
            }
        }

        public void Detach()
        {
            _contentWindow.Close();
            AdornedElement.LayoutUpdated -= ParentLayoutUpdated;
            _parentLayer.Remove(this);
        }
    }
}