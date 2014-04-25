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
        /// The _entity assist
        /// </summary>
        private readonly MDataAssist _entityAssist;

        /// <summary>
        /// Gets the entity assist.
        /// </summary>
        /// <value>The entity assist.</value>
        public MDataAssist EntityAssist
        {
            get { return _entityAssist; }
        }

        /// <summary>
        /// The _entity UI mode
        /// </summary>
        private readonly EntityUIModes _entityUIMode;

        /// <summary>
        /// The _camera dic
        /// </summary>
        private readonly Dictionary<string, DataBase> _cameraDic;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraOperator"/> class.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public CameraOperator(EntityUIModes mode)
        {
            _entityUIMode = mode;
            _entityAssist = new MDataAssist();
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
            switch (_entityUIMode)
            {
                case EntityUIModes.TreeView:
                    CameraCol = CreateEntityColWithTree(_entityAssist.DataDic.Values);
                    break;
                case EntityUIModes.TreeListView:
                    CameraCol = CreateEntityColWithTreeList(_entityAssist.DataDic.Values);
                    break;
            }

            var col = new ObservableCollection<DataBase>();
            col.Add(CameraCol[0]);
            _selectedCol = col;
        }

        /// <summary>
        /// Creates the entity col with tree.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>ObservableCollection&lt;DataBase&gt;.</returns>
        public ObservableCollection<DataBase> CreateEntityColWithTree(IEnumerable<MDataBase> entities)
        {
            _cameraDic.Clear();

            var entityCol = new ObservableCollection<DataBase>();
            foreach (MDataBase entity in entities)
            {
                var vmEntity = new DataBase(entity) {IsExpanded = true};
                if (string.IsNullOrEmpty(entity.ParentID) || entity.ParentID == "0")
                    entityCol.Add(vmEntity);
                else
                {
                    DataBase parentEntity;
                    if (_cameraDic.TryGetValue(entity.ParentID, out parentEntity))
                        parentEntity.EntityCol.Add(vmEntity);
                }

                _cameraDic.Add(entity.ID, vmEntity);
            }

            return entityCol;
        }

        /// <summary>
        /// Creates the entity col with tree list.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>ObservableCollection&lt;DataBase&gt;.</returns>
        public ObservableCollection<DataBase> CreateEntityColWithTreeList(IEnumerable<MDataBase> entities)
        {
            _cameraDic.Clear();

            var entityCol = new ObservableCollection<DataBase>();
            foreach (MDataBase entity in entities)
            {
                var vmEntity = new DataBase(entity) {IsExpanded = true};
                entityCol.Add(vmEntity);
                _cameraDic.Add(entity.ID, vmEntity);
            }

            return entityCol;
        }
    }
}