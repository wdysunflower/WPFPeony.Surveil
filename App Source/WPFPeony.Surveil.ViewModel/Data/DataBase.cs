﻿// ***********************************************************************
// <copyright file="ADataBase.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-24-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-24-2014
// ***********************************************************************

using System.Collections.ObjectModel;
using WPFPeony.Surveil.Model;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class ADataBase.
    /// </summary>
    public class DataBase : UIDataBase
    {
        /// <summary>
        /// The _data base
        /// </summary>
        private readonly MDataBase _dataBase;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBase" /> class.
        /// </summary>
        /// <param name="dataBase">The data base.</param>
        public DataBase(MDataBase dataBase)
        {
            _dataBase = dataBase;
            ControlName = dataBase.Name;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string ID
        {
            get { return _dataBase.ID; }
        }

        /// <summary>
        /// Gets the parent identifier.
        /// </summary>
        /// <value>The parent identifier.</value>
        public string ParentID
        {
            get { return _dataBase.ParentID; }
        }

        /// <summary>
        /// The _is expanded
        /// </summary>
        public bool _isExpanded;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is expanded.
        /// </summary>
        /// <value><c>true</c> if this instance is expanded; otherwise, <c>false</c>.</value>
        public bool IsExpanded
        {
            get { return _isExpanded; }
            set { SetProperty(ref _isExpanded, value, () => IsExpanded); }
        }

        /// <summary>
        /// The _child col
        /// </summary>
        private ObservableCollection<DataBase> _childCol;

        /// <summary>
        /// Gets the child col.
        /// </summary>
        /// <value>The child col.</value>
        public ObservableCollection<DataBase> ChildCol
        {
            get { return _childCol ?? (_childCol = new ObservableCollection<DataBase>()); }
        }
    }
}