using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using CSCore.CoreAudioAPI;
using System.Threading;

namespace Mr_Fish
{
    public partial class Form1 : Form
    {
        static public int highVal = 0;
        static public int fished = 0;
        static public Thread botThread;
        static public bool active = false;
        static public int pID = 0;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            addProcesses();
            //System.Threading.Thread.Sleep(5000);
            //SendKeys.Send("Hello");
            //VirtualMouse.MoveTo(0, 0);
            //VirtualMouse.RightClick();
        }

        private void addProcesses()
        {
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                if (theprocess.MainWindowTitle != "")
                {
                    var txt = String.Format("{0} - {1}", theprocess.MainWindowTitle, theprocess.ProcessName);
                    var pID = theprocess.Id;
                    var i = cb_processes.Items.Add(new Item(txt, pID));

                    if (txt.Contains("Minecraft"))
                    {
                        cb_processes.SelectedIndex = i;
                        tssl_status.Text = "Minecraft gefunden...";
                    }
                }
            }
        }
        private void fish()
        {
            try
            {
                //VirtualMouse.RightClick();
                send_rightclick(pID);
                var oVol = 0;
                System.Threading.Thread.Sleep(4000);
                while (oVol <= int.Parse(tb_volume.Text) && active)
                {
                    var cVol = getVolume();
                    if (cVol > oVol)
                    {
                        oVol = cVol;
                    }
                }

                //VirtualMouse.RightClick();
                send_rightclick(pID);
                fished = fished + 1;
            }
            catch { }
        }
        private void bot_start()
        {
            System.Threading.Thread.Sleep(2500);
            while (active)
            {
                fish();
            }
        }
        private int getVolume()
        {
            int ret = 0;
            Item item;
            item = (Item) GetControlPropertyThreadSafe(cb_processes, t => t.SelectedItem);


            if (item.Name != null)
            {
                var pID = item.Value;

                var sessionManager = GetDefaultAudioSessionManager2(CSCore.CoreAudioAPI.DataFlow.Render);
                var sessionEnumerator = sessionManager.GetSessionEnumerator();
                foreach (var session in sessionEnumerator)
                {
                    var audioMeterInformation = session.QueryInterface<CSCore.CoreAudioAPI.AudioMeterInformation>();
                    var session2 = session.QueryInterface<AudioSessionControl2>();
                    var processID = session2.ProcessID;
                    if (processID == pID)
                    {
                        ret = (int)(audioMeterInformation.GetPeakValue() * 1000);
                    }
                }
            }

            return ret;
        }
        public static U GetControlPropertyThreadSafe<T, U>(T control, Func<T, U> func) where T : Control
        {
            if (control.InvokeRequired)
            {
                return (U)control.Invoke(func, new object[] { control });
            }
            else
            {
                return func(control);
            }
        }
        private static AudioSessionManager2 GetDefaultAudioSessionManager2(CSCore.CoreAudioAPI.DataFlow dataFlow)
        {
            using (var enumerator = new CSCore.CoreAudioAPI.MMDeviceEnumerator())
            {
                using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, CSCore.CoreAudioAPI.Role.Multimedia))
                {
                    Console.WriteLine("DefaultDevice: " + device.FriendlyName);
                    var sessionManager = AudioSessionManager2.FromMMDevice(device);
                    return sessionManager;
                }
            }
        }
        public static void send_rightclick(int pID)
        {
            const uint WM_KEYDOWN = 0x100;
            const uint WM_SYSCOMMAND = 0x018;
            const uint WM_KEYUP = 0x0101;
            const uint SC_CLOSE = 0x053;
            const uint WM_RBUTTONDOWN = 0x0204;
            const uint WM_RBUTTONUP = 0x0205;
            const uint WM_ACTIVE = 0x0006;

            Process p = Process.GetProcessById(pID);
            IntPtr windowHandle = p.MainWindowHandle;

            //IntPtr result3 = PostMessage(WindowToFind, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
            //IntPtr result4 = PostMessage(WindowToFind, WM_KEYUP, ((IntPtr)k), (IntPtr)0);
            //IntPtr result0 = PostMessage(WindowToFind, WM_ACTIVE, ((IntPtr)0), (IntPtr)0);
            //IntPtr result1 = PostMessage(WindowToFind, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
            //IntPtr result2 = PostMessage(WindowToFind, WM_KEYUP, ((IntPtr)k), (IntPtr)0);
            IntPtr result3 = PostMessage(windowHandle, WM_RBUTTONDOWN, ((IntPtr)0), (IntPtr)0);
            IntPtr result4 = PostMessage(windowHandle, WM_RBUTTONUP, ((IntPtr)0), (IntPtr)0);

        }

        private void cb_processes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item itm = (Item)cb_processes.SelectedItem;
            pID = itm.Value;
        }
        private void tb_volume_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (cb_processes.SelectedItem != null)
            {
                try
                {
                    lbl_fished.Text = fished.ToString();
                    Item itm = (Item)cb_processes.SelectedItem;

                    var sessionManager = GetDefaultAudioSessionManager2(CSCore.CoreAudioAPI.DataFlow.Render);
                    var sessionEnumerator = sessionManager.GetSessionEnumerator();
                    foreach (var session in sessionEnumerator)
                    {
                        var audioMeterInformation = session.QueryInterface<CSCore.CoreAudioAPI.AudioMeterInformation>();
                        var session2 = session.QueryInterface<AudioSessionControl2>();
                        var processID = session2.ProcessID;
                        if (processID == pID)
                        {
                            pb_audio.Maximum = 1000;
                            var vol = (int)(audioMeterInformation.GetPeakValue() * 1000);
                            pb_audio.Value = vol;
                            lbl_curVol.Text = vol.ToString();

                            if (highVal < vol)
                            {
                                highVal = vol;
                                lbl_maxVol.Text = highVal.ToString();
                            }
                        }
                    }
                } catch {}
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            active = true;
            botThread = new Thread(new ThreadStart(bot_start));
            botThread.IsBackground = true;
            botThread.Start();
            tssl_status.Text = "Aktiv" + " (" + DateTime.Now.ToString("HH:mm:ss tt") + "Uhr )";
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
            active = false;
            botThread.Abort();
            botThread = null;
            tssl_status.Text = "Inaktiv";
            } catch { };
        }
        private void btn_calibrate_Click(object sender, EventArgs e)
        {
            highVal = 0;
            lbl_maxVol.Text = highVal.ToString();
        }
    }

    public class Item
    {
        public string Name;
        public int Value;
        public Item(string name, int value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    public static class VirtualMouse
    {
        // import the necessary API function so .NET can
        // marshall parameters appropriately
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        // constants for the mouse_input() API function
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const int MOUSEEVENTF_LEFTUP = 0x0004;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;


        // simulates movement of the mouse.  parameters specify changes
        // in relative position.  positive values indicate movement
        // right or down
        public static void Move(int xDelta, int yDelta)
        {
            mouse_event(MOUSEEVENTF_MOVE, xDelta, yDelta, 0, 0);
        }


        // simulates movement of the mouse.  parameters specify an
        // absolute location, with the top left corner being the
        // origin
        public static void MoveTo(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, 0);
        }


        // simulates a click-and-release action of the left mouse
        // button at its current position
        public static void LeftClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);
        }

        // simulates a click-and-release action of the left mouse
        // button at its current position
        public static void RightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, Control.MousePosition.X, Control.MousePosition.Y, 0, 0);
        }
    }
}
