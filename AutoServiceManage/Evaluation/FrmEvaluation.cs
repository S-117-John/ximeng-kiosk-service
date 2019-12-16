using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceSDK.SDK;
using Skynet.Framework.Common;
using BusinessFacade.Common;
using BusinessFacade.His.Common;
using EntityData.His.Common;

namespace AutoServiceManage.Evaluation
{
    public partial class FrmEvaluation : Form
    {

        #region 自定义变量
        private static int ItemCountInPage = 5;

        private int CurrentPageNum = 1;

        private int AllPageNum = 1;

        private DataTable dtEvaluation = new DataTable();
        #endregion


        #region 构造函数及LOAD
        public FrmEvaluation()
        {
            InitializeComponent();
        }
        DateTime dtBort = new DateTime();
        private void FrmprintClinicEmrNote_Load(object sender, EventArgs e)
        {
            this.ucTime1.Sec = 90;
            this.ucTime1.timer1.Start();
            if (checkEvaluation())
            {
                this.lblPatient.Text = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["PATIENTNAME"].ToString();
                string sex = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["SEX"].ToString();
                if (sex != "")
                {
                    if (sex == "男")
                        this.lblPatient.Text = this.lblPatient.Text + "先生";
                    else
                        this.lblPatient.Text = this.lblPatient.Text + "女士";
                }
                if (!BindData())
                {
                    this.Close();
                }
            }
            else
            {
                Close();
            }
        }
        #endregion

        #region 数据绑定及处理
        private bool BindData()
        {
            try
            {
                TEvaluationTemplateFacade facade = new TEvaluationTemplateFacade();

                DataSet dsEvaluation = facade.GetEvaluationTemp();//加载满意度调查信息
                if (dsEvaluation == null || dsEvaluation.Tables.Count == 0 || dsEvaluation.Tables[0].Rows.Count == 0)
                {
                    SkyComm.ShowMessageInfo("未找到满意度调查信息！");
                    return false;
                }

                dtEvaluation = dsEvaluation.Tables[0];
                //if (!dtEvaluation.Columns.Contains("CurrentScore"))
                //{
                //    dtEvaluation.Columns.Add("CurrentScore");
                //    foreach (DataRow dr in dtEvaluation.Rows)
                //    {
                //        dr["CurrentScore"] = dr["DEFAULTSCORE"].ToString();
                //    }
                //}

                AllPageNum = dtEvaluation.Rows.Count / ItemCountInPage;
                if (dtEvaluation.Rows.Count % ItemCountInPage != 0)
                    AllPageNum++;

                BindUC();
                return true;
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("加载满意度调查信息失败：" + ex.Message);
                SkyComm.ShowMessageInfo("加载满意度调查信息失败！");
                return false;
            }
        }

        private void BindUC()
        {
            this.lblLast.Enabled = true;
            this.lblNext.Enabled = true;
            panel1.Controls.Clear();
            if (CurrentPageNum == 1)
                this.lblLast.Enabled = false;
            if (CurrentPageNum == AllPageNum)
                this.lblNext.Enabled = false;
            int StartIndex = 0;
            if (CurrentPageNum != 1)
                StartIndex=(CurrentPageNum - 1) * ItemCountInPage;
            int endIndex = StartIndex + ItemCountInPage - 1;

            int locationCount = 0;
            for (int i = StartIndex; i <=endIndex; i++)
            {
                if (i >= dtEvaluation.Rows.Count)
                    break;
                Inc.UcStarScore ucStar = new Inc.UcStarScore();
                ucStar.ItemContentID = dtEvaluation.Rows[i]["EVALUATIONID"].ToString();
                ucStar.ItemScore = dtEvaluation.Rows[i]["DEFAULTSCORE"].ToString();
                ucStar.ItemContent = dtEvaluation.Rows[i]["EVALUATIONCONTENT"].ToString();
                ucStar.star_click += UcStar_star_click;
                ucStar.Location = new Point(0, locationCount * ucStar.Height);
                panel1.Controls.Add(ucStar);
                locationCount++;
            }
        }

        private bool checkEvaluation()
        {
            try
            {
                TEvaluationResultFacade facade = new TEvaluationResultFacade();
                bool flag = facade.CheckEvaluation(SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString());
                if (!flag)
                {
                    SkyComm.ShowMessageInfo("您今日已提交满意度调查信息，满意度调查一天只可提交一次。谢谢合作！");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        private void UcStar_star_click()
        {
            InitTime();
        }
        private void InitTime()
        {
            this.ucTime1.Sec = 90;
        }

        private void GetScore()
        {
            foreach (Control c in panel1.Controls)
            {
                Inc.UcStarScore ucStar = (Inc.UcStarScore)c;
                foreach (DataRow dr in dtEvaluation.Rows)
                {
                    if (dr["EVALUATIONID"].ToString() == ucStar.ItemContentID)
                    {
                        dr["DEFAULTSCORE"] = ucStar.ItemScore;
                    }
                }
            }
        }
        #endregion

        #region 返回，退出
        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            SkyComm.CloseWin(this);
        }
        #endregion

        private void lblNext_Click(object sender, EventArgs e)
        {
            InitTime();
            GetScore();
            if (CurrentPageNum < AllPageNum)
                CurrentPageNum++;
            this.BindUC();
        }

        private void lblLast_Click(object sender, EventArgs e)
        {
            InitTime();
            GetScore();
            if (CurrentPageNum != 1)
                CurrentPageNum--;
            this.BindUC();
        }

        private void lblOK_Click(object sender, EventArgs e)
        {
            try
            {
                GetScore();
                TEvaluationResultData entity = new TEvaluationResultData();
                TEvaluationResultFacade facade = new TEvaluationResultFacade();
                foreach (DataRow dr in dtEvaluation.Rows)
                {
                    entity.Evaluationid = dr["EVALUATIONID"].ToString();
                    entity.Resultid = Guid.NewGuid().ToString();
                    entity.Resultscore = Convert.ToInt32(dr["DEFAULTSCORE"].ToString());
                    entity.Diagnoseid = SkyComm.eCardAuthorizationData.Tables[0].Rows[0]["DIAGNOSEID"].ToString();
                    entity.Operatetime = DateTime.Now;
                    facade.Insert(entity);
                }
                SkyComm.ShowMessageInfo("满意度调查信息提交成功，谢谢！");
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalInfoMessage("满意度调查保存失败：" + ex.Message);
                SkyComm.ShowMessageInfo("保存满意度调查信息失败！");
            }
            finally
            {
                SkyComm.CloseWin(this);
            }
        }
    }
}
