using System;
using System.Collections.Generic;

namespace WEngine
{
    /// <summary>
    /// 核心引擎
    /// </summary>
    public sealed class CoreEngine : BehaviourSingleton<CoreEngine>
    {
        #region Module
        private static UIModuleManager _ui;
        /// <summary>
        /// UI管理器
        /// </summary>
        public static UIModuleManager UI => _ui??= Get<UIModuleManager>();

        private static FsmModuleManager _fsm;
        /// <summary>
        /// 状态机管理器
        /// </summary>
        public static FsmModuleManager Fsm => _fsm ??= Get<FsmModuleManager>();

        private static MCModuleManager _mc;
        /// <summary>
        /// 数据逻辑层管理器
        /// </summary>
        public static MCModuleManager MC => _mc ??= Get<MCModuleManager>();

        private static EventModuleManager _event;
        /// <summary>
        /// 全局事件管理器
        /// </summary>
        public static EventModuleManager Event => _event ??= new EventModuleManager();

        private static TimerModuleManager _timer;
        /// <summary>
        /// 计时器管理器
        /// </summary>
        public static TimerModuleManager Timer => _timer ??= Get<TimerModuleManager>();

        private static AssetLoadModuleManager _assetLoad;
        /// <summary>
        /// 资源加载管理器
        /// </summary>
        public static AssetLoadModuleManager AssetLoad => _assetLoad ??= Get<AssetLoadModuleManager>();

        private static LocalizationModuleManager _localization;
        /// <summary>
        /// 本地化多语言管理器
        /// </summary>
        public static LocalizationModuleManager Localization=> _localization ??= Get<LocalizationModuleManager>();

        private static PoolModuleManager _pool;
        /// <summary>
        /// 对象缓存池管理器
        /// </summary>
        public static PoolModuleManager Pool=> _pool ??= Get<PoolModuleManager>();
                    
        #endregion

        private static Dictionary<Type, ModuleManagerBase> _moduleManagers = new Dictionary<Type, ModuleManagerBase>();

        // Start is called before the first frame update
        void Start()
        {
            ModuleSystem.Init();            
        }

        // Update is called once per frame
        void Update()
        {
            ModuleSystem.Update();
        }

        private void OnDestroy()
        {
            ModuleSystem.Dispose();
        }
        /// <summary>
        /// 注册模块管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Registe(ModuleManagerBase mm)
        {
            Type type = mm.GetType();
            if (_moduleManagers.ContainsKey(type))
            {
                Log.Debug($"模块管理器重复注册：{type.Name}");
                return;
            }
            _moduleManagers.Add(type, mm);
        }
        /// <summary>
        /// 获取模块管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>() where T: ModuleManagerBase
        {
            Type type = typeof(T);
            if (_moduleManagers.ContainsKey(type))
            {
                return _moduleManagers[type] as T;
            }
            Log.Warning($"模块管理器未注册：{type}");
            return null;
        }
        /// <summary>
        /// 移除模块管理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Remove(ModuleManagerBase mm)
        {
            Type type = mm.GetType();
            if (_moduleManagers.ContainsKey(type))
            {
                _moduleManagers.Remove(type);
            }
        }
    }
}