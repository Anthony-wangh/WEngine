using System;
using System.Collections.Generic;

namespace WEngine
{
    public interface IModelModule
    {
        T Get<T>() where T : ModuleImp,new ();
    }
    /// <summary>
    /// ���ݲ�ģ��
    /// </summary>
    public class ModelModule : ModuleImp, IModelModule
    {
        private Dictionary<Type, ModuleImp> _models = new Dictionary<Type, ModuleImp>();
        /// <summary>
        /// ��ȡ���ݲ�ʵ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Get<T>() where T : ModuleImp, new()
        {
            Type type = typeof(T);
            if (_models.ContainsKey(type))
            {
                return _models[type] as T;
            }
            T t = new T();
            t.Init();
            _models.Add(type, t);
            return t;
        }
        public override void OnDispose()
        {
            base.OnDispose();
            _models.Clear();
            _models = null;
        }
    }
}