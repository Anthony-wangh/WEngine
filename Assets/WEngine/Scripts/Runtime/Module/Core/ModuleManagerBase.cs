using System;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// ģ�����������
    /// </summary>
    public class ModuleManagerBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            CoreEngine.Registe(this);
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <typeparam name="T">�¼��ṹ����̳���IEvent</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected IEventObject Subscribe<T>(Action<T> action) where T : IEvent
        {
            var e= CoreEngine.Event.Subscribe(action);
            return e;
        }
        /// <summary>
        /// ȡ�������¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        protected void UnSubscribe<T>(Action<T> action) where T : IEvent
        {
            CoreEngine.Event.UnSubscribe(action);
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        protected void Publish<T>(T t) where T : IEvent
        {
            CoreEngine.Event.Publish(t);
        }

    }
}