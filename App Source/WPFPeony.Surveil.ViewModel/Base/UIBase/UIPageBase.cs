using DevExpress.Xpf.Mvvm;

namespace WPFPeony.Surveil.ViewModel
{
    public class UIPageBase : BindableBase
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; protected set; }

        public object RelativeObject { get; protected set; }
    }
}
