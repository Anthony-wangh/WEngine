using System;

namespace WEngine
{
    /// <summary>
    /// 消息模块管理器
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
        /// 订阅事件
        /// </summary>
        /// <typeparam name="T">事件结构必须继承自IEvent</typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public IEventObject Subscribe<T>(Action<T> action) where T : IEvent
        {
            CheckModule();
            return _eventModule.Subscribe<T>(action);
        }
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public void UnSubscribe<T>(Action<T> action) where T : IEvent
        {
            CheckModule();
            _eventModule.UnSubscribe<T>(action);
        }
        /// <summary>
        /// 发布事件
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