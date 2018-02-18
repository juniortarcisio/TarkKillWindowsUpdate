using System;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace TarkKillUpdate
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            SetStartup();
            timer1.Start();
            KeepAlive();
            notifyIcon1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void KeepAlive()
        {
            try
            {
                var sb = new System.Text.StringBuilder();

                sb.Append("/k ");
                sb.Append("sc config wuauserv start= disabled & ");
                sb.Append("sc config BITS start= disabled & ");
                //sb.Append("sc config ClickToRunSvc start= disabled & ");
                sb.Append("sc config dosvc start= disabled & ");
                sb.Append("net stop BITS & ");
                sb.Append("net stop wuauserv & ");
                sb.Append("net stop ClickToRunSvc & ");
                sb.Append("net stop dosvc & ");
                sb.Append("taskkill -f -im Windows10UpgraderApp.exe & ");
                sb.Append("taskkill -f -im BackgroundTransferHost.exe & ");
                sb.Append("taskkill -f -im AdobeARM.exe & ");
                sb.Append("taskkill -f -im HttpHelper.exe & ");
                sb.Append("taskkill -f -im UpdateAssistant.exe & ");
                sb.Append("taskkill -f -im GoogleUpdate.exe & ");
                sb.Append("taskkill -f -im OfficeClickToRun.exe & ");
                sb.Append("exit");

                Process p = new Process();
                p.StartInfo.FileName = "CMD.exe";
                p.StartInfo.Arguments = sb.ToString();
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                lblErro.Text = $"Error {ex.Message}";

                notifyIcon1.BalloonTipText = ex.Message;
                notifyIcon1.ShowBalloonTip(2000);
                txtIP.Text = "-";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            KeepAlive();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
        }

        //Startup registry key and value
        private static readonly string StartupKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        private static readonly string StartupValue = "MyApplicationName";

        private static void SetStartup()
        {
            //Set the application to run at startup
            //RegistryKey key = Registry.CurrentUser.OpenSubKey(StartupKey, true);
            //key.SetValue(StartupValue, Application.ExecutablePath.ToString());
        }

        private void btnUpdateNow_Click(object sender, EventArgs e)
        {
            KeepAlive();
        }

        private void txtIP_Click(object sender, EventArgs e)
        {
           // Clipboard.SetText(txtIP.Text);
        }

        private void txtIP_MouseDown(object sender, MouseEventArgs e)
        {
            //txtIP.BackColor = Color.Blue;
        }

        private void txtIP_MouseUp(object sender, MouseEventArgs e)
        {
            //txtIP.BackColor = Color.DeepSkyBlue;
        }
    }
}
