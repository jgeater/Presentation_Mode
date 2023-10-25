using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace Presentation_Mode
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags); //import c++ Kernel API

        //set FlagsAttribute that will allow us to disable screensaver and Screen powermgnt
        [FlagsAttribute]
        enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }
        static void Main(string[] args)
        {
            SetThreadExecutionState(EXECUTION_STATE.ES_DISPLAY_REQUIRED | EXECUTION_STATE.ES_CONTINUOUS);

            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Text = "Presentation Mode";
            trayIcon.Icon = new Icon(SystemIcons.Information, 40, 40);

            ContextMenu trayMenu = new ContextMenu();

            trayMenu.MenuItems.Add("Exit Presentation Mode", item1_Click);

            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            Application.Run();
        }

        private static void item1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
