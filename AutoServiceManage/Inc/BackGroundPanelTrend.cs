using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inc
{
    public class BackGroundPanelTrend : Panel
    {
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            return;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            if (this.BackgroundImage != null)
            {

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                e.Graphics.DrawImage(this.BackgroundImage, new System.Drawing.Rectangle(0, 0, this.Width, this.Height),
                0, 0, this.BackgroundImage.Width, this.BackgroundImage.Height,
                System.Drawing.GraphicsUnit.Pixel);

            }
            base.OnPaint(e);
        }

        /// <summary>
        /// 防止闪烁panel
        /// </summary>
        public BackGroundPanelTrend()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
        }

        public BackGroundPanelTrend(IContainer container)
        {
            container.Add(this);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackGroundPanelTrend));
            this.SuspendLayout();
            // 
            // BackGroundPanelTrend
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ResumeLayout(false);

        }
    }
}
