using System;
using System.Collections.Generic;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// UI管理模块
    /// </summary>
    public class UIModule :  ModuleImp, IUIModule
    {
        private Dictionary<Type, IUIBase> _uiList = new Dictionary<Type, IUIBase>();
        private Dictionary<UILayer, Transform> _uiLayers = new Dictionary<UILayer, Transform>();
        private const string ViewGroup = "View_";
        public override void OnInit()
        {
            for (int i = 0; i < 3; i++)
            {
                var name = (UILayer)i;
                GameObject go = new GameObject(name.ToString());
                go.SetParent(CoreEngine.UI.Canvas.transform);
                go.ResetPos();
                _uiLayers.Add(name, go.transform);
            }
        }
        /// <summary>
        /// 同步打开界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Show<T>() where T : IUIBase,new()
        {
            var type = typeof(T);
            if (_uiList.ContainsKey(type))
            {
                var ui = _uiList[type];
                ui.Show();
                return;
            }
            var handle = CoreEngine.AssetLoad.Load<GameObject>(ViewGroup + type.Name);
            InstantiateUI<T>(handle, type);
        }
        /// <summary>
        /// 异步打开界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public async void ShowAsync<T>() where T: IUIBase, new()
        {
            var type = typeof(T);
            if (_uiList.ContainsKey(type))
            {
                var ui = _uiList[type];
                ui.Show();
                return;
            }
            
            var handle = await CoreEngine.AssetLoad.LoadAsync<GameObject>(ViewGroup + type.Name);
            
            InstantiateUI<T>(handle, type);
        }

        //实例化界面
        private void InstantiateUI<T>(GameObject go,Type type) where T : IUIBase, new()
        {
            GameObject obj = UnityEngine.Object.Instantiate(go, GetUIParent(type));
            T t = new T();
            t.Init(obj.transform);
            t.Show();
            _uiList.Add(type, t);
        }

        private Transform GetUIParent(Type type)
        {
            UIAttribe attribute = Attribute.GetCustomAttribute(type, typeof(UIAttribe)) as UIAttribe;
            if (attribute == null)
                return _uiLayers[UILayer.Normal];
            return _uiLayers[attribute.UILayer];
        }
        /// <summary>
        /// 隐藏界面，设置界面的activesalf=false
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Hide<T>() where T : IUIBase
        {
            var type = typeof(T);
            if (_uiList.ContainsKey(type))
            {
                var ui = _uiList[type];
                ui.Hide();
            }
        }
        /// <summary>
        /// 销毁界面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Close<T>() where T : IUIBase
        {
            var type = typeof(T);
            if (_uiList.ContainsKey(type))
            {
                var ui = _uiList[type];
                ui.Close();
            }
        }

        public void DestroyView(UIBase uIBase)
        {
            Type type = uIBase.GetType();
            if (_uiList.ContainsKey(type))
            {
                UnityEngine.Object.Destroy(uIBase.ViewObj);
            }
            _uiList.Remove(type);
        }
       
        public override void OnUpdate()
        {
            foreach (var item in _uiList)
            {
                if (item.Value.IsShow)
                    item.Value.OnUpdate();
            }
        }
        public override void OnDispose()
        {
            _uiList.Clear();
        }

        
    }
}