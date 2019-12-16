namespace AutoServiceManage.Inc
{
    partial class UcStarScore
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblContent = new System.Windows.Forms.Label();
            this.star1 = new System.Windows.Forms.Label();
            this.star2 = new System.Windows.Forms.Label();
            this.star3 = new System.Windows.Forms.Label();
            this.star4 = new System.Windows.Forms.Label();
            this.star5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblEvaluationID = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblContent
            // 
            this.lblContent.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(192)))), ((int)(((byte)(144)))));
            this.lblContent.Location = new System.Drawing.Point(-4, 3);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(360, 49);
            this.lblContent.TabIndex = 0;
            this.lblContent.Text = "评分评分评分：";
            this.lblContent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // star1
            // 
            this.star1.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.star1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(192)))), ((int)(((byte)(144)))));
            this.star1.Location = new System.Drawing.Point(4, 9);
            this.star1.Name = "star1";
            this.star1.Size = new System.Drawing.Size(40, 32);
            this.star1.TabIndex = 1;
            this.star1.Text = "★";
            this.star1.Click += new System.EventHandler(this.Star_Click);
            // 
            // star2
            // 
            this.star2.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.star2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(192)))), ((int)(((byte)(144)))));
            this.star2.Location = new System.Drawing.Point(50, 9);
            this.star2.Name = "star2";
            this.star2.Size = new System.Drawing.Size(40, 32);
            this.star2.TabIndex = 2;
            this.star2.Text = "★";
            this.star2.Click += new System.EventHandler(this.Star_Click);
            // 
            // star3
            // 
            this.star3.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.star3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(192)))), ((int)(((byte)(144)))));
            this.star3.Location = new System.Drawing.Point(96, 9);
            this.star3.Name = "star3";
            this.star3.Size = new System.Drawing.Size(40, 32);
            this.star3.TabIndex = 3;
            this.star3.Text = "★";
            this.star3.Click += new System.EventHandler(this.Star_Click);
            // 
            // star4
            // 
            this.star4.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.star4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(192)))), ((int)(((byte)(144)))));
            this.star4.Location = new System.Drawing.Point(142, 9);
            this.star4.Name = "star4";
            this.star4.Size = new System.Drawing.Size(40, 32);
            this.star4.TabIndex = 4;
            this.star4.Text = "★";
            this.star4.Click += new System.EventHandler(this.Star_Click);
            // 
            // star5
            // 
            this.star5.Font = new System.Drawing.Font("宋体", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.star5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(192)))), ((int)(((byte)(144)))));
            this.star5.Location = new System.Drawing.Point(188, 9);
            this.star5.Name = "star5";
            this.star5.Size = new System.Drawing.Size(40, 32);
            this.star5.TabIndex = 5;
            this.star5.Text = "★";
            this.star5.Click += new System.EventHandler(this.Star_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.star5);
            this.panel1.Controls.Add(this.star1);
            this.panel1.Controls.Add(this.star2);
            this.panel1.Controls.Add(this.star3);
            this.panel1.Controls.Add(this.star4);
            this.panel1.Location = new System.Drawing.Point(341, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(358, 48);
            this.panel1.TabIndex = 6;
            // 
            // lblEvaluationID
            // 
            this.lblEvaluationID.AutoSize = true;
            this.lblEvaluationID.Location = new System.Drawing.Point(8, 19);
            this.lblEvaluationID.Name = "lblEvaluationID";
            this.lblEvaluationID.Size = new System.Drawing.Size(0, 12);
            this.lblEvaluationID.TabIndex = 7;
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(12, 44);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(0, 12);
            this.lblScore.TabIndex = 8;
            // 
            // UcStarScore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblEvaluationID);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblContent);
            this.Name = "UcStarScore";
            this.Size = new System.Drawing.Size(724, 54);
            this.Load += new System.EventHandler(this.UcStarScore_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label star1;
        private System.Windows.Forms.Label star2;
        private System.Windows.Forms.Label star3;
        private System.Windows.Forms.Label star4;
        private System.Windows.Forms.Label star5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblEvaluationID;
        private System.Windows.Forms.Label lblScore;
    }
}
