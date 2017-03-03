using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WFMATC
{
    /// <summary>
    /// Interaction logic for PreferencesForm.xaml
    /// </summary>
    public partial class PreferencesForm : Window
    {
        bool AcceptNewHotkey = false;
        public PreferencesForm()
        {
            InitializeComponent();
        }

        private void PreferencesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.GlobalKeyDownEvent += Program_GlobalKeyDownEvent;

            txt_RecordingHotkey.Text = GetKeyChar(Properties.Settings.Default.RecordHotKey);
            txt_OutputLocation.Content = Properties.Settings.Default.OutputLocation;
        }

        private void PreferencesWindow_Closed(object sender, EventArgs e)
        {
            AcceptNewHotkey = false;
            MainWindow.GlobalKeyDownEvent -= Program_GlobalKeyDownEvent;
        }

        private void Program_GlobalKeyDownEvent(int key)
        {
            if (AcceptNewHotkey)
            {
                string key_char = GetKeyChar(key);
                AcceptNewHotkey = false;
                MainWindow.RecordHotKey = key;
                txt_RecordingHotkey.Text = key_char;
                Properties.Settings.Default.RecordHotKey = key;
                Properties.Settings.Default.Save();

                Console.WriteLine("Set the record global hotkey to: " + key_char + "(" + (key).ToString() + ")");
            }
        }

        private void btn_EditHotKey_Click(object sender, RoutedEventArgs e)
        {
            txt_RecordingHotkey.Text = "";
            AcceptNewHotkey = true;
        }

        private void btn_Browse_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog folder_dialog = new CommonOpenFileDialog();
            folder_dialog.Title = "";
            folder_dialog.IsFolderPicker = true;
            folder_dialog.InitialDirectory = Properties.Settings.Default.OutputLocation;
            folder_dialog.AddToMostRecentlyUsedList = false;
            folder_dialog.AllowNonFileSystemItems = false;
            folder_dialog.DefaultDirectory = Properties.Settings.Default.OutputLocation;
            folder_dialog.EnsureFileExists = true;
            folder_dialog.EnsurePathExists = true;
            folder_dialog.EnsureReadOnly = false;
            folder_dialog.EnsureValidNames = true;
            folder_dialog.Multiselect = false;
            folder_dialog.ShowPlacesList = true;

            CommonFileDialogResult dr = folder_dialog.ShowDialog();
            if (dr == CommonFileDialogResult.Ok)
            {
                MainWindow.OutputLocation = folder_dialog.FileName;
                txt_OutputLocation.Content = folder_dialog.FileName;
                Properties.Settings.Default.OutputLocation = folder_dialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private string GetKeyChar(int key)
        {
            string key_char = System.Windows.Input.KeyInterop.KeyFromVirtualKey(key).ToString();
            return key_char;
        }
    }
}
