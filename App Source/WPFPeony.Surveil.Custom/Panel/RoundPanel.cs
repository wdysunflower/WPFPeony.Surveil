using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFPeony.Surveil.Custom
{
    /// <summary>
    /// 环形布局容器
    /// </summary>
    public class RoundPanel : Panel
    {
        #region Override

        protected override Size MeasureOverride(Size availableSize)
        {
            Size childrenSize = new Size(0, 0);
            foreach (UIElement child in this.Children)
            {
                child.Measure(availableSize);
                childrenSize.Height = Math.Max(child.DesiredSize.Height, childrenSize.Height);
                childrenSize.Width = Math.Max(child.DesiredSize.Width, childrenSize.Width);
            }
            return childrenSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            double angle = Math.PI * 2 / this.Children.Count;
            double a = finalSize.Width / 2;
            double b = finalSize.Height / 2;

            foreach (UIElement child in this.Children)
            {
                double childangle = this.Children.IndexOf(child) * angle;

                Point point = new Point();
                point.X = a * Math.Cos(childangle) + a - child.DesiredSize.Width / 2;
                point.Y = b * Math.Sin(childangle) + b - child.DesiredSize.Height / 2;

                Rect rectChild = new Rect(point, child.DesiredSize);
                child.Arrange(rectChild);
            }
            return finalSize;
        }

        #endregion
    }
}
