using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoServiceManage.Common;
using AutoServiceManage.Model;

namespace AutoServiceManage.Presenter
{
    public class OfficeChoosePresenter
    {
        private OfficeChooseModel mOfficeChooseModel;

        private FrmOfficeChoose mFrmOfficeChoose;


        public OfficeChoosePresenter(FrmOfficeChoose mFrmOfficeChoose)
        {
            this.mFrmOfficeChoose = mFrmOfficeChoose;
        }

        public OfficeChoosePresenter()
        {
            mOfficeChooseModel = new OfficeChooseModel();
        }

        /// <summary>
        /// 根据科室ID查询科室名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string getOfficeNameByOfficeId(string id)
        {
            DataSet mDataSet = mOfficeChooseModel.getOfficeNameByOfficeId(id);

            if (mDataSet == null || mDataSet.Tables[0].Rows.Count == 0)
            {
                return "";
            }



            return mDataSet.Tables[0].Rows[0]["office"].ToString();
        }

        /// <summary>
        /// 获取二级科室table
        /// </summary>
        /// <param name="levelOneOfficeName"></param>
        /// <returns></returns>
        public DataTable getLevelTwoOfficeDataTable(string levelOneOfficeName)
        {
            List<string> mList = mOfficeChooseModel.getLevelTwoOfficeIDList(levelOneOfficeName);//二级科室id集合

            DataTable mTable = new DataTable();

            mTable.Columns.Add("officeName", typeof(string));
            mTable.Columns.Add("officeId", typeof(string));

            foreach (string s in mList)
            {
                string mName = getOfficeNameByOfficeId(s);

                DataRow mDataRow = mTable.NewRow();
                mDataRow["officeName"] = mName;
                mDataRow["officeId"] = s;

                mTable.Rows.Add(mDataRow);
            }

            return mTable;
        }

        public List<string> getAlphabetQueryFromLevelOne(string A,List<string> list)
        {
            List<string> mList = new List<string>();

            foreach (string s in list)
            {
                string pinYin = Essentials.StrConvertToPinyin(s);
                int first = pinYin.IndexOf(A);

                if (first == 0)
                {
                    mList.Add(s);
                }
            }

            return mList;
        }

        /// <summary>
        /// 二级科室拼音检索
        /// </summary>
        /// <param name="A"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public DataTable getAlphabetQueryFromLevelTwo(string A,DataTable dataTable)
        {
            DataTable mDataTable = new DataTable();

            mDataTable = dataTable.Clone();

            foreach (DataRow dataRow in dataTable.Rows)
            {
                string pinYin = Essentials.StrConvertToPinyin(dataRow["officeName"].ToString());

                int first = pinYin.IndexOf(A);

                if (first == 0)
                {
                    mDataTable.ImportRow(dataRow);
                }
            }
            return mDataTable;
        }

        /// <summary>
        /// 筛选二级科室
        /// </summary>
        /// <param name="levelOneOfficeName"></param>
        /// <param name="dataSet"></param>
        public DataSet fliterLevelTwoOfficeDataSet(string levelOneOfficeName,DataSet dataSet)
        {
            List<string> mList = mOfficeChooseModel.getLevelTwoOfficeIDList(levelOneOfficeName);//二级科室id集合

            DataSet mDataSet = dataSet.Clone();

            foreach (string s in mList)
            {

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    if (row["officeId"].ToString().Equals(s))
                    {
                        mDataSet.Tables[0].ImportRow(row);
                    }
                }
               
            }

            return mDataSet;

        }
    }
}
