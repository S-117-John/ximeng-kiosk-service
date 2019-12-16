using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceSDK.POSInterface.POS003.Dialogs
{
    public partial class FrmReadCard : Form
    {
        #region 构造函数
        public FrmReadCard()
        {
            InitializeComponent();
        }
        #endregion

        #region 绘制圆角
        private void SetWindowRegion(Control sender, int length, float tension)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddClosedCurve(new Point[]{
            new Point(0, sender.Height / length),
            new Point(sender.Width / length, 0),
            new Point(sender.Width - sender.Width / length, 0),
            new Point(sender.Width, sender.Height / length),
            new Point(sender.Width, sender.Height - sender.Height / length),
            new Point(sender.Width - sender.Width / length, sender.Height),
            new Point(sender.Width / length, sender.Height),
            new Point(0, sender.Height - sender.Height / length)}, tension);

            sender.Region = new Region(path);
        }

        private void FrmEnterPassword_Paint(object sender, PaintEventArgs e)
        {
            SetWindowRegion(this, 28, 0.1f);
        }
        #endregion

        private void lblCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
