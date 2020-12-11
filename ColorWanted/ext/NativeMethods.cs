﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using ColorWanted.hotkey;

/**
* winapi与头文件参考：
* 
* user32.dll   WinUser.h
* gdi32.dll    WinGDI.h
* kernel32.dll WinBase.h
*/

namespace ColorWanted.ext
{
    /// <summary>
    /// Win API 接口
    /// </summary>
    internal class NativeMethods
    {
        #region 屏幕取色

        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hDC, int XPos, int YPos);
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateDC(string driverName, string deviceName, string output, IntPtr lpinitData);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr DC);

        #endregion

        #region 全局快捷键

        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）           
            KeyModifier fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、等键时才会生效
            System.Windows.Forms.Keys vk                     //定义热键的内容
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );
        #endregion

        #region 在窗体任意位置拖动窗体

        // 这几个常量定义在 winuser.h 中
        public const uint WM_SYSCOMMAND = 0x0112;
        public const uint SC_MOVE = 0xF010;
        public const uint HTCAPTION = 0x0002;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hwnd, uint wMsg, IntPtr wParam, IntPtr lParam);
        #endregion

        #region INI 文件读写
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString", CharSet = CharSet.Unicode)]
        public static extern bool WriteIni(string section, string key, string val, string filePath);

        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString", CharSet = CharSet.Unicode)]
        public static extern bool ReadIni(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        #endregion

        #region 剪贴板操作

        [DllImport("user32.dll")]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);
        [DllImport("user32.dll")]
        public static extern bool EmptyClipboard();
        [DllImport("user32.dll")]
        public static extern IntPtr SetClipboardData(uint uFormat, IntPtr hMem);
        [DllImport("user32.dll")]
        public static extern bool CloseClipboard();

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalAlloc(uint uFlags, int dwBytes);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);
        public const int WM_CLIPBOARDUPDATE = 0x031D;

        [DllImport("user32.dll")]
        public static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        #endregion

        #region 窗体阴影
        public const int CS_DropSHADOW = 0x20000;
        public const int GCL_STYLE = -26;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);
        #endregion

        #region 鼠标光标
        public const int WH_MOUSE_LL = 14;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_MBUTTONUP = 0x208;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_MBUTTONDBLCLK = 0x209;

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        //安装钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        //卸载钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        //调用下一个钩子
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        //获取鼠标所在位置
        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern bool GetCursorPos(ref System.Drawing.Point lpPoint);
        #endregion

        #region GDI
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr o);
        #endregion

        #region 屏幕缩放
        [DllImport("user32")]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32")]
        public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        public const int HORZRES = 8;
        public const int VERTRES = 10;
        public const int DESKTOPVERTRES = 117;
        public const int DESKTOPHORZRES = 118;
        #endregion

        #region 获取窗口
        public struct RECT
        {
            public long Left;
            public long Top;
            public long Right;
            public long Bottom;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr handle, out RECT rect);

        public static System.Drawing.Rectangle GetWindowBounds(IntPtr handle)
        {
            GetWindowRect(handle, out RECT rect);

            return new System.Drawing.Rectangle((int)rect.Left, (int)rect.Top, (int)(rect.Right - rect.Left), (int)(rect.Bottom - rect.Top));
        }
        #endregion
        #region 获取键状态
        /// <summary>
        /// 参考 https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        /// </summary>
        /// <param name="nVirtKey"></param>
        /// <returns>
        /// The return value specifies the status of the specified virtual key, as follows:
        /// If the high-order bit is 1, the key is down; otherwise, it is up.
        /// If the low-order bit is 1, the key is toggled.A key, such as the CAPS LOCK key, 
        /// is toggled if it is turned on.The key is off and untoggled if the low-order bit is 0. 
        /// A toggle key's indicator light (if any) on the keyboard will be on when the key is toggled, 
        /// and off when the key is untoggled.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);
        #endregion
    }
}
