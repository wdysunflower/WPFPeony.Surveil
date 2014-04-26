// ***********************************************************************
// <copyright file="MRecord.cs" company="Peony">
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
    ///     Class MRecord.
    /// </summary>
    public class MRecord : MDataBase
    {
        public MRecord()
        {
            DataType = DataTypes.Record;
        }

        /// <summary>
        /// Gets or sets the filename.
        /// </summary>
        /// <value>The filename.</value>
        public string Filename { get; set; }
    }
}