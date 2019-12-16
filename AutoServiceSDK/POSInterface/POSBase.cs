using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Skynet.LoggingService;

namespace AutoServiceSDK.POSInterface
{
    public abstract class POSBase
    {
        /// <summary>
        /// 交易方法
        /// </summary>
        /// <param name="TranType">交易类型，1为消费，2消费确认，-2取消消费</param>
        /// <param name="htParams">交易参数，HIS流水号，病人ID,金额</param>
        /// <returns></returns>
        public virtual int Trans(string TranType, Hashtable htParams)
        {
            //adConfigxml();
            return 0;
        }

        public string PosInterfaceName { get; set; }
        /// <summary>
        /// 扩展配置标结合
        /// </summary>
        private DataSet ParameterSet { get; set; }

        private bool ReadConfigxml()
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "PosConfig.xml");

                if (!File.Exists(path))
                {
                    throw new Exception("未找到POS配置文件：PosConfig.xml");
                }

                XElement medicares = XElement.Load(path);

                string PosIntefacename = medicares.Element("PosIntefaceName").Value;
                foreach (XElement medicare in medicares.Elements("PosConfig"))
                {
                    if (medicare.Element("PosName").Value == PosIntefacename)
                    {
                        IEnumerable<XElement> tmp = medicare.Elements();

                        for (int i = 0; i < tmp.Count(); i++)
                        {
                            if (tmp.ElementAt(i).Name.LocalName == "Parameters")
                            {
                                tmp = tmp.Union(tmp.ElementAt(i).Elements());

                                continue;
                            }

                            if (tmp.ElementAt(i).Name.LocalName == "ParameterSet")
                            {
                                ParameterSet = new DataSet("ParameterSet");

                                IEnumerable<XElement> xTables = tmp.ElementAt(i).Elements("Table");

                                foreach (XElement xTable in xTables)
                                {
                                    DataTable dt = new DataTable(xTable.Attribute("Name").Value);

                                    string[] coloumNames = xTable.Attribute("ColoumNames").Value.Split(',');

                                    //创建列
                                    foreach (string coloumName in coloumNames)
                                    {
                                        dt.Columns.Add(coloumName, typeof(String));
                                    }

                                    //创建行
                                    IEnumerable<XElement> xRows = xTable.Elements("Row");

                                    for (int j = 0; j < xRows.Count(); j++)
                                    {
                                        DataRow row = dt.NewRow();

                                        IEnumerable<XElement> xColoums = xRows.ElementAt(j).Elements();

                                        foreach (XElement xCell in xColoums)
                                        {
                                            row[xCell.Name.LocalName] = xCell.Value;
                                        }

                                        dt.Rows.Add(row);
                                    }

                                    ParameterSet.Tables.Add(dt);
                                }

                                continue;
                            }

                            PropertyInfo pro = GetType().GetProperty(tmp.ElementAt(i).Name.LocalName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                            if (pro != null)
                            {
                                object obj = null;

                                if (pro.PropertyType.Name == "String")
                                {
                                    obj = tmp.ElementAt(i).Value;
                                }
                                else
                                {
                                    obj = pro.PropertyType.GetMethod("Parse", new Type[] { typeof(String) }).Invoke(pro.PropertyType, new object[] { tmp.ElementAt(i).Value });
                                }

                                pro.SetValue(this, obj, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public, null, null, null);
                            }

                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogService.GlobalErrorMessage(ex.Message);
                throw new Exception("读取配置文件失败：" + ex.Message);
            }
        }
    }
}
