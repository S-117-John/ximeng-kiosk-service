using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Web.Services.Description;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using BusinessFacade.His.ErpPad;

namespace AutoServiceManage.Charge
{
    public class ServiceTool
    {
        #region 发送检查申请信息
        /// < summary>           
        /// 发送检查申请信息       
        /// < /summary>          
        /// < param name="infoString">预约数据< /param>       
        /// < returns>< /returns>          
        public static string SendExamApp(string infoString)
        {
            try
            {
                EInterfaceinfoFacade interfacefacade = new EInterfaceinfoFacade();
                DataSet dsInterface = interfacefacade.dsInterfaceInfo("门诊", "医技接口","医技报到接口");
                string url = "";
                string methodname = "SendExamApp";
                if (dsInterface.Tables[0].Rows.Count > 0)
                {
                    url = dsInterface.Tables[0].Rows[0]["IURL"].ToString().Replace("http@", "http:");
                }
                else
                {
                    return "错误：请先配置医技报到接口！";
                }

                object[] args = new object[1];
                args[0] = infoString;

                object result = ServiceTool.InvokeWebService(url, null, methodname, args);
                string message = result.ToString();
                Skynet.LoggingService.LogService.GlobalInfoMessage("pacs接口返回数据：" + message);
                DataSet dsReturn = ConvertXMLToDataSet(message);
                if (dsReturn != null && dsReturn.Tables.Count > 0)
                {
                    if (dsReturn.Tables[0].Rows.Count > 0)
                    {
                        return dsReturn.Tables[0].Rows[0][0].ToString();
                    }
                }
                return "错误：未获取到返回值";
            }
            catch (Exception ex)
            {
                Skynet.LoggingService.LogService.GlobalErrorMessage("调用医技报到接口错误：" + ex.Message);
                return "调用PACS报到接口错误：" + ex.Message;
                
            }
        }
        /// <summary>
        /// xml转换为DataSet
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        private static DataSet ConvertXMLToDataSet(string xmlData)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmlData);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                return xmlDS;
            }
            catch (Exception ex)
            {
                string strTest = ex.Message;
                return null;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        #endregion
        #region 动态调用WebService动态调用地址

        /// <summary>
        /// 动态调用WebService
        /// </summary>
        /// <param name="url">WebService地址</param>
        /// <param name="methodname">方法名(模块名)</param>
        /// <param name="args">参数列表</param>
        /// <returns>object</returns>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return ServiceTool.InvokeWebService(url, null, methodname, args);
        }
        /// <summary>
        /// 动态调用web服务
        /// </summary>
        /// <param name="url">WSDL服务地址</param>
        /// <param name="classname">服务接口类名</param>
        /// <param name="methodname">方法名</param>
        /// <param name="args">参数值</param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {

            string @namespace = "EnterpriseServerBase.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = ServiceTool.GetWsClassName(url);
            }
            try
            {

                //获取WSDL   
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(url + "?WSDL");
                ServiceDescription sd = ServiceDescription.Read(stream);
                //注意classname一定要赋值获取 
                classname = sd.Services[0].Name;

                ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();
                sdi.AddServiceDescription(sd, "", "");
                CodeNamespace cn = new CodeNamespace(@namespace);

                //生成客户端代理类代码          
                CodeCompileUnit ccu = new CodeCompileUnit();
                ccu.Namespaces.Add(cn);
                sdi.Import(cn, ccu);
                CSharpCodeProvider icc = new CSharpCodeProvider();


                //设定编译参数                 
                CompilerParameters cplist = new CompilerParameters();
                cplist.GenerateExecutable = false;
                cplist.GenerateInMemory = true;
                cplist.ReferencedAssemblies.Add("System.dll");
                cplist.ReferencedAssemblies.Add("System.XML.dll");
                cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
                cplist.ReferencedAssemblies.Add("System.Data.dll");
                //编译代理类                 
                CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
                if (true == cr.Errors.HasErrors)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                    {
                        sb.Append(ce.ToString());
                        sb.Append(System.Environment.NewLine);
                    }
                    throw new Exception(sb.ToString());
                }
                //生成代理实例，并调用方法                 
                System.Reflection.Assembly assembly = cr.CompiledAssembly;
                Type t = assembly.GetType(@namespace + "." + classname, true, true);
                object obj = Activator.CreateInstance(t);
                System.Reflection.MethodInfo mi = t.GetMethod(methodname);
                return mi.Invoke(obj, args);

            }
            catch (Exception ex)
            {
                //throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
                throw new Exception(ex.Message);
                Skynet.LoggingService.LogService.GlobalErrorMessage("调用医技报到接口错误：" + ex.InnerException.Message);
                // return "Error:WebService调用错误！" + ex.Message;
            }
        }
        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');
            return pps[0];
        }
        #endregion
    }
}
