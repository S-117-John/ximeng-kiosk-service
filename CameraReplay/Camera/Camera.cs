using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Drawing;

namespace SunRise.HOSP
{
    public class Camera
    {
        FilterInfo m_VideoDevice = null;
        VideoCaptureDevice m_CaptureAForge = null;
        public delegate void CallbackBitmap(Bitmap bmp);
        private CallbackBitmap m_CallbackBitmap = null;
        private System.Timers.Timer myTimer = new System.Timers.Timer();
        //private CallbackBitmap m_CallbackShotBitmap = null;
        public Camera(object obj, CallbackBitmap cbi)
        {
            if(obj.GetType()==typeof(int))
            {
                ConstuctionCamera((int)obj, cbi);
            }
            if(obj.GetType()==typeof(string))
            {
                ConstuctionCamera(obj as string, cbi);
            }
        }
        public void ConstuctionCamera(string DeviceName, CallbackBitmap cbi)
        {
            m_CallbackBitmap = cbi;
            IsInit = false;

            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                if (device.Name == DeviceName)
                {
                    m_VideoDevice = device;
                    break;
                }
            }
            if (m_VideoDevice == null)
                return;
            InitVideo();
        }
        public void ConstuctionCamera(int DeviceIndex, CallbackBitmap cbi)
        {
            m_CallbackBitmap = cbi;
            IsInit = false;
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (DeviceIndex >= videoDevices.Count)
                return;
            m_VideoDevice = videoDevices[DeviceIndex];
            InitVideo();
        }
        public static List<string> GetVideoList()
        {
            List<string> list = new List<string>();
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            for(int i=0;i<videoDevices.Count;i++)
            {
                list.Add(videoDevices[i].Name);
            }
            return list;
        }

        public bool IsInit { get; set; }

        public void InitVideo()
        {
            //VideoCapabilities cap = m_CaptureAForge.VideoCapabilities;

            m_CaptureAForge = new VideoCaptureDevice(m_VideoDevice.MonikerString);
            //m_CaptureAForge.DesiredFrameSize = new Size(320, 240); 
            //m_CaptureAForge.DesiredFrameRate = rate;
            m_CaptureAForge.NewFrame += new NewFrameEventHandler(m_CaptureAForge_NewFrame);
            IsInit = true;
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(myTimer_Elapsed);

            //m_CaptureAForge.ProvideSnapshots = true;
            //m_CaptureAForge.DesiredSnapshotSize = new Size(320, 240);
            //m_CaptureAForge.SnapshotFrame += new NewFrameEventHandler(m_CaptureAForge_SnapshotFrame);
        }


        public void Start()
        {
            if (!IsInit)
                return;
            if (m_CaptureAForge.IsRunning)
                return;
            m_CaptureAForge.Start();
        }


        private void m_CaptureAForge_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (bCapture)
            {
                if (m_CallbackBitmap != null)
                    m_CallbackBitmap((Bitmap)eventArgs.Frame.Clone());
                bCapture = false;
            }

        }

       
        private bool bCapture = false;
        public void BeginSnapshot(int Shotinterval = 500)
        {
            Start();
            myTimer.Interval = Shotinterval;
            myTimer.Enabled = true;
            //m_CaptureAForge.Stop();
        }

        public void EndSnapshot()
        {
            myTimer.Enabled = false;
            Stop();
        }

        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bCapture = true;
            //if (m_CaptureAForge != null && m_CaptureAForge.ProvideSnapshots)
            //{
            //    m_CaptureAForge.SimulateTrigger();
            //}
        }

        //private void m_CaptureAForge_SnapshotFrame(object sender, NewFrameEventArgs eventArgs)
        //{
        //    if (m_CallbackShotBitmap != null)
        //        m_CallbackShotBitmap((Bitmap)eventArgs.Frame.Clone());

        //}
        public void Stop()
        {
            if (m_CaptureAForge != null)
            {
                if (m_CaptureAForge.IsRunning)
                {
                    m_CaptureAForge.SignalToStop();
                    m_CaptureAForge.WaitForStop();
                }
            }
        }

    }

}
