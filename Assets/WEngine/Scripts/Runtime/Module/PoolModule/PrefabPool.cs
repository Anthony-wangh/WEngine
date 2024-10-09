using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WEngine
{
    /// <summary>
    /// 预制对象池 该类是基类
    /// </summary>
    public class PrefabPool
    {
        //正在使用的对象
        private readonly List<GameObject> _useObjList;
        //未使用的对象
        private readonly List<GameObject> _idleObjList;
        //预制对象
        private readonly GameObject _prefab;
        //预加载数量
        public int PreloadCount = 5;
        //限制池子里最大的Prefab数量
        public int MaxCount = 10;
        //自动剔除 未使用的预制
        public bool IsAutoCull = true;
        //剔除时间 默认每秒
        public float CullTime = 1;

        //保留数量
        public float KeepCount = 10;
        //是否预加载完成
        private bool _ispreload;

        private float _cullTime;

        private Transform _parent;
        //使用的数量
        public int UseCount
        {
            get
            {
                if (_useObjList == null)
                    return 0;

                return _useObjList.Count;
            }
        }

        //未使用的数量
        public int IdleCount
        {
            get
            {
                if (_idleObjList == null)
                    return 0;

                return _idleObjList.Count;
            }
        }

        public int TotalCount => UseCount + IdleCount;
        private ObjectModule _objectUtility;

        public PrefabPool(GameObject prefab)
        {
            if (prefab == null)
            {
                throw new Exception("预制是空的");
            }
            _prefab = prefab;
            _useObjList = new List<GameObject>();
            _idleObjList = new List<GameObject>();
            _objectUtility = ModuleSystem.Get<ObjectModule>();
        }

        public void SetParent(GameObject parent)
        {
            if (_prefab == null || parent == null)
            {
                throw new Exception("设置父物体  预制或父物体为空");
            }
            _parent = new GameObject(_prefab.name).transform;
            _parent.SetParent(parent.transform, false);
        }

        public void Update()
        {
            PreLoad();
            Cull();
        }

        private void Cull()
        {
            if (_ispreload == false)
                return;

            if (KeepCount >= IdleCount || IdleCount == 0)
                return;

            _cullTime += Time.deltaTime;
            if (_cullTime > CullTime)
            {
                GameObject obj = _idleObjList[0];
                _idleObjList.RemoveAt(0);
                _objectUtility.Destroy(obj);
                _cullTime = 0;
            }
        }

        //预加载 加载到初始数量就结束
        private void PreLoad()
        {
            if (_ispreload)
                return;

            if (TotalCount >= PreloadCount)
            {
                _ispreload = true;
                return;
            }

            AddToIdle(Create());
        }

        private GameObject Create()
        {
            GameObject obj = Object.Instantiate(_prefab);
            obj.transform.SetParent(_parent, false);
            return obj;
        }

        public GameObject Get()
        {
            GameObject obj;
            if (_idleObjList.Count > 0)
            {
                obj = _idleObjList[0];
                _idleObjList.RemoveAt(0);
            }
            else
            {
                obj = Create();
            }
            obj.SetActive(true);
            return obj;
        }

        public void Remove(GameObject obj)
        {
            AddToIdle(obj);
            ResetObj(obj);
        }

        private void AddToIdle(GameObject obj)
        {
            obj.SetActive(false);
            if (_useObjList.Contains(obj))
            {
                _useObjList.Remove(obj);
            }
            if (_idleObjList.Contains(obj) == false)
            {
                _idleObjList.Add(obj);
            }
            obj.transform.SetParent(_parent, false);
        }

        protected virtual void ResetObj(GameObject obj)
        {
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localEulerAngles = Vector3.zero;
            obj.transform.localScale = Vector3.one;
        }
    }
}
