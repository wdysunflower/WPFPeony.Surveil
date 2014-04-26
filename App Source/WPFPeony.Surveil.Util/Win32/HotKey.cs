// ***********************************************************************
// <copyright file="HotKey.cs" company="Peony">
//     Copyright (c) Peony. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
// Assembly         : WPFPeony.Surveil.Util
// Author           : wdysunflower
// Created          : 04-25-2014
//
// Last Modified By : wdysunflower
// Last Modified On : 04-25-2014
// ***********************************************************************

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace WPFPeony.Surveil.Util
{
    /// <summary>
    /// 直接构造类实例即可注册
    /// 自动完成注销
    /// 注意注册时会抛出异常
    /// 注册系统热键类
    /// 热键会随着程序结束自动解除,不会写入注册表
    /// </summary>
    public class HotKey
    {
        #region Member

        /// <summary>
        /// The _key identifier
        /// </summary>
        private readonly int _keyId;

        /// <summary>
        /// The _handle
        /// </summary>
        private readonly IntPtr _handle; //窗体句柄

        /// <summary>
        /// The _window
        /// </summary>
        private readonly Window _window; //热键所在窗体

        /// <summary>
        /// Delegate OnHotKeyEventHandler
        /// </summary>
        public delegate void OnHotKeyEventHandler(); //热键事件委托

        /// <summary>
        /// Occurs when [on hot key].
        /// </summary>
        public event OnHotKeyEventHandler OnHotKey = null; //热键事件

        /// <summary>
        /// The key pair
        /// </summary>
        private static readonly Hashtable KeyPair = new Hashtable(); //热键哈希表

        /// <summary>
        /// The w m_ hot key
        /// </summary>
        private const int WM_HOTKEY = 0x0312; // 热键消息编号

        /// <summary>
        /// Enum KeyFlags
        /// </summary>
        public enum KeyFlags //控制键编码
        {
            /// <summary>
            /// The mo d_ none
            /// </summary>
            MOD_NONE = 0x0,

            /// <summary>
            /// The mo d_ alt
            /// </summary>
            MOD_ALT = 0x1,

            /// <summary>
            /// The mo d_ control
            /// </summary>
            MOD_CONTROL = 0x2,

            /// <summary>
            /// The mo d_ shift
            /// </summary>
            MOD_SHIFT = 0x4,

            /// <summary>
            /// The mo d_ win
            /// </summary>
            MOD_WIN = 0x8
        }

        /// <summary>
        /// Registers the hot key.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="controlKey">The control key.</param>
        /// <param name="virtualKey">The virtual key.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint controlKey, uint virtualKey);

        /// <summary>
        /// Unregisters the hot key.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        [DllImport("user32")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="win">注册窗体</param>
        /// <param name="control">控制键</param>
        /// <param name="key">主键</param>
        /// <exception cref="System.Exception">
        /// 热键已经被注册!
        /// or
        /// 热键注册失败!
        /// or
        /// 消息挂钩连接失败!
        /// </exception>
        public HotKey(Window win, KeyFlags control, Keys key)
        {
            _handle = new WindowInteropHelper(win).Handle;
            _window = win;
            uint controlKey = (uint) control;
            uint key1 = (uint) key;
            _keyId = (int) controlKey + (int) key1*10;

            if (KeyPair.ContainsKey(_keyId))
            {
                throw new Exception("热键已经被注册!");
            }

            //注册热键
            if (false == RegisterHotKey(_handle, _keyId, controlKey, key1))
            {
                throw new Exception("热键注册失败!");
            }

            //消息挂钩只能连接一次!!
            if (KeyPair.Count == 0)
            {
                if (false == InstallHotKeyHook(this))
                {
                    throw new Exception("消息挂钩连接失败!");
                }
            }

            //添加这个热键索引
            KeyPair.Add(_keyId, this);
        }

        //析构函数,解除热键
        /// <summary>
        /// Finalizes an instance of the <see cref="HotKey"/> class.
        /// </summary>
        ~HotKey()
        {
            UnregisterHotKey(_handle, _keyId);
        }

        #region core

        //安装热键处理挂钩
        /// <summary>
        /// Installs the hot key hook.
        /// </summary>
        /// <param name="hk">The hk.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool InstallHotKeyHook(HotKey hk)
        {
            if (hk._window == null || hk._handle == IntPtr.Zero)
            {
                return false;
            }

            //获得消息源
            var source = HwndSource.FromHwnd(hk._handle);
            if (source == null)
            {
                return false;
            }

            //挂接事件
            source.AddHook(HotKeyHook);
            return true;
        }

        //热键处理过程
        /// <summary>
        /// Hots the key hook.
        /// </summary>
        /// <param name="hwnd">The HWND.</param>
        /// <param name="msg">The MSG.</param>
        /// <param name="wParam">The w parameter.</param>
        /// <param name="lParam">The l parameter.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        /// <returns>IntPtr.</returns>
        private static IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                HotKey hk = (HotKey) KeyPair[(int) wParam];
                if (hk.OnHotKey != null)
                {
                    hk.OnHotKey();
                }
            }
            return IntPtr.Zero;
        }

        #endregion
    }
}