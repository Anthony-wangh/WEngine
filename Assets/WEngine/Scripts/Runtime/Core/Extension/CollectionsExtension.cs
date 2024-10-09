using System.Collections.Generic;

namespace WEngine
{
    public static class CollectionsExtension
    {
        #region 数组
        /// <summary>数组为空或者没有元素</summary>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>获取元素在数组中的索引</summary>
        public static int GetIndex<T>(this T[] array, T t)
        {
            int index = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Equals(t))
                {
                    return i;
                }
            }
            return index;
        }

        /// <summary>获取最后一个元素</summary>
        public static T GetLast<T>(this T[] array)
        {
            T t = default;
            if (array != null && array.Length > 0)
            {
                return array[array.Length - 1];
            }

            return t;
        }
        #endregion

        #region List
        /// <summary>List为空或者没有元素</summary>
        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return list == null || list.Count == 0;
        }

        /// <summary>获取唯一值列表（去重）</summary>
        public static List<T> GetUniqueValue<T>(this List<T> list)
        {
            List<T> tempList = new List<T>();
            foreach (var t in list)
            {
                if (!tempList.Contains(t))
                {
                    tempList.Add(t);
                }
            }

            return tempList;
        }

        /// <summary>获取元素在列表中的索引</summary>
        public static int GetIndex<T>(this List<T> list, T t)
        {
            int index = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Equals(t))
                {
                    return i;
                }
            }
            return index;
        }

        /// <summary>获取最后一个元素</summary>
        public static T GetLast<T>(this List<T> list)
        {
            T t = default;
            if (list != null && list.Count > 0)
            {
                return list[list.Count - 1];
            }

            return t;
        }

        #endregion

        #region Dictionary
        /// <summary>
        /// 获取位置的字典值
        /// </summary>
        public static T2 GetValue<T1, T2>(this Dictionary<T1, T2> dict, int index)
        {
            var keys = dict.GetKeyList();
            if (index < 0 || index >= keys.Count)
            {
                return default;
            }

            return dict[keys[index]];
        }
        /// <summary>字典为空或者没有元素</summary>
        public static bool IsNullOrEmpty<T1, T2>(this Dictionary<T1, T2> dict)
        {
            return dict == null || dict.Count == 0;
        }

        /// <summary>添加键值对</summary>
        public static void Add<T1, T2>(this Dictionary<T1, T2> dict, KeyValuePair<T1, T2> data)
        {
            dict.Add(data.Key, data.Value);
        }

        /// <summary>添加或者替换（如果有存在就替换，如果没有就添加）</summary>
        public static void AddOrReplace<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T2 value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key] = value;
            }
            else
            {
                dict.Add(key, value);
            }
        }

        /// <summary>添加字典集合</summary>
        public static void AddRange<T1, T2>(this Dictionary<T1, T2> dict, Dictionary<T1, T2> data)
        {
            foreach (var keyValue in data)
            {
                dict.TryAdd(keyValue);
            }
        }

        /// <summary>尝试添加键值对，如果添加成功返回true，否则返回false</summary>
        public static bool TryAdd<T1, T2>(this Dictionary<T1, T2> dict, KeyValuePair<T1, T2> data)
        {
            if (dict.ContainsKey(data.Key) || data.Equals(null))
            {
                return false;
            }
            else
            {
                dict.Add(data);
                return true;
            }
        }

        /// <summary>尝试移除键值对，如果移除成功返回true，否则返回false</summary>
        public static bool TryRemove<T1, T2>(this Dictionary<T1, T2> dict, KeyValuePair<T1, T2> data)
        {
            if (!dict.ContainsKey(data.Key) || data.Equals(null))
            {
                return false;
            }
            else
            {
                dict.Remove(data.Key);
                return true;
            }
        }

        /// <summary>获取key列表</summary>
        public static List<T1> GetKeyList<T1, T2>(this Dictionary<T1, T2> dict)
        {
            List<T1> keyList = new List<T1>();
            foreach (var keyvalue in dict)
            {
                keyList.Add(keyvalue.Key);
            }
            return keyList;
        }

        /// <summary>获取value列表</summary>
        public static List<T2> GetValueList<T1, T2>(this Dictionary<T1, T2> dict)
        {
            List<T2> valueList = new List<T2>();
            foreach (var keyvalue in dict)
            {
                valueList.Add(keyvalue.Value);
            }
            return valueList;
        }

        /// <summary>获取唯一值列表（去重）</summary>
        public static List<T2> GetUniqueValue<T1, T2>(this Dictionary<T1, T2> dict)
        {
            return dict.GetValueList().GetUniqueValue();
        }

        /// <summary>获取key在字典中的索引</summary>
        public static int GetKeyIndex<T1, T2>(this Dictionary<T1, T2> dict, T1 t1)
        {
            int index = -1;
            foreach (var keyvalue in dict)
            {
                index++;
                if (t1.Equals(keyvalue.Key))
                {
                    return index;
                }
            }
            return index;
        }

        /// <summary>获取value在字典中的索引</summary>
        public static int GetValueIndex<T1, T2>(this Dictionary<T1, T2> dict, T2 t2)
        {
            int index = -1;
            foreach (var keyValue in dict)
            {
                index++;
                if (t2.Equals(keyValue.Value))
                {
                    return index;
                }
            }
            return index;
        }

        /// <summary>获取最后一个元素</summary>
        public static KeyValuePair<T1, T2> GetLast<T1, T2>(this Dictionary<T1, T2> dict)
        {
            KeyValuePair<T1, T2> kv = default;
            if (dict != null && dict.Count > 0)
            {
                foreach (var keyValue in dict)
                {
                    kv = keyValue;
                }
            }
            return kv;
        }

        /// <summary> 往字典集合插入元素，在Key之后 </summary>
        public static bool Insert<T1, T2>(this Dictionary<T1, T2> dict, T1 key, T1 newKey, T2 newValue)
        {
            bool isInsert = false;
            Dictionary<T1, T2> tempDict = new Dictionary<T1, T2>();
            if (dict != null)
            {
                foreach (var kv in dict)
                {
                    if (kv.Key.Equals(key))
                    {
                        isInsert = true;
                        tempDict.Add(newKey, newValue);
                    }
                    else
                    {
                        tempDict.Add(kv);
                    }
                }
            }

            if (isInsert)
            {
                dict.Clear();
                dict.AddRange(tempDict);
            }

            return isInsert;
        }

        #endregion
    }
}
