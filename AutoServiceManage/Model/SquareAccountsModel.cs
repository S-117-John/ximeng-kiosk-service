using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BusinessFacade.His.CardClubManager;

namespace AutoServiceManage.Model
{
    public class SquareAccountsModel
    {
        public SquareAccountsModel()
        {
        }

        /// <summary>
        /// 发卡工本费
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataSet mGetCardMakeMoney(string operatorID, DateTime startTime, DateTime endTime)
        {
            CardSavingFacade cardSavingFacade = new CardSavingFacade();
            return cardSavingFacade.GetCardMakeMoney(operatorID, startTime, endTime);
        }

        /// <summary>
        /// 银行卡交易统计
        /// </summary>
        /// <param name="operatorID"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataSet getTotalBankCardTransactions(string operatorID, DateTime startTime, DateTime endTime)
        {
            CardSavingFacade cardSavingFacade = new CardSavingFacade();
            return cardSavingFacade.getTotalBankCardTransactions(operatorID, startTime, endTime);
        }
    }
}
