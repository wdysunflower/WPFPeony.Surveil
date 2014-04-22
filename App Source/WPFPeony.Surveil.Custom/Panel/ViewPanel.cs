using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WPFPeony.Surveil.Custom
{
    /// <summary>
    /// 布局类型
    /// </summary>
    public enum ViewLayoutTypes
    {
        [Description("0")]
        None,

        [Description("1")]
        One = 1,

        [Description("2")]
        Two = 2,

        [Description("3")]
        Three = 3,

        [Description("4")]
        Four = 4,

        [Description("5")]
        Five = 5,

        [Description("6")]
        Six = 6,

        [Description("7")]
        Seven = 7,

        [Description("8")]
        Eight = 8,

        [Description("9")]
        Nine = 9,

        [Description("10")]
        Ten = 10,

        [Description("11")]
        Eleven = 11,

        [Description("12")]
        Twelve = 12,

        [Description("13")]
        Thirteen = 13,

        [Description("14")]
        Fourteen = 14,

        [Description("15")]
        Fifteen = 15,

        [Description("16")]
        Sixteen = 16,

        More = 100,

        //Special
        [Description("1")]
        SpecialOne = 101,

        [Description("5")]
        SpecialFive = 105,

        [Description("7")]
        SpecialSeven = 107,

        [Description("10")]
        SpecialTen = 110,
    }

    /// <summary>
    /// 视频预览视图布局容器
    /// </summary>
    public class ViewPanel : Panel
    {
        #region Loaded

        public ViewPanel()
        {
            this.Loaded += ViewPanelLoaded;
        }

        void ViewPanelLoaded(object sender, RoutedEventArgs e)
        {
            SetLayoutCount();
        }

        #endregion

        #region Dependency Property

        #region LayoutType

        public static DependencyProperty LayoutTypeProperty =
               DependencyProperty.Register("LayoutType", typeof(ViewLayoutTypes), typeof(ViewPanel),
               new PropertyMetadata(ViewLayoutTypes.Five, OnLayoutChanged));

        public ViewLayoutTypes LayoutType
        {
            get { return (ViewLayoutTypes)GetValue(LayoutTypeProperty); }
            set { SetValue(LayoutTypeProperty, value); }
        }

        #endregion

        #region Rows

        public static DependencyProperty RowsProperty =
        DependencyProperty.Register("Rows", typeof(int), typeof(ViewPanel), new PropertyMetadata(0, OnLayoutChanged));

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        #endregion

        #region Columns

        public static DependencyProperty ColumnsProperty =
        DependencyProperty.Register("Columns", typeof(int), typeof(ViewPanel), new PropertyMetadata(0, OnLayoutChanged));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        #endregion

        #region LayoutCount

        public static DependencyProperty LayoutCountProperty =
        DependencyProperty.Register("LayoutCount", typeof(int), typeof(ViewPanel));

        public int LayoutCount
        {
            get { return (int)GetValue(LayoutCountProperty); }
            set { SetValue(LayoutCountProperty, value); }
        }

        #endregion

        private static void OnLayoutChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ViewPanel panel = obj as ViewPanel;
            if (panel != null)
            {
                if (panel.IsLoaded)
                    panel.SetLayoutCount();
                panel.InvalidateMeasure();
            }
        }

        #endregion

        #region MeasureOverride

        protected override Size MeasureOverride(Size availableSize)
        {
            if (double.IsInfinity(availableSize.Width) || double.IsInfinity(availableSize.Height))
            {
                FrameworkElement parentElement = Parent as FrameworkElement;
                if (parentElement != null)
                {
                    availableSize.Width = parentElement.ActualWidth;
                    availableSize.Height = parentElement.ActualHeight;
                }
                else
                {
                    availableSize = new Size(100, 100);
                }
            }

            switch (LayoutType)
            {
                case ViewLayoutTypes.None:
                    return availableSize;
                case ViewLayoutTypes.More:
                    SetMeasureWithRegular(availableSize);
                    break;
                default:
                    {
                        string methodName = "SetMeasureWith" + LayoutType.ToString();
                        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                        MethodInfo info = this.GetType().GetMethod(methodName, flags);
                        if (info != null)
                            info.Invoke(this, new object[] { availableSize });
                    }
                    break;
            }
            return availableSize;
        }

        private void SetMeasureWithOne(Size availableSize)
        {
            foreach (UIElement child in this.Children)
            {
                Size childSize = this.Children.IndexOf(child) < 1 ?
                    new Size(availableSize.Width, availableSize.Height) : Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithTwo(Size availableSize)
        {
            double cellWidth = availableSize.Width / 2;

            foreach (UIElement child in this.Children)
            {
                Size childSize = this.Children.IndexOf(child) < 2 ?
                    new Size(cellWidth, availableSize.Height) : Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithThree(Size availableSize)
        {
            double cellWidth = availableSize.Width / 2;
            double cellHeight = availableSize.Height / 2;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) < 2)
                    childSize = new Size(cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 3)
                    childSize = new Size(cellWidth * 2, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithFour(Size availableSize)
        {
            double cellWidth = availableSize.Width / 2;
            double cellHeight = availableSize.Height / 2;

            foreach (UIElement child in this.Children)
            {
                Size childSize = this.Children.IndexOf(child) < 4 ?
                    new Size(cellWidth, cellHeight) : Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithFive(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) == 0)
                    childSize = new Size(cellWidth * 3, cellHeight * 4);
                else if (this.Children.IndexOf(child) < 5)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithSix(Size availableSize)
        {
            double cellWidth = availableSize.Width / 3;
            double cellHeight = availableSize.Height / 3;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) == 0)
                    childSize = new Size(cellWidth * 2, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 6)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithSeven(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) < 3)
                {
                    childSize = new Size(cellWidth * 2, cellHeight * 2);
                }
                else if (this.Children.IndexOf(child) < 7)
                {
                    childSize = new Size(cellWidth, cellHeight);
                }
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithEight(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) == 0)
                    childSize = new Size(cellWidth * 3, cellHeight * 3);
                else if (this.Children.IndexOf(child) < 8)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithNine(Size availableSize)
        {
            double cellWidth = availableSize.Width / 3;
            double cellHeight = availableSize.Height / 3;

            foreach (UIElement child in this.Children)
            {
                Size childSize = this.Children.IndexOf(child) < 9 ?
                    new Size(cellWidth, cellHeight) : Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithTen(Size availableSize)
        {
            double cellWidth = availableSize.Width / 5;
            double cellHeight = availableSize.Height / 5;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) == 0)
                    childSize = new Size(cellWidth * 4, cellHeight * 4);
                else if (this.Children.IndexOf(child) < 10)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithEleven(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) == 0)
                    childSize = new Size(cellWidth * 3, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 11)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithTwelve(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) < 2)
                    childSize = new Size(cellWidth * 1.5, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 12)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithThirteen(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) < 6)
                    childSize = new Size(cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) == 6)
                    childSize = new Size(cellWidth * 2, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 9)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithFourteen(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) < 2)
                    childSize = new Size(cellWidth, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 14)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithFifteen(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) < 1)
                    childSize = new Size(cellWidth, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 15)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithSixteen(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Size childSize = this.Children.IndexOf(child) < 16 ?
                    new Size(cellWidth, cellHeight) : Size.Empty;

                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithSpecialOne(Size availableSize)
        {
            foreach (UIElement child in this.Children)
            {
                if (child.Visibility == Visibility.Visible)
                {
                    Size childSize = new Size(availableSize.Width, availableSize.Height);
                    if (childSize != Size.Empty)
                        child.Measure(childSize);
                    return;
                }
            }
        }

        private void SetMeasureWithSpecialSeven(Size availableSize)
        {
            Size largeChild = new Size(availableSize.Width / 2, availableSize.Height / 2);
            Size smallChild = new Size(availableSize.Width / 4, availableSize.Height / 4);

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) == 0)
                {
                    child.Measure(largeChild);
                }
                else if (this.Children.IndexOf(child) < 5)
                {
                    child.Measure(smallChild);
                }
                else if (this.Children.IndexOf(child) < 7)
                {
                    child.Measure(largeChild);
                }
                else
                    child.Measure(Size.Empty);
            }
        }

        private void SetMeasureWithSpecialFive(Size availableSize)
        {
            double cellWidth = availableSize.Width / 3;
            double cellHeight = availableSize.Height / 3;

            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) == 0)
                    childSize = new Size(cellWidth * 2, cellHeight * 2);
                else if (this.Children.IndexOf(child) == 3)
                    childSize = new Size(cellWidth * 2, cellHeight);
                else if (this.Children.IndexOf(child) < 5)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;
                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithSpecialTen(Size availableSize)
        {
            double cellWidth = availableSize.Width / 4;
            double cellHeight = availableSize.Height / 4;
            foreach (UIElement child in this.Children)
            {
                Size childSize;
                if (this.Children.IndexOf(child) < 2)
                    childSize = new Size(cellWidth * 2, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 10)
                    childSize = new Size(cellWidth, cellHeight);
                else
                    childSize = Size.Empty;
                if (childSize != Size.Empty)
                    child.Measure(childSize);
            }
        }

        private void SetMeasureWithRegular(Size availableSize)
        {
            Size childSize = Size.Empty;
            double cellWidth = availableSize.Width / Columns;
            double cellHeight = availableSize.Height / Rows;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < Columns * Rows)
                {
                    childSize = new Size(cellWidth, cellHeight);
                }
                child.Measure(childSize);
            }
        }

        #endregion

        #region Arrange

        protected override Size ArrangeOverride(Size finalSize)
        {
            switch (LayoutType)
            {
                case ViewLayoutTypes.None:
                    return finalSize;
                case ViewLayoutTypes.More:
                    SetArrangeWithRegular(finalSize);
                    break;
                default:
                    {
                        string methodName = "SetArrangeWith" + LayoutType.ToString();
                        const BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
                        MethodInfo info = this.GetType().GetMethod(methodName, flags);
                        if (info != null)
                            info.Invoke(this, new object[] { finalSize });
                    }
                    break;
            }
            return finalSize;
        }

        private void SetArrangeWithOne(Size finalSize)
        {
            foreach (UIElement child in this.Children)
            {
                Rect childBounds = this.Children.IndexOf(child) < 1 ?
                    new Rect(0, 0, finalSize.Width, finalSize.Height) : new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithTwo(Size finalSize)
        {
            double cellWidth = finalSize.Width / 2;

            foreach (UIElement child in this.Children)
            {
                Rect childBounds = this.Children.IndexOf(child) < 2 ?
                    new Rect(cellWidth * this.Children.IndexOf(child), 0, cellWidth, finalSize.Height) :
                    new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithThree(Size finalSize)
        {
            double cellWidth = finalSize.Width / 2;
            double cellHeight = finalSize.Height / 2;

            foreach (UIElement child in this.Children)
            {
                Rect childBounds;
                if (this.Children.IndexOf(child) < 2)
                    childBounds = new Rect(cellWidth * this.Children.IndexOf(child), 0, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 3)
                    childBounds = new Rect(0, cellHeight, cellWidth * 2, cellHeight);
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithFour(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 2;
            double cellHeight = finalSize.Height / 2;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < 4)
                {
                    if (this.Children.IndexOf(child) == 0)
                        childBounds = new Rect(0, 0, cellWidth, cellHeight);
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithFive(Size finalSize)
        {
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Rect childBounds;
                if (this.Children.IndexOf(child) == 0)
                    childBounds = new Rect(0, 0, cellWidth * 3, cellHeight * 4);
                else if (this.Children.IndexOf(child) < 5)
                    childBounds = new Rect(cellWidth * 3, (this.Children.IndexOf(child) - 1) * cellHeight, cellWidth, cellHeight);
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithSix(Size finalSize)
        {
            double cellWidth = finalSize.Width / 3;
            double cellHeight = finalSize.Height / 3;

            foreach (UIElement child in this.Children)
            {
                Rect childBounds;
                if (this.Children.IndexOf(child) == 0)
                    childBounds = new Rect(0, 0, cellWidth * 2, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 3)
                    childBounds = new Rect(cellWidth * 2, (this.Children.IndexOf(child) - 1) * cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 6)
                    childBounds = new Rect((this.Children.IndexOf(child) - 3) * cellWidth, cellHeight * 2, cellWidth, cellHeight);
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithSeven(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < 3)
                {
                    if (this.Children.IndexOf(child) == 0)
                        childBounds = new Rect(0, 0, cellWidth * 2, cellHeight * 2);
                    else
                        childBounds.X += cellWidth * 2;
                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else if (this.Children.IndexOf(child) < 7)
                {
                    if (this.Children.IndexOf(child) == 3)
                    {
                        childBounds = new Rect(cellWidth * 2, cellHeight * 2, cellWidth, cellHeight);
                    }
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = cellWidth * 2;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithEight(Size finalSize)
        {
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Rect childBounds;
                if (this.Children.IndexOf(child) == 0)
                    childBounds = new Rect(0, 0, cellWidth * 3, cellHeight * 3);
                else if (this.Children.IndexOf(child) < 4)
                    childBounds = new Rect(cellWidth * 3, (this.Children.IndexOf(child) - 1) * cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 8)
                    childBounds = new Rect((this.Children.IndexOf(child) - 4) * cellWidth, cellHeight * 3, cellWidth, cellHeight);
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithNine(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 3;
            double cellHeight = finalSize.Height / 3;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < 9)
                {
                    if (this.Children.IndexOf(child) == 0)
                        childBounds = new Rect(0, 0, cellWidth, cellHeight);
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithTen(Size finalSize)
        {
            double cellWidth = finalSize.Width / 5;
            double cellHeight = finalSize.Height / 5;

            foreach (UIElement child in this.Children)
            {
                Rect childBounds;
                if (this.Children.IndexOf(child) == 0)
                    childBounds = new Rect(0, 0, cellWidth * 4, cellHeight * 4);
                else if (this.Children.IndexOf(child) < 5)
                    childBounds = new Rect(cellWidth * 4, (this.Children.IndexOf(child) - 1) * cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 10)
                    childBounds = new Rect((this.Children.IndexOf(child) - 5) * cellWidth, cellHeight * 4, cellWidth, cellHeight);
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithEleven(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) == 0)
                    childBounds = new Rect(0, 0, cellWidth * 3, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 3)
                    childBounds = new Rect(cellWidth * 3, (this.Children.IndexOf(child) - 1) * cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 11)
                {
                    if (this.Children.IndexOf(child) == 3)
                    {
                        childBounds = new Rect(0, cellHeight * 2, cellWidth, cellHeight);
                    }
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithTwelve(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < 2)
                    childBounds = new Rect(this.Children.IndexOf(child) * cellWidth * 1.5, 0, cellWidth * 1.5, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 4)
                    childBounds = new Rect(cellWidth * 3, (this.Children.IndexOf(child) - 2) * cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 12)
                {
                    if (this.Children.IndexOf(child) == 4)
                    {
                        childBounds = new Rect(0, cellHeight * 2, cellWidth, cellHeight);
                    }
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithThirteen(Size finalSize)
        {
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                Rect childBounds;
                if (this.Children.IndexOf(child) < 4)
                    childBounds = new Rect(this.Children.IndexOf(child) * cellWidth, 0, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 6)
                    childBounds = new Rect(0, (this.Children.IndexOf(child) - 3) * cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) == 6)
                    childBounds = new Rect(cellWidth, cellHeight, cellWidth * 2, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 9)
                    childBounds = new Rect(cellWidth * 3, (this.Children.IndexOf(child) - 6) * cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) < 13)
                    childBounds = new Rect((this.Children.IndexOf(child) - 9) * cellWidth, cellHeight * 3, cellWidth, cellHeight);
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithFourteen(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < 2)
                    childBounds = new Rect(this.Children.IndexOf(child) * cellWidth, 0, cellWidth, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 6)
                {
                    if (this.Children.IndexOf(child) == 2)
                    {
                        childBounds = new Rect(cellWidth * 2, 0, cellWidth, cellHeight);
                    }
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = cellWidth * 2;
                    }
                }
                else if (this.Children.IndexOf(child) < 14)
                {
                    if (this.Children.IndexOf(child) == 6)
                    {
                        childBounds = new Rect(0, cellHeight * 2, cellWidth, cellHeight);
                    }
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithFifteen(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < 1)
                    childBounds = new Rect(0, 0, cellWidth, cellHeight * 2);
                else if (this.Children.IndexOf(child) < 7)
                {
                    if (this.Children.IndexOf(child) == 1)
                    {
                        childBounds = new Rect(cellWidth, 0, cellWidth, cellHeight);
                    }
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = cellWidth;
                    }
                }
                else if (this.Children.IndexOf(child) < 15)
                {
                    if (this.Children.IndexOf(child) == 7)
                    {
                        childBounds = new Rect(0, cellHeight * 2, cellWidth, cellHeight);
                    }
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithSixteen(Size finalSize)
        {
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < 16)
                {
                    if (this.Children.IndexOf(child) == 0)
                        childBounds = new Rect(0, 0, cellWidth, cellHeight);
                    else
                        childBounds.X += cellWidth;

                    if (childBounds.X >= finalSize.Width)
                    {
                        childBounds.Y += childBounds.Height;
                        childBounds.X = 0;
                    }
                }
                else
                    childBounds = new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithSpecialOne(Size finalSize)
        {
            foreach (UIElement child in this.Children)
            {
                Rect childBounds = child.Visibility == Visibility.Visible ?
                    new Rect(0, 0, finalSize.Width, finalSize.Height) : new Rect(0, 0, 0, 0);

                if (childBounds != Rect.Empty)
                    child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithSpecialSeven(Size finalSize)
        {
            if (InternalChildren.Count < 7)
                return;

            Size largeChild = new Size(finalSize.Width / 2, finalSize.Height / 2);
            Size smallChild = new Size(finalSize.Width / 4, finalSize.Height / 4);

            Point p1 = new Point(0, 0);
            Point p2 = new Point(largeChild.Width, 0);
            Point p3 = new Point(largeChild.Width + smallChild.Width, 0);
            Point p4 = new Point(largeChild.Width, smallChild.Height);
            Point p5 = new Point(largeChild.Width + smallChild.Width, smallChild.Height);
            Point p6 = new Point(0, largeChild.Height);
            Point p7 = new Point(largeChild.Width, largeChild.Height);

            InternalChildren[0].Arrange(new Rect(p1, largeChild));
            InternalChildren[1].Arrange(new Rect(p2, smallChild));
            InternalChildren[2].Arrange(new Rect(p3, smallChild));
            InternalChildren[3].Arrange(new Rect(p4, smallChild));
            InternalChildren[4].Arrange(new Rect(p5, smallChild));
            InternalChildren[5].Arrange(new Rect(p6, largeChild));
            InternalChildren[6].Arrange(new Rect(p7, largeChild));

            for (int i = 7; i < InternalChildren.Count; i++)
            {
                InternalChildren[i].Arrange(new Rect(0, 0, 0, 0));
            }
        }

        private void SetArrangeWithSpecialFive(Size finalSize)
        {
            if (InternalChildren.Count < 5)
                return;
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 3;
            double cellHeight = finalSize.Height / 3;
            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) == 0)
                    childBounds = new Rect(0, 0, cellWidth * 2, cellHeight * 2);
                else if (this.Children.IndexOf(child) == 3)
                    childBounds = new Rect(0, cellHeight * 2, cellWidth * 2, cellHeight);
                else if (this.Children.IndexOf(child) == 1)
                    childBounds = new Rect(cellWidth * 2, 0, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) == 2)
                    childBounds = new Rect(cellWidth * 2, cellHeight, cellWidth, cellHeight);
                else if (this.Children.IndexOf(child) == 4)
                    childBounds = new Rect(cellWidth * 2, cellHeight * 2, cellWidth, cellHeight);
                child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithSpecialTen(Size finalSize)
        {
            if (InternalChildren.Count < 10)
                return;
            Rect childBounds = Rect.Empty;
            double cellWidth = finalSize.Width / 4;
            double cellHeight = finalSize.Height / 4;
            foreach (UIElement child in this.Children)
            {
                int childIndex = this.Children.IndexOf(child);
                if (this.Children.IndexOf(child) < 2)
                {
                    childBounds = new Rect(childIndex * cellWidth * 2, 0, cellWidth * 2, cellHeight * 2);
                }
                else if (this.Children.IndexOf(child) < 10)
                {
                    int px = (childIndex - 2) % 4;
                    int py = (childIndex - 2) / 4 + 2;
                    childBounds = new Rect(px * cellWidth, py * cellHeight, cellWidth, cellHeight);
                }
                child.Arrange(childBounds);
            }
        }

        private void SetArrangeWithRegular(Size finalSize)
        {
            double cellWidth = finalSize.Width / Columns;
            double cellHeight = finalSize.Height / Rows;

            foreach (UIElement child in this.Children)
            {
                if (this.Children.IndexOf(child) < Columns * Rows)
                {
                    int xCount = this.Children.IndexOf(child) % Columns;
                    int yCount = this.Children.IndexOf(child) / Columns;
                    Rect childBounds = new Rect(xCount * cellWidth, yCount * cellHeight, cellWidth, cellHeight);
                    child.Arrange(childBounds);
                }
            }
        }

        #endregion

        #region Method

        private static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes.Length > 0)
                return attributes[0].Description;
            return value.ToString();
        }

        void SetLayoutCount()
        {
            if (Enum.IsDefined(typeof(ViewLayoutTypes), LayoutType))
            {
                if (LayoutType == ViewLayoutTypes.More)
                    LayoutCount = Rows * Columns;
                else
                {
                    string count = GetEnumDescription(LayoutType);
                    LayoutCount = int.Parse(count);
                }
            }
            else
                LayoutCount = 0;
        }

        #endregion
    }
}
