using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AutoServiceManage.Model;

namespace AutoServiceManage.Presenter
{
    public class DoctorChoosePresenter
    {
        private DoctorChooseModel mDoctorChooseModel;

        public DoctorChoosePresenter()
        {
            mDoctorChooseModel = new DoctorChooseModel();
        }

        /// <summary>
        /// 按配置文件对医生排序
        /// </summary>
        /// <param name="dataSet"></param>
        public DataSet getNewSortDataSet(DataSet dataSet)
        {
            DataSet mNewDataSet = dataSet.Clone();

            DataTable mProfessorDataTable = dataSet.Tables[0].Clone();//专家
            DataTable mDoctorDataTable = dataSet.Tables[0].Clone();//普通
            DataTable mNormalDataTable = dataSet.Tables[0].Clone();//非专家、普通
            string mProfessorRegisterTypeIdConfig = SkyComm.getvalue("专家挂号");
            string mDoctorRegisterTypeIdConfig = SkyComm.getvalue("普通挂号");

            //专家集合
            if (!string.IsNullOrEmpty(mProfessorRegisterTypeIdConfig))
            {
                foreach (string s in mProfessorRegisterTypeIdConfig.Split('|'))
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        if (dataRow.RowState == DataRowState.Deleted)
                            continue;

                        string mRole = dataRow["ROLE"].ToString();//职称

                        string mDoctorId = dataRow["DOCTORID"].ToString();//医生id

                        string mRegisterTypeId = mDoctorChooseModel.getRegisterTypeId(mRole);



                        if (string.IsNullOrEmpty(mRegisterTypeId))
                        {
                            mNormalDataTable.ImportRow(dataRow);//加入到非专家、普通表
                            continue;
                        }

                        if (s.Equals(mRegisterTypeId))
                        {
                            mProfessorDataTable.ImportRow(dataRow);

                            dataRow.Delete();
                        }

                    }
                    dataSet.AcceptChanges();
                }

            }

            //普通集合
            if (!string.IsNullOrEmpty(mDoctorRegisterTypeIdConfig))
            {
                foreach (string s in mDoctorRegisterTypeIdConfig.Split('|'))
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        if (dataRow.RowState == DataRowState.Deleted)
                            continue;

                        string mRole = dataRow["ROLE"].ToString();//职称

                        string mDoctorId = dataRow["DOCTORID"].ToString();//医生id

                        string mRegisterTypeId = mDoctorChooseModel.getRegisterTypeId(mRole);



                        if (string.IsNullOrEmpty(mRegisterTypeId))
                        {
                            mNormalDataTable.ImportRow(dataRow);//加入到非专家、普通表
                            continue;
                        }

                        if (s.Equals(mRegisterTypeId))
                        {
                            mProfessorDataTable.ImportRow(dataRow);

                            dataRow.Delete();
                        }

                    }
                    dataSet.AcceptChanges();
                }

            }


            if (mProfessorDataTable == null || mProfessorDataTable.Rows.Count == 0)
            {
                return dataSet;
            }
            //将专家号先加入总集合
            foreach (DataRow mProfessorDataRow in mProfessorDataTable.Rows)
            {
                mNewDataSet.Tables[0].ImportRow(mProfessorDataRow);
            }


            foreach (DataRow mNormalRow in mNormalDataTable.Rows)
            {
                mNewDataSet.Tables[0].ImportRow(mNormalRow);
            }


            if (mNewDataSet == null || mNewDataSet.Tables[0].Rows.Count == 0)
            {
                return dataSet;

            }

            return mNewDataSet;
        }
    }
}
