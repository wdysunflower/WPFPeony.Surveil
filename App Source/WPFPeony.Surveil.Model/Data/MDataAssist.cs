// ***********************************************************************
// <copyright file="MDataAssist.cs" company="Peony">
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

using System.Collections.Generic;

namespace WPFPeony.Surveil.Model
{
    /// <summary>
    /// Class MDataAssist.
    /// </summary>
    public class MDataAssist
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MDataAssist" /> class.
        /// </summary>
        public MDataAssist()
        {
            DataDic = new Dictionary<string, MDataBase>();
            ListEntities();
        }

        /// <summary>
        /// Gets or sets the data dic.
        /// </summary>
        /// <value>The data dic.</value>
        public Dictionary<string, MDataBase> DataDic { get; set; }

        /// <summary>
        /// Lists the entities.
        /// </summary>
        public void ListEntities()
        {
            int count = 1;
            var server = new MDataBase {Name = "服务器", ID = count.ToString(), ParentID = "0"};
            DataDic.Add(server.ID, server);
            count++;
            for (int i = 1; i < 5; i++)
            {
                var group = new MDataBase
                {
                    Name = "分组" + i,
                    ID = count.ToString(),
                    ParentID = server.ID
                };
                DataDic.Add(group.ID, group);
                count++;

                var camera = new MCamera
                {
                    Name = "Camera" + i,
                    ID = count.ToString(),
                    ParentID = group.ID
                };
                DataDic.Add(camera.ID, camera);
                count++;
            }
        }
    }
}