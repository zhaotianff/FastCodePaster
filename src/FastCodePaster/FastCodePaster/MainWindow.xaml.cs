using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsX.Shell.Util;

namespace FastCodePaster
{
    public struct POINT
    {
        public int x;
        public int y;
    };

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : TianXiaTech.BlurWindow
    {
        private NotifyIconHelper notifyIconHelper = new NotifyIconHelper();
        private bool canExit = false;

        public static readonly uint MOD_ALT = 0x0001;
        public static readonly uint MOD_CONTROL = 0x0002;
        public static readonly uint MOD_SHIFT = 0x0004;
        public static readonly uint MOD_WIN = 0x0008;

        public const int WM_INPUT = 0x00FF;
        public const int WM_HOTKEY = 0x0312;
        public const int WM_COPY = 0x0301;

        public const byte KEYEVENTF_KEYUP = 0x0002;
        public const byte VK_CONTROL = 0x11;
        public const byte C = 0x43;
        public const byte V = 0x56;

        [DllImport("User32.dll")]
        public static extern bool RegisterHotKey(IntPtr hwnd, uint hotKeyId,uint modifier, uint vkCode);

        [DllImport("User32.dll")]
        public static extern IntPtr WindowFromPoint(POINT pOINT);

        [DllImport("User32.dll")]
        public static extern int GetCursorPos(ref POINT point);

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll")]
        public static extern void keybd_event(byte bVk,byte bScan, uint dwFlags,IntPtr dwExtraInfo);


        public MainWindow()
        {
            InitializeComponent();
            this.SourceInitialized += MainWindow_SourceInitialized;
            this.tbox.Focus();
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            HwndSource hwndSource = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            hwndSource.AddHook(HwndProc);

            NotifyIconCreateData data = new NotifyIconCreateData();
            data.ClickHandler = ShowOrHiderMainWindow;
            data.ContextMenu = this.TryFindResource("NotifyIconContextMenu") as ContextMenu;
            InitContextMenuEvent(data.ContextMenu);
            data.IconRelativePath = "paste.ico";
            data.Tooltip = "FastCodePaster";
            notifyIconHelper.CreateNotifyIcon(data);
            notifyIconHelper.SetNotifyIconState(true);
        }

        private void InitContextMenuEvent(ContextMenu contextMenu)
        {
            if (contextMenu == null)
                return;

            MenuItem exitMenu = contextMenu.Items[0] as MenuItem;
            exitMenu.Click += (a, b) =>
            {
                notifyIconHelper.SetNotifyIconState(false);
                canExit = true;
                this.Close();
            };
        }

        private void ShowOrHiderMainWindow()
        {
            if (this.Visibility == Visibility.Visible)
            {
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Visibility = Visibility.Visible;
            }
        }

        private IntPtr HwndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_HOTKEY:

                    if ((int)wParam == 1)
                    {
                        ShowOrHiderMainWindow();

                        if (Clipboard.ContainsText())
                        {
                            this.tbox.Text = Clipboard.GetText();
                            this.Button_Click(null, null);
                        }
                        else
                        {
                            POINT pt = new POINT();
                            GetCursorPos(ref pt);
                            this.Left = pt.x;
                            this.Top = pt.y;
                            this.tbox.Focus();
                        }
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void BlurWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterHotKey(new WindowInteropHelper(this).Handle, 1, MOD_WIN | MOD_SHIFT, 'A');

            this.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var input = this.tbox.Text.Trim();

            var content = @"Version:0.9
StartHTML:0000000200
EndHTML:0000002074
StartFragment:0000000236
EndFragment:0000002038
SourceURL:
<html>
<body>
    <!--StartFragment--><p style=""box-sizing: inherit; outline-color: inherit; margin: 1rem 0px 0px; padding: 0px; overflow-wrap: break-word; color: rgb(22, 22, 22); font-family: &quot;Segoe UI&quot;, SegoeUI, &quot;Helvetica Neue&quot;, Helvetica, Arial, sans-serif; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; font-weight: 400; letter-spacing: normal; orphans: 2; text-align: start; text-indent: 0px; text-transform: none; widows: 2; word-spacing: 0px; -webkit-text-stroke-width: 0px; white-space: normal; background-color: rgb(255, 255, 255); text-decoration-thickness: initial; text-decoration-style: initial; text-decoration-color: initial;""><code style=""box-sizing: inherit; outline-color: inherit; font-family: SFMono-Regular, Consolas, &quot;Liberation Mono&quot;, Menlo, Courier, monospace; font-size: 13.6px; direction: ltr; background-color: var(--theme-inline-code); overflow-wrap: break-word; border-radius: 3px; padding: 0.1em 0.2em;"">@code</code></p><!--EndFragment-->
</body>
</html>";

            Clipboard.SetData("HTML Format", content.Replace("@code", input));
            ShowOrHiderMainWindow();
            this.tbox.Clear();
        }

        private void BlurWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !canExit;

            if (canExit == false)
                ShowOrHiderMainWindow();
        }
    }
}
