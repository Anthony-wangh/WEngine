using System;

namespace WEngine
{
    /// <summary>
    /// ��Ϣģ�������
    /// </summary>
    public class EventModuleManager :IEventModule
    {
        private EventModule _eventModule;


        private void CheckModule()
        {
            if(_eventModule==null)
                _eventModule = ModuleSystem.Get<EventModule>();
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <typeparam name="T">�¼��ṹ����̳���IEvent</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEventObject Subscribe<T>(Action<T> action) where T : IEvent
        {
            CheckModule();
            return _eventModule.Subscribe<T>(action);
        }
        /// <summary>
        /// ȡ�������¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void UnSubscribe<T>(Action<T> action) where T : IEvent
        {
            CheckModule();
            _eventModule.UnSubscribe<T>(action);
        }
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        public void Publish<T>(T t) where T : IEvent
        {
            CheckModule();
            _eventModule.Publish(t);
        }
    }
}