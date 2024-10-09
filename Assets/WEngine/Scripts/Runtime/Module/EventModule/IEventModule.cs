using System;
using UnityEngine;

namespace WEngine
{
    public interface IEvent
    {

    }
    public interface IEventObject:IUnSubscribeOnDestroy
    {
        //IEventObject AddTo(GameObject gameObject);
    }
    public interface ISubscribe
    {
        IEventObject Subscribe<T>(Action<T> action) where T : IEvent;
    }

    public interface IPublish
    {
        void Publish<T>(T t) where T : IEvent;
    }
    public interface IUnsubscribe
    {
        void UnSubscribe<T>(Action<T> action) where T : IEvent;
    }
    public interface IEventModule : ISubscribe, IUnsubscribe, IPublish
    {
    }

    public interface IUnSubscribeOnDestroy
    {
        void UnSubscribeOnDestroy();
    }

    public static class EventObjectExtension
    {
        /// <summary>
        /// 将事件绑定到Gameobject上，当Gameobject被销毁时，移除事件监听
        /// </summary>
        /// <param name="eventObject"></param>
        /// <param name="gameObject"></param>
        public static void AddTo(this IEventObject eventObject,GameObject gameObject)
        {
            var _unSubscribeEventObject = gameObject.GetComponent<UnSubscribeEventObject>();
            if (_unSubscribeEventObject == null)
                _unSubscribeEventObject = gameObject.AddComponent<UnSubscribeEventObject>();

            _unSubscribeEventObject.Add(eventObject);
        }
    }
}