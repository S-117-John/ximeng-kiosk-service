using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AutoServiceManage.Presenter
{
    public class TimeDetailChoosePresenter
    {
        public TimeDetailChoosePresenter()
        {
        }

        /// <summary>
        /// 数据重新排列
        /// </summary>
        public DataSet reArrayDatas(DataSet dataSet)
        {

            

            DateTime mNowTime = DateTime.Now;//

            DataTable mDataTable = dataSet.Tables[0].Clone();
            DataSet mDataSet = dataSet.Clone();
            
            List<string> mList = new List<string>();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                string mDetailTime = dataRow["DETAILTIME"].ToString();
                string exeDdate = Convert.ToDateTime(dataRow["EXECDATE"].ToString()).ToString("yyyy-MM-dd"); ;
                string mQueueId = dataRow["QUEUEID"].ToString();
                DateTime mDateTime = Convert.ToDateTime(exeDdate + " " + mDetailTime);

                if (mNowTime > mDateTime)
                {
                    //加入到过时的集合中
                    mDataTable.ImportRow(dataRow);                   
                    mList.Add(mQueueId);                   
                }
                else
                {
                    //未过时
                    mDataSet.Tables[0].ImportRow(dataRow);
                }
            }

            if (mDataSet == null || mDataSet.Tables[0].Rows.Count == 0)
            {
                string mLastTime1 = "";

                foreach (DataRow row in mDataTable.Rows)
                {
                    mLastTime1 = row["DETAILTIME"].ToString();
                }

                foreach (DataRow row in mDataTable.Rows)
                {
                    row["DETAILTIME"] = mLastTime1;
                    mDataSet.Tables[0].ImportRow(row);
                }

                return mDataSet;
            }

            if (mDataTable == null || mDataTable.Rows.Count == 0)
            {
                return dataSet;
            }

            //获取当前时间之后的号源最后一个序号
            int mLastNo = 0;
            //获取当前时间之后的号源最后一个时间
            string mLastTime = "";


            //foreach (DataRow dataRow in mDataSet.Tables[0].Rows)
            //{
            //    mLastNo = Convert.ToInt32(dataRow["QUEUEID"].ToString());
            //    mLastTime = dataRow["DETAILTIME"].ToString();
            //}
            //自助机预留现场号（1,2,3），若过时未被挂出，则甩到当前剩余号源的末尾
            try
            {
                var lastInfo = mDataSet.Tables[0].AsEnumerable().Select(a => new
                {
                    lastNo = a.Field<Int32>("QUEUEID"),
                    lastTime = a.Field<string>("DETAILTIME")
                }).OrderByDescending(b => b.lastNo).FirstOrDefault();
                mLastNo = lastInfo.lastNo;
                mLastTime = lastInfo.lastTime;
            }
            catch (Exception ex)
            {
                
                throw;
            }
          

         


            foreach (DataRow row in mDataTable.Rows)
            {
                mLastNo++;

                row["QUEUEID"] = mLastNo;
                row["DETAILTIME"] = mLastTime;
                mDataSet.Tables[0].ImportRow(row);
            }

            return mDataSet;
        }
    }
}
