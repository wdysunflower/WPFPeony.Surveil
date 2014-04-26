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
using WPFPeony.Surveil.Model;

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class CameraOperator.
    /// </summary>
    public class CameraOperator : OperatorBase
    {
        #region Member

        /// <summary>
        /// The _data assist
        /// </summary>
        private MDataAssist _dataAssist;

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
        private Dictionary<string, DataBase> _cameraDic;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraOperator"/> class.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public CameraOperator(DataUIModes mode)
        {
            _dataUIMode = mode;
            InitialData();
        }

        #region Binding Property

        /// <summary>
        /// The _list modes
        /// </summary>
        private List<UIBindBase> _listModes;

        /// <summary>
        /// Gets the list modes.
        /// </summary>
        /// <value>The list modes.</value>
        public List<UIBindBase> ListModes
        {
            get { return _listModes; }
        }

        /// <summary>
        /// The _list mode
        /// </summary>
        private UIBindBase _listMode;

        /// <summary>
        /// Gets or sets the list mode.
        /// </summary>
        /// <value>The list mode.</value>
        public UIBindBase ListMode
        {
            get { return _listMode; }
            set { SetProperty(ref _listMode, value, () => ListMode); }
        }

        private List<DataBase> _cameraList;
        /// <summary>
        /// Gets the camera list.
        /// </summary>
        /// <value>The camera list.</value>
        public List<DataBase> CameraList
        {
            get { return _cameraList; }
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

        public void InitialData()
        {
            _listModes = new List<UIBindBase>();
            UIBindBase dataBase1 = new UIBindBase { ControlViewKey = "Pine-Tree", RelationData = DataUIModes.TreeView };
            UIBindBase dataBase2 = new UIBindBase
            {
                ControlViewKey = "Table-of-Contents",
                RelationData = DataUIModes.List
            };
            _listModes.Add(dataBase1);
            _listModes.Add(dataBase2);
            _listMode = dataBase1;
            
            _dataAssist = new MDataAssist();
            _cameraDic = new Dictionary<string, DataBase>();
            _cameraList = new List<DataBase>();

            ListEntities();
        }

        /// <summary>
        /// Lists the entities.
        /// </summary>
        public void ListEntities()
        {
            switch (_dataUIMode)
            {
                case DataUIModes.TreeView:
                    ObservableCol = CreateCameraColWithTree(_dataAssist.DataDic.Values);
                    break;
                case DataUIModes.TreeListView:
                    ObservableCol = CreateCameraColWithTreeList(_dataAssist.DataDic.Values);
                    break;
            }

            var col = new ObservableCollection<UIBindBase> { ObservableCol[0] };
            _selectedCol = col;
        }

        /// <summary>
        /// Creates the camera col with tree.
        /// </summary>
        /// <param name="dataBases">The entities.</param>
        /// <returns>ObservableCollection&lt;DataBase&gt;.</returns>
        public ObservableCollection<UIBindBase> CreateCameraColWithTree(IEnumerable<MDataBase> dataBases)
        {
            _cameraDic.Clear();

            var cameraCol = new ObservableCollection<UIBindBase>();
            foreach (MDataBase data in dataBases)
            {
                var vmData = new DataBase(data) { IsExpanded = true };
                if (string.IsNullOrEmpty(data.ParentID) || data.ParentID == "0")
                    cameraCol.Add(vmData);
                else
                {
                    DataBase parentData;
                    if (_cameraDic.TryGetValue(data.ParentID, out parentData))
                        parentData.ObservableCol.Add(vmData);
                }

                _cameraDic.Add(data.ID, vmData);
                if (data.DataType == DataTypes.Camera)
                    _cameraList.Add(vmData);
            }

            return cameraCol;
        }

        /// <summary>
        /// Creates the camera col with tree list.
        /// </summary>
        /// <param name="dataBases">The entities.</param>
        /// <returns>ObservableCollection&lt;DataBase&gt;.</returns>
        public ObservableCollection<UIBindBase> CreateCameraColWithTreeList(IEnumerable<MDataBase> dataBases)
        {
            _cameraDic.Clear();

            var cameraCol = new ObservableCollection<UIBindBase>();
            foreach (MDataBase data in dataBases)
            {
                var vmData = new DataBase(data) { IsExpanded = true };
                cameraCol.Add(vmData);
                _cameraDic.Add(data.ID, vmData);
            }

            return cameraCol;
        }
    }
}