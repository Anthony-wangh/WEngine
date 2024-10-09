using UnityEngine;

namespace WEngine
{
    /// <summary>
    /// 本地化模块管理器
    /// </summary>
    public class LocalizationModuleManager : ModuleManagerBase,ILocalization
    {
        [SerializeField]
        private string TableName;
        private LocalizationModule _localization;
        void Start()
        {
            _localization = ModuleSystem.Get<LocalizationModule>();
            SetTable(TableName);
        }

        public string GetString(string key)
        {
            return _localization.GetString(key);
        }

        public void SetTable(string tableName)
        {
            _localization.SetTable(tableName);
        }
    }
}