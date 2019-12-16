using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessFacade.His.CardClubManager;
using BusinessFacade.His.Register;
using EntityData.His.Register;
using Skynet.Framework.Common;
using SystemFramework.NewCommon;
using AutoServiceManage.Presenter;
using AutoServiceSDK.SdkService;
using System.Collections;
using BusinessFacade.His.Common;
using BusinessFacade.His.Clinic;
using System.Net;
using System.Net.Sockets;
using TiuWeb.ReportBase;

namespace AutoServiceManage.SystemManage
{
    public partial class FrmBalance : Form
    {
        private bool open;

        private SquareAccountsPresenter _mSquareAccountsPresenter;

        private string _mBeginTime;

        private string _mEndTime;

        public FrmBalance()
        {
            InitializeComponent();
            _mSquareAccountsPresenter = new SquareAccountsPresenter(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            QueryData();
        }

        private void FrmBalance_Load(object sender, EventArgs e)
        {
            QueryData();
            string startTime =  GetStartTime();

            label2.Text = "起始时间：" + startTime;

            label4.Text = "终止时间：" + DateTime.Now.ToLocalTime().ToString();


        }

        private void QueryData()
        {
            _mSquareAccountsPresenter.GetBeginTime();

            label2.Text = "起始时间：" + SquareAccountsPresenter.MBeginTime;

            label4.Text = "终止时间：" + SquareAccountsPresenter.MEndTime;

            DataSet dataPre = _mSquareAccountsPresenter.GetPreData(SysOperatorInfo.OperatorID, Convert.ToDateTime(SquareAccountsPresenter.MBeginTime), Convert.ToDateTime(SquareAccountsPresenter.MEndTime), 1, true);
            //gridControl1.DataSource = dataPre.Tables[0];

            int mAmount = _mSquareAccountsPresenter.getTotalBankCardTransactions(SysOperatorInfo.OperatorID,
                Convert.ToDateTime(SquareAccountsPresenter.MBeginTime),
                Convert.ToDateTime(SquareAccountsPresenter.MEndTime));

            label7.Text = mAmount.ToString();

            backgroundWorker1.RunWorkerAsync();

            AutoInMoneyRecordFacade theAutoInMoneyRecordFacade = new AutoInMoneyRecordFacade();
            DataSet dsInmoney = theAutoInMoneyRecordFacade.GetInMoneyInfoByOperatorID(AutoHostConfig.Machineno, SysOperatorInfo.OperatorID, DateTime.Today);
           
            lblInMoney.Text = dsInmoney.Tables[0].Compute("SUM(INMONEY)", "").ToString();
            gdcInmoney.DataSource = dsInmoney.Tables[0];
                        
            CardSavingFacade theCardSavingFacade = new CardSavingFacade();
            DataSet dsCardSaving = theCardSavingFacade.GetAddMoneyRecord(DateTime.Today, SysOperatorInfo.OperatorID,"现金");
            //foreach (DataRow row in dsInmoney.Tables[0].Rows)
            //{
            //    int intlenght = row["CARDNO"].ToString().Length;
            //    if (intlenght > 10)
            //    {
            //        row["CARDNO1"] = row["CARDNO"].ToString().Substring(0, 6) + "*".PadLeft(intlenght - 10) + row["CARDNO"].ToString().Substring(intlenght - 4, 4);
            //    }
            //}
            //gdcAddMoney.DataSource = dsCardSaving.Tables[0];
            lblCardSaving.Text = dsCardSaving.Tables[0].Compute("SUM(ADDMONEY)", "").ToString();

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string s = _mSquareAccountsPresenter.mGetCardMakeMoney(SysOperatorInfo.OperatorID,
                    Convert.ToDateTime(SquareAccountsPresenter.MBeginTime),
                    Convert.ToDateTime(SquareAccountsPresenter.MEndTime));

                e.Result = s;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw exception;
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                SkyComm.ShowMessageInfo(e.Error.Message);
            }
            else
            {
                label8.Text = (string)e.Result;
            }
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();
            DetailAccountFacade detailAccountFacade = new DetailAccountFacade();
            string mechinNo = string.Empty;
            try
            {
                #region 获取ip
                string ipAddress = null;
                try
                {
                    string hostName = Dns.GetHostName();
                    IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                    for (int i = 0; i < ipEntry.AddressList.Length; i++)
                    {
                        if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = ipEntry.AddressList[i].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    SkyComm.ShowMessageInfo(ex.Message);
                    return;
                }

                #endregion
                #region 获取机器码
                string sql1 = "select * from T_AUTOSERVICEMACHINE_INFO where IPADDRESS = @IPADDRESS";
                Hashtable hashtable1 = new Hashtable();
                hashtable1.Add("@IPADDRESS", ipAddress);
                DataSet dataSet = querySolutionFacade.ExeQuery(sql1, hashtable1);
               
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    mechinNo = dataSet.Tables[0].Rows[0]["MACHINENO"].ToString();
                }
                #endregion


                #region 判断结算


                Hashtable hashtablejs = new Hashtable();

                string sqljs = "select * from SETTLEMENT_RECORD where MECHINNO = @MECHINNO";// and BANKSTATE = '1'
                hashtablejs.Add("@MECHINNO", mechinNo);



                DataSet dataSet1 = querySolutionFacade.ExeQuery(sqljs, hashtablejs);

                if (dataSet1.Tables[0].Rows.Count>0)
                {

                    if (dataSet1.Tables[0].Rows[0]["SETTLEMENT_TIME"].ToString().Equals(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        SkyComm.ShowMessageInfo("此台机器已经结算过");
                        return;
                    }
                    else
                    {
                       
                    }

                    
                    
                }

                #endregion



                #region 获取结算时间
                string MBeginTime = DateTime.Now.ToLocalTime().ToString(); ;
                string MEndTime = DateTime.Now.ToLocalTime().ToString(); ;

                string sql3 = "select * from SETTLEMENT_TIME where M_NO = @M_NO";
                Hashtable hashtable3 = new Hashtable();
                hashtable3.Add("@M_NO", mechinNo);
                DataSet dataSet3 = querySolutionFacade.ExeQuery(sql3, hashtable3);
                if (dataSet3.Tables[0].Rows.Count>0)
                {
                    MBeginTime = dataSet3.Tables[0].Rows[0]["TIMES"].ToString();
                }



                #endregion


                #region 获取现金记录
                string sql2 = "select * from T_AUTOINMONEY_RECORD where MACHINENO = @MACHINENO and OPERATORTIME between @starttime and @endtime";
                Hashtable hashtable2 = new Hashtable();
                hashtable2.Add("@MACHINENO", mechinNo);
                
                hashtable2.Add("@starttime", MBeginTime);
                hashtable2.Add("@endtime", MEndTime);
                DataSet dataSet2 = querySolutionFacade.ExeQuery(sql2, hashtable2);

                if (dataSet2.Tables[0].Rows.Count==0)
                {
                    SkyComm.ShowMessageInfo("此台机器没有结算信息，已经记录结算时间");

                }
                else
                {
                    dataSet2.WriteXml(Application.StartupPath + @"\\ReportXml\\自助机结算.xml");
                    string path = Application.StartupPath + @"\\Reports\\自助机结算.frx";
                    PrintManager print = new PrintManager();
                    print.InitReport("自助机结算");
                    print.AddParam("结算时间", DateTime.Now.ToLocalTime().ToString());
                    print.AddData(dataSet2.Tables[0], "report");
                    PrintManager.CanDesign = true;
                    print.Print();
                    print.Dispose();
                }

               
                #endregion
            }
            catch (Exception e1)
            {
                SkyComm.ShowMessageInfo(e1.Message);
                return;

            }


            #region 记录结算
           


            DataSet data = new DataSet();

            Hashtable hashtable = new Hashtable();

            string mSql = "delete from SETTLEMENT_RECORD where MECHINNO = @MECHINNO";// and BANKSTATE = '1'
            hashtable.Add("@MECHINNO", mechinNo);



            querySolutionFacade.ExeNonQuery(mSql, hashtable);


            

            string sql = "insert into SETTLEMENT_RECORD values (@SETTLEMENT_TIME,'已结算',@MECHINNO);";
            hashtable.Add("@SETTLEMENT_TIME", DateTime.Now.ToString("yyyy-MM-dd"));
          
            
            data = querySolutionFacade.ExeQuery(sql, hashtable);
            #endregion


            #region 记录结算时间
            string sql4 = "delete from SETTLEMENT_TIME where M_NO = @M_NO";
            Hashtable hashtable4 = new Hashtable();
            hashtable4.Add("@M_NO", mechinNo);
            int a = querySolutionFacade.ExeNonQuery(sql4, hashtable4);
            string sql5 = "insert into SETTLEMENT_TIME values (@TIMES,@M_NO)";
            Hashtable hashtable5 = new Hashtable();
            hashtable5.Add("@TIMES", DateTime.Now.ToLocalTime().ToString());
            hashtable5.Add("@M_NO", mechinNo);
            int b = querySolutionFacade.ExeNonQuery(sql5, hashtable5);

            #endregion



            SkyComm.ShowMessageInfo("现金结算成功");

            //_mSquareAccountsPresenter = new SquareAccountsPresenter(this);

            //_mSquareAccountsPresenter.GetSquareAccount();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Common_XH theCommon_XH = new Common_XH();
            if (simpleButton1.Text.Equals("打开钞箱"))
            {
                theCommon_XH.Doorlock(2, 1);

                simpleButton1.Text = "关闭钞箱";

            }
            else
            {
                theCommon_XH.Doorlock(2, 0);
                simpleButton1.Text = "打开钞箱";
            }
        }



        public string GetStartTime()
        {
            QuerySolutionFacade querySolutionFacade = new QuerySolutionFacade();
            DetailAccountFacade detailAccountFacade = new DetailAccountFacade();
            string mechinNo = string.Empty;

            #region 获取ip
            string ipAddress = null;
            try
            {
                string hostName = Dns.GetHostName();
                IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
                for (int i = 0; i < ipEntry.AddressList.Length; i++)
                {
                    if (ipEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        ipAddress = ipEntry.AddressList[i].ToString();
                    }
                }

            }
            catch (Exception ex)
            {

            }

            #endregion
            #region 获取机器码
            string sql1 = "select * from T_AUTOSERVICEMACHINE_INFO where IPADDRESS = @IPADDRESS";
            Hashtable hashtable1 = new Hashtable();
            hashtable1.Add("@IPADDRESS", ipAddress);
            DataSet dataSet = querySolutionFacade.ExeQuery(sql1, hashtable1);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                mechinNo = dataSet.Tables[0].Rows[0]["MACHINENO"].ToString();
            }
            string MBeginTime = DateTime.Now.ToLocalTime().ToString(); ;
            string MEndTime = DateTime.Now.ToLocalTime().ToString(); ;

            string sql3 = "select * from SETTLEMENT_TIME where M_NO = @M_NO";
            Hashtable hashtable3 = new Hashtable();
            hashtable3.Add("@M_NO", mechinNo);
            DataSet dataSet3 = querySolutionFacade.ExeQuery(sql3, hashtable3);
            if (dataSet3.Tables[0].Rows.Count > 0)
            {
                MBeginTime = dataSet3.Tables[0].Rows[0]["TIMES"].ToString();
            }
            return MBeginTime;
            #endregion
        }
    }
}
