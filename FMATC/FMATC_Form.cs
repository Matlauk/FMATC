using FMATC.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FMATC
{
    public partial class FMATC_Form : Form
    {
        [DllImport("user32.dll")] 
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        const int MYACTION_HOTKEY_ID = 1;
        const int WM_HOTKEY = 0x0312;

        private bool Recording = false;
        public static Keys RecordHotKey = (Keys)Properties.Settings.Default.RecordHotKey;
        public static string OutputLocation = Properties.Settings.Default.OutputLocation;
        private List<Device> RecordingDevices;

        public FMATC_Form()
        {
            InitializeComponent();

            this.lstIAVDevices.MouseDoubleClick += lstIAVDevices_MouseDoubleClick;
            this.lstIACDevices.MouseDoubleClick += lstIACDevices_MouseDoubleClick;
            this.lstOAVDevices.MouseDoubleClick += lstOAVDevices_MouseDoubleClick;
            this.lstOACDevices.MouseDoubleClick += lstOACDevices_MouseDoubleClick;

            FMATC_Form.RecordHotKey = (Keys)Properties.Settings.Default.RecordHotKey;
            RegisterHotKey(this.Handle, MYACTION_HOTKEY_ID, 0, (int)RecordHotKey);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == MYACTION_HOTKEY_ID) 
            {
                ToggleRecording();
            }
            base.WndProc(ref m);
        }

        private void btn_Record_Click(object sender, EventArgs e)
        {
            ToggleRecording();
        }

        private void ToggleRecording()
        {
            this.Recording = !this.Recording;

            if (this.Recording)
            {
                this.StartRecording();
            }
            else
            {
                this.StopRecording();
            }

            this.MenuBar.Enabled = !Recording;

            if (Recording)
            {
                this.btn_Record.BackColor = Color.Red;
                this.btn_Record.ForeColor = Color.White;
                this.btn_Record.Text = "STOP RECORDING";
            }
            else
            {
                this.btn_Record.BackColor = Button.DefaultBackColor;
                this.btn_Record.ForeColor = Button.DefaultForeColor;
                this.btn_Record.Text = "START RECORDING";
            }
        }

        private void StartRecording()
        {
            this.RecordingDevices = new List<Device>();
            RecordingDevices.AddRange(this.lstIACDevices.Items.Cast<Device>());
            RecordingDevices.AddRange(this.lstOACDevices.Items.Cast<Device>());

            if (this.RecordingDevices.Count == 0)
            {
                return;
            }

            foreach (Device device in this.RecordingDevices)
            {
                device.StartRecording(FMATC_Form.OutputLocation);
            }
        }

        private void StopRecording()
        {
            foreach (Device device in this.RecordingDevices)
            {
                device.StopRecording();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if (this.Recording)
            {
                this.StopRecording();
            }

            UnregisterHotKey(this.Handle, MYACTION_HOTKEY_ID);

            base.OnClosed(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<InputDevice> InSources = Devices.GetInputDevices();
            List<OutputDevice> OutSources = Devices.GetOutputDevices();

            List<string> ACID_Strings = Properties.Settings.Default.ActiveInputDevices.Split(',').ToList();
            List<string> ACOD_Strings = Properties.Settings.Default.ActiveOutputDevices.Split(',').ToList();

            List<InputDevice> AVID = InSources.Where(o => !ACID_Strings.Contains(o.DeviceName)).ToList();
            List<InputDevice> ACID = InSources.Where(o => ACID_Strings.Contains(o.DeviceName)).ToList();
            List<OutputDevice> AVOD = OutSources.Where(o => !ACOD_Strings.Contains(o.DeviceName)).ToList();
            List<OutputDevice> ACOD = OutSources.Where(o => ACOD_Strings.Contains(o.DeviceName)).ToList();

            this.lstIAVDevices.Items.AddRange(
                AVID.ToArray());

            this.lstIACDevices.Items.AddRange(
                ACID.ToArray());

            this.lstOAVDevices.Items.AddRange(
                AVOD.ToArray());

            this.lstOACDevices.Items.AddRange(
                ACOD.ToArray());
        }

        void lstIAVDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Recording)
                return;

            int index = this.lstIAVDevices.IndexFromPoint(e.Location);
            if (index != -1)
            {
                object dev = this.lstIAVDevices.Items[index];
                this.lstIAVDevices.Items.RemoveAt(index);
                this.lstIACDevices.Items.Add(dev);
            }

            this.UpdateActiveInputDevicesSetting();
        }

        void lstIACDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Recording)
                return;

            int index = this.lstIACDevices.IndexFromPoint(e.Location);
            if (index != -1)
            {
                object dev = this.lstIACDevices.Items[index];
                this.lstIACDevices.Items.RemoveAt(index);
                this.lstIAVDevices.Items.Add(dev);
            }

            this.UpdateActiveInputDevicesSetting();
        }

        void lstOAVDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Recording)
                return;

            int index = this.lstOAVDevices.IndexFromPoint(e.Location);
            if (index != -1)
            {
                object dev = this.lstOAVDevices.Items[index];
                this.lstOAVDevices.Items.RemoveAt(index);
                this.lstOACDevices.Items.Add(dev);
            }

            this.UpdateActiveOutputDevicesSetting();
        }

        void lstOACDevices_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Recording)
                return;

            int index = this.lstOACDevices.IndexFromPoint(e.Location);
            if (index != -1)
            {
                object dev = this.lstOACDevices.Items[index];
                this.lstOACDevices.Items.RemoveAt(index);
                this.lstOAVDevices.Items.Add(dev);
            }

            this.UpdateActiveOutputDevicesSetting();
        }

        private void UpdateActiveInputDevicesSetting()
        {
            List<string> lstUpdateString = new List<string>();
            foreach (Device device in this.lstIACDevices.Items)
            {
                lstUpdateString.Add(device.DeviceName);
            }

            string joined = string.Join(",", lstUpdateString);

            Properties.Settings.Default.ActiveInputDevices = joined;
            Properties.Settings.Default.Save();
        }

        private void UpdateActiveOutputDevicesSetting()
        {
            List<string> lstUpdateString = new List<string>();
            foreach (Device device in this.lstOACDevices.Items)
            {
                lstUpdateString.Add(device.DeviceName);
            }

            string joined = string.Join(",", lstUpdateString);

            Properties.Settings.Default.ActiveOutputDevices = joined;
            Properties.Settings.Default.Save();
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Preferences_Form().ShowDialog();
        }
    }
}
