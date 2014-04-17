using System.Windows;
using System.Windows.Media;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 视觉树辅助类
    /// </summary>
    public static class VisualItemHelper
    {
        /// <summary>
        /// 根据控件类型及名称，获取视觉树上的子控件
        /// </summary>
        /// <typeparam name="T">子控件类型</typeparam>
        /// <param name="obj">父控件</param>
        /// <returns>符合条件的子控件</returns>
        public static T FindFirstVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return (T)child;

                var childofChild = FindFirstVisualChild<T>(child);
                if (childofChild != null)
                    return childofChild;
            }
            return null;
        }

        /// <summary>
        /// 根据控件类型及名称，获取视觉树上的子控件
        /// </summary>
        /// <typeparam name="T">子控件类型</typeparam>
        /// <param name="obj">父控件</param>
        /// <param name="name">子控件名称</param>
        /// <returns>符合条件的子控件</returns>
        public static T FindFirstVisualChild<T>(DependencyObject obj, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T && child.GetValue(FrameworkElement.NameProperty).ToString() == name)
                    return (T)child;

                var childofChild = FindFirstVisualChild<T>(child, name);
                if (childofChild != null)
                    return childofChild;
            }
            return null;
        }

        /// <summary>
        /// 根据控件类型，获取视觉树上的父控件
        /// </summary>
        /// <typeparam name="T">父控件类型</typeparam>
        /// <param name="obj">子控件</param>
        /// <returns>符合条件的父控件</returns>
        public static T GetParentElement<T>(DependencyObject obj) where T : DependencyObject
        {
            DependencyObject contentControl = obj;
            T itemsControl = null;
            while (itemsControl == null && contentControl != null)
            {
                itemsControl = VisualTreeHelper.GetParent(contentControl) as T;
                contentControl = VisualTreeHelper.GetParent(contentControl);
            }
            return itemsControl;
        }

        /// <summary>
        /// 根据控件类型及名称，获取视觉树上的父控件
        /// </summary>
        /// <typeparam name="T">父控件类型</typeparam>
        /// <param name="obj">子控件</param>
        /// <param name="name">父控件名称</param>
        /// <returns>符合条件的父控件</returns>
        public static T GetParentElement<T>(DependencyObject obj, string name) where T : DependencyObject
        {
            DependencyObject contentControl = obj;
            T itemsControl = null;
            string controlName = "";
            while (itemsControl == null && contentControl != null && controlName != name)
            {
                itemsControl = VisualTreeHelper.GetParent(contentControl) as T;
                if (itemsControl != null)
                {
                    controlName = itemsControl.GetType().GetProperty("Name").GetValue(itemsControl, null) as string;
                    if (controlName != name)
                        itemsControl = null;
                }
                contentControl = VisualTreeHelper.GetParent(contentControl);
            }
            return itemsControl;
        }

        /// <summary>
        /// 根据控件类型，获取视觉树上的父控件
        /// </summary>
        /// <param name="obj">子控件</param>
        /// <param name="parent"> </param>
        /// <returns>符合条件的父控件</returns>
        public static bool EqualParentElement(DependencyObject obj, DependencyObject parent)
        {
            DependencyObject parentItem = obj;
            while (parentItem != null)
            {
                if (Equals(parent, parentItem))
                    return true;
                parentItem = VisualTreeHelper.GetParent(parentItem);
            }
            return false;
        }
    }
}