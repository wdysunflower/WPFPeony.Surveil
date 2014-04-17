using System;
using System.Windows;
using System.Windows.Media;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 资源字典操作
    /// </summary>
    public static class ResourceCom
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static object GetObject(string key)
        {
            try
            {
                var obj = Application.Current.Resources[key];
                return obj ?? key;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取String类型资源
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static string GetString(string key)
        {
            return GetObject(key) as String;
        }

        /// <summary>
        /// 获取Brush资源
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static Brush GetBrush(string key)
        {
            return GetObject(key) as Brush;
        }

        /// <summary>
        /// 获取ImageSource类型资源
        /// </summary>
        /// <param name="key">Key值</param>
        /// <returns>Value值</returns>
        public static ImageSource GetImageSource(string key)
        {
            return GetObject(key) as ImageSource;
        }
    }
}