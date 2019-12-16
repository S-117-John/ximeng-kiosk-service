using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoServiceSDK.SDK
{
    public class XuHuiInterface_DLL
    {
        [DllImport("GGXmlTcp.dll", EntryPoint = "XmlTcp", CharSet = CharSet.Ansi)]
        public static extern string XmlTcp(StringBuilder xmlbuf, Int32 timeout);
    }
}
