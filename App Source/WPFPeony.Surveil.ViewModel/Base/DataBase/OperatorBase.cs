// ***********************************************************************
// <copyright file="OperatorBase.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-25-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-25-2014
// ***********************************************************************

using System.Collections.ObjectModel;
using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class OperatorBase.
    /// </summary>
    public class OperatorBase : BindableBase
    {
        /// <summary>
        /// The _observable col
        /// </summary>
        private ObservableCollection<UIBindBase> _observableCol;

        /// <summary>
        /// Gets the observable col.
        /// </summary>
        /// <value>The observable col.</value>
        public ObservableCollection<UIBindBase> ObservableCol
        {
            get { return _observableCol ?? (_observableCol = new ObservableCollection<UIBindBase>()); }
            set { SetProperty(ref _observableCol, value, () => ObservableCol); }
        }
    }
}