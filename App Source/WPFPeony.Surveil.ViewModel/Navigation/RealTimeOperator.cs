namespace WPFPeony.Surveil.ViewModel
{
    public class RealTimeOperator
    {
        private VideoViewOperator _videoViewOper;

        public VideoViewOperator VideoViewOper
        {
            get { return _videoViewOper ?? (_videoViewOper = new VideoViewOperator()); }
        }
    }
}
