using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFPeony.Surveil.Custom
{
    /// <summary>
    /// 尺寸计算依据类型
    /// </summary>
    public enum RadialPanelOrientation
    {
        ByWidth,
        ByHeight
    }

    /// <summary>
    /// 高级环形布局容器
    /// </summary>
    public class RadialPanel : Panel
    {
        #region Member

        bool _showPieLines;
        double _angleEach;       // angle for each child
        Size _sizeLargest;       // size of largest child
        double _radius;          // radius of circle
        double _outerEdgeFromCenter;
        double _innerEdgeFromCenter;

        #endregion

        #region Constructor

        // Static constructor to create Orientation dependency property.
        static RadialPanel()
        {
            OrientationProperty =
                DependencyProperty.Register("Orientation",
                    typeof(RadialPanelOrientation), typeof(RadialPanel),
                    new FrameworkPropertyMetadata(RadialPanelOrientation.ByWidth,
                            FrameworkPropertyMetadataOptions.AffectsMeasure));
        }

        #endregion

        #region Dependency Property

        // Orientation property.
        public static readonly DependencyProperty OrientationProperty;

        public RadialPanelOrientation Orientation
        {
            set { SetValue(OrientationProperty, value); }
            get { return (RadialPanelOrientation)GetValue(OrientationProperty); }
        }

        #endregion

        #region Property

        // ShowPieLines property.
        public bool ShowPieLines
        {
            set
            {
                if (value != _showPieLines)
                    InvalidateVisual();

                _showPieLines = value;
            }
            get
            {
                return _showPieLines;
            }
        }

        #endregion

        #region Override

        // Override of MeasureOverride.
        protected override Size MeasureOverride(Size sizeAvailable)
        {
            if (InternalChildren.Count == 0)
                return new Size(0, 0);

            _angleEach = 360.0 / InternalChildren.Count;
            _sizeLargest = new Size(0, 0);

            foreach (UIElement child in InternalChildren)
            {
                // Call Measure for each child ...
                //child.Measure(new Size(Double.PositiveInfinity,
                //                       Double.PositiveInfinity));

                // ... and then examine DesiredSize property of child.
                child.Measure(sizeAvailable);
                _sizeLargest.Width = Math.Max(_sizeLargest.Width,
                                             child.DesiredSize.Width);

                _sizeLargest.Height = Math.Max(_sizeLargest.Height,
                                              child.DesiredSize.Height);
            }
            if (Orientation == RadialPanelOrientation.ByWidth)
            {
                // Calculate the distance from the center to element edges.
                _innerEdgeFromCenter = _sizeLargest.Width / 2 /
                                        Math.Tan(Math.PI * _angleEach / 360);
                _outerEdgeFromCenter = _innerEdgeFromCenter + _sizeLargest.Height;

                // Calculate the radius of the circle based on the largest child.
                _radius = Math.Sqrt(Math.Pow(_sizeLargest.Width / 2, 2) +
                                   Math.Pow(_outerEdgeFromCenter, 2));
            }
            else
            {
                // Calculate the distance from the center to element edges.
                _innerEdgeFromCenter = _sizeLargest.Height / 2 /
                                        Math.Tan(Math.PI * _angleEach / 360);
                _outerEdgeFromCenter = _innerEdgeFromCenter + _sizeLargest.Width;

                // Calculate the radius of the circle based on the largest child.
                _radius = Math.Sqrt(Math.Pow(_sizeLargest.Height / 2, 2) +
                                   Math.Pow(_outerEdgeFromCenter, 2));
            }
            // Return the size of that circle.
            return new Size(2 * _radius, 2 * _radius);
        }

        // Override of ArrangeOverride.
        protected override Size ArrangeOverride(Size sizeFinal)
        {
            double angleChild = 0;
            Point ptCenter = new Point(sizeFinal.Width / 2, sizeFinal.Height / 2);
            double multiplier = 0;
            if (Math.Abs(_radius - 0) > double.Epsilon)
                multiplier = Math.Min(sizeFinal.Width / (2 * _radius),
                                          sizeFinal.Height / (2 * _radius));
            foreach (UIElement child in InternalChildren)
            {
                // Reset RenderTransform.
                child.RenderTransform = Transform.Identity;

                if (Orientation == RadialPanelOrientation.ByWidth)
                {
                    // Position the child at the top.
                    child.Arrange(
                        new Rect(ptCenter.X - multiplier * _sizeLargest.Width / 2,
                                 ptCenter.Y - multiplier * _outerEdgeFromCenter,
                                 multiplier * _sizeLargest.Width,
                                 multiplier * _sizeLargest.Height));
                }
                else
                {
                    // Position the child at the right.
                    child.Arrange(
                        new Rect(ptCenter.X + multiplier * _innerEdgeFromCenter,
                                 ptCenter.Y - multiplier * _sizeLargest.Height / 2,
                                 multiplier * _sizeLargest.Width,
                                 multiplier * _sizeLargest.Height));
                }
                // Rotate the child around the center (relative to the child).
                Point pt = TranslatePoint(ptCenter, child);
                child.RenderTransform =
                                new RotateTransform(angleChild, pt.X, pt.Y);

                // Increment the angle.
                angleChild += _angleEach;
            }

            return sizeFinal;
        }

        // Override OnRender to display optional pie lines.
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (ShowPieLines)
            {
                Point ptCenter =
                    new Point(RenderSize.Width / 2, RenderSize.Height / 2);
                double multiplier = Math.Min(RenderSize.Width / (2 * _radius),
                                             RenderSize.Height / (2 * _radius));
                Pen pen = new Pen(SystemColors.WindowTextBrush, 1) {DashStyle = DashStyles.Dash};

                // Display circle.
                dc.DrawEllipse(null, pen, ptCenter, multiplier * _radius,
                                                    multiplier * _radius);
                // Initialize angle.
                double angleChild = -_angleEach / 2;

                if (Orientation == RadialPanelOrientation.ByWidth)
                    angleChild += 90;

                // Loop through each child to draw radial lines from center.
                foreach (UIElement child in InternalChildren)
                {
                    dc.DrawLine(pen, ptCenter,
                        new Point(ptCenter.X + multiplier * _radius *
                                    Math.Cos(2 * Math.PI * angleChild / 360),
                                  ptCenter.Y + multiplier * _radius *
                                    Math.Sin(2 * Math.PI * angleChild / 360)));
                    angleChild += _angleEach;
                }
            }
        }

        #endregion
    }
}
