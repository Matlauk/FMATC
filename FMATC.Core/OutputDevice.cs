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
    public class OutputDevice : Device
    {
        private MMDevice mMDevice;
        private IWaveIn SourceStream;

        public OutputDevice(MMDevice device)
            : base()
        {
            this.mMDevice = device;
            this.DeviceName = this.mMDevice.FriendlyName;
        }

        public override void StartRecording(string fileDestination)
        {
            this.SourceStream = new WasapiLoopbackCapture(this.mMDevice);
            this.SourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);
            this.WaveWriter = new WaveFileWriter(
                Path.Combine(fileDestination, this.DeviceName + "_" + DateTime.Now.ToString("MMddyyyy-hhmmss") + ".wav"),
                this.SourceStream.WaveFormat);

            this.SourceStream.StartRecording();
        }

        public override void StopRecording()
        {
            if (this.SourceStream != null)
            {
                this.SourceStream.StopRecording();
                this.SourceStream.Dispose();
                this.SourceStream = null;
            }

            base.StopRecording();
        }
    }
}
