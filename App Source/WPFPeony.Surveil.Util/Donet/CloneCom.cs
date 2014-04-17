using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 通过二进制序列化从内存中克隆对象
    /// </summary>
    public class CloneCom
    {
        public static T DeepCopy<T>(T obj)
        {
            try
            {
                object retval;
                using (var ms = new MemoryStream())
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    retval = bf.Deserialize(ms);
                    ms.Close();
                }
                return (T) retval;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}