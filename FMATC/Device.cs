using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMATC
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
}
