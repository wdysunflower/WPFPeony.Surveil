using DevExpress.Xpf.Utils.Themes;

namespace WPFPeony.Surveil.Resource
{
    public enum ThemeBrushKey
    {
        WindowBorder,
        WindowUnActiveBorder,
        WindowBack,

        ContentBack,
        ContentBorder,
        ContentOverBorder,
        ContentSelectedBorder,
        ContentInactiveBorder,
    }

    public class ThemeBrushKeyExtension : ThemeKeyExtensionBase<ThemeBrushKey>
    {
        public ThemeBrushKeyExtension()
        {
        }
    }
}