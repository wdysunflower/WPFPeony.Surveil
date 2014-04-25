// ***********************************************************************
// <copyright file="VideoWinOperator.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.ViewModel
// Author           : wdysunflower
// Created          : 04-25-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-25-2014
// ***********************************************************************

namespace WPFPeony.Surveil.ViewModel
{
    /// <summary>
    /// Class VideoWinOperator.
    /// </summary>
    public class VideoWinOperator : OperatorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VideoWinOperator"/> class.
        /// </summary>
        public VideoWinOperator()
        {
            InitialData();
        }

        /// <summary>
        /// Initials the data.
        /// </summary>
        public void InitialData()
        {
            for (int i = 0; i < 9; i++)
            {
                ObservableCol.Add(new UIBindBase());
            }
        }
    }
}