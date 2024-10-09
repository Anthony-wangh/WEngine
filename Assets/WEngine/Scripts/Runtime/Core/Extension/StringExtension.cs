using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace WEngine
{
    public static class StringExtension
    {

        #region 转Int
        /// <summary>转Int,失败返回0</summary>
        public static int ToInt(this string t)
        {
            if (!int.TryParse(t, out var n))
                return 0;
            return n;
        }

        /// <summary>转Int,失败返回pReturn</summary>
        public static int ToInt(this string t, int pReturn)
        {
            if (!int.TryParse(t, out var n))
                return pReturn;
            return n;
        }

        /// <summary>是否是Int true:是 false:否</summary>
        public static bool IsInt(this string t)
        {
            return int.TryParse(t, out _);
        }
        #endregion

        #region 转Int16
        /// <summary>转Int,失败返回0</summary>
        public static Int16 ToInt16(this string t)
        {
            if (!Int16.TryParse(t, out var n))
                return 0;
            return n;
        }

        /// <summary>转Int,失败返回pReturn</summary>
        public static Int16 ToInt16(this string t, Int16 pReturn)
        {
            if (!Int16.TryParse(t, out var n))
                return pReturn;
            return n;
        }

        /// <summary> 是否是Int true:是 false:否</summary>
        public static bool IsInt16(this string t)
        {
            return Int16.TryParse(t, out _);
        }
        #endregion

        #region 转byte
        /// <summary>转byte,失败返回0</summary>
        public static byte Tobyte(this string t)
        {
            if (!byte.TryParse(t, out var n))
                return 0;
            return n;
        }

        /// <summary>转byte,失败返回pReturn</summary>
        public static byte Tobyte(this string t, byte pReturn)
        {
            if (!byte.TryParse(t, out var n))
                return pReturn;
            return n;
        }

        /// <summary> 是否是byte true:是 false:否</summary>
        public static bool Isbyte(this string t)
        {
            return byte.TryParse(t, out _);
        }
        #endregion

        #region 转Long
        /// <summary>转Long,失败返回0</summary>
        public static long ToLong(this string t)
        {
            if (!long.TryParse(t, out var n))
                return 0;
            return n;
        }

        /// <summary>转Long,失败返回pReturn</summary>
        public static long ToLong(this string t, long pReturn)
        {
            if (!long.TryParse(t, out var n))
                return pReturn;
            return n;
        }

        /// <summary>是否是Long true:是 false:否</summary>
        public static bool IsLong(this string t)
        {
            return long.TryParse(t, out _);
        }
        #endregion

        #region 转Double
        /// <summary>转Int,失败返回0</summary>
        public static double ToDouble(this string t)
        {
            if (!double.TryParse(t, out var n))
                return 0;
            return n;
        }

        /// <summary>转Double,失败返回pReturn</summary>
        public static double ToDouble(this string t, double pReturn)
        {
            if (!double.TryParse(t, out var n))
                return pReturn;
            return n;
        }

        /// <summary>是否是Double true:是 false:否</summary>
        public static bool IsDouble(this string t)
        {
            return double.TryParse(t, out _);
        }
        #endregion

        #region 转Decimal
        /// <summary>转Decimal,失败返回0</summary>
        public static decimal ToDecimal(this string t)
        {
            if (!decimal.TryParse(t, out var n))
                return 0;
            return n;
        }

        /// <summary>转Decimal,失败返回pReturn</summary>
        public static decimal ToDecimal(this string t, decimal pReturn)
        {
            if (!decimal.TryParse(t, out var n))
                return pReturn;
            return n;
        }

        /// <summary>是否是Decimal true:是 false:否</summary>
        public static bool IsDecimal(this string t)
        {
            return decimal.TryParse(t, out _);
        }
        #endregion

        #region 转DateTime
        /// <summary>转DateTime,失败返回当前时间</summary>
        public static DateTime ToDateTime(this string t)
        {
            if (!DateTime.TryParse(t, out var n))
                return DateTime.Now;
            return n;
        }

        /// <summary>转DateTime,失败返回pReturn</summary>
        public static DateTime ToDateTime(this string t, DateTime pReturn)
        {
            if (!DateTime.TryParse(t, out var n))
                return pReturn;
            return n;
        }

        /// <summary>转DateTime,失败返回pReturn</summary>
        public static string ToDateTime(this string t, string pFormat, string pReturn)
        {
            if (!DateTime.TryParse(t, out var n))
                return pReturn;
            return n.ToString(pFormat);
        }

        /// <summary>转DateTime,失败返回空</summary>
        public static string ToDateTime(this string t, string pFormat)
        {
            return t.ToDateTime(pFormat, string.Empty);
        }

        public static string ToShortDateTime(this string t)
        {
            return t.ToDateTime("yyyy-MM-dd", string.Empty);
        }

        /// <summary>是否是DateTime true:是 false:否</summary>
        public static bool IsDateTime(this string t)
        {
            return DateTime.TryParse(t, out _);
        }
        #endregion

        #region 与int[]相关
        /// <summary>转int[],字符串以逗号(,)隔开,请确保字符串内容都合法,否则会出错</summary>
        public static int[] ToIntArr(this string t)
        {
            if (t.IsNumber())
            {
                return t.ToIntArr(new[] { ',' });
            }

            return null;
        }

        /// <summary>转int[],字符串以逗号(,)隔开</summary>
        public static int[] ToIntArr(this string t, char[] pSplit)
        {
            if (new string(pSplit).IsNumber()==false)
            {
                return null;
            }

            if (t.Length == 0)
            {
                return new int[] { };
            }

            string[] arrStr = t.Split(pSplit, StringSplitOptions.None);
            int[] iStr = new int[arrStr.Length];

            for (int i = 0; i < arrStr.Length; i++)
                iStr[i] = int.Parse(arrStr[i]);

            return iStr;
        }
        #endregion

        #region 过滤字符串的非int,重新组合成字符串
        /// <summary>过滤字符串的非int,重新组合成字符串</summary>
        public static string ClearNoInt(this string t, char pSplit)
        {
            string sStr = string.Empty;
            string[] arrStr = t.Split(pSplit);

            foreach (var lsStr in arrStr)
            {
                if (!lsStr.IsInt())
                    continue;

                sStr += lsStr + pSplit;
            }

            if (sStr.Length > 0)
                sStr = sStr.TrimEnd(pSplit);

            return sStr;
        }

        /// <summary>过滤字符串的非int,重新组合成字符串</summary>
        public static string ClearNoInt(this string t)
        {
            return t.ClearNoInt(',');
        }
        #endregion

        #region 是否可以转换成int[]
        /// <summary>是否可以转换成int[],true:是,false:否</summary>
        public static bool IsIntArr(this string t, char pSplit)
        {
            string[] arrStr = t.Split(pSplit);
            bool b = true;

            foreach (var t1 in arrStr)
            {
                if (!t1.IsInt())
                {
                    b = false;
                    break;
                }
            }

            return b;
        }

        /// <summary>是否可以转换成int[],true:是,false:否 </summary>
        public static bool IsIntArr(this string t)
        {
            return t.IsIntArr(',');
        }
        #endregion

        #region 编码解码
        /// <summary>以默认编码返回字符串所对应的字节数组</summary>
        public static byte[] GetBytes(this string data)
        {
            return Encoding.Default.GetBytes(data);
        }

        /// <summary>以自定义编码返回字符串所对应的字节数组</summary>
        public static byte[] GetBytes(this string data, Encoding encoding)
        {
            return encoding.GetBytes(data);
        }

        /// <summary>将字符串以Base64方式编码</summary>
        public static string EncodeBase64(this string value)
        {
            return value.EncodeBase64(Encoding.UTF8);
        }

        /// <summary>将字符串以Base64方式编码</summary>
        public static string EncodeBase64(this string value, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = encoding.GetBytes(value);

            return Convert.ToBase64String(bytes);
        }

        /// <summary>将Base64方式编码后的字符串解码</summary>
        public static string DecodeBase64(this string encodedValue)
        {
            return encodedValue.DecodeBase64(Encoding.UTF8);
        }

        /// <summary>将Base64方式编码后的字符串解码</summary>
        public static string DecodeBase64(this string encodedValue, Encoding encoding)
        {
            encoding = (encoding ?? Encoding.UTF8);
            var bytes = Convert.FromBase64String(encodedValue);

            return encoding.GetString(bytes);
        }
        #endregion

        #region 进制转换
        /// <summary>16进制转二进制</summary>
        public static string Change16To2(this string t)
        {
            String binAll = string.Empty;
            char[] nums = t.ToCharArray();
            foreach (var t1 in nums)
            {
                string number = t1.ToString();
                int num = Int32.Parse(number, NumberStyles.HexNumber);

                var binOne = Convert.ToString(num, 2).PadLeft(4, '0');
                binAll = binAll + binOne;
            }
            return binAll;
        }

        /// <summary>二进制转十进制</summary>
        public static Int64 Change2To10(this string t)
        {
            char[] arrc = t.ToCharArray();
            Int64 all = 0, indexC = 1;
            for (int i = arrc.Length - 1; i >= 0; i--)
            {
                if (arrc[i] == '1')
                {
                    all += indexC;
                }
                indexC = indexC * 2;
            }

            return all;
        }

        /// <summary>二进制转换byte[]数组</summary>
        public static byte[] Change2ToBytes(this string t)
        {
            List<byte> list = new List<byte>();

            char[] arrc = t.ToCharArray();
            byte n = 0;
            int j = 0;
            //倒序获取位
            for (int i = arrc.Length - 1; i >= 0; i--)
            {
                var c = arrc[i];

                if (c == '1')
                {
                    n += Convert.ToByte(Math.Pow(2, j));
                }
                j++;

                if (j % 8 == 0)
                {
                    list.Add(n);
                    j = 0;
                    n = 0;
                }
            }

            //剩余最高位
            if (n > 0)
                list.Add(n);

            byte[] arrb = new byte[list.Count];

            int j1 = 0;
            //倒序
            for (int i = list.Count - 1; i >= 0; i--)
            {
                arrb[j1] = list[i];
                j1++;
            }
            return arrb;
        }

        /// <summary>二进制转化为索引id数据,从右到左</summary>
        public static int[] Change2ToIndex(this string t)
        {
            List<int> list = new List<int>();
            char[] arrc = t.ToCharArray();
            int j = 0;

            //倒序获取位
            for (int i = arrc.Length - 1; i >= 0; i--)
            {
                j++;
                var c = arrc[i];

                if (c == '1')
                {
                    list.Add(j);
                }
            }

            return list.ToArray();
        }
        #endregion

        #region 字符串处理
        #region 删除文件名或路径的特殊字符
        public static readonly string[] UnSafeStr = { "/", "\\", ":", "*", "?", "\"", "<", ">", "|" };

        /// <summary>删除文件名或路径的特殊字符</summary>
        public static string ClearPathUnsafe(this string t)
        {
            foreach (string s in UnSafeStr)
            {
                t = t.Replace(s, "");
            }

            return t;
        }
        #endregion

        #region 载取左字符
        /// <summary>载取左字符 </summary>
        public static string Left(this string t, int pLen, string pReturn)
        {
            if (string.IsNullOrEmpty(t))
                return string.Empty;
            pLen *= 2;
            int i = 0, j = 0;
            foreach (char c in t)
            {
                if (c > 127)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }

                if (i > pLen)
                {
                    return t.Substring(0, j) + pReturn;
                }

                j++;
            }

            return t;
        }

        public static string Left(this string t, int pLen)
        {
            return Left(t, pLen, string.Empty);
        }

        public static string StrLeft(this string t, int pLen)
        {
            if (t == null)
            {
                return "";
            }
            if (t.Length > pLen)
            {
                return t.Substring(0, pLen);
            }
            return t;
        }
        #endregion
        /// <summary>字符串真实长度 如:一个汉字为两个字节</summary>
        public static int LengthReal(this string s)
        {
            return Encoding.Default.GetBytes(s).Length;
        }

        /// <summary>去除小数位最后为0的</summary>
        public static decimal ClearDecimal0(this string t)
        {
            if (decimal.TryParse(t, out var d))
            {
                return decimal.Parse(double.Parse(d.ToString("g")).ToString(CultureInfo.InvariantCulture));
            }
            return 0;
        }

        /// <summary>反转字符</summary>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>去掉空格</summary>
        public static string RemoveSpaces(this string value)
        {
            return value.Replace(" ", string.Empty);
        }

        /// <summary>去掉换行</summary>
        public static string RemoveLine(this string value)
        {
            return value.Replace("\n", string.Empty);
        }

        /// <summary>从字符串中移除对应字符</summary>
        public static string RemoveChar(this string s, char c)
        {
            return (!string.IsNullOrEmpty(s)) ? s.Replace(c.ToString(), string.Empty) : string.Empty;
        }

        /// <summary>是否IP</summary>
        public static bool IsValidIpAddress(this string s)
        {
            return Regex.IsMatch(s, @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
        }

        /// <summary>字符串是否由数字构成</summary>
        public static bool IsNumber(this string input)
        {
            return (!String.IsNullOrEmpty(input)) && (new Regex(@"^-?[0-9]*\.?[0-9]+$").IsMatch(input.Trim()));
        }

        /// <summary>字符串是否包含数字</summary>
        public static bool HasNumber(this string input)
        {
            return (!String.IsNullOrEmpty(input)) && (new Regex(@"[0-9]+").IsMatch(input));
        }

        /// <summary>对字符串进行截取换行（根据传入的字符串长度）</summary>
        public static string NewLine(this string input, int lineLength)
        {
            int lineCount = input.Length / lineLength;
            for (int i = 0; i < lineCount; i++)
            {
                input = input.Insert(lineLength * (i + 1) + i, "\n");
            }

            if (input.EndsWith("\n"))//结尾是换行符截取掉
            {
                input = input.Substring(0, input.Length - 1);
            }

            return input;
        }
        #endregion
    }
}
