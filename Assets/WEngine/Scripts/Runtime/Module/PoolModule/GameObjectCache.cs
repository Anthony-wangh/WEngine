using System.Collections.Generic;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 游戏对象缓存
    /// </summary>
    public class GameObjectCache
    {
        private List<GameObject> _goList = new List<GameObject>();
        private GameObject _item;
        private GameObject _parent;
        public GameObjectCache(GameObject go, GameObject parent)
        {
            _item = go;
            if (_item == null)
            {
                Debug.LogError("Item不能为空");
            }
            _parent = parent;
        }

        /// <summary>
        /// 获取缓存池内的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public GameObject Get(int index)
        {
            while (index > _goList.Count - 1)
            {
                GameObject obj = Object.Instantiate(_item);
                obj.SetParent(_parent, false);
                _goList.Add(obj);
            }

            var go = _goList[index];
            go?.SetActive(true);
            return go;
        }
        /// <summary>
        /// 回收所有的元素
        /// </summary>
        public void HideAll()
        {
            foreach (var go in _goList)
            {
                go?.SetActive(false);
            }
        }
        /// <summary>
        /// 回收对应序列号的元素
        /// </summary>
        /// <param name="index"></param>
        public void Hide(int index)
        {
            if (index < 0 || index > _goList.Count - 1)
                return;

            _goList[index]?.SetActive(false);
        }
        /// <summary>
        /// 释放缓存
        /// </summary>
        public void Dispose()
        {
            _item = null;
            _parent = null;
            _goList?.Clear();
            _goList = null;
        }
    }
}
