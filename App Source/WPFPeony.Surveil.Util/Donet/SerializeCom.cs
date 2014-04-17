using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Xml.Serialization;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 序列化操作
    /// </summary>
    public static class SerializeCom
    {
        #region 单一类型 Xml

        #region File

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="source">序列化的对象</param>
        /// <returns>是否成功</returns>
        public static bool XmlSerialize(string filename, object source)
        {
            var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
            try
            {
                var formatter = new XmlSerializer(source.GetType());
                formatter.Serialize(stream, source);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                stream.Close();
                return false;
            }
            stream.Close();
            return true;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="type">反序列化所需的对象类型</param>
        /// <returns>对象</returns>
        public static object XmlDeSerialize(string filename, Type type)
        {
            var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            object obj;
            try
            {
                var formatter = new XmlSerializer(type);
                obj = formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                obj = null;
            }
            stream.Close();
            return obj;
        }

        #endregion

        #region Stream

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="stream">流对象</param>
        /// <param name="source">序列化的对象</param>
        /// <returns>是否成功</returns>
        public static bool XmlSerialize(ref Stream stream, object source)
        {
            try
            {
                var formatter = new XmlSerializer(source.GetType());
                formatter.Serialize(stream, source);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="stream">流对象</param>
        /// <param name="type">反序列化所需的对象类型</param>
        /// <returns>对象</returns>
        public static object XmlDeSerialize(Stream stream, Type type)
        {
            object obj;
            try
            {
                var formatter = new XmlSerializer(type);
                obj = formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                obj = null;
            }
            return obj;
        }

        #endregion

        #endregion

        #region 多种类型 Xml

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="source">序列化的对象</param>
        /// <param name="types">隐式类型</param>
        /// <returns>是否成功</returns>
        public static bool XmlSerialize(string filename, object source, Type[] types)
        {
            var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
            try
            {
                var formatter = new XmlSerializer(source.GetType(), types);
                formatter.Serialize(stream, source);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                stream.Close();
                return false;
            }
            stream.Close();
            return true;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="type">反序列化所需的对象类型</param>
        /// <param name="types">隐式类型</param>
        /// <returns>对象</returns>
        public static object XmlDeSerialize(string filename, Type type, Type[] types)
        {
            if (!File.Exists(filename))
                return null;

            var stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            object obj;
            try
            {
                var formatter = new XmlSerializer(type, types);
                obj = formatter.Deserialize(stream);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                obj = null;
            }
            stream.Close();
            return obj;
        }

        #endregion

        #region Json 字符串

        public static string JsonSerialize(object source, Type type)
        {
            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, source);
                    string str = Encoding.UTF8.GetString(stream.ToArray());
                    return str;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static object JsonDeSerialize(string source, Type type)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(source)))
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(type);
                    object obj = serializer.ReadObject(ms);
                    return obj;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion
    }
}