using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using CSCore.CoreAudioAPI;
using System.Threading;
using System.Threading.Tasks;

namespace Mr_Fish
{
    public partial class Form1 : Form
    {
        static public int highVal = 0;
        static public int fished = 0;
        static public Thread botThread;
        static public bool active = false;
        static public int pID = 0;
        private DateTime startTime;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public Form1()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            InitializeComponent();
            updateStartStopButtons();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            addProcesses();
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

                    if (txt.Contains("Minecraft") && txt != "Minecraft Launcher")
                    {
                        cb_processes.SelectedIndex = i;
                        tssl_status.Text = "Minecraft found...";
                    }
                }
            }
        }
        private void fish()
        {
            try
            {
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

        private void formatStatusText()
        {
            if (active)
            {
                TimeSpan duration = DateTime.Now.Subtract(startTime);
                string durationString = duration.ToString(@"d\:hh\:mm\:ss");
                string perHour = Math.Floor(fished / duration.TotalHours).ToString();
                tssl_status.Text = "Active for " + durationString + ", ~" + perHour + " catches/hour, current " + fished;
            }
        }
        private void updateStartStopButtons()
        {
            btn_start.Enabled = !active;
            btn_stop.Enabled = active;
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
                    formatStatusText();
                    Item itm = (Item)cb_processes.SelectedItem;

                    Task.Run(() =>
                    {
                        var sessionManager = GetDefaultAudioSessionManager2(CSCore.CoreAudioAPI.DataFlow.Render);
                        var sessionEnumerator = sessionManager.GetSessionEnumerator();
                        foreach (var session in sessionEnumerator)
                        {
                            var audioMeterInformation = session.QueryInterface<CSCore.CoreAudioAPI.AudioMeterInformation>();
                            var session2 = session.QueryInterface<AudioSessionControl2>();
                            var processID = session2.ProcessID;

                            if (processID == pID)
                            {
                            
                            
                                var vol = (int)(audioMeterInformation.GetPeakValue() * 1000);
                            
                                this.pb_audio.Invoke((MethodInvoker)delegate
                                {
                                    pb_audio.Maximum = 1000;
                                    pb_audio.Value = vol;
                                    lbl_curVol.Text = vol.ToString();
                                });

                                Debug.WriteLine(audioMeterInformation.GetPeakValue());
                                if (highVal < vol)
                                {

                                    this.lbl_maxVol.Invoke((MethodInvoker)delegate
                                    {
                                        highVal = vol;
                                        lbl_maxVol.Text = highVal.ToString();
                                    });
                                }
                            }
                        }
                    });

                    
                } catch {}
            }
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            active = true;
            botThread = new Thread(new ThreadStart(bot_start));
            botThread.IsBackground = true;
            botThread.Start();
            startTime = DateTime.Now;
            formatStatusText();
            updateStartStopButtons();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
                active = false;
                botThread.Abort();
                botThread = null;
                tssl_status.Text = "Inactive";
                updateStartStopButtons();
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
}
