using System;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 模块管理器基类
    /// </summary>
    public class ModuleManagerBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            CoreEngine.Registe(this);
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T">事件结构必须继承自IEvent</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected IEventObject Subscribe<T>(Action<T> action) where T : IEvent
        {
            var e= CoreEngine.Event.Subscribe(action);
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

    }
}