// ***********************************************************************
// <copyright file="MDataBase.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.Model
// Author           : wdysunflower
// Created          : 04-24-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-24-2014
// ***********************************************************************

namespace WPFPeony.Surveil.Model
{
    /// <summary>
    /// Enum DataTypes
    /// </summary>
    public enum DataTypes
    {
        /// <summary>
        /// The root
        /// </summary>
        Root,
        /// <summary>
        /// The group
        /// </summary>
        Group,
        /// <summary>
        /// The camera
        /// </summary>
        Camera,
        /// <summary>
        /// The record
        /// </summary>
        Record
    }

    /// <summary>
    /// Class MDataBase.
    /// </summary>
    public class MDataBase
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        /// <value>The identifier.</value>
        public string ID { get; set; }

        /// <summary>
        /// 父节点ID
        /// </summary>
        /// <value>The parent identifier.</value>
        public string ParentID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        /// <value>The type of the data.</value>
        public DataTypes DataType { get; set; }
    }
}