using System;
using UnityEngine;

namespace WEngine
{
    public enum UILayer
    {
        Bottom,
        Normal,
        Pop
    }
    [AttributeUsage(AttributeTargets.Class)]
    public class UIAttribe : Attribute
    {
        public UILayer UILayer;
        public UIAttribe(UILayer layer)
        {
            UILayer = layer;
        }
    }

    /// <summary>
    /// UI基类
    /// </summary>
    public  class UIBase : IUIBase
    {
        public bool IsShow 
        { 
            get 
            {
                if (ViewObj == null)
                    return false;
                return ViewObj.activeSelf;
            }
        }

        public GameObject ViewObj;
        protected Transform _viewThransform;
        #region Public
        public void Init(Transform viewThransform)
        {
            ViewObj = viewThransform.gameObject;
            _viewThransform = viewThransform;
            AutoGetComponent();
            OnInit();
            OnSubscribe();
        }

        public void Show()
        {
            if (!IsShow)
            {
                ViewObj?.SetActive(true);
                OnShow();
            }                
        }

        public void Hide()
        {
            if (IsShow)
            {
                ViewObj?.SetActive(false);
                OnHide();
            }                
        }

        public void Close()
        {
            UnSubscribe();
            OnClose();
            CoreEngine.UI.DestroyView(this);
        }
        #endregion

        #region AutoCode 自动生成获取组件的脚本
        public virtual void AutoGetComponent()
        {

        }
        #endregion

        #region UI生命周期
        public virtual void OnInit()
        {
        }
        
        public virtual void OnShow()
        {
        }

        public virtual void OnHide()
        {
        }

        public virtual void OnClose()
        {
        }
        public virtual void OnUpdate()
        {
        }
        public virtual void OnSubscribe()
        {
        }

        public virtual void UnSubscribe()
        {
        }
        #endregion

        #region Helper
        protected T FindChildComponent<T>(string path) where T : Component
        {
            var child = _viewThransform.Find(path);
            if (child == null)
                return null;
            var com = child.GetComponent<T>();
            if(com==null)
            {
                Log.Debug($"UI-子组件查找失败：{0}", path);
                return null;
            }
            return com;
        }
        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T">事件结构必须继承自IEvent</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected IEventObject Subscribe<T>(Action<T> action) where T : IEvent
        {
            var e = CoreEngine.Event.Subscribe(action);
            return e;
        }
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        protected void UnSubscribe<T>(Action<T> action) where T : IEvent
        {
            CoreEngine.Event.UnSubscribe(action);
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        protected void Publish<T>(T t) where T : IEvent
        {
            CoreEngine.Event.Publish(t);
        }

       

        #endregion
    }
}