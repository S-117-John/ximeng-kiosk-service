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
    public partial class UcWeekList : UserControl
    {
        public UcWeekList()
        {
            InitializeComponent();
        }

        private void WeekItem1_Click(object sender, EventArgs e)
        {
            try
            {
                WeekItem1.BackgroundImage = Image.FromFile(Application.StartupPath + "\\日期背景.png");
            }
            catch { WeekItem1.Visible = false; }

            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;

            chooseDate(WeekItem1.lblTodayDate.Text);
        }

        private void chooseDate(string chooseDate)
        {
           
        }
        private void UcWeekList_Load(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;

            WeekItem1.lblToday.Text = "今天";
            WeekItem1.lblTodayDate.Text = dateTime.Date.ToString("MM-dd");

            WeekItem2.lblToday.Text = Week(dateTime.AddDays(+1).DayOfWeek.ToString());
            WeekItem2.lblTodayDate.Text = dateTime.AddDays(+1).Date.ToString("MM-dd");

            WeekItem3.lblToday.Text = Week(dateTime.AddDays(+2).DayOfWeek.ToString());
            WeekItem3.lblTodayDate.Text = dateTime.AddDays(+2).Date.ToString("MM-dd");

            WeekItem4.lblToday.Text = Week(dateTime.AddDays(+3).DayOfWeek.ToString());
            WeekItem4.lblTodayDate.Text = dateTime.AddDays(+3).Date.ToString("MM-dd");

            WeekItem5.lblToday.Text = Week(dateTime.AddDays(+4).DayOfWeek.ToString());
            WeekItem5.lblTodayDate.Text = dateTime.AddDays(+4).Date.ToString("MM-dd");

            WeekItem6.lblToday.Text = Week(dateTime.AddDays(+5).DayOfWeek.ToString());
            WeekItem6.lblTodayDate.Text = dateTime.AddDays(+5).Date.ToString("MM-dd");

            WeekItem7.lblToday.Text = Week(dateTime.AddDays(+6).DayOfWeek.ToString());
            WeekItem7.lblTodayDate.Text = dateTime.AddDays(+6).Date.ToString("MM-dd");

            //默认当前选中
            try
            {
                WeekItem1.BackgroundImage = Image.FromFile("...\\Resources\\日期背景.png");
            }
            catch { WeekItem1.Visible = false; }

        }

        public string Week(string dayOfWeek)
        {
            string week;
            switch (dayOfWeek)
            {
                case "Sunday":
                    week = "周日";
                    break;
                case "Monday":
                    week = "周一";
                    break;
                case "Tuesday":
                    week = "周二";
                    break;
                case "Wednesday":
                    week = "周三";
                    break;
                case "Thursday":
                    week = "周四";
                    break;
                case "Friday":
                    week = "周五";
                    break;
                case "Saturday":
                    week = "周六";
                    break;
                default:
                    week = "未知";
                    break;
            }
            return week;
        }

        private void WeekItem2_Click(object sender, EventArgs e)
        {
            WeekItem2.BackgroundImage = Image.FromFile(Application.StartupPath + "\\日期背景.png");
            WeekItem1.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
        }

        private void WeekItem3_Click(object sender, EventArgs e)
        {
            try
            {
                WeekItem3.BackgroundImage = Image.FromFile(Application.StartupPath + "\\日期背景.png");
            }
            catch { WeekItem1.Visible = false; }

            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
        }

        private void WeekItem4_Click(object sender, EventArgs e)
        {
            try
            {
                WeekItem4.BackgroundImage = Image.FromFile(Application.StartupPath + "\\日期背景.png");
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
        }

        private void WeekItem5_Click(object sender, EventArgs e)
        {
            try
            {
                WeekItem5.BackgroundImage = Image.FromFile(Application.StartupPath + "\\日期背景.png");
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
        }

        private void WeekItem6_Click(object sender, EventArgs e)
        {
            try
            {
                WeekItem6.BackgroundImage = Image.FromFile(Application.StartupPath + "\\日期背景.png");
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem7.BackgroundImage = null;
        }

        private void WeekItem7_Click(object sender, EventArgs e)
        {
            try
            {
                WeekItem7.BackgroundImage = Image.FromFile(Application.StartupPath + "\\日期背景.png");
            }
            catch { WeekItem1.Visible = false; }
            WeekItem1.BackgroundImage = null;
            WeekItem2.BackgroundImage = null;
            WeekItem3.BackgroundImage = null;
            WeekItem4.BackgroundImage = null;
            WeekItem5.BackgroundImage = null;
            WeekItem6.BackgroundImage = null;
        }
    }
}
