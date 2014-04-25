// ***********************************************************************
// <copyright file="DirectoryCom.cs" company="Peony">
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

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 目录操作
    /// </summary>
    public class DirectoryCom
    {
        #region GetFreeFilePath

        /// <summary>
        /// 获取指定路径下不重复的文件名
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="filename">基本文件名</param>
        /// <returns>不重复的文件名</returns>
        public static string GetFreeFilePath(string filePath, string filename)
        {
            int pictureCount = 0;

            string picturePath = string.Format("{0}\\{1}{2}", filePath, filename, pictureCount);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            while (FileNameExists(filePath, picturePath))
            {
                pictureCount++;
                picturePath = string.Format("{0}\\{1}{2}", filePath, filename, pictureCount);
            }

            return picturePath;
        }

        /// <summary>
        /// 文件名称是否存在(排除扩展名)
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="filename">文件名称</param>
        /// <returns>是否存在</returns>
        private static bool FileNameExists(string filepath, string filename)
        {
            string[] files = Directory.GetFiles(filepath);
            return files.Select(file => file.Substring(0, file.LastIndexOf('.'))).Any(filehead => filename == filehead);
        }

        #endregion

        #region CheckDirectory

        /// <summary>
        /// 获取文件夹下某一扩展名的文件
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="extension">扩展名</param>
        /// <returns>文件列表</returns>
        public static List<FileInfo> CheckDirectory(string path, string extension)
        {
            List<FileInfo> extensionPaths = new List<FileInfo>();
            try
            {
                if (!File.Exists(path))
                {
                    if (Directory.Exists(path))
                    {
                        DirectoryInfo dir = new DirectoryInfo(path);
                        FileInfo[] infos = dir.GetFiles();
                        extensionPaths.AddRange(infos.Where(info => Path.GetExtension(info.FullName) == extension));
                    }
                }
                else
                    return null;
            }
            catch (System.Exception)
            {
                return null;
            }

            return extensionPaths;
        }

        #endregion
    }
}