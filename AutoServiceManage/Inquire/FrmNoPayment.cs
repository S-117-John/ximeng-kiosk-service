using BusinessFacade.His.ClinicDoctor;
using Skynet.Framework.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoServiceManage.Inquire
{
    public partial class FrmNoPayment : Form
    {
        public FrmNoPayment()
        {
            InitializeComponent();
        }

        public DataSet dsRecipe { get; set; }

        private void lblCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmNoPayment_Load(object sender, EventArgs e)
        {
            //查询未缴费的处方信息
            ClinicPhysicianRecipeFacade clinicPhysicianRecipeFacade = new ClinicPhysicianRecipeFacade();
            DataSet dsRecipe = clinicPhysicianRecipeFacade.FindRecipeDetailByDiagnoseID(SkyComm.DiagnoseID);
            if (dsRecipe.Tables[0].Rows.Count == 0)
            {
                this.gdcMain.DataSource = null;
                return;
            }

            dsRecipe.Tables[0].Columns.Add("PRICECHANGE", typeof(decimal));  //单价
            dsRecipe.Tables[0].Columns.Add("AMOUNTCHANGE", typeof(decimal));  //数量
            dsRecipe.Tables[0].Columns.Add("UNITCHANGE", typeof(string));  //单位
            dsRecipe.Tables[0].Columns.Add("PITCHON1", typeof(bool));  //单位

            UnitToPack unitToPack = new UnitToPack();
            foreach (DataRow item in dsRecipe.Tables[0].Rows)
            {
                item["PITCHON1"] = Convert.ToBoolean(item["PITCHON"]);
                if (item["OUTPATIENTUNIT"].ToString().Trim() == "包装")
                {
                    item.BeginEdit();
                    int amount = Convert.ToInt32(item["AMOUNT"]);
                    int changeratio = Convert.ToInt32(item["CHANGERATIO"]);
                    item["AMOUNTCHANGE"] = unitToPack.GetPackAmount(Convert.ToInt32(item["CHANGERATIO"]), Convert.ToInt32(item["AMOUNT"]));
                    item["UNITCHANGE"] = item["PACK"];
                    item["PRICECHANGE"] = Convert.ToDecimal(item["UNITPRICE"]) * changeratio;
                    item.EndEdit();
                }
                else
                {
                    item.BeginEdit();
                    item["PRICECHANGE"] = item["UNITPRICE"];
                    item["AMOUNTCHANGE"] = item["AMOUNT"];
                    item["UNITCHANGE"] = item["UNIT"];
                    item.EndEdit();
                }
            }

            this.gdcMain.DataSource = dsRecipe.Tables[0].Select("", "CLINICRECIPEID").CopyToDataTable();

        }
    }
}
