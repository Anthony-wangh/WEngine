using System;
using System.Collections.Generic;

namespace WEngine
{
    /// <summary>
    /// ģ�����ϵͳ
    /// </summary>
    public  static class ModuleSystem
    {
        private static Dictionary<Type, IModule> _modules = new Dictionary<Type, IModule>();

        
        /// <summary>
        /// ��ȡ�ܹ�ģ��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>() where T : IModule
        {
            Type type =typeof(T);
            if (_modules.ContainsKey(type))
                return (T)_modules[type];

            var module= CreateModule<T>();
            if (module == null)
            {
                Log.Debug($"ModuleSystem-��ȡģ��ʧ�ܣ�" + type);
                return default;
            }
            _modules.Add(type, module);
            return module;
        }

        private static T CreateModule<T>() where T:IModule
        {
            Type type = typeof(T);
            T t= (T)Activator.CreateInstance(type);
            t.Init();
            return t;
        }

        public static void Init()
        {
        }

        public static void Update()
        {
            foreach (var item in _modules)
            {
                item.Value.Update();
            }
        }

        public static void Dispose()
        {
            foreach (var item in _modules)
            {
                item.Value.Dispose();
            }
            _modules.Clear();
        }
    }
}