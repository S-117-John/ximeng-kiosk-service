using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inc
{
    public partial class UcStarScore : UserControl
    {

        public string ItemScore { get; set; }

        public string ItemContentID { get; set; }
        public string ItemContent { get; set; }

        public delegate void StarClick();

        public event StarClick star_click;
        public UcStarScore()
        {
            InitializeComponent();
        }

        private void UcStarScore_Load(object sender, EventArgs e)
        {
            this.lblContent.Text = ItemContent;
            if (!ItemContent.Contains(":") && !ItemContent.Contains("："))
            {
                this.lblContent.Text = this.lblContent.Text + "：";
            }

            if (this.ItemScore.ToString().Trim() != "" || this.ItemScore.ToString().Trim() != "0")
            {
                int score = Convert.ToInt32(this.ItemScore.Trim());
                foreach (Control c in panel1.Controls)
                {
                    Label lblOther = (Label)c;
                    int curNo = Convert.ToInt32(lblOther.Name.ToString().Replace("star", ""));
                    if (curNo <= score)
                    {
                        lblOther.Text = "★";
                    }
                    else
                    {
                        lblOther.Text = "☆";
                    }
                }
            }
        }

        private void Star_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            int lblNo =Convert.ToInt32(lbl.Name.ToString().Replace("star", ""));

            foreach (Control c in panel1.Controls)
            {
                Label lblOther = (Label)c;
                int curNo = Convert.ToInt32(lblOther.Name.ToString().Replace("star", ""));
                if (curNo <= lblNo)
                {
                    lblOther.Text = "★";
                }
                else
                {
                    lblOther.Text = "☆";
                }
            }
            this.ItemScore = lblNo.ToString();

            star_click();
            //this.lblScore.Text = lblNo.ToString();

        }
        
    }
}
