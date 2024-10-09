using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 对象池管理
    /// </summary>
    public class PoolModuleManager : ModuleManagerBase
    {
        private readonly Dictionary<string, PrefabPool> PrefabPoolDict = new Dictionary<string, PrefabPool>();
        private string[] _prefabKeys;

        /// <summary>
        /// 添加GameObject对象池
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pool"></param>
        public void AddPool(string key, PrefabPool pool)
        {
            if (PrefabPoolDict.ContainsKey(key))
                throw new Exception(string.Format("对象池已经存在:" + key));

            pool.SetParent(gameObject);
            PrefabPoolDict.Add(key, pool);
            _prefabKeys = PrefabPoolDict.Keys.ToArray();
        }

        /// <summary>
        /// 获取GameObject对象池
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public PrefabPool GetPool(string key)
        {
            if (!PrefabPoolDict.ContainsKey(key))
                throw new Exception(string.Format("对象池不存在:" + key));

            return PrefabPoolDict[key];
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="key">对象的key值</param>
        /// <returns></returns>
        public GameObject Get(string key)
        {
            if (!PrefabPoolDict.ContainsKey(key))
                throw new Exception(string.Format("对象池不存在:" + key));

            return PrefabPoolDict[key].Get();
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        public void Remove(string key, GameObject obj)
        {
            if (!PrefabPoolDict.ContainsKey(key))
                throw new Exception(string.Format("对象池不存在:" + key));

            PrefabPoolDict[key].Remove(obj);
        }

        // ReSharper disable once UnusedMember.Local
        private void Update()
        {
            if (_prefabKeys == null || PrefabPoolDict.Count == 0)
                return;

            foreach (string key in _prefabKeys)
            {
                if (PrefabPoolDict.ContainsKey(key) == false)
                    continue;

                PrefabPoolDict[key].Update();
            }
        }
    }
}
