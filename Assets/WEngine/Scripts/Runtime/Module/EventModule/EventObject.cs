using System;
using UnityEngine;

namespace WEngine
{


    /// <summary>
    /// 事件对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EventObject<T> : IEventObject where T : IEvent
    {
        private Action<T> _event;


        public void UnSubscribeOnDestroy()
        {
            _event = null;
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEventObject Subscribe(Action<T> action)
        {
            _event += action;
            return this;
        }
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="action"></param>
        public void UnSubscribe(Action<T> action)
        {
            _event -= action;
        }
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="t"></param>
        public void Publish(T t)
        {
            _event?.Invoke(t);
        }

        
    }
}