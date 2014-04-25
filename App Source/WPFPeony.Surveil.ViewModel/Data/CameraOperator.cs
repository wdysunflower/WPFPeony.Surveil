// ***********************************************************************
// <copyright file="CameraOperator.cs" company="Peony">
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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Xpf.Mvvm;
using WPFPeony.Surveil.Model;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class CameraOperator.
    /// </summary>
    public class CameraOperator : ViewModelBase
    {
        #region Member

        /// <summary>
        /// The _data assist
        /// </summary>
        private readonly MDataAssist _dataAssist;

        /// <summary>
        /// Gets the data assist.
        /// </summary>
        /// <value>The data assist.</value>
        public MDataAssist DataAssist
        {
            get { return _dataAssist; }
        }

        /// <summary>
        /// The _data UI mode
        /// </summary>
        private readonly DataUIModes _dataUIMode;

        /// <summary>
        /// The _camera dic
        /// </summary>
        private readonly Dictionary<string, DataBase> _cameraDic;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraOperator"/> class.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public CameraOperator(DataUIModes mode)
        {
            _dataUIMode = mode;
            _dataAssist = new MDataAssist();
            _cameraDic = new Dictionary<string, DataBase>();
            ListEntities();
        }

        #region Binding Property

        /// <summary>
        /// The _camera col
        /// </summary>
        private ObservableCollection<DataBase> _cameraCol;

        /// <summary>
        /// Gets or sets the camera col.
        /// </summary>
        /// <value>The camera col.</value>
        public ObservableCollection<DataBase> CameraCol
        {
            get { return _cameraCol; }
            set { SetProperty(ref _cameraCol, value, () => CameraCol); }
        }

        /// <summary>
        /// The _selected col
        /// </summary>
        private object _selectedCol;

        /// <summary>
        /// Gets or sets the selected col.
        /// </summary>
        /// <value>The selected col.</value>
        public object SelectedCol
        {
            get { return _selectedCol; }
            set { SetProperty(ref _selectedCol, value, () => SelectedCol); }
        }

        #endregion

        /// <summary>
        /// Lists the entities.
        /// </summary>
        public void ListEntities()
        {
            switch (_dataUIMode)
            {
                case DataUIModes.TreeView:
                    CameraCol = CreateCameraColWithTree(_dataAssist.DataDic.Values);
                    break;
                case DataUIModes.TreeListView:
                    CameraCol = CreateCameraColWithTreeList(_dataAssist.DataDic.Values);
                    break;
            }

            var col = new ObservableCollection<DataBase>();
            col.Add(CameraCol[0]);
            _selectedCol = col;
        }

        /// <summary>
        /// Creates the camera col with tree.
        /// </summary>
        /// <param name="dataBases">The entities.</param>
        /// <returns>ObservableCollection&lt;DataBase&gt;.</returns>
        public ObservableCollection<DataBase> CreateCameraColWithTree(IEnumerable<MDataBase> dataBases)
        {
            _cameraDic.Clear();

            var cameraCol = new ObservableCollection<DataBase>();
            foreach (MDataBase data in dataBases)
            {
                var vmData = new DataBase(data) {IsExpanded = true};
                if (string.IsNullOrEmpty(data.ParentID) || data.ParentID == "0")
                    cameraCol.Add(vmData);
                else
                {
                    DataBase parentData;
                    if (_cameraDic.TryGetValue(data.ParentID, out parentData))
                        parentData.ChildCol.Add(vmData);
                }

                _cameraDic.Add(data.ID, vmData);
            }

            return cameraCol;
        }

        /// <summary>
        /// Creates the camera col with tree list.
        /// </summary>
        /// <param name="dataBases">The entities.</param>
        /// <returns>ObservableCollection&lt;DataBase&gt;.</returns>
        public ObservableCollection<DataBase> CreateCameraColWithTreeList(IEnumerable<MDataBase> dataBases)
        {
            _cameraDic.Clear();

            var cameraCol = new ObservableCollection<DataBase>();
            foreach (MDataBase data in dataBases)
            {
                var vmData = new DataBase(data) {IsExpanded = true};
                cameraCol.Add(vmData);
                _cameraDic.Add(data.ID, vmData);
            }

            return cameraCol;
        }
    }
}