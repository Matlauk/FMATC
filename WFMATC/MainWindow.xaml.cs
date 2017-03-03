using FMATC.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WFMATC
{
    public delegate void GlobalKeyDownHandler(int key);

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool Recording = false;
        private bool CanRecord = true;
        public static int RecordHotKey = Properties.Settings.Default.RecordHotKey;
        public static string OutputLocation = Properties.Settings.Default.OutputLocation;
        private List<Device> RecordingDevices;

        public MainWindow()
        {
            _hookID = SetHook(_proc);
            InitializeComponent();
            RecordHotKey = Properties.Settings.Default.RecordHotKey;
            
            Title += " " + "v1.0.0";
        }
        
        protected override void OnClosed(EventArgs e)
        {
            UnhookWindowsHookEx(_hookID);
            base.OnClosed(e);
        }

        private ObservableCollection<FMATC.Core.InputDevice> AvailableInputDevices = new ObservableCollection<FMATC.Core.InputDevice>();
        private ObservableCollection<FMATC.Core.InputDevice> ActiveInputDevices = new ObservableCollection<FMATC.Core.InputDevice>();
        private ObservableCollection<OutputDevice> AvailableOutputDevices = new ObservableCollection<OutputDevice>();
        private ObservableCollection<OutputDevice> ActiveOutputDevices = new ObservableCollection<OutputDevice>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<FMATC.Core.InputDevice> InSources = Devices.GetInputDevices();
            List<OutputDevice> OutSources = Devices.GetOutputDevices();

            List<string> active_input_device_strings = Properties.Settings.Default.ActiveInputDevices.Split(',').ToList();
            List<string> active_output_device_strings = Properties.Settings.Default.ActiveOutputDevices.Split(',').ToList();

            List<FMATC.Core.InputDevice> available_input_devices = InSources.Where(o => !active_input_device_strings.Contains(o.DeviceName)).ToList();
            List<FMATC.Core.InputDevice> active_input_devices = InSources.Where(o => active_input_device_strings.Contains(o.DeviceName)).ToList();
            List<OutputDevice> available_output_devices = OutSources.Where(o => !active_output_device_strings.Contains(o.DeviceName)).ToList();
            List<OutputDevice> active_output_devices = OutSources.Where(o => active_output_device_strings.Contains(o.DeviceName)).ToList();

            foreach (var item in available_input_devices)
            {
                AvailableInputDevices.Add(item);
            }

            foreach (var item in active_input_devices)
            {
                ActiveInputDevices.Add(item);
            }

            foreach (var item in available_output_devices)
            {
                AvailableOutputDevices.Add(item);
            }

            foreach (var item in active_output_devices)
            {
                ActiveOutputDevices.Add(item);
            }

            lst_AvailableInputDevices.ItemsSource = AvailableInputDevices;
            lst_ActiveInputDevices.ItemsSource = ActiveInputDevices;
            lst_AvailableOutputDevices.ItemsSource = AvailableOutputDevices;
            lst_ActiveOutputDevices.ItemsSource = ActiveOutputDevices;
            
            GlobalKeyDownEvent += MainWindow_GlobalKeyDownEvent;
        }

        private void MainWindow_GlobalKeyDownEvent(int key)
        {
            if (key == RecordHotKey)
            {
                ToggleRecording();
            }
        }
        
        private void btn_StartStopRecord_Click(object sender, RoutedEventArgs e)
        {
            ToggleRecording();
        }

        private void ToggleRecording()
        {
            bool was_recording = Recording;
            Recording = !Recording && CanRecord;

            if (Recording)
            {
                StartRecording();
            }
            else if (was_recording)
            {
                StopRecording();
            }

            MenuBar.IsEnabled = !Recording;

            if (Recording)
            {
                btn_StartStopRecord.Background = new SolidColorBrush(Colors.Red);
                btn_StartStopRecordingText.Foreground = new SolidColorBrush(Colors.White);
                btn_StartStopRecordingText.Text = "STOP RECORDING";
            }
            else if (was_recording)
            {
                btn_StartStopRecord.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                btn_StartStopRecordingText.Foreground = new SolidColorBrush(Colors.Black);
                btn_StartStopRecordingText.Text = "START RECORDING";
            }
        }

        private void StartRecording()
        {
            RecordingDevices = new List<Device>();
            RecordingDevices.AddRange(lst_ActiveInputDevices.Items.Cast<Device>());
            RecordingDevices.AddRange(lst_ActiveOutputDevices.Items.Cast<Device>());

            if (RecordingDevices.Count == 0)
            {
                return;
            }

            if (!Directory.Exists(OutputLocation))
                Directory.CreateDirectory(OutputLocation);

            foreach (Device device in RecordingDevices)
            {
                device.StartRecording(OutputLocation);
            }
        }

        private void StopRecording()
        {
            foreach (Device device in RecordingDevices)
            {
                device.StopRecording();
            }
        }

        private void mi_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mi_Preferences_Click(object sender, RoutedEventArgs e)
        {
            CanRecord = false;
            new PreferencesForm().ShowDialog();
            CanRecord = true;
        }

        private void lst_AvailableInputDevices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Recording)
                return;

            var dev = lst_AvailableInputDevices.SelectedItem as FMATC.Core.InputDevice;
            if (dev == null) return;
            AvailableInputDevices.Remove(dev);
            ActiveInputDevices.Add(dev);

            UpdateActiveInputDevicesSetting();
        }

        private void lst_ActiveInputDevices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Recording)
                return;

            var dev = lst_ActiveInputDevices.SelectedItem as FMATC.Core.InputDevice;
            if (dev == null) return;
            ActiveInputDevices.Remove(dev);
            AvailableInputDevices.Add(dev);

            UpdateActiveInputDevicesSetting();
        }

        private void lst_AvailableOutputDevices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Recording)
                return;

            var dev = lst_AvailableOutputDevices.SelectedItem as OutputDevice;
            if (dev == null) return;
            AvailableOutputDevices.Remove(dev);
            ActiveOutputDevices.Add(dev);

            UpdateActiveOutputDevicesSetting();
        }

        private void lst_ActiveOutputDevices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Recording)
                return;

            var dev = lst_ActiveOutputDevices.SelectedItem as OutputDevice;
            if (dev == null) return;
            ActiveOutputDevices.Remove(dev);
            AvailableOutputDevices.Add(dev);

            UpdateActiveOutputDevicesSetting();
        }

        private void UpdateActiveInputDevicesSetting()
        {
            List<string> lstUpdateString = new List<string>();
            foreach (Device device in lst_ActiveInputDevices.Items)
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
            foreach (Device device in lst_ActiveOutputDevices.Items)
            {
                lstUpdateString.Add(device.DeviceName);
            }

            string joined = string.Join(",", lstUpdateString);

            Properties.Settings.Default.ActiveOutputDevices = joined;
            Properties.Settings.Default.Save();
        }

        private const int WH_KEYBOARD_LL = 13;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;
        public static event GlobalKeyDownHandler GlobalKeyDownEvent = null;
        public static List<int> PressedKeys = new List<int>();

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(
            int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(
            int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WinMSG.WM_KEYDOWN || wParam == (IntPtr)WinMSG.WM_SYSKEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (!PressedKeys.Contains(vkCode))
                {
                    PressedKeys.Add(vkCode);
                    Console.WriteLine(vkCode);

                    if (GlobalKeyDownEvent != null)
                    {
                        GlobalKeyDownEvent(vkCode);
                    }
                }
            }

            if (nCode >= 0 && (wParam == (IntPtr)WinMSG.WM_KEYUP || wParam == (IntPtr)WinMSG.WM_SYSKEYUP))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (PressedKeys.Contains(vkCode))
                {
                    PressedKeys.Remove(vkCode);
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        
    }
}
