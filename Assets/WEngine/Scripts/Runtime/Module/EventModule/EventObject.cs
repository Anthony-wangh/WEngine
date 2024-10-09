using System;
using UnityEngine;

namespace WEngine
{


    /// <summary>
    /// �¼�����
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
        /// �����¼�
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEventObject Subscribe(Action<T> action)
        {
            _event += action;
            return this;
        }
        /// <summary>
        /// ȡ�������¼�
        /// </summary>
        /// <param name="action"></param>
        public void UnSubscribe(Action<T> action)
        {
            _event -= action;
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="t"></param>
        public void Publish(T t)
        {
            _event?.Invoke(t);
        }

        
    }
}