using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;



namespace SunRise.HOSP.CLIENT
{
    public class CameraHelper
    {
        private static Camera m_TopCamera = null;
       private static Camera m_BottomCamera = null;
        private static string m_CamSavePath = string.Empty;

        public static string m_CurCamSavePath = string.Empty;
        public static string m_CurBottomCamSavePath = string.Empty;
        private static string m_UserName = string.Empty;

        public static string GetSavePath()
        {
            if(String.IsNullOrWhiteSpace(m_CamSavePath))
                return string.Format("{0}{1}",System.AppDomain.CurrentDomain.BaseDirectory,"CameraSave");
            return m_CamSavePath;
        }
        public static bool Init(object TopCameraNameorIndex,  object BottomCameraNameOrIndex, string CamSavePath)
        {
            m_CamSavePath = CamSavePath;
            if(string.IsNullOrWhiteSpace(m_CamSavePath))
            {
                m_CamSavePath = GetSavePath();
            }
            if(m_CamSavePath.EndsWith("\\"))
            {
                m_CamSavePath = m_CamSavePath.Substring(0, m_CamSavePath.Length - 1);
            }
            bool bResult = true;
            if (TopCameraNameorIndex.GetType() == typeof(int) && (int)TopCameraNameorIndex != -1)
            {
                m_TopCamera = new Camera(TopCameraNameorIndex, bmp =>
                {
                    string topPath = GetTopFilePath();
                    if (String.IsNullOrWhiteSpace(topPath))
                        return;
                    SaveBitmap(topPath, bmp);
                });
                if (!m_TopCamera.IsInit)
                {
                    bResult = false;
                }
            }

            if (BottomCameraNameOrIndex.GetType() == typeof(int) && (int)BottomCameraNameOrIndex != -1)
            {
                m_BottomCamera = new Camera(BottomCameraNameOrIndex, bmp =>
                {
                    string bottomPath = GetBottomFilePath();
                    if (String.IsNullOrWhiteSpace(bottomPath))
                        return;
                    SaveBitmap(bottomPath, bmp);
                });
                if (!m_BottomCamera.IsInit)
                {
                    bResult = false;
                }
            }
            return bResult;
        }

        private static string GetTopFilePath()
        {
            return string.Format("{0}\\{1}.jpg", m_CurCamSavePath, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        }

        private static string GetBottomFilePath()
        {
            return string.Format("{0}\\{1}.jpg", m_CurBottomCamSavePath, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
        }

        private static void SaveBitmap(String path, Bitmap bmp)
        {
            if (m_UserName.Length > 0)
                bmp.Save(path, ImageFormat.Jpeg);
        }

        public static void StartTopCamera(string UserName, int Shotinterval = 500)
        {
            if (m_TopCamera == null)
                return;
            m_UserName = UserName;
            m_CurCamSavePath = string.Format("{0}\\{1}\\{2}_{3}\\Top", m_CamSavePath, DateTime.Now.ToString("yyyyMMdd"), m_UserName, DateTime.Now.ToString("HHmmss"));
            if (!Directory.Exists(m_CurCamSavePath))
            {
                Directory.CreateDirectory(m_CurCamSavePath);
            }
            m_TopCamera.BeginSnapshot(Shotinterval);
        }
        public static void StopTopCamera()
        {
            if (m_TopCamera == null)
                return;
            m_TopCamera.Stop();
            m_UserName = "";
        }

        public static void StartBottomCamera(string UserName, int Shotinterval =500)
        {
            if (m_BottomCamera == null)
                return; 
            m_UserName = UserName;
            m_CurBottomCamSavePath = string.Format("{0}\\{1}\\{2}_{3}\\Bottom\\", m_CamSavePath, DateTime.Now.ToString("yyyyMMdd"), m_UserName, DateTime.Now.ToString("HHmmss"));
            if (!Directory.Exists(m_CurBottomCamSavePath))
            {
                Directory.CreateDirectory(m_CurBottomCamSavePath);
            }
            m_BottomCamera.BeginSnapshot(Shotinterval);
        }
        public static void StopBottomCamera()
        {
            if (m_BottomCamera == null)
                return;
            m_BottomCamera.Stop();
            m_UserName = "";
        }

        public static void StopAll()
        {
            if (m_TopCamera == null || m_BottomCamera==null)
                return;
            m_TopCamera.Stop();
            m_BottomCamera.Stop();
        }
    }    
}
