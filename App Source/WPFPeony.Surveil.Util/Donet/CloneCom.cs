// ***********************************************************************
// <copyright file="CloneCom.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.Util
// Author           : wdysunflower
// Created          : 04-17-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-17-2014
// ***********************************************************************

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
        /// <summary>
        /// Deeps the copy.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>T.</returns>
        public static T DeepCopy<T>(T obj)
        {
            try
            {
                object returnValue;
                using (var ms = new MemoryStream())
                {
                    var bf = new BinaryFormatter();
                    bf.Serialize(ms, obj);
                    ms.Seek(0, SeekOrigin.Begin);
                    returnValue = bf.Deserialize(ms);
                    ms.Close();
                }
                return (T) returnValue;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}