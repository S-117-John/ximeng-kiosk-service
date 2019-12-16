using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Skynet.ExceptionManagement;

namespace AutoServiceSDK.POSInterface
{
    public class IPOSFactory
    {
        public static POSBase CreateIPOS(string PosType)
        {
            POSBase ipos = null;
            Skynet.LoggingService.LogService.GlobalInfoMessage("POS接口类型：" + PosType);
            switch (PosType)
            {
                case ("银联商务"):
                    ipos = new POS001.YLSWPosClass();
                    ipos.PosInterfaceName = PosType;
                    break;
                case ("嘉利兴业"):
                    ipos = new POS002.JLXYPosClass();
                    ipos.PosInterfaceName = PosType;
                    break;
                case ("锡盟新利"):
                    ipos = new POS003.SingleePosClass();
                    ipos.PosInterfaceName = PosType;
                    break;
                default:
                    break;
            }

            //if (ipos == null)
            //{
            //    throw new LogonException("POS接口配置不正确，没有找到【" + PosType + "】");
            //}

            return ipos;

        }
    }
}
