﻿// ***********************************************************************
// <copyright file="UIPageBase.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-17-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-17-2014
// ***********************************************************************

using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class UIPageBase.
    /// </summary>
    public class UIPageBase : BindableBase
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        /// <value>The name of the module.</value>
        public string ModuleName { get; protected set; }

        /// <summary>
        /// Gets or sets the relative object.
        /// </summary>
        /// <value>The relative object.</value>
        public object RelativeObject { get; protected set; }
    }
}