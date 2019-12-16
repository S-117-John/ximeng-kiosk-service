using System;

namespace AutoServiceSDK.POSInterface
{
    /// <summary>
    /// 摘要：
    ///     POS 接口交易类型。
    ///     
    /// 说明：
    ///     该结构用来规范 POS 接口交易操作类型。
    /// </summary>
    public struct POSTransType
    {
        /// <summary>
        /// 设备初始化
        /// </summary>
        public const string TRANS_EQPT_INIT = "0";

        /// <summary>
        /// 开始支付（消费）
        /// </summary>
        public const string TRANS_BEGIN_PAY = "1";

        /// <summary>
        /// 完成支付（消费确认）
        /// </summary>
        public const string TRANS_END_PAY = "2";

        /// <summary>
        /// 业务冲正（取消消费）
        /// </summary>
        public const string TRANS_REVOKE = "-2";

        /// <summary>
        /// 开始退款（退货）
        /// </summary>
        public const string TRANS_BEGIN_REFUND = "-3";

        /// <summary>
        /// 完成退款（退货确认）
        /// </summary>
        public const string TRANS_END_REFUND = "-4";

        /// <summary>
        /// 签到认证
        /// </summary>
        public const string TRANS_SIGN_IN = "5";

        /// <summary>
        /// 系统结算
        /// </summary>
        public const string TRANS_SETTLE = "6";

        /// <summary>
        /// 预付
        /// </summary>
        public const string TRANS_PREPAY = "7";
    }
}
