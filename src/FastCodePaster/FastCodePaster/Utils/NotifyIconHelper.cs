﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsX.Shell.Util
{
    public class NotifyIconCreateData
    {
        public string Tooltip { get; set; }
        public string IconRelativePath { get; set; }
        public System.Windows.Controls.ContextMenu ContextMenu { get; set; }
        public Action ClickHandler { get; set; }
    }

    public class NotifyIconHelper
    {
        private System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        private System.Windows.Controls.ContextMenu contextMenu;
        private Action clickHander;

        public void SetNotifyIconState(bool isShow)
        {
            notifyIcon.Visible = isShow;
        }

        public void ShowBallonTipText(string tipTitle,string tipText,int timeMillonSeconds)
        {
            if (notifyIcon == null)
                return;

            notifyIcon.BalloonTipTitle = tipTitle;
            notifyIcon.BalloonTipText = tipText;
            notifyIcon.ShowBalloonTip(timeMillonSeconds);
        }

        public void CreateNotifyIcon(NotifyIconCreateData data)
        {
            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.Text = data.Tooltip;
            var stream = System.Windows.Application.GetResourceStream(new Uri($"pack://application:,,,/{data.IconRelativePath}"));
            notifyIcon.Icon = new System.Drawing.Icon(stream.Stream);
            stream.Stream.Close();
            notifyIcon.Visible = false;
            notifyIcon.MouseClick += NotifyIcon_MouseClick;

            clickHander = data.ClickHandler;

            contextMenu = data.ContextMenu;
        }

        public void RemoveNotifyIcon()
        {
            notifyIcon?.Dispose();
        }

        private void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if(e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if(contextMenu != null)
                {
                    contextMenu.IsOpen = true;
                    FastCodePaster.App.Current.MainWindow.Activate();
                }
            }

            if(e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                clickHander?.Invoke();
            }
        }
    }
}
