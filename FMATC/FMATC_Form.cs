using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private bool Recording = false;
        public static Keys RecordHotKey = (Keys)Properties.Settings.Default.RecordHotKey;
        public static string OutputLocation = Properties.Settings.Default.OutputLocation;
        private List<Device> RecordingDevices;

        public FMATC_Form()
        {
            Program.GlobalKeyDownEvent += Program_GlobalKeyDownEvent;

            InitializeComponent();

            this.lstIAVDevices.MouseDoubleClick += lstIAVDevices_MouseDoubleClick;
            this.lstIACDevices.MouseDoubleClick += lstIACDevices_MouseDoubleClick;
            this.lstOAVDevices.MouseDoubleClick += lstOAVDevices_MouseDoubleClick;
            this.lstOACDevices.MouseDoubleClick += lstOACDevices_MouseDoubleClick;

            FMATC_Form.RecordHotKey = (Keys)Properties.Settings.Default.RecordHotKey;
        }

        void Program_GlobalKeyDownEvent(Keys key)
        {
            if (key == RecordHotKey)
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
            }
        }

        private void StartRecording()
        {
            this.MenuBar.BackColor = Color.Red;

            this.Enabled = false;

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
            this.MenuBar.BackColor = MenuStrip.DefaultBackColor;

            this.Enabled = true;

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

            base.OnClosed(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<InputDevice> InSources = new List<InputDevice>();
            List<OutputDevice> OutSources = new List<OutputDevice>();

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                InSources.Add(new InputDevice(NAudio.Wave.WaveIn.GetCapabilities(i), i));
            }

            MMDeviceEnumerator deviceEnum = new MMDeviceEnumerator();
            MMDeviceCollection deviceCol = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            for (int i = 0; i < deviceCol.Count; i++)
            {
                OutSources.Add(new OutputDevice(deviceCol[i]));
            }

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
