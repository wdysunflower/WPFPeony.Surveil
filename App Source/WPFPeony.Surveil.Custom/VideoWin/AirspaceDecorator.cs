using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFPeony.Surveil.Custom
{
    /// <summary>
    /// 利用AirspaceAdorner实现可跨“空域”的控件
    /// </summary>
    public class AirspaceDecorator : ContentControl
    {
        private AirspaceAdorner _airspaceAdorner;

        #region  Overlay

        public static readonly DependencyProperty OverlayProperty = DependencyProperty.Register("Overlay",
            typeof(object), typeof(AirspaceDecorator), new PropertyMetadata((d, e) =>
            {
                AirspaceDecorator decorator = (AirspaceDecorator)d;
                decorator.OnOverlayChanged(e.OldValue, e.NewValue);
            }));

        /// <summary>
        /// 跨空域呈现的WPF内容
        /// </summary>
        public object Overlay
        {
            get { return GetValue(OverlayProperty); }
            set { SetValue(OverlayProperty, value); }
        }
                
        protected virtual void OnOverlayChanged(object oldContent, object newContent)
        {
            if (_airspaceAdorner != null)
                _airspaceAdorner.WinChild = newContent;
        }

        #endregion

        #region IsOpen

        public static readonly DependencyProperty IsOpenProperty = DependencyProperty.Register("IsOpen",
            typeof(bool), typeof(AirspaceDecorator),
              new PropertyMetadata((d, e) => d.Dispatcher.BeginInvoke((Action)delegate
              {
                  var decorator = (AirspaceDecorator)d;
                  decorator.IsOpenChanged();

              }, System.Windows.Threading.DispatcherPriority.Render)));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private void IsOpenChanged()
        {
            if (IsOpen)
            {
                //得到装饰层容器，为层赋值
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                _airspaceAdorner = new AirspaceAdorner(this, adornerLayer) { WinChild = Overlay };
            }
            else
            {
                //关闭层，移除层装饰
                if (_airspaceAdorner != null)
                    _airspaceAdorner.Detach();
            }
        }

        #endregion
    }
}
