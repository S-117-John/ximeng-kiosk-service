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
    public partial class ucBespeakSelectList : UserControl
    {
        public DataSet DataSource { get; set; }
        public event Action<decimal> moneyChanged;
        public decimal totalMoney;
        public ucBespeakSelectList()
        {
            InitializeComponent();
        }

        public void SetDataSource(DataSet ds)
        {
            if (!ds.Tables[0].Columns.Contains("SELECT"))
            {
                ds.Tables[0].Columns.Add("SELECT", typeof(bool));
            }
            for (int idx = 0; idx < ds.Tables[0].Rows.Count; idx++)
            {
                ds.Tables[0].Rows[idx]["SELECT"] = true;
                int inty = idx * 50;
                UcBespeakItem Newuc = new UcBespeakItem();
                Newuc.BackColor = System.Drawing.Color.Transparent;
                Newuc.Location = new System.Drawing.Point(0, inty);
                Newuc.Name = "ucBespeakItem" + idx;
                Newuc.Row = ds.Tables[0].Rows[idx];
                Newuc.Size = new System.Drawing.Size(681, 35);
                Newuc.TabIndex = 0;
                Newuc.SetData(ds.Tables[0].Rows[idx]);

                Newuc.moneyChanged += (money, isChecked) =>
                {
                    if (this.moneyChanged != null)
                    {
                        if (isChecked)
                            this.totalMoney += money;
                        else
                            this.totalMoney -= money;
                        this.moneyChanged(totalMoney);
                    }
                };
                this.Controls.Add(Newuc);
            }
        }

    }
}
