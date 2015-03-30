using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMATC
{
    public class InputDevice : Device
    {
        private WaveInCapabilities WaveIn_C;
        private WaveIn SourceStream;
        private int DeviceNumber = -1;

        public InputDevice(WaveInCapabilities wic, int deviceNumber)
            : base()
        {
            this.WaveIn_C = wic;
            this.DeviceName = this.WaveIn_C.ProductName;
        }

        public override void StartRecording(string fileDestination)
        {
            this.SourceStream = new WaveIn();
            this.SourceStream.DeviceNumber = this.DeviceNumber;
            this.SourceStream.WaveFormat = 
                new NAudio.Wave.WaveFormat(
                    44100, 
                    NAudio.Wave.WaveIn.GetCapabilities(this.DeviceNumber).Channels);

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
