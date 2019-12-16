using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace SunRise.HOSP.CLIENT
{
    public class CameraReplay
    {
        public delegate void CallbackPlay(Image bmp, int CurPos);
        private CallbackPlay m_CallbackPlay = null;
        private List<FileInfo> m_FileList = null;
        private int m_CurrentPos = 0;
        private System.Timers.Timer m_TimerPlay = new System.Timers.Timer();
        public CameraReplay()
        {

            m_TimerPlay.Elapsed += new System.Timers.ElapsedEventHandler(m_TimerPlay_Elapsed);
        }

        void m_TimerPlay_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DoPlay();
        }

        private void DoPlay()
        {
            if (m_CallbackPlay != null)
            {
                m_CallbackPlay(GetCurBmp(), m_CurrentPos);
                m_CurrentPos++;
            }
        }

        private Image GetCurBmp()
        {
            if (m_FileList.Count == 0)
                return null;
            if (m_CurrentPos >= m_FileList.Count)
            {
                m_CurrentPos = 0;
            }
            return Image.FromFile(m_FileList[m_CurrentPos].FullName);
        }

        public int GetImgCount()
        {
            return m_FileList.Count;
        }

        public int GetCurrentPos()
        {
            return m_CurrentPos;
        }

        public void SetPos(int Pos)
        {
            m_CurrentPos = Pos;
            DoPlay();
        }
        public void Init(string path, CallbackPlay cb)
        {
            m_CurrentPos = 0;
            m_CallbackPlay = cb;
            m_FileList = GetAllFilesInDirectory(path);

        }
        public void Play(int Interval = 300)
        {
            m_TimerPlay.Stop();
            m_TimerPlay.Interval = Interval;
            m_TimerPlay.Start();
        }

        public void Pause()
        {
            m_TimerPlay.Stop();
        }

        public void Resume()
        {
            m_TimerPlay.Start();
        }

        public void ReStart()
        {
            m_CurrentPos = 0;
            m_TimerPlay.Stop();
            m_TimerPlay.Start();
        }

        public List<FileInfo> GetAllFilesInDirectory(string strDirectory)
        {
            List<FileInfo> listFiles = new List<FileInfo>(); //保存所有的文件信息  
            if (!Directory.Exists(strDirectory))
            {
                return listFiles;
            }
            DirectoryInfo directory = new DirectoryInfo(strDirectory);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            FileInfo[] fileInfoArray = directory.GetFiles();
            if (fileInfoArray.Length > 0)
            {
                listFiles.AddRange(fileInfoArray.Where(fi => { return fi.Extension.ToLower() == ".jpg"; }));
            }
            foreach (DirectoryInfo _directoryInfo in directoryArray)
            {
                DirectoryInfo directoryA = new DirectoryInfo(_directoryInfo.FullName);
                DirectoryInfo[] directoryArrayA = directoryA.GetDirectories();
                FileInfo[] fileInfoArrayA = directoryA.GetFiles();
                if (fileInfoArrayA.Length > 0)
                {

                    listFiles.AddRange(fileInfoArrayA.Where(fi => { return fi.Extension.ToLower() == ".jpg"; }));
                }
                GetAllFilesInDirectory(_directoryInfo.FullName);//递归遍历  
            }
            return listFiles;
        }

    }
}
