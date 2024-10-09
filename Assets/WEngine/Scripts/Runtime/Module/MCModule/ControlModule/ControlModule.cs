using System;
using System.Collections.Generic;

namespace WEngine
{
    public interface IControlModule
    {
        void Register<T>() where T : ControlBase, new();
        T Get<T>() where T : ControlBase;
    }
    /// <summary>
    /// �߼�����ģ��
    /// </summary>
    public class ControlModule : ModuleImp, IControlModule
    {
        private Dictionary<Type, ControlBase> _controls = new Dictionary<Type, ControlBase>();
        /// <summary>
        /// ע���߼���ģ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Register<T>() where T : ControlBase,new()
        {
            Type type = typeof(T);
            if (_controls.ContainsKey(type))
            {
                return;
            }
            T t = new T();
            t.Init();
            _controls.Add(type, t);
        }
        /// <summary>
        /// ��ȡ�߼���ģ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : ControlBase
        {
            Type type = typeof(T);
            if (_controls.ContainsKey(type))
            {
                return _controls[type] as T;
            }
            return null;
        }

        public override void OnDispose()
        {
            base.OnDispose();
            _controls.Clear();
            _controls = null;
        }
    }
}