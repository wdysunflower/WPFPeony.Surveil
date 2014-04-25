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

using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public delegate void VideoWinChangeHandler(VideoWin sorWin, VideoWin dirWin);

    /// <summary>
    /// Class VideoWinOperator.
    /// </summary>
    public class VideoWinOperator : OperatorBase
    {
        private readonly VideoViewOperator _videoViewOper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoWinOperator"/> class.
        /// </summary>
        public VideoWinOperator(VideoViewOperator videoViewOper)
        {
            _videoViewOper = videoViewOper;
            InitialData();
        }

        /// <summary>
        /// Initials the data.
        /// </summary>
        public void InitialData()
        {
            for (int i = 0; i < 9; i++)
            {
                VideoWin videoWin = new VideoWin
                {
                    ControlName = "窗格" + i,
                    DoubleClickCmd = new DelegateCommand<VideoWin>(_videoViewOper.ExVideoWinDoubleClickCmd),

                };
                videoWin.VideoWinChangeEvent += OnVideoWinChangeEvent;
                ObservableCol.Add(videoWin);
            }
        }

        void OnVideoWinChangeEvent(VideoWin sorWin, VideoWin dirWin)
        {
            int oldIndex = ObservableCol.IndexOf(sorWin);
            int newIndex = ObservableCol.IndexOf(dirWin);
            ObservableCol.Move(oldIndex, newIndex);

            if (newIndex > oldIndex)
                ObservableCol.Move(newIndex - 1, oldIndex);
            else
                ObservableCol.Move(newIndex + 1, oldIndex);
        }
    }
}