using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace WPFPeony.Surveil.Util
{
    public class PageTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate template;
            if (item == null)
            {
                FrameworkElementFactory elementFactory= new FrameworkElementFactory();
                elementFactory.Text = "rtr";
                template = new DataTemplate(typeof(TextBox));
            }
            else
            {
                Type vmType = item.GetType();
                string pageViewStr = vmType.Name + "View";
                Type viewType = Assembly.GetEntryAssembly().GetType("CAPE2.View." + pageViewStr);

                FrameworkElementFactory elementFactory = new FrameworkElementFactory(viewType);
                template = new DataTemplate(vmType);
                template.VisualTree = elementFactory;
            }
            return template;
        }
    }
}