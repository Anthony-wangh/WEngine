using System;
using System.Collections.Generic;

namespace WEngine
{
    /// <summary>
    /// 堆对象管理
    /// </summary>
    public class ReferencePool
    {
        private static readonly Dictionary<string, Stack<IReference>> _poolDict = new Dictionary<string, Stack<IReference>>();

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        public static T Acquire<T>() where T : IReference, new()
        {
            string key = typeof(T).FullName;
            if (string.IsNullOrEmpty(key))
                return new T();

            if (_poolDict.ContainsKey(key))
            {
                var stack = _poolDict[key];
                return stack.Count > 0 ? (T)stack.Pop() : new T();
            }
            _poolDict[key] = new Stack<IReference>();
            return new T();
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="obj"></param>
        public static void Release(IReference obj)
        {
            if (obj == null)
                return;

            try
            {
                obj.Dispose();
            }
            catch (Exception e)
            {
                Log.Fatal(e.Message);
            }

            string key = obj.GetType().FullName;
            if (string.IsNullOrEmpty(key))
                return;

            if (_poolDict.ContainsKey(key))
            {
                var stack = _poolDict[key];
                stack.Push(obj);
            }
        }

        /// <summary>
        /// 清除对象
        /// </summary>
        public static void Clear<T>() where T : IReference
        {
            string key = typeof(T).FullName;
            if (string.IsNullOrEmpty(key))
                return;

            if (_poolDict.ContainsKey(key))
            {
                var stack = _poolDict[key];
                stack.Clear();
                _poolDict.Remove(key);
            }
        }

        public static void ClearAll()
        {
            foreach (var pool in _poolDict)
            {
                pool.Value.Clear();
            }
            _poolDict.Clear();
        }
    }
}
