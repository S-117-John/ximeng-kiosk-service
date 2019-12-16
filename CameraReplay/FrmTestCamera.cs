using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace SunRise.HOSP.CLIENT
{
    public partial class FrmTestCamera : ExControl.BaseForm
    {
        Camera m_Camera1 = null;
        Camera m_Camera2 = null;
        public FrmTestCamera()
        {
            InitializeComponent();
           
        }


        private void FrmTestCamera_Load(object sender, EventArgs e)
        {

            m_Camera1 = new Camera(0, bmp =>
            {
                pbCapture.Image = bmp;
            });

            label1.Text = string.Format("当前Top索引 {0}:", 0);
            m_Camera2 = new Camera(1, bmp =>
            {
                pbCapture.Image = bmp;
            });
            label2.Text = string.Format("当前Bottom索引 {0}:", 1);
            listBox1.DataSource = Camera.GetVideoList();
        }

        private void rbCameraSelTop_Click(object sender, EventArgs e)
        {
            pbCapture.Image = null;
            m_Camera2.Stop();
            m_Camera1.BeginSnapshot(100);
        }

        private void rbCameraSelBottom_Click(object sender, EventArgs e)
        {
            pbCapture.Image = null;
            m_Camera1.Stop();
            m_Camera2.BeginSnapshot(100);
        }


        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void FrmTestCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Camera1.Stop();
            m_Camera2.Stop();
        }
      

    }
}
