using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace AeroGlass
{
    public class Glass
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);
        [DllImport("dwmapi.dll", PreserveSig = false)]
        static extern bool DwmIsCompositionEnabled();
        public static bool ExtendFrame(Window window, Margins margins)
        {
            if (!DwmIsCompositionEnabled())
                return false;
            IntPtr hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero)
                throw new InvalidOperationException("The window must be shown before extending frame.");
            window.Background = Brushes.Transparent;
            HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;
            DwmExtendFrameIntoClientArea(hwnd, ref margins);
            return true;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins
    {
        public int Left;
        public int Right;
        public int Top;
        public int Bottom;
        public Margins(int left,int right,int top,int bottom)
        {
            Left=left;
            Right=right;
            Top=top;
            Bottom=bottom;
        }
    }
}
