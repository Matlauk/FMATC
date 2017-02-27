using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMATC.Core
{
    public abstract class Device
    {
        public string DeviceName { get; protected set; }
        protected WaveFileWriter WaveWriter;

        public abstract void StartRecording(string fileDestination);

        public virtual void StopRecording()
        {
            if (this.WaveWriter != null)
            {
                this.WaveWriter.Dispose();
                this.WaveWriter = null;
            }
        }

        public override string ToString()
        {
            return this.DeviceName;
        }

        protected void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (this.WaveWriter == null) return;

            this.WaveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            this.WaveWriter.Flush();
        }
    }

    public static class Devices
    {
        public static List<InputDevice> GetInputDevices()
        {
            List<InputDevice> InSources = new List<InputDevice>();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                InSources.Add(new InputDevice(NAudio.Wave.WaveIn.GetCapabilities(i), i));
            return InSources;
        }

        public static List<OutputDevice> GetOutputDevices()
        {
            List<OutputDevice> OutSources = new List<OutputDevice>();
            MMDeviceEnumerator deviceEnum = new MMDeviceEnumerator();
            MMDeviceCollection deviceCol = deviceEnum.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            for (int i = 0; i < deviceCol.Count; i++)
                OutSources.Add(new OutputDevice(deviceCol[i]));
            return OutSources;
        }
    }
}
