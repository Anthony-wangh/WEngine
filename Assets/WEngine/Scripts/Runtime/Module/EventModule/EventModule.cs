using System;
using System.Collections.Generic;

namespace WEngine
{
    /// <summary>
    /// 全局消息模块
    /// </summary>
    public class EventModule : ModuleImp,IEventModule
    {
        private Dictionary<Type, IEventObject> _eventList = new Dictionary<Type, IEventObject>();
        /// <summary>
        /// 订阅消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEventObject Subscribe<T>(Action<T> action) where T : IEvent
        {
            Type type = typeof(T);
            
            if (!_eventList.ContainsKey(type))
                _eventList.Add(type, new EventObject<T>());

            EventObject<T> eObject = _eventList[type] as EventObject<T>;
            eObject.Subscribe(action);
            return eObject;
        }


        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void UnSubscribe<T>(Action<T> action) where T : IEvent
        {
            Type type = typeof(T);
            if (_eventList.ContainsKey(type))
            {
                EventObject<T> eObject = _eventList[type] as EventObject<T>;
                eObject.UnSubscribe(action);
            }
        }
        /// <summary>
        /// 发布订阅消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Publish<T>(T t) where T : IEvent
        {
            Type type = typeof(T);
            if (_eventList.ContainsKey(type))
            {
                EventObject<T> eObject = _eventList[type] as EventObject<T>;
                eObject.Publish(t);
            }
        }
    }
}