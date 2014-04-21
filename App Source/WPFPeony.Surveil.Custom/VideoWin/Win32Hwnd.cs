using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using WPFPeony.Surveil.Util;

namespace WPFPeony.Surveil.Custom
{
    /// <summary>
    /// 带句柄的控件
    /// </summary>
    public class Win32Hwnd : HwndHost
    {
        #region Dependency Property

        #region WinHwnd

        public static readonly DependencyProperty WinHwndProperty =
            DependencyProperty.Register(
                "WinHwnd",
                typeof (IntPtr),
                typeof (Win32Hwnd),
                new PropertyMetadata(new IntPtr(-1)));

        public IntPtr WinHwnd
        {
            get { return (IntPtr) GetValue(WinHwndProperty); }
            set { SetValue(WinHwndProperty, value); }
        }

        #endregion

        #endregion

        #region Override

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            uint hwnd = Win32API.CreateWindowEx(0,
                "static",
                string.Empty,
                (int) WindowStyles.WS_CHILD,
                0,
                0,
                (int) ActualWidth,
                (int) ActualHeight,
                (uint) hwndParent.Handle,
                0,
                0,
                0);

            Win32API.SetWindowPos(hwnd, WindowPos.HWND_Top, 0, 0, 0, 0, PosFlags.SWP_NOMOVE);

            IntPtr handle = (IntPtr) hwnd;
            if (!Equals(WinHwnd, handle))
                WinHwnd = handle;

            return new HandleRef(this, handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            if (WinHwnd != IntPtr.Zero)
            {
                Win32API.DestroyWindow((uint) WinHwnd);
                WinHwnd = IntPtr.Zero;
            }
        }

        #endregion
    }
}