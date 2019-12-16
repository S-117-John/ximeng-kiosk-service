using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessFacade.His.Common;

namespace AutoServiceManage.Model
{
    public class OfficeChooseModel
    {
        public OfficeChooseModel()
        {
        }

        /// <summary>
        /// 获取一级科室菜单名
        /// </summary>
        /// <returns></returns>
        public List<string> getTotalOfficeNameList()
        {
            List<string> mList = new List<string>();

            string mOfficeIdConfig = SkyComm.getvalue("预约挂号一级科室分类");

            string[] officeNames = mOfficeIdConfig.Split('|');

            foreach (string officeName in officeNames)
            {

                string mSecondName = SkyComm.getvalue(officeName);

                if (!string.IsNullOrEmpty(mSecondName))
                {
                    mList.Add(officeName);
                }


            }


            return mList;
        }

        /// <summary>
        /// 根据一级科室名称找到二级科室id集合
        /// </summary>
        /// <param name="levelOneOfficeName"></param>
        /// <returns></returns>
        public List<string> getLevelTwoOfficeIDList(string levelOneOfficeName)
        {
            List<string> mList = new List<string>();

            string ids = SkyComm.getvalue(levelOneOfficeName);

            string[] leveTwoOfficeIds = ids.Split('|');

            foreach (string leveTwoOfficeId in leveTwoOfficeIds)
            {
                mList.Add(leveTwoOfficeId);
            }

            return mList;
        }

        /// <summary>
        /// 根据科室ID找科室名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet getOfficeNameByOfficeId(string id)
        {
            OfficeFacade mOfficeFacade = new OfficeFacade();

            DataSet mDataSet = new DataSet();
            mDataSet = mOfficeFacade.FindAllNameByOfficeID(id);
            return mDataSet;

        }
    }
}
