using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FMATC
{
    public partial class Preferences_Form : Form
    {
        private static Preferences_Form _Instance = new Preferences_Form();
        public static Preferences_Form Instance
        {
            get
            {
                return _Instance;
            }
        }

        bool AcceptNewHotkey = false;

        private Preferences_Form()
        {
            InitializeComponent();

            Program.GlobalKeyDownEvent += Program_GlobalKeyDownEvent;

            this.txtHotkey.Text = ((Keys)Properties.Settings.Default.RecordHotKey).ToString();
            this.lblOutputLocation.Text = Properties.Settings.Default.OutputLocation;
        }

        private void Program_GlobalKeyDownEvent(Keys key)
        {
            if (this.AcceptNewHotkey)
            {
                this.AcceptNewHotkey = false;
                FMATC_Form.Instance.RecordHotKey = key;
                this.txtHotkey.Text = key.ToString();
                Properties.Settings.Default.RecordHotKey = (int)key;
                Properties.Settings.Default.Save();

                Console.WriteLine("Set the record global hotkey to: " + key.ToString() + "(" + ((int)key).ToString() + ")");
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            this.txtHotkey.Text = "";
            this.AcceptNewHotkey = true;
        }

        private void btnOutputLocation_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                FMATC_Form.Instance.OutputLocation = fbd.SelectedPath;
                this.lblOutputLocation.Text = fbd.SelectedPath;
                Properties.Settings.Default.OutputLocation = fbd.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
    }
}
