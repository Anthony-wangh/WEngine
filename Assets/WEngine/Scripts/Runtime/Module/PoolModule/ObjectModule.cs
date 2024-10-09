using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WEngine
{
    /// <summary>
    /// 对象删除与回收(内存不足 自动触发)
    /// </summary>
    public class ObjectModule : ModuleImp
    {
        private readonly List<Object> _objList = new List<Object>();
        private bool _isDirty;

        private const int UnloadTime = 3;

        private float _curTime;

        public override void OnInit()
        {
            Application.lowMemory += LowMemory;
        }


        private void LowMemory()
        {
            UnloadUnusedAssets();
        }

        public override void OnUpdate()
        {
            if (_objList == null || _objList.Count == 0)
            {
                Unload();
                return;
            }


            _curTime = 0;
            var obj = _objList[0];
            _objList.RemoveAt(0);
            if (obj == null)
                return;

            Object.Destroy(obj);
        }

        private void Unload()
        {
            if (!_isDirty)
                return;

            _curTime += Time.deltaTime;
            if (_curTime >= UnloadTime)
            {
                Resources.UnloadUnusedAssets();
                GC.Collect();
                _isDirty = false;
            }
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj"></param>
        public void Destroy(Object obj)
        {
            if (obj == null)
                return;

            _objList.Add(obj);
        }

        /// <summary>
        /// 立刻删除对象
        /// </summary>
        /// <param name="obj"></param>
        public void DestroyImmediate(GameObject obj)
        {
            if (obj == null)
                return;

            Object.DestroyImmediate(obj);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj"></param>
        public void Destroy(GameObject obj)
        {
            if (obj == null)
                return;

            obj.SetActive(false);
            _objList.Add(obj);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="obj"></param>
        public void Destroy(Transform obj)
        {
            if (obj == null)
                return;

            Destroy(obj.gameObject);
        }

        /// <summary>
        /// 回收未使用的资源（会触发GC回收）
        /// </summary>
        public void UnloadUnusedAssets()
        {
            _isDirty = true;
        }
    }
}
