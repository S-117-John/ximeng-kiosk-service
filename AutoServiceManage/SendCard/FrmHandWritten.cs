using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using Microsoft.Ink;

namespace AutoServiceManage.SendCard
{
    public partial class FrmHandWritten : Form
    {
        RecognizerContext rct;
        string FullCACText;
        public string ChildName { get; set; }
        public FrmHandWritten()
        {
            InitializeComponent();
        }

        private void FrmHandWritten_Load(object sender, EventArgs e)
        {
            ink_();
            this.rct.RecognitionWithAlternates += new RecognizerContextRecognitionWithAlternatesEventHandler(rct_RecognitionWithAlternates);
            rct.Strokes = PictureboxInk.Ink.Strokes;

            this.lblInput.Text = ChildName;
        }

        void rct_RecognitionWithAlternates(object sender, RecognizerContextRecognitionWithAlternatesEventArgs e)
        {

            string ResultString = "";
            RecognitionAlternates alts;

            if (e.RecognitionStatus == RecognitionStatus.NoError)
            {
                alts = e.Result.GetAlternatesFromSelection();

                foreach (RecognitionAlternate alt in alts)
                {
                    ResultString = ResultString + alt.ToString() + " ";
                }
            }
            FullCACText = ResultString.Trim();
            Control.CheckForIllegalCrossThreadCalls = false;
            returnString(FullCACText);
            Control.CheckForIllegalCrossThreadCalls = true;

        }
        private void returnString(string str)
        {
            string[] str_temp = str.Split(' ');
            string str_temp1 = "lbl";
            string str_temp2 = "";
            if (str_temp.Length > 0)
            {
                for (int i = 0; i < 7; i++)
                {
                    str_temp2 = str_temp1 + i.ToString();
                    Control[] con_temp = Controls.Find(str_temp2, true);
                    if (con_temp.Length > 0)
                    {
                        ((Label)(con_temp[0])).Text = str_temp[i];
                    }
                }
            }
        }
        void ic_Stroke(object sender, InkCollectorStrokeEventArgs e)
        {
            rct.StopBackgroundRecognition();
            rct.Strokes.Add(e.Stroke);
            //rct.CharacterAutoCompletion = RecognizerCharacterAutoCompletionMode.Prefix;
            rct.BackgroundRecognizeWithAlternates(0);
        }
        private void ink_()
        {
            Recognizers recos = new Recognizers();
            Recognizer chineseReco = recos.GetDefaultRecognizer();
            //Recognizer chineseReco = recos.GetDefaultRecognizer(1033);
            rct = chineseReco.CreateRecognizerContext();
        }
        private void lbl0_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if (lbl.Text != "")
            {
                this.lblInput.Text += lbl.Text;
                lblClear_Click(null, null);
            }
        }
        private void lblOk_Click(object sender, EventArgs e)
        {
            if (this.lblInput.Text.Length == 0)
                return;

            this.ChildName = this.lblInput.Text;
            DialogResult = DialogResult.OK;
        }

        private void lblCancel_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            if (!PictureboxInk.CollectingInk)
            {
                Strokes strokesToDelete = PictureboxInk.Ink.Strokes;
                rct.StopBackgroundRecognition();
                PictureboxInk.Ink.DeleteStrokes(strokesToDelete);
                rct.Strokes = PictureboxInk.Ink.Strokes;
                PictureboxInk.Ink.DeleteStrokes();//清除手写区域笔画;
                PictureboxInk.Refresh();//刷新手写区域
            }
        }

        private void lblDelete_Click(object sender, EventArgs e)
        {
            if (lblInput.Text.Length > 0)
            {
                string tempStr = lblInput.Text.Substring(0, lblInput.Text.Length - 1);
                lblInput.Text = tempStr;
            }
        }

        #region 窗体圆角实现
        public void SetWindowRegion()
        {

            System.Drawing.Drawing2D.GraphicsPath FormPath;

            FormPath = new System.Drawing.Drawing2D.GraphicsPath();

            Rectangle rect = new Rectangle(0, 22, this.Width, this.Height - 22);//this.Left-10,this.Top-10,this.Width-10,this.Height-10);                 

            FormPath = GetRoundedRectPath(rect, 50);

            this.Region = new Region(FormPath);

        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {

            int diameter = radius;

            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));

            GraphicsPath path = new GraphicsPath();

            //   左上角   

            path.AddArc(arcRect, 180, 90);

            //   右上角   

            arcRect.X = rect.Right - diameter;

            path.AddArc(arcRect, 270, 90);

            //   右下角   

            arcRect.Y = rect.Bottom - diameter;

            path.AddArc(arcRect, 0, 90);

            //   左下角   

            arcRect.X = rect.Left;

            path.AddArc(arcRect, 90, 90);

            path.CloseFigure();

            return path;

        }

        protected override void OnResize(System.EventArgs e)
        {
            Type(this, 15, 0.08);
        }
        #endregion 窗体圆角实现

        #region 圆角效果
        private void SetFromCircle()
        {
            int radian = 7;
            int w = this.Width;
            int h = this.Height;

            Point p1 = new Point(radian, 0);
            Point p2 = new Point(w - radian, 0);
            Point p3 = new Point(w, radian);
            Point p4 = new Point(w, h - radian);
            Point p5 = new Point(w - radian, h);
            Point p6 = new Point(radian, h);
            Point p7 = new Point(0, h - radian);
            Point p8 = new Point(0, radian);

            GraphicsPath shape = new GraphicsPath();

            Point[] p = new Point[] { p1, p2, p3, p4, p5, p6, p7, p8 };
            shape.AddPolygon(p);
            this.Region = new Region(shape);
        }

        private void Type(Control sender, int p_1, double p_2)
        {

            int radian = 25;
            int w = this.Width;
            int h = this.Height;

            Point p1 = new Point(radian, 0);
            Point p2 = new Point(w - radian, 0);
            Point p3 = new Point(w, radian);
            Point p4 = new Point(w, h - radian);
            Point p5 = new Point(w - radian, h);
            Point p6 = new Point(radian, h);
            Point p7 = new Point(0, h - radian);
            Point p8 = new Point(0, radian);

            GraphicsPath gp = GetRoundedRectPath(sender.ClientRectangle, 20);
            GraphicsPath shape = new GraphicsPath();

            Point[] p = new Point[] { p1, p2, p3, p4, p5, p6, p7, p8 };
            shape.AddPolygon(p);
            this.Region = new Region(gp);

        }
        #endregion
    }
}
