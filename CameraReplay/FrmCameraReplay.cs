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
    public partial class FrmCameraReplay : HOSP.ExControl.BaseForm
    {
        private CameraReplay m_Replay = new CameraReplay();
        public FrmCameraReplay()
        {
            InitializeComponent();
         
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlaySel();
        }
        private void PlaySel()
        {
            List<DataGridViewRow> list = gvPager.GetCheckedRowList(0);
            if (list.Count == 0)
                return;
            VideoItem item = list[0].DataBoundItem as VideoItem;
            if (item == null)
                return;

            string path = item.Path;
            m_Replay.Init(path, (img, pos) =>
            {
                if (img == null)
                    return;
                this.Invoke(new MethodInvoker(delegate() {
                    pbCapture.Image = img;
                    if (pos > trackBar1.Maximum)
                    {
                        pos = trackBar1.Maximum;
                    }
                    trackBar1.Value = pos;
                    UpateState();
                }));
            });
            trackBar1.Minimum = 0;
            trackBar1.Maximum = m_Replay.GetImgCount();
            m_Replay.Play();
            UpateState();
            
        }
        private void UpateState()
        {
            lbState.Text = string.Format("视频总帧数：{0} 当前第{1}帧", trackBar1.Maximum, trackBar1.Value+1);   
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            m_Replay.Pause();
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            m_Replay.Resume();
        }

        private void CameraReplay_Load(object sender, EventArgs e)
        {
            gvPager.Init(btn_Prev, btn_Next, lbPage);
            gvPager.AddCheckBoxColumn(60, false, false, "播放", "", DataGridViewContentAlignment.MiddleLeft,
                ContentClick: (gv, ee) =>
                {
                    gv.ClearAllCheck();
                    pbCapture.Image = null;
                    return false;
                },
                CellMouseUp: (gv, cell, ee) => {
                    HOSP.ExControl.DataGridViewTransCheckBoxCell cb = cell as HOSP.ExControl.DataGridViewTransCheckBoxCell;
                    if (cb == null)
                        return;
                    if (cb.Checked)
                        PlaySel();
                }
                );
            gvPager.AddColumn("姓名", "Name", 120, false, DataGridViewContentAlignment.MiddleLeft);
            gvPager.AddColumn("位置", "Position", 70, false, DataGridViewContentAlignment.MiddleLeft);
            gvPager.AddColumn("路径", "Path", 500, false, DataGridViewContentAlignment.MiddleLeft).AutoSizeMode= DataGridViewAutoSizeColumnMode.Fill;
            Query(DateTime.Now);
        }
      

        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query(dateTimePicker1.Value);
        }
        private void Query(DateTime time)
        {
            if (String.IsNullOrWhiteSpace(CameraHelper.GetSavePath()))
                return;
            
            string position = string.Empty;
            if(rbCameraSelTop.Checked)
                position ="Top";
            if(rbCameraSelBottom.Checked)
                position ="Bottom";
            List<VideoItem> list = GetFileList(CameraHelper.GetSavePath() + "\\" + time.ToString("yyyyMMdd")).FindAll(vi => {
                if(position.Length==0)
                    return true;
                return vi.Position == position;
            }); 
            gvPager.DoPreBindFirstSource = () =>
            {
                gvPager.PagerSize = gvPager.Grid.Height / gvPager.Grid.RowTemplate.Height;
                gvPager.SetRecordCount(list.Count);
                return true;
            };

            gvPager.DoBindDataSource = () =>
            {
                gvPager.DataSource = list;
                return true;
            };
            gvPager.BindSource();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            m_Replay.SetPos(trackBar1.Value);
            UpateState();
        }


        public class VideoItem
        {
            public string Name { get; set; }
            public string Path { get; set; }
            public string Position { get; set; }
        }

        public List<VideoItem> GetFileList(string FindPath)
        {
            List<VideoItem> list = new List<VideoItem>();
            if (!Directory.Exists(FindPath))
            {
                return list;
            }
            DirectoryInfo directory = new DirectoryInfo(FindPath);
            DirectoryInfo[] directoryArray = directory.GetDirectories();
            foreach (DirectoryInfo diName in directoryArray)
            {
                //foreach (DirectoryInfo diPos in diName.GetDirectories())
                {
                    list.Add(new VideoItem() { Name = diName.Name, Path = diName.FullName, Position = diName.Name });
                }
            }
            return list;
        }

        private void rbCameraSelAll_CheckedChanged(object sender, EventArgs e)
        {
            Query(dateTimePicker1.Value);
        }

        private void rbCameraSelTop_CheckedChanged(object sender, EventArgs e)
        {
            Query(dateTimePicker1.Value);
        }

        private void rbCameraSelBottom_CheckedChanged(object sender, EventArgs e)
        {
            Query(dateTimePicker1.Value);
        }

        private void rbCameraSlow_CheckedChanged(object sender, EventArgs e)
        {
            m_Replay.Play(500);
        }

        private void rbCameraNormal_CheckedChanged(object sender, EventArgs e)
        {
            m_Replay.Play(300);
        }

        private void rbCameraFast_CheckedChanged(object sender, EventArgs e)
        {
            m_Replay.Play(200);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTestCamera_Click(object sender, EventArgs e)
        {
            FrmTestCamera frm = new FrmTestCamera();
            frm.Owner = this;
            frm.ShowDialog();
        }
   
    }
}
