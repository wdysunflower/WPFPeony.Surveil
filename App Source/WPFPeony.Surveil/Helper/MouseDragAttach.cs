using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WPFPeony.Surveil.Util;

namespace WPFPeony.Surveil
{
    public class MouseDragAttach
    {
        #region MouseDragCommandProperty

        public static DependencyProperty MouseDragCommandProperty = DependencyProperty.RegisterAttached
            ("MouseDragCommand",
             typeof(ICommand),
             typeof(MouseDragAttach),
             new FrameworkPropertyMetadata(null, SetMouseDrag));

        public static void SetMouseDragCommand(DependencyObject target, ICommand value)
        {
            target.SetValue(MouseDragCommandProperty, value);
        }

        public static ICommand GetMouseDragCommand(DependencyObject target)
        {
            return (ICommand)target.GetValue(MouseDragCommandProperty);
        }

        private static void SetMouseDrag(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var element = target as FrameworkElement;
            if (element != null)
            {
                if ((e.NewValue != null) && (e.OldValue == null))
                {
                    element.PreviewMouseLeftButtonDown += ElementPreviewMouseLeftButtonDown;
                    element.PreviewMouseMove += ElementPreviewMouseMove;
                    element.PreviewMouseUp += ElementPreviewMouseUp;
                }
                else if ((e.NewValue == null) && (e.OldValue != null))
                {
                    element.PreviewMouseLeftButtonDown -= ElementPreviewMouseLeftButtonDown;
                    element.PreviewMouseMove -= ElementPreviewMouseMove;
                    element.PreviewMouseUp -= ElementPreviewMouseUp;
                }
            }
        }

        private static bool _isMouseDown;
        private static void ElementPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;
            if (element != null && e.ClickCount < 2 && TestTreeViewItem(element, e.OriginalSource))
            {
                _isMouseDown = true;
            }
        }

        private static void ElementPreviewMouseMove(object sender, MouseEventArgs e)
        {
            var element = (FrameworkElement)sender;
            IInputElement hit = element.InputHitTest(e.GetPosition(element));

            if (_isMouseDown && e.LeftButton == MouseButtonState.Pressed && TestTreeViewItem(element, e.OriginalSource))
            {
                _isMouseDown = false;
                element.CaptureMouse();
            }

            if (element.IsMouseCaptured && hit == null && TestTreeViewItem(element, e.OriginalSource))
            {
                var command = (ICommand)element.GetValue(MouseDragCommandProperty);
                command.Execute(sender);
            }
        }

        private static void ElementPreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = (FrameworkElement)sender;

            if (element != null && element.IsMouseCaptured)
            {
                _isMouseDown = false;
                element.ReleaseMouseCapture();
            }
        }

        static bool TestTreeViewItem(UIElement element, object original)
        {
            if (element is TreeViewItem && original is Visual)
            {
                if (original is TreeViewItem)
                    return true;

                DependencyObject obj = VisualItemHelper.GetParentElement<TreeViewItem>(
                    (DependencyObject)original);
                if (obj == null)
                    return false;

                if (!obj.Equals(element))
                    return false;
            }
            return true;
        }

        #endregion
    }
}