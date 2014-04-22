using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFPeony.Surveil.Custom
{
    /// <summary>
    /// 矩形布局容器
    /// </summary>
    public class RectanglePanel : Panel
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
            int multiple = this.Children.Count / 4;
            int remainder = this.Children.Count % 4;
            int up, right, down, left;
            up = right = down = left = 0;
            if (remainder == 0)
            {
                up = multiple;
                right = multiple * 2;
                down = multiple * 3;
                left = multiple * 4;

            }
            else if (remainder == 1)
            {
                up = multiple + 1;
                right = multiple * 2 + remainder;
                down = multiple * 3 + remainder;
                left = multiple * 4 + remainder;

            }
            else if (remainder == 2)
            {
                up = multiple + 1;
                right = multiple * 2 + remainder;
                down = multiple * 3 + remainder;
                left = multiple * 4 + remainder;

            }
            else if (remainder == 3)
            {
                up = multiple + 1;
                right = multiple * 2 + 2;
                down = multiple * 3 + remainder;
                left = multiple * 4 + remainder;

            }

            foreach (UIElement child in this.Children)
            {
                int count = this.Children.IndexOf(child);
                Point point = new Point();
                if (count < up)
                {
                    point.X = finalSize.Width * count / up - child.DesiredSize.Width / 2;
                    point.Y = -child.DesiredSize.Height / 2;
                }
                else if (count < right)
                {
                    point.X = finalSize.Width - child.DesiredSize.Width / 2;
                    point.Y = finalSize.Height * (count - up) / (right - up) - child.DesiredSize.Height / 2;
                }
                else if (count < down)
                {
                    point.X = finalSize.Width - finalSize.Width * (count - right) / (down - right) - child.DesiredSize.Width / 2;
                    point.Y = finalSize.Height - child.DesiredSize.Height / 2;
                }
                else if (count < left)
                {
                    point.X = -child.DesiredSize.Width / 2;
                    point.Y = finalSize.Height - finalSize.Height * (count - down) / (left - down) - child.DesiredSize.Height / 2;
                }

                Rect rectChild = new Rect(point, child.DesiredSize);
                child.Arrange(rectChild);
            }
            return finalSize;
        }

        #endregion
    }
}
