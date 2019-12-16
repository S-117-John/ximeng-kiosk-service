using System;
using System.Text;
using System.Collections.Generic;

namespace AutoServiceSDK.POSInterface.POS003
{
    internal static class DictionaryHelper
    {
        /// <summary>
        /// 获取 Dictionary 中值的字符串
        /// </summary>
        internal static string GetValueString(this Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string key in dic.Keys)
            {
                sb.Append(dic[key]);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将 Dictionary 中的键值对转换为简单的 Json 字符串
        /// </summary>
        internal static string ToJson(this Dictionary<string, string> dictionary)
        {
            StringBuilder sb = new StringBuilder();

            if (dictionary != null && dictionary.Count > 0)
            {
                sb.Append("{");
                foreach (var item in dictionary)
                {
                    sb.AppendFormat("\"{0}\":\"{1}\"", item.Key, item.Value);
                    sb.Append(",");
                }
                sb.Remove(sb.Length - 1, 1); //移除尾部多余的逗号
                sb.Append("}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 将 Dictionary 中的键值对转换为简单的 Json 字符串
        /// </summary>
        internal static string ToJson(this Dictionary<string, object> dictionary)
        {
            StringBuilder sb = new StringBuilder();

            if (dictionary != null && dictionary.Count > 0)
            {
                sb.Append("{");
                foreach (var item in dictionary)
                {
                    if (item.Value != null)
                    {
                        switch (item.Value.GetType().Name)
                        {
                            case "Int16":
                            case "Int32":
                            case "Int64":
                            case "UInt16":
                            case "UInt32":
                            case "UInt64":
                            case "Double":
                            case "Single":
                            case "Decimal":
                                sb.AppendFormat("\"{0}\":{1}", item.Key, item.Value);
                                sb.Append(",");
                                break;

                            case "Boolean":
                                sb.AppendFormat("\"{0}\":{1}", item.Key, item.Value.ToString().ToLower());
                                sb.Append(",");
                                break;

                            case "String":
                                sb.AppendFormat("\"{0}\":\"{1}\"", item.Key, item.Value);
                                sb.Append(",");
                                break;

                            default:
                                sb.AppendFormat("\"{0}\":\"{1}\"", item.Key, item.Value);
                                sb.Append(",");
                                break;
                        }
                    }
                    else
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\"", item.Key, "null");
                        sb.Append(",");
                    }
                }
                sb.Remove(sb.Length - 1, 1); //移除尾部多余的逗号
                sb.Append("}");
            }

            return sb.ToString();
        }
    }
}
