using System;
using System.Collections.Generic;
using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 有限状态机模块。
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class FsmModuleManager : ModuleManagerBase
    {
        private static Dictionary<Type, IFsmManager> _modules = new Dictionary<Type, IFsmManager>();

        /// <summary>
        /// 游戏框架模块初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            
        }

        /// <summary>
        /// 创建状态机管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        internal T CreateFsmManager<T>() where T :FsmManager
        {
            Type type = typeof(T);
            if (_modules.ContainsKey(type))
                return (T)_modules[type];

            var module = ModuleSystem.Get<T>();//  CreateModule<T>();
            if (module == null)
            {
                Log.Debug($"FsmModuleManager-获取状态机管理器失败：" + type);
                return default;
            }
            _modules.Add(type, module);
            return module;
        }

        /// <summary>
        /// 释放状态机管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        internal void DiposeFsmManager<T>() where T : FsmManager
        {
            Type type = typeof(T);
            if (_modules.ContainsKey(type))
            {
                FsmManager fsmManager = (FsmManager)_modules[type];
                fsmManager.Dispose();
                _modules.Remove(type);
            }
        }
    }
}
