using System;
using System.Collections.Generic;

namespace WEngine
{
    /// <summary>
    /// ��������
    /// </summary>
    public sealed class CoreEngine : BehaviourSingleton<CoreEngine>
    {
        #region Module
        private static UIModuleManager _ui;
        /// <summary>
        /// UI������
        /// </summary>
        public static UIModuleManager UI => _ui??= Get<UIModuleManager>();

        private static FsmModuleManager _fsm;
        /// <summary>
        /// ״̬��������
        /// </summary>
        public static FsmModuleManager Fsm => _fsm ??= Get<FsmModuleManager>();

        private static MCModuleManager _mc;
        /// <summary>
        /// �����߼��������
        /// </summary>
        public static MCModuleManager MC => _mc ??= Get<MCModuleManager>();

        private static EventModuleManager _event;
        /// <summary>
        /// ȫ���¼�������
        /// </summary>
        public static EventModuleManager Event => _event ??= new EventModuleManager();

        private static TimerModuleManager _timer;
        /// <summary>
        /// ��ʱ��������
        /// </summary>
        public static TimerModuleManager Timer => _timer ??= Get<TimerModuleManager>();

        private static AssetLoadModuleManager _assetLoad;
        /// <summary>
        /// ��Դ���ع�����
        /// </summary>
        public static AssetLoadModuleManager AssetLoad => _assetLoad ??= Get<AssetLoadModuleManager>();

        private static LocalizationModuleManager _localization;
        /// <summary>
        /// ���ػ������Թ�����
        /// </summary>
        public static LocalizationModuleManager Localization=> _localization ??= Get<LocalizationModuleManager>();

        private static PoolModuleManager _pool;
        /// <summary>
        /// ���󻺴�ع�����
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
        /// ע��ģ�������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Registe(ModuleManagerBase mm)
        {
            Type type = mm.GetType();
            if (_moduleManagers.ContainsKey(type))
            {
                Log.Debug($"ģ��������ظ�ע�᣺{type.Name}");
                return;
            }
            _moduleManagers.Add(type, mm);
        }
        /// <summary>
        /// ��ȡģ�������
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
            Log.Warning($"ģ�������δע�᣺{type}");
            return null;
        }
        /// <summary>
        /// �Ƴ�ģ�������
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